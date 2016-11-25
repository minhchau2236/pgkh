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
    public class CssFont
    {
        string _fontFamily = string.Empty;
        string _fontSize = string.Empty;
        string _fontWeight = string.Empty;
        string _fontStyle = string.Empty;
        string _fontVariant = string.Empty;
        string _fontColor = string.Empty;
        string _textTransform = string.Empty;

        string _underline = string.Empty;
        string _overline = string.Empty;
        string _linethrough = string.Empty;
        string _blink = string.Empty;
        string _none = string.Empty;       

        public string FontColor
        {
            get
            {
                return _fontColor;
            }
            set
            {
                _fontColor= value;
            }
        }

        /// <summary>
        /// //////////
        /// </summary>
        public string FontFamily
        {
            get
            {
                return _fontFamily;
            }
            set
            {
                _fontFamily = value;
            }
        }

        public string FontSize
        {
            get
            {
                return _fontSize;
            }
            set
            {
                _fontSize=value;
            }
        }

        public string FontWeight
        {
            get
            {
                return _fontWeight;
            }
            set
            {
                _fontWeight=value;
            }
        }

        public string FontStyle
        {
            get
            {
                return _fontStyle;
            }
            set
            {
                _fontStyle = value;
            }
        }

        public string FontVariant
        {
            get
            {
                return _fontVariant;
            }
            set
            {
                _fontVariant = value;
            }
        }
        
        public string Underline
        {
            get
            {
               return _underline; 
            }
            set
            {
                _underline = value;
            }
        }

        public string TextTransform
        {
            get
            {
                return _textTransform;
            }
            set
            {
                _textTransform = value;
            }
        }

        public string Overline
        {
            get
            {
                return _overline;
            }
            set
            {
                _overline = value;
            }
        }

        public string LineThrough
        {
            get
            {
                return _linethrough;
            }
            set
            {
                _linethrough = value;
            }
        }

        public string Blink
        {
            get
            {
                return _blink;
            }
            set
            {
                _blink = value;
            }
        }

        public string None
        {
            get
            {
                return _none;
            }
            set
            {
                _none = value;
            }
        }

        public void SetUnderline(bool underline)
        {
            if (underline)
                _underline = "underline";
            else
                _underline = string.Empty;
        }

        public void SetOverline(bool overline)
        {
            if (overline)
                _overline = "overline";
            else
                _overline = string.Empty;
        }

        public void SetLineThrough(bool linethrough)
        {
            if (linethrough)
                _linethrough = "line-through";
            else
                _linethrough = string.Empty;
        }

        public void SetBlink(bool blink)
        {
            if (blink)
                _blink = "blink";
            else
                _blink = string.Empty;
        }

        public void SetNone(bool none)
        {
            if (none)
                _none = "none";
            else
                _none = string.Empty;
        }



        public string BuildCssString()
        {
            string fontCss = string.Empty;
            fontCss += (FontFamily != string.Empty?"font-family:"+FontFamily+";":string.Empty);     
            fontCss += (FontSize != string.Empty?"font-size:"+FontSize+";":string.Empty);
            fontCss += (FontWeight != string.Empty ? "font-weight:" + FontWeight + ";" : string.Empty);
            fontCss += (FontStyle != string.Empty ? "font-style:" + FontStyle + ";" : string.Empty);
            fontCss += (FontVariant != string.Empty ? "font-variant:" + FontVariant + ";" : string.Empty);
            fontCss += (TextTransform != string.Empty ? "text-transform:" + TextTransform + ";" : string.Empty);

            fontCss += (FontColor != string.Empty ? "color:" + FontColor + ";" : string.Empty);

            if (Underline != string.Empty || Overline != string.Empty || LineThrough != string.Empty || Blink != string.Empty || None != string.Empty)
            {
                fontCss += "text-decoration:";
                fontCss += (Underline != string.Empty ? " " + Underline : string.Empty);
                fontCss += (Overline != string.Empty ? " " + Overline : string.Empty);
                fontCss += (LineThrough != string.Empty ? " " + LineThrough : string.Empty);
                fontCss += (Blink != string.Empty ? " " + Blink : string.Empty);
                fontCss += (None != string.Empty ? " " + None : string.Empty);
            }
            if (fontCss.EndsWith(";"))
                fontCss = fontCss.Remove(fontCss.Length - 1);
            return fontCss;
        }

        public void SetAttributes(Dictionary<string, string> styles)
        {
            FontFamily = styles.ContainsKey("font-family") ? styles["font-family"] : string.Empty;
            FontSize = styles.ContainsKey("font-size") ? styles["font-size"] : string.Empty;
            FontWeight = styles.ContainsKey("font-weight") ? styles["font-weight"] : string.Empty;
            FontStyle = styles.ContainsKey("font-style")? styles["font-style"] : string.Empty;
            FontVariant = styles.ContainsKey("font-variant") ? styles["font-variant"] : string.Empty;
            TextTransform = styles.ContainsKey("text-transform") ? styles["text-transform"] : string.Empty;
            FontColor = styles.ContainsKey("color") ? styles["color"] : string.Empty;

            string textDecoration = styles.ContainsKey("text-decoration") ? styles["text-decoration"] : string.Empty;
            if (textDecoration != string.Empty)
            {
                string[] keys = textDecoration.Split(" ".ToCharArray());

                Dictionary<string, string> keyAndValue = new Dictionary<string, string>();

                foreach (string key in keys)
                {
                    keyAndValue.Add(key, key);
                }

                Underline = keyAndValue.ContainsKey("underline") ? keyAndValue["underline"] : string.Empty;
                Overline = keyAndValue.ContainsKey("overline") ? keyAndValue["overline"] : string.Empty;
                LineThrough = keyAndValue.ContainsKey("line-through") ? keyAndValue["line-through"] : string.Empty;
                Blink = keyAndValue.ContainsKey("blink") ? keyAndValue["blink"] : string.Empty;
                None = keyAndValue.ContainsKey("none") ? keyAndValue["none"] : string.Empty;
            }
        }
    }
}
