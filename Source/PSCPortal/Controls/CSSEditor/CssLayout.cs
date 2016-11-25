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
    public class CssLayout
    {
        private string _visibility=string.Empty;
        public string Visibility
        {
            get
            {
                return _visibility;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _visibility = value;
            }
        }
        private string _display = string.Empty;
        public string Display
        {
            get
            {
                return _display;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _display = value;
            }
        }
        private string _float = string.Empty;
        public string Float
        {
            get
            {
                return _float;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _float = value;

            }
        }
        private string _clear = string.Empty;
        public string Clear
        {
            get
            {
                return _clear;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _clear = value;
            }
        }
        private string _cursor = string.Empty;
        public string Cursor
        {
            get
            {
                return _cursor;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _cursor = value;
            }
        }
        private string _overflow = string.Empty;
        public string Overflow
        {
            get
            {
                return _overflow;

            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _overflow = value;
            }
        }
        private string _top = string.Empty;
        public string Top
        {
            get
            {
                return _top;

            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _top = value;
            }
        }
        private string _bottom = string.Empty;
        public string Bottom
        {
            get
            {
                return _bottom;

            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _bottom = value;
            }
        }
        private string _left = string.Empty;
        public string Left
        {
            get
            {
                return _left;

            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _left = value;
            }
        }
        private string _right = string.Empty;
        public string Right
        {
            get
            {
                return _right;

            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _right = value;
            }
        }

        public string BuildCssString()
        {
            string layoutCss = string.Empty;
            layoutCss += (Visibility != string.Empty ? "visibility:" + Visibility + ";" : string.Empty);
            layoutCss += (Display != string.Empty ? "display:" + Display + ";" : string.Empty);
            layoutCss += (Float != string.Empty ? "float:" + Float + ";" : string.Empty);
            layoutCss += (Clear != string.Empty ? "clear:" + Clear + ";" : string.Empty);
            layoutCss += (Cursor != string.Empty ? "cursor:" + Cursor + ";" : string.Empty);
            layoutCss += (Overflow != string.Empty ? "overflow:" + Overflow + ";" : string.Empty);

            if (Top != string.Empty || Right != string.Empty || Bottom != string.Empty || Left != string.Empty)
            {
                layoutCss += "clip: rect(";
                layoutCss += (Top != string.Empty ? " " + Top +",": "auto,");
                layoutCss += (Right != string.Empty ? " " + Right + "," : "auto,");
                layoutCss += (Bottom != string.Empty ? " " + Bottom + "," : "auto,");
                layoutCss += (Left != string.Empty ? " " + Left : "auto");
                layoutCss += ");";
                
            }
            if (layoutCss.EndsWith(";"))
                layoutCss = layoutCss.Remove(layoutCss.Length - 1);
            return layoutCss;
        }


        public void SetAttributes(Dictionary<string, string> styles)
        {
            Visibility = styles.ContainsKey("visibility") ? styles["visibility"] : string.Empty;
            Display = styles.ContainsKey("display") ? styles["display"] : string.Empty;
            Float = styles.ContainsKey("float") ? styles["float"] : string.Empty;
            Clear = styles.ContainsKey("clear") ? styles["clear"] : string.Empty;
            Cursor = styles.ContainsKey("cursor") ? styles["cursor"] : string.Empty;
            Overflow = styles.ContainsKey("overflow") ? styles["overflow"] : string.Empty;

            string clip = styles.ContainsKey("clip") ? styles["clip"] : string.Empty;
            
            if (clip != string.Empty)
            {
                clip = clip.Trim();
                clip = clip.Substring(5, clip.Length - 6);
                string[] keys = clip.Split(",".ToCharArray());

                Top = keys[0].Trim();
                Right = keys[1].Trim();
                Bottom = keys[2].Trim();
                Left = keys[3].Trim();
                
            }
        }
    }
}
