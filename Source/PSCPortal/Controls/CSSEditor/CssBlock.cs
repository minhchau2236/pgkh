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
    public class CssBlock
    {
        string _lineHeight = string.Empty;
        string _verticalAlign = string.Empty;
        string _textAlign = string.Empty;
        string _textIndent = string.Empty;
        string _whileSpace = string.Empty;
        string _wordSpacing = string.Empty;
        string _letterSpacing = string.Empty;

        public string LineHeight
        {
            get
            {
                return _lineHeight;
            }
            set
            {
                _lineHeight = value;
            }

        }

        public string TextAlign
        {
            get
            {
                return _textAlign;
            }
            set
            {
                _textAlign = value;
            }
        }

        public string TextIndent
        {
            get
            {
                return _textIndent;
            }
            set
            {
                _textIndent = value;
            }
        }

        public string WhileSpace
        {
            get
            {
                return _whileSpace;
            }
            set
            {
                _whileSpace = value;
            }
        }

        public string WordSpacing
        {
            get
            {
                return _wordSpacing;
            }
            set
            {
                _wordSpacing = value;
            }

        }

        public string VerticalAlign
        {
            get
            {
                return _verticalAlign;
            }
            set
            {
                _verticalAlign = value;
            }

        }

        public string LetterSpacing
        {
            get
            {
                return _letterSpacing;
            }
            set
            {
                _letterSpacing = value;
            }
        }


        public string BuildCssString()
        {
            string blockCss = string.Empty;
            blockCss += ( LineHeight!= string.Empty ? "line-height:" + LineHeight + ";" : string.Empty);
            blockCss += (VerticalAlign != string.Empty ? "vertical-align:" + VerticalAlign + ";" : string.Empty);
            blockCss += (TextAlign!= string.Empty ? "text-align:" + TextAlign + ";" : string.Empty);
            blockCss += (TextIndent != string.Empty ? "text-indent:" + TextIndent + ";" : string.Empty);
            blockCss += (WhileSpace != string.Empty ? "white-space:" + WhileSpace + ";" : string.Empty);
            blockCss += (WordSpacing != string.Empty ? "word-spacing:" +WordSpacing + ";" : string.Empty);
            blockCss += (LetterSpacing != string.Empty ? "letter-spacing:"+LetterSpacing +";" : string.Empty);
            
            if (blockCss.EndsWith(";"))
                blockCss = blockCss.Remove(blockCss.Length - 1);
            return blockCss;
        }

        public void SetAttributes(Dictionary<string, string> styles)
        {
            LineHeight = styles.ContainsKey("line-height") ? styles["line-height"] : string.Empty;
            VerticalAlign = styles.ContainsKey("vertical-align") ? styles["vertical-align"] : string.Empty;
            TextAlign = styles.ContainsKey("text-align") ? styles["text-align"] : string.Empty;
            TextIndent = styles.ContainsKey("text-indent") ? styles["text-indent"] : string.Empty;
            WhileSpace = styles.ContainsKey("white-space") ? styles["white-space"] : string.Empty;
            WordSpacing = styles.ContainsKey("word-spacing") ? styles["word-spacing"] : string.Empty;
            LetterSpacing = styles.ContainsKey("letter-spacing:") ? styles["letter-spacing:"] : string.Empty;
        }

    }
}
