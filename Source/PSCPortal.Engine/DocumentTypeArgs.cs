using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSCPortal.Engine
{
    public class DocumentTypeArgs : IPanelArgs
    {
        #region IPanelArgs Members

        public string Path
        {
            get { return "~/Modules/DMS/DocumentTypeDisplay.ascx"; }
        }

        #endregion
    }
}
