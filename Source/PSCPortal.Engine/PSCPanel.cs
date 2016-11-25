using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace PSCPortal.Engine
{
    public class PSCPanel: System.Web.UI.WebControls.Panel
    {
        public override string ClientID
        {
            get
            {
                return this.ID;
            }
        }
        public override string UniqueID
        {
            get
            {
                return this.ID;
            }
        }
    }
}
