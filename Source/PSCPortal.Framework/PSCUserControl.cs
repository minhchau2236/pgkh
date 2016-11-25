using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSCPortal.Framework
{
    public class PSCUserControl:System.Web.UI.UserControl, IDataContainer
    {
        private DataContainer Data
        {
            get
            {
                object parent = Parent;
                while (parent.GetType().GetInterface(typeof(IDataContainer).ToString()) == null)
                    parent = ((System.Web.UI.Control)parent).Parent;
                if (parent.GetType().GetInterface(typeof(IDataContainer).ToString()) != null)
                {
                    return ((IDataContainer)parent).GetDataChild(this.ID);
                }
                return null;
            }
        }
        public override string ID
        {
            get
            {
                if (base.ID == null)
                    return this.GetType().FullName;
                return base.ID;
            }
            set
            {
                base.ID = value;
            }
        }
        protected DataContainer DataSelf
        {
            get
            {
                if (Data != null)
                    return Data;
                return null;
            }
        }
        public DataContainer GetDataChild(string key)
        {
            if (Data == null)
                return null;
            if (!Data.DataChilds.ContainsKey(key))
                Data.DataChilds.Add(key, new DataContainer());
            return Data.DataChilds[key] as DataContainer;
        }
    }
}
