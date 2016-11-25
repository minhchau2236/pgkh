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
    public class CssBackground
    {
        string _backgroundColor = string.Empty;
        string _backgroundImage = string.Empty;
        string _backgroundRepeat = string.Empty;
        string _backgroundAttachment = string.Empty;
        string _backgroundPositionX = string.Empty;
        string _backgroundPositionY = string.Empty;

        public string BackgroundColor
        {
            get
            {
                return _backgroundColor;
            }
            set
            {
                _backgroundColor=value;
            }
        }

        public string BackgroundImage
        {
            get
            {
                return _backgroundImage;
            }
            set
            {
                _backgroundImage=value;
            }
        }
        
        public string BackgroundRepeat
        {
            get
            {
                return _backgroundRepeat;
            }
            set
            {
                _backgroundRepeat =value;
            }
        }
        
        public string BackgroundAttachment
        {
            get
            {
                return _backgroundAttachment;
            }
            set
            {
                _backgroundAttachment=value;
            }
        }
        
        public string BackgroundPositionX
        {
            get
            {
                return _backgroundPositionX;
            }
            set
            {
                _backgroundPositionX=value;
            }
        }
        
        public string BackgroundPositionY
        {
            get
            {
                return _backgroundPositionY;
            }
            set
            {
               _backgroundPositionY =value;
            }
        }

        public string BuildCssString()
        {
            string backgroundCss = string.Empty;
            backgroundCss += (BackgroundColor != string.Empty ? "background-color:" + BackgroundColor + ";" : string.Empty);
            backgroundCss += (BackgroundImage != string.Empty ? "background-image:" + BackgroundImage + ";" : string.Empty);
            backgroundCss += (BackgroundRepeat != string.Empty ? "background-repeat:" + BackgroundRepeat + ";" : string.Empty);
            backgroundCss += (BackgroundAttachment != string.Empty ? "background-attachment:" + BackgroundAttachment + ";" : string.Empty);

            if (BackgroundPositionX != string.Empty || BackgroundPositionY != string.Empty)
            {
                backgroundCss += "background-position:";
                backgroundCss += (BackgroundPositionX != string.Empty ? " " + BackgroundPositionX : string.Empty);
                backgroundCss += (BackgroundPositionY != string.Empty ? " " + BackgroundPositionY : string.Empty);
            }
            if (backgroundCss.EndsWith(";"))
                backgroundCss = backgroundCss.Remove(backgroundCss.Length - 1);
            return backgroundCss;
        }

        public void SetAttributes(Dictionary<string, string> styles)
        {
            BackgroundColor = styles.ContainsKey("background-color") ? styles["background-color"] : string.Empty;
            BackgroundImage = styles.ContainsKey("background-image") ? styles["background-image"] : string.Empty;
            BackgroundRepeat = styles.ContainsKey("background-repeat") ? styles["background-repeat"] : string.Empty;
            BackgroundAttachment = styles.ContainsKey("background-attachment") ? styles["background-attachment"] : string.Empty;

            string backgroundPosition = styles.ContainsKey("background-position") ? styles["background-position"] : string.Empty;
            if (backgroundPosition != string.Empty)
            {
                string[] keys = backgroundPosition.Split(" ".ToCharArray());
                if (keys.Length == 2)
                {
                    BackgroundPositionX = keys[0];
                    BackgroundPositionY = keys[1];
                }
                else if (backgroundPosition.Length == 1)
                {
                    if (keys[0] == "left" || keys[0] == "right" || keys[0] == "center")
                        BackgroundPositionX = keys[0];
                    else
                        BackgroundPositionY = keys[0];
                }
            }
        }

    }
}
