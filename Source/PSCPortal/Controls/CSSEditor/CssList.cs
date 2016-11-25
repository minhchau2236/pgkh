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
    public class CssList
    {
        private string _liststyletype=string.Empty;
        public string ListStyleType
        {
            get
            {
                return _liststyletype;
            }
            set
            {
                _liststyletype = value;
            }
        }
        private string _liststyleimage=string.Empty;
        public string ListStyleImage
        {
            get
            {
                return _liststyleimage;
            }
            set
            {
                _liststyleimage = value;
            }
        }
        private string _liststyleposition=string.Empty;
        public string ListStylePosition
        {
            get
            {
                return _liststyleposition;
            }
            set
            {
                _liststyleposition = value;
            }
        }
       
        public string BuildCssString()
        {
            string ListCss = string.Empty;
            ListCss += (ListStyleType!=string.Empty?"list-style-type:"+ListStyleType+";":string.Empty);
            ListCss += (ListStyleImage != string.Empty ? "list-style-image:" + ListStyleImage+";" : string.Empty);
            ListCss += (ListStylePosition != string.Empty ? "list-style-position:" + ListStylePosition+";" : string.Empty);
            if (ListCss.EndsWith(";"))
            {
                ListCss = ListCss.Remove(ListCss.Length - 1);
            }
            return ListCss;
        }
       
        public void SetAttributes(Dictionary<string, string> styles)
        {
            ListStyleType = styles.ContainsKey("list-style-type")?styles["list-style-type"]:string.Empty;
            ListStyleImage = styles.ContainsKey("list-style-image") ? styles["list-style-image"] : string.Empty;
            ListStylePosition = styles.ContainsKey("list-style-position") ? styles["list-style-position"] : string.Empty;
            
        }
    }
}
