using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSCPortal.Engine
{
    public class ArticleArgs : IPanelArgs
    {
        #region IPanelArgs Members

        private string _path = "~/Modules/CMS/ArticleDisplay.ascx";
        //public string Path { get; set; }
        public string Path
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
            }
        }

        #endregion
    }
}
