using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSCPortal.Framework
{
    public class PSCPageBase:System.Web.UI.Page
    {
        protected override void InitializeCulture()
        {
            string language = string.Empty;
            if (Request.Cookies["UICulture"] != null)
                language = Request.Cookies["UICulture"].Value;
            UICulture = language;
        }
    }
}
