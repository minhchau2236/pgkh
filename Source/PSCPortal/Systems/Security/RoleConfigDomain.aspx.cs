using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using PSCPortal.Engine;
using PSCPortal.Security;

namespace PSCPortal.Systems.Security
{
    public partial class RoleConfigDomain : PSCPortal.Framework.PSCDialog
    {
        private static RoleArgs Args
        {
            get
            {
                return DataShare as RoleArgs;
            }
        }
        # region SubDomain
        protected static SubDomainCollection SubDomainList
        {
            get
            {
                if (DataStatic["SubDomainList"] == null)
                {
                    SubDomainCollection subList = SubDomainCollection.GetSubDomainCollection();
                    subList.Add(new SubDomain { Id = new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), Name = "HomePage" });
                    DataStatic["SubDomainList"] = subList;
                }

                return DataStatic["SubDomainList"] as SubDomainCollection;
            }
            set
            {
                DataStatic["SubDomainList"] = value;
            }
        }
        protected static SubDomainCollection BeforeSubDomainList
        {
            get
            {
                if (DataStatic["BeforeSubDomainList"] == null)
                {
                    DataStatic["BeforeSubDomainList"] = SubDomainCollection.GetSubDomainCollection(Args.Role);
                }
                return DataStatic["BeforeSubDomainList"] as SubDomainCollection;
            }
            set
            {
                DataStatic["BeforeSubDomainList"] = value;
            }
        }
        protected static SubDomainCollection AfterSubDomainList
        {
            get
            {
                return DataStatic["AfterSubDomainList"] as SubDomainCollection;
            }
            set
            {
                DataStatic["AfterSubDomainList"] = value;
            }
        }
        protected static SubDomainCollection DisplaySubDomainList
        {
            get
            {
                SubDomainCollection result = SubDomainList;
                if (DataStatic["DisplaySubDomainList"] == null)
                {
                    foreach (var item in BeforeSubDomainList)
                        result.Single(i => i.Id == item.Id).IsCheck = true;
                    DataStatic["DisplaySubDomainList"] = result;
                }
                return DataStatic["DisplaySubDomainList"] as SubDomainCollection;
            }
            set
            {
                DataStatic["DisplaySubDomainList"] = value;
            }
        }
        [System.Web.Services.WebMethod]
        public static string GetSubDomainList()
        {
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            string results = js.Serialize(DisplaySubDomainList);
            return results;
        }
        public static SubDomainCollection SubDomainListChoice(string[] arrId)
        {
            SubDomainCollection result = new SubDomainCollection();
            foreach (string id in arrId)
            {
                Guid idSubDomain = new Guid(id);
                SubDomain subDomain = SubDomainList.Where(u => u.Id == idSubDomain).Single();
                result.Add(subDomain);
            }
            AfterSubDomainList = result;
            return result;
        }
        #endregion

        [System.Web.Services.WebMethod]
        public static void Save(string[] arrSubDomainId)
        {
            #region SubDomain
            SubDomainCollection listSubDomainBefore = BeforeSubDomainList;
            SubDomainCollection listSubDomainAfter = SubDomainListChoice(arrSubDomainId);
            IEnumerable<SubDomain> addSubDomainMaster = listSubDomainAfter.Except(listSubDomainBefore);
            IEnumerable<SubDomain> removeSubDomainMaster = listSubDomainBefore.Except(listSubDomainAfter);
            //remove list
            foreach (var item in removeSubDomainMaster)
            {
                SubDomainInRole sdil = new SubDomainInRole();
                sdil.SubDomainId = item.Id;
                sdil.RoleId = Args.Role.Id;
                sdil.RemoveDB();
            }
            // add list
            foreach (var item in addSubDomainMaster)
            {
                SubDomainInRole sdil = new SubDomainInRole();
                sdil.SubDomainId = item.Id;
                sdil.RoleId = Args.Role.Id;
                sdil.AddDB();
            }

            #endregion
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataBind();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (!IsPostBack)
            {
                gvSubDomain.MasterTableView.PageSize = DisplaySubDomainList.Count();
            }
        }
    }
}
