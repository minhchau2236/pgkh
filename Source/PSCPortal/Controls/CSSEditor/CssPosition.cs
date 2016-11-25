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
    public class CssPosition
    {
        string _position = string.Empty;
        string _zIndex = string.Empty;
        string _width = string.Empty;
        string _height = string.Empty;
        string _top = string.Empty;
        string _right = string.Empty;
        string _bottom = string.Empty;
        string _left = string.Empty;

        public string Postion
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
            }
        }

        public string ZIndex 
        {
            get
            {
                return _zIndex;
            }
            set
            {
                _zIndex = value;
            }
        }

        public string Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
            }
        }
        public string Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        }
        public string Top
        {
            get
            {
                return _top;
            }
            set
            {
                _top = value;
            }
        }
        public string Bottom
        {
            get
            {
                return _bottom;
            }
            set
            {
                _bottom = value;
            }
        }
        public string Left
        {
            get
            {
                return _left;
            }
            set
            {
                _left = value;
            }
        }
        public string Right
        {
            get
            {
                return _right;
            }
            set
            {
                _right = value;
            }
        }


        public string BuildCssString()
        {
            string positionCss = string.Empty;
            positionCss += (Postion != string.Empty ? "position:" + Postion + ";" : string.Empty);
            positionCss += (ZIndex != string.Empty ? "z-index:" + ZIndex + ";" : string.Empty);
            positionCss += (Width != string.Empty ? "width:" + Width + ";" : string.Empty);
            positionCss += (Height!= string.Empty ? "height:" + Height + ";" : string.Empty);
            positionCss += (Top != string.Empty ? "top:" + Top + ";" : string.Empty);
            positionCss += (Left != string.Empty ? "left:" + Left + ";" : string.Empty);
            positionCss += (Right != string.Empty ? "right:" + Right + ";" : string.Empty);
            positionCss += (Bottom != string.Empty ? "bottom:" + Bottom + ";" : string.Empty);

            if (positionCss.EndsWith(";"))
                positionCss = positionCss.Remove(positionCss.Length - 1);
            return positionCss;
        }

        public void SetAttributes(Dictionary<string, string> styles)
        {
           Postion = styles.ContainsKey("position") ? styles["position"] : string.Empty;
           ZIndex = styles.ContainsKey("z-index") ? styles["z-index"] : string.Empty;
           Width = styles.ContainsKey("width") ? styles["width"] : string.Empty;
           Height = styles.ContainsKey("height") ? styles["height"] : string.Empty;
           Top = styles.ContainsKey("top") ? styles["top"] : string.Empty;
           Left = styles.ContainsKey("left") ? styles["left"] : string.Empty;
           Right = styles.ContainsKey("right") ? styles["right"] : string.Empty;
           Bottom = styles.ContainsKey("bottom") ? styles["bottom"] : string.Empty;
        }
    }
}
