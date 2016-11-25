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
    public class CssTable
    {
        private string _tablelayout=string.Empty;
        public string TableLayout
        {
            get
            {
                return _tablelayout;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _tablelayout = value;
            }
        }
        private string _bordercolapse = string.Empty;
        public string BorderColapse
        {
            get
            {
                return _bordercolapse;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _bordercolapse = value;
            }
        }
        private string _borderspacing = string.Empty;
        public string BorderSpacing
        {
            get
            {
                return _borderspacing;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _borderspacing = value;
            }
        }

        private string _emptycells = string.Empty;
        public string EmptyCells
        {
            get
            {
                return _emptycells;

            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _emptycells = value;
            }
        }
        private string _captionside = string.Empty;
        public string CaptionSide
        {
            get
            {
                return _captionside;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _captionside = value;
            }
        }
      
        public string BuildCssString()
        {
            string TableCss = string.Empty;
            TableCss += (TableLayout != string.Empty ? "table-layout:" + TableLayout + ";" : string.Empty);
            TableCss += (BorderColapse != string.Empty ? "border-colapse:" + BorderColapse + ";" : string.Empty);
            TableCss += (BorderSpacing != string.Empty ? "border-spacing:" + BorderSpacing + ";" : string.Empty);
            TableCss += (EmptyCells != string.Empty ? "empty-cells:" + EmptyCells + ";" : string.Empty);
            TableCss += (CaptionSide != string.Empty ? "caption-side:" + CaptionSide + ";" : string.Empty);
            if (TableCss.EndsWith(";"))
                TableCss = TableCss.Remove(TableCss.Length - 1);
            return TableCss;
        }
        public void SetAttributes(Dictionary<string, string> styles)
        {
            TableLayout = styles.ContainsKey("table-layout") ? styles["table-layout"] : string.Empty;
            BorderColapse = styles.ContainsKey("border-colapse") ? styles["border-colapse"] : string.Empty;
            BorderSpacing = styles.ContainsKey("border-spacing") ? styles["border-spacing"] : string.Empty;
            EmptyCells = styles.ContainsKey("empty-cells") ? styles["empty-cells"] : string.Empty;
            CaptionSide = styles.ContainsKey("caption-side") ? styles["caption-side"] : string.Empty;
 
        }
    }
}
