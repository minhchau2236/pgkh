using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSCPortal.Framework
{
    [Serializable]
    public abstract class BusinessObjectCollection<T, C> : Core.BusinessObjectCollection<C>
        where T : BusinessObjectCollection<T, C>
        where C : Core.BusinessObject
    {
    }
}
