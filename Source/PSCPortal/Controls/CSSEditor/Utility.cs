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
    public static class Utility
    {
        public static Dictionary<string,string> ParserCss(string css)
        {
            string[] styles = css.Split(";".ToCharArray());
            Dictionary<string, string> stylesTable = new Dictionary<string, string>();

            for (int i = 0; i < styles.Length; i++)
            {
                string[] keyAndValue = new string[2];
                if (styles[i] != string.Empty)
                {
                    try
                    {
                        keyAndValue = styles[i].Split(":".ToCharArray());
                        stylesTable.Add(keyAndValue[0].Trim(), keyAndValue[1].Trim());
                    }
                    catch
                    {
                    }
                }
            }
            return stylesTable;
        }

        // kiem tra xem co chua don vi tinh hay o? nhu px,%,em...
        public static bool ContainUnit(string fontsize)
        {
            return (fontsize.Contains("px") || fontsize.Contains("pt") || fontsize.Contains("cm") || fontsize.Contains("mm") || fontsize.Contains("pc") ||
                fontsize.Contains("em") || fontsize.Contains("ex") || fontsize.Contains("%"));
        }
    }
}
