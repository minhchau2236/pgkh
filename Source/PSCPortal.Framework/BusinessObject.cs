using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace PSCPortal.Framework
{
    [Serializable]  
    public abstract class BusinessObject<T> : Core.BusinessObject where T : BusinessObject<T>
    {
        protected BusinessObject(DbDataReader reader)
            :base(reader)
        {
        }
        protected BusinessObject()
            : base()
        {
        }
    }
}
