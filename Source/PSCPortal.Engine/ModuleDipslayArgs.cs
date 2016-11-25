using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;

namespace PSCPortal.Engine
{
    public class ModuleDipslayArgs : IPanelArgs
    {
        #region IPanelArgs Members
        public string moduleId = string.Empty;
        public string Path
        {
            get
            {
                return ModuleCollection.GetModuleCollection().Where(m => m.Id == new Guid(moduleId)).Single().DisplayURL;
            }
            set
            {
                moduleId = value;
            }
        }

        #endregion
    }
}
