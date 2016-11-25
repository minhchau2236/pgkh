using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSCPortal.Engine
{
    public class DocumentArgs:IPanelArgs
    {

        #region IPanelArgs Members

        public string Path
        {
            get { return "~/Modules/DMS/DocumentDisplay.ascx"; }
        }

        #endregion
    }
}
