using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSCPortal.Framework
{    
    public class PSCPage:PSCPageBase, IDataContainer
    {        
        private static string NameSessionShare = "PSCPageNameSessionShare";
        public static object DataShare
        {
            get
            {
                return System.Web.HttpContext.Current.Session[NameSessionShare];
            }
            set
            {
                System.Web.HttpContext.Current.Session[NameSessionShare] = value;
            }
        }
        private static string NameSession = "PSCPage";     
        private DataContainer Data
        {
            get
            {
                if (Session[NameSession] == null)
                    Session[NameSession] = new DataContainer();
                return Session[NameSession] as DataContainer;
            }            
        }
        protected static DataContainer DataStatic
        {
            get
            {
                if (System.Web.HttpContext.Current.Session[NameSession] == null)
                    System.Web.HttpContext.Current.Session[NameSession] = new DataContainer();
                return System.Web.HttpContext.Current.Session[NameSession] as DataContainer;                
            }
        }
        protected DataContainer DataSelf
        {
            get
            {
                return Data;
            }
        }
        public DataContainer GetDataChild (string key)
        {            
            if(!Data.DataChilds.ContainsKey(key))
                Data.DataChilds.Add(key, new DataContainer());
            return Data.DataChilds[key] as DataContainer;
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!IsPostBack)
                Session.Remove(NameSession);
            base.OnLoad(e);
        }
    }
}
