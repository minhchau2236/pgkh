using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSCPortal.Framework
{
    internal interface IDataContainer
    {
        DataContainer GetDataChild(string key);
    }
}
