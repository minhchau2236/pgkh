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
    public class CssBorder
    {
        string _borderStyle = string.Empty;
        string _borderWidth = string.Empty;
        string _borderColor = string.Empty;

        public string BorderStyleCss
        {
            get
            {
                return _borderStyle;
            }
            set
            {
                _borderStyle = value;
            }
        }

        public string BorderWidth
        {
            get
            {
                return _borderWidth;
            }
            set
            {
                _borderWidth = value;
            }
        }

        public string BorderColor
        {
            get
            {
                return _borderColor;
            }
            set
            {
                _borderColor = value;
            }
        }

        public string BuildCssString()
        {
            string borderCss = string.Empty;
            borderCss += (BorderStyleCss != string.Empty ? "border-style:" + BorderStyleCss + ";" : string.Empty);
            borderCss += (BorderWidth != string.Empty ? "border-width:" + BorderWidth + ";" : string.Empty);
            borderCss += (BorderColor != string.Empty ? "border-color:" + BorderColor + ";" : string.Empty);
        
            if (borderCss.EndsWith(";"))
                borderCss = borderCss.Remove(borderCss.Length - 1);
            return borderCss;
        }

        public void SetAttributes(Dictionary<string, string> styles)
        {
            BorderStyleCss = styles.ContainsKey("border-style") ? styles["border-style"] : string.Empty;
            BorderWidth = styles.ContainsKey("border-width") ? styles["border-width"] : string.Empty;
            BorderColor = styles.ContainsKey("border-color") ? styles["border-color"] : string.Empty;
        }


    }
}
