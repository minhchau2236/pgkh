using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PSCPortal.Security;
using PSCPortal.Framework;
using PSCPortal.Framework.Helpler;

namespace PSCPortal.Systems.Security
{
    public partial class UserInRoleManage : PSCPortal.Framework.PSCDialog
    {
        protected static UserCollection UsersOfSubDomain
        {
            get
            {
                if (DataStatic["UsersOfSubDomain"] == null)
                {
                    string subdomainId = SessionHelper.GetSession(SessionKey.SubDomain);
                    DataStatic["UsersOfSubDomain"] = UserCollection.GetAllUserBySubDomain(new Guid(subdomainId));
                }
                return DataStatic["UsersOfSubDomain"] as UserCollection;
            }
            set
            {
                DataStatic["UsersOfSubDomain"] = value;
            }
        }
        protected static UserCollection UserList
        {
            get
            {
                if (DataStatic["UserList"] == null)
                {
                    UserCollection result = new UserCollection();
                    foreach (var user in Args.Role.GetUsersInRole())
                    {
                        if (UsersOfSubDomain.Any(u => u.Id == user.Id))
                            result.Add(user);
                    }
                    DataStatic["UserList"] = result;
                }
                return DataStatic["UserList"] as UserCollection;
            }
            set
            {
                DataStatic["UserList"] = value;
            }
        }

        protected static UserInRoleCollection UserInRoleList
        {
            get
            {
                if (DataStatic["UserInRoleList"] == null)
                {

                    UserInRoleCollection result = new UserInRoleCollection();
                    foreach (var uir in UserInRoleCollection.GetUserInRoleCollectionByRoleID(Args.Role.Id))
                    {
                        if (UsersOfSubDomain.Any(u => u.Id == uir.UserId))
                            result.Add(uir);
                    }
                    DataStatic["UserInRoleList"] = result;
                }
                return DataStatic["UserInRoleList"] as UserInRoleCollection;
            }
            set
            {
                DataStatic["UserList"] = value;
            }
        }

        protected static UserInRoleCollection CopyOfUserInRoleList
        {
            get
            {
                if (DataStatic["CopyOfUserInRoleList"] == null)
                {
                    UserInRoleCollection results = new UserInRoleCollection();
                    foreach (UserInRole u in UserInRoleList)
                    {
                        results.Add(u);
                    }
                    DataStatic["CopyOfUserInRoleList"] = results;
                }
                return DataStatic["CopyOfUserInRoleList"] as UserInRoleCollection;
            }
            set
            {
                DataStatic["CopyOfUserInRoleList"] = value;
            }
        }

        protected static UserCollection UserNotInRoleCollection
        {
            get
            {
                if (DataStatic["UserNotInRoleCollection"] == null)
                {
                    UserCollection result = new UserCollection();
                    foreach (var user in UsersOfSubDomain)
                    {
                        if (!UserList.Any(u => u.Id == user.Id))
                            result.Add(user);
                    }
                    DataStatic["UserNotInRoleCollection"] = result;
                }
                return DataStatic["UserNotInRoleCollection"] as UserCollection;
            }
            set
            {
                DataStatic["UserNotInRoleCollection"] = value;
            }
        }

        protected static UserInRoleCollection UserNotInRoleList
        {
            get
            {
                if (DataStatic["UserNotInRoleList"] == null)
                {
                    UserInRoleCollection result = new UserInRoleCollection();
                    foreach (var uir in UserInRoleCollection.GetUserInRoleCollection())
                    {
                        if (UsersOfSubDomain.Any(u => u.Id == uir.UserId))
                        {
                            result.Add(uir);
                        }
                    }
                    DataStatic["UserNotInRoleList"] = result;
                }
                return DataStatic["UserNotInRoleList"] as UserInRoleCollection;
            }
            set
            {
                DataStatic["UserNotInRoleList"] = value;
            }
        }

        private static RoleArgs Args
        {
            get
            {
                return DataShare as RoleArgs;
            }
        }


        [System.Web.Services.WebMethod]
        public static string GetUserList(int startIndex, int maximumRows, string sortExpressions)
        {
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            string results = js.Serialize(UserList.GetSegment(startIndex, maximumRows, sortExpressions)) + "_" + Args.Role.Id;
            int count = CopyOfUserInRoleList.Count;
            return results;
        }

        [System.Web.Services.WebMethod]
        public static string GetUserNotInRoleList()
        {
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(UserNotInRoleCollection);
        }

        [System.Web.Services.WebMethod]
        public static int GetUserCount()
        {
            return UserList.Count;
        }
        [System.Web.Services.WebMethod]
        public static void UserNew()
        {
            //  User item = new User { CreationDate = DateTime.Now, LastActivityDate = DateTime.Now, LastLoginDate = DateTime.Now, LastPasswordChangeDate = DateTime.Now, IsApproved = true, IsOnline = false };
            //  item.Id = Guid.NewGuid();
            PSCSubDialog.DataShare = Args;
        }
        [System.Web.Services.WebMethod]
        public static void AddUser(string[] arrId)
        {
            foreach (string Id in arrId)
            {
                Guid uId = new Guid(Id);
                User user = UserNotInRoleCollection.Where(u => u.Id == uId).Single();
                UserList.Add(user);
                UserNotInRoleCollection.Remove(user);
                UserInRoleList.Add(new UserInRole { RoleId = Args.Role.Id, UserId = uId });
            }
        }
        [System.Web.Services.WebMethod]
        public static void UserEdit(string id)
        {
            Guid idUser = new Guid(id);
            PSCDialog.DataShare = new UserArgs(UserList.Where(a => a.Id == idUser).Single(), true);
        }
        [System.Web.Services.WebMethod]
        public static void UserUpdate()
        {
            (PSCDialog.DataShare as PSCPortal.Security.UserArgs).User.Update();
        }
        [System.Web.Services.WebMethod]
        public static void UserDelete(string[] arrId)
        {
            foreach (string id in arrId)
            {
                Guid idUser = new Guid(id);
                User user = UserList.Where(u => u.Id == idUser).Single();
                UserList.Remove(user);

                UserInRole userInRole = UserInRoleList.Where(u => u.UserId == idUser && u.RoleId == Args.Role.Id).Single();
                UserInRoleList.Remove(userInRole);

                UserNotInRoleCollection.Add(user);
                UserNotInRoleList.Add(userInRole);
            }
        }

        [System.Web.Services.WebMethod]
        public static void Save()
        {
            //list of user added
            IEnumerable<UserInRole> addedUsers = UserInRoleList.Except(CopyOfUserInRoleList, new UserInRoleEqualityComparer());
            IEnumerable<UserInRole> deletedUsers = CopyOfUserInRoleList.Except(UserInRoleList, new UserInRoleEqualityComparer());
            foreach (UserInRole u in addedUsers)
            {
                CopyOfUserInRoleList.AddDB(u);
            }
            List<UserInRole> list = deletedUsers.ToList<UserInRole>();
            for (int i = 0; i < list.Count; i++)
            {
                UserInRole u = list[i];
                CopyOfUserInRoleList.RemoveDB(u);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DataBind();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (!IsPostBack)
            {
                UserList = null;
                UserInRoleList = null;
                UserNotInRoleCollection = null;
                UserNotInRoleList = null;

            }
        }
    }
}
