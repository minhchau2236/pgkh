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
    public partial class RolesOfUser : PSCPortal.Framework.PSCDialog
    {
        protected static RoleCollection RolesOfSubDomain
        {
            get
            {
                if (DataStatic["RolesOfSubDomain"] == null)
                {
                    string subdomainId = SessionHelper.GetSession(SessionKey.SubDomain);
                    DataStatic["RolesOfSubDomain"] = RoleCollection.GetRoleCollectionBySubDomain(new Guid(subdomainId));
                }
                return DataStatic["RolesOfSubDomain"] as RoleCollection;
            }
            set
            {
                DataStatic["RolesOfSubDomain"] = value;
            }
        }
        protected static RoleCollection RoleList
        {
            get
            {
                if (DataStatic["RoleList"] == null)
                {
                    RoleCollection result = new RoleCollection();
                    foreach (var role in Args.User.GetRolesBelongTo())
                    {
                        if (RolesOfSubDomain.Any(r => r.Id == role.Id))
                            result.Add(role);
                    }
                    DataStatic["RoleList"] = result;
                }
                return DataStatic["RoleList"] as RoleCollection;
            }
            set
            {
                DataStatic["RoleList"] = value;
            }
        }

        protected static UserInRoleCollection RoleOfUserList
        {
            get
            {
                if (DataStatic["RoleOfUserList"] == null)
                {
                    UserInRoleCollection result = new UserInRoleCollection();
                    foreach (var uir in UserInRoleCollection.GetRolesByUserID(Args.User.Id))
                    {
                        if (RolesOfSubDomain.Any(r => r.Id == uir.RoleId))
                        {
                            result.Add(uir);
                        }
                    }
                    DataStatic["RoleOfUserList"] = result;
                }
                return DataStatic["RoleOfUserList"] as UserInRoleCollection;
            }
            set
            {
                DataStatic["RoleOfUserList"] = value;
            }
        }

        protected static UserInRoleCollection CopyOfRoleOfUserList
        {
            get
            {
                if (DataStatic["CopyOfRoleOfUserList"] == null)
                {
                    UserInRoleCollection results = new UserInRoleCollection();
                    foreach (UserInRole u in RoleOfUserList)
                    {
                        results.Add(u);
                    }
                    DataStatic["CopyOfRoleOfUserList"] = results;
                }
                return DataStatic["CopyOfRoleOfUserList"] as UserInRoleCollection;
            }
            set
            {
                DataStatic["CopyOfRoleOfUserList"] = value;
            }
        }

        protected static RoleCollection RoleNotOfUserCollection
        {
            get
            {
                if (DataStatic["RoleNotOfUserCollection"] == null)
                {
                    RoleCollection result = new RoleCollection();
                    foreach (var role in RolesOfSubDomain)
                    {
                        if (!RoleList.Any(r => r.Id == role.Id))
                            result.Add(role);
                    }
                    DataStatic["RoleNotOfUserCollection"] = result;
                }

                return DataStatic["RoleNotOfUserCollection"] as RoleCollection;
            }
            set
            {
                DataStatic["RoleNotOfUserCollection"] = value;
            }
        }

        protected static UserInRoleCollection RoleNotOfUserList
        {
            get
            {
                if (DataStatic["RoleNotOfUserList"] == null)
                {
                    UserInRoleCollection result = new UserInRoleCollection();
                    foreach (var uir in UserInRoleCollection.GetRolesNotOfUserCollection(Args.User.Id))
                    {
                        if (RolesOfSubDomain.Any(r => r.Id == uir.RoleId))
                        {
                            result.Add(uir);
                        }
                    }
                    DataStatic["RoleNotOfUserList"] = result;
                }

                return DataStatic["RoleNotOfUserList"] as UserInRoleCollection;
            }
            set
            {
                DataStatic["RoleNotOfUserList"] = value;
            }
        }

        private static UserArgs Args
        {
            get
            {
                return DataShare as UserArgs;
            }
        }

        [System.Web.Services.WebMethod]
        public static string GetRoleList(int startIndex, int maximumRows, string sortExpressions)
        {
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            string results = js.Serialize(RoleList.GetSegment(startIndex, maximumRows, sortExpressions)) + "_" + Args.User.Id;
            int count = CopyOfRoleOfUserList.Count;
            return results;
        }

        [System.Web.Services.WebMethod]
        public static string GetRoleNotOfUserList()
        {
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(RoleNotOfUserCollection);
        }

        [System.Web.Services.WebMethod]
        public static int GetRoleCount()
        {
            return RoleList.Count;
        }
        [System.Web.Services.WebMethod]
        public static void RoleNew()
        {
            //  User item = new User { CreationDate = DateTime.Now, LastActivityDate = DateTime.Now, LastLoginDate = DateTime.Now, LastPasswordChangeDate = DateTime.Now, IsApproved = true, IsOnline = false };
            //  item.Id = Guid.NewGuid();
            PSCSubDialog.DataShare = Args;
        }
        [System.Web.Services.WebMethod]
        public static void AddRole(string[] arrId)
        {
            foreach (string Id in arrId)
            {
                Guid rId = new Guid(Id);
                Role role = RoleNotOfUserCollection.Where(r => r.Id == rId).Single();
                RoleList.Add(role);
                RoleNotOfUserCollection.Remove(role);
                RoleOfUserList.Add(new UserInRole { UserId = Args.User.Id, RoleId = rId });
            }
        }
        [System.Web.Services.WebMethod]
        public static void RoleEdit(string id)
        {
            Guid idRole = new Guid(id);
            PSCDialog.DataShare = new RoleArgs(RoleList.Where(a => a.Id == idRole).Single(), true);
        }
        [System.Web.Services.WebMethod]
        public static void RoleUpdate()
        {
            (PSCDialog.DataShare as PSCPortal.Security.RoleArgs).Role.Update();
        }
        [System.Web.Services.WebMethod]
        public static void RoleDelete(string[] arrId)
        {
            foreach (string id in arrId)
            {
                Guid idRole = new Guid(id);
                Role role = RoleList.Where(u => u.Id == idRole).Single();
                RoleList.Remove(role);

                UserInRole roleOfUser = RoleOfUserList.Where(u => u.RoleId == idRole && u.UserId == Args.User.Id).Single();
                RoleOfUserList.Remove(roleOfUser);

                RoleNotOfUserCollection.Add(role);
                RoleNotOfUserList.Add(roleOfUser);
            }
        }

        [System.Web.Services.WebMethod]
        public static void Save()
        {
            //list of user added
            IEnumerable<UserInRole> addedUsers = RoleOfUserList.Except(CopyOfRoleOfUserList, new UserInRoleEqualityComparer());
            IEnumerable<UserInRole> deletedUsers = CopyOfRoleOfUserList.Except(RoleOfUserList, new UserInRoleEqualityComparer());
            foreach (UserInRole u in addedUsers)
            {
                CopyOfRoleOfUserList.AddDB(u);
            }
            List<UserInRole> list = deletedUsers.ToList<UserInRole>();
            for (int i = 0; i < list.Count; i++)
            {
                UserInRole u = list[i];
                CopyOfRoleOfUserList.RemoveDB(u);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DataBind();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (!IsPostBack)
            {
                RoleList = null;
                RoleOfUserList = null;
                RoleNotOfUserList = null;
                RoleNotOfUserCollection = null;

            }
        }
    }
}
