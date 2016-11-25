using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSCPortal.Framework
{
    public interface IChildControl
    {
        object ParentControl{get;}
    }
}
