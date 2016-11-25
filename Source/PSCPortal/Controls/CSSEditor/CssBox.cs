using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

namespace CssEditor
{
    [Serializable]
    public class CssBox
    {
        string _padding = string.Empty;
        string _margin = string.Empty;

        public string Padding
        {
            get
            {
                return _padding;
            }
            set
            {
                _padding = value;
            }
        }

        public string Margin
        {
            get
            {
                return _margin;
            }
            set
            {
                _margin = value;
            }
        }

        public string BuildCssString()
        {
            string boxCss = string.Empty;
            boxCss += (Padding != string.Empty ? "padding:" + Padding + ";" : string.Empty);
            boxCss += (Margin != string.Empty ? "marging:" + Margin + ";" : string.Empty);

            if (boxCss.EndsWith(";"))
                boxCss = boxCss.Remove(boxCss.Length - 1);
            return boxCss;
        }

        public void SetAttributes(Dictionary<string, string> styles)
        {
            Margin = styles.ContainsKey("margin") ? styles["margin"] : string.Empty;
            Padding = styles.ContainsKey("padding") ? styles["padding"] : string.Empty;
        }
    }
}
