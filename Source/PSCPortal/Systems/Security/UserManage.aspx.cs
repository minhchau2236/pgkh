using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHibernate.Criterion;
using PSCPortal.Framework;
using PSCPortal.Security;
using PSCPortal.Framework.Helpler;

namespace PSCPortal.Systems.Security
{
    public partial class UserManage : PSCPage
    {
        protected static UserCollection UserList
        {
            get
            {
                if (DataStatic["UserList"] == null)
                    DataStatic["UserList"] = UserCollection.GetUserCollection();
                return DataStatic["UserList"] as UserCollection;
            }
        }

        protected static UserCollection DisplayUserList
        {
            get
            {
                if (DataStatic["DisplayUserList"] == null)
                {
                    Guid subId = new Guid(SessionHelper.GetSession(SessionKey.SubDomain));
                    DataStatic["DisplayUserList"] = subId == Guid.Empty ? UserList : UserCollection.GetAllUserBySubDomain(subId);
                }
                return DataStatic["DisplayUserList"] as UserCollection;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                DataBind();
        }
        [System.Web.Services.WebMethod]
        public static string GetUserList(int startIndex, int maximumRows, string sortExpressions)
        {
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(Libs.IEnumerableExtentionMethods.GetSegmentList(DisplayUserList, startIndex, maximumRows, sortExpressions));
        }
        [System.Web.Services.WebMethod]
        public static int GetUserCount()
        {
            return DisplayUserList.Count;
        }
        [System.Web.Services.WebMethod]
        public static void UserNew()
        {
            User item = new User { CreationDate = DateTime.Now, LastActivityDate = DateTime.Now, LastLoginDate = DateTime.Now, LastPasswordChangeDate = DateTime.Now, IsApproved = true, IsOnline = false };
            item.Id = Guid.NewGuid();
            PSCDialog.DataShare = new UserArgs(item, false);
        }
        [System.Web.Services.WebMethod]
        public static void UserAdd()
        {
            UserArgs args = (UserArgs)PSCDialog.DataShare;
            User user = ((UserArgs)PSCDialog.DataShare).User;
            CustomMembershipProvider customMembership = new CustomMembershipProvider();
            user.Password = customMembership.TransformPassword(user.Password);
            bool validate = customMembership.ValidateUserNameAndEmail(user);
            if (user.Name.Trim() == "" || validate == false)
                return;
            UserList.AddDB(user);
            // roles of user
            UserInRoleCollection userInRoleCollection = new UserInRoleCollection();
            UserInRole userInRole = null;
            string subdomain = SessionHelper.GetSession(SessionKey.SubDomain);
            if (!(subdomain == Guid.Empty.ToString())) // thuoc it nhat 1 subdomain
            {
                UserInSubDomainCollection userInSubDomainCollection = new UserInSubDomainCollection();
                UserInSubDomain userInSubDomain = new UserInSubDomain();
                userInSubDomain.UserId = user.Id;
                userInSubDomain.SubDomainId = new Guid(subdomain);
                userInSubDomainCollection.AddDB(userInSubDomain);
                // administrators
                if (args.IsAdministrator)
                {
                    Role roleAdministrator = RoleCollection.GetRoleCollection()
                            .Single(r => r.Name == System.Configuration.ConfigurationManager.AppSettings["GroupAdmin"]);
                    userInRole = new UserInRole();
                    userInRole.RoleId = roleAdministrator.Id;
                    userInRole.UserId = user.Id;
                    userInRoleCollection.AddDB(userInRole);
                }
            }
            else // subdomain : all
            {
                // administrators
                if (args.IsAdministrator)
                {
                    Role roleAdministrator = RoleCollection.GetRoleCollection().Single(r => r.Name == System.Configuration.ConfigurationManager.AppSettings["GroupAdmin"]);
                    userInRole = new UserInRole();
                    userInRole.RoleId = roleAdministrator.Id;
                    userInRole.UserId = user.Id;
                    userInRoleCollection.AddDB(userInRole);
                }
            }
            DataStatic["DisplayUserList"] = null;
        }
        [System.Web.Services.WebMethod]
        public static void UserEdit(string id)
        {
            Guid idUser = new Guid(id);
            User user = UserList.Where(a => a.Id == idUser).Single();
            string groupAdmin = System.Configuration.ConfigurationManager.AppSettings["GroupAdmin"];
            PSCDialog.DataShare = RoleCollection.GetRoleCollection(user.Name).Any(r => r.Name == groupAdmin)
                ? new UserArgs(user, true, true)
                : new UserArgs(user, true, false);
        }
        [System.Web.Services.WebMethod]
        public static void UserChangePass(string id)
        {
            Guid idUser = new Guid(id);
            PSCDialog.DataShare = new UserArgs(UserList.Where(a => a.Id == idUser).Single(), true);
        }

        [System.Web.Services.WebMethod]
        public static void UserAuthenticationEdit(string id)
        {
            Guid idUser = new Guid(id);
            PSCDialog.DataShare = new UserArgs(UserList.Where(a => a.Id == idUser).Single(), true);
        }
        [System.Web.Services.WebMethod]
        public static void UserUpdate()
        {
            UserArgs args = PSCDialog.DataShare as PSCPortal.Security.UserArgs;
            CustomMembershipProvider customMembership = new CustomMembershipProvider();
            bool validate = customMembership.ValidateEmail(args.User);
            if ( validate == false)
                return;
            args.User.Update();
            string groupAdmin = System.Configuration.ConfigurationManager.AppSettings["GroupAdmin"];
            RoleCollection rolesOfUser = RoleCollection.GetRoleCollection(args.User.Name);
            Role roleAdmin = rolesOfUser.SingleOrDefault(r => r.Name == groupAdmin);
            UserInRoleCollection userInRoleCollection = new UserInRoleCollection();
            UserInRole userInRole = null;
            if (args.IsAdministrator)
            {
                // if user exist role administrator
                if (roleAdmin == null)
                {
                    userInRole = new UserInRole();
                    Role radmin = RoleCollection.GetRoleCollection().Single(r => r.Name == groupAdmin);
                    userInRole.RoleId = radmin.Id;
                    userInRole.UserId = args.User.Id;
                    userInRoleCollection.AddDB(userInRole);
                }
            }
            else
            {
                if (roleAdmin != null)
                {
                    userInRole = new UserInRole();
                    userInRole.RoleId = roleAdmin.Id;
                    userInRole.UserId = args.User.Id;
                    userInRoleCollection.Add(userInRole);
                    userInRoleCollection.RemoveDB(userInRole);
                }
            }
            DataStatic["DisplayUserList"] = null;
            DataStatic["UserList"] = null;
        }
        [System.Web.Services.WebMethod]
        public static void UserDelete(string[] arrId)
        {
            foreach (string id in arrId)
            {
                Guid idUser = new Guid(id);
                UserList.RemoveDB(UserList.Where(a => a.Id == idUser).Single());
            }
            DataStatic["DisplayUserList"] = null;
        }
        [System.Web.Services.WebMethod]
        public static string GetPermission(int[] arr)
        {
            Dictionary<string, bool> result = new Dictionary<string, bool>();
            foreach (int idFun in arr)
            {
                result.Add(idFun.ToString(), PSCPortal.Security.SystemAuthentication.CheckAllowFunction(PSCPortal.Security.Function.Parse(idFun)));
            }
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(result);
        }

        [System.Web.Services.WebMethod]
        public static void UserConfigSubDomain(string id)
        {
            Guid idUser = new Guid(id);
            PSCDialog.DataShare = new UserArgs(UserList.Where(a => a.Id == idUser).Single(), true);
        }
    }
}
