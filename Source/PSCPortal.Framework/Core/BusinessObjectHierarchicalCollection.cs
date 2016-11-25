using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSCPortal.Framework.Core
{
    [Serializable] 
    public class BusinessObjectHierarchicalCollection : List<BusinessObjectHierarchical>, System.Web.UI.IHierarchicalEnumerable
    {
        #region IHierarchicalEnumerable Members

        public System.Web.UI.IHierarchyData GetHierarchyData(object enumeratedItem)
        {
            return enumeratedItem as System.Web.UI.IHierarchyData;
        }

        #endregion       
    }
}
