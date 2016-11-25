using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace PSCPortal.Framework
{
    [Serializable] 
    public abstract class BusinessObjectHierarchical<T> : Core.BusinessObjectHierarchical where T : BusinessObjectHierarchical<T>
    {
        protected BusinessObjectHierarchical(DbDataReader reader)
            : base(reader)
        {
        }
        protected BusinessObjectHierarchical()
            : base()
        {
        }
    }
}
