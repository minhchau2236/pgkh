using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PSCPortal.Framework.Core;

namespace PSCPortal.Framework
{
    [Serializable] 
    public abstract class BusinessObjectTree<T, C> : Core.BusinessObjectTree
        where T : BusinessObjectTree<T, C>
        where C : Core.BusinessObjectHierarchical
    {
        protected BusinessObjectTree(BusinessObjectHierarchical root)
            :base(root)
        {
        }
    }
}
