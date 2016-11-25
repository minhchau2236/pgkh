using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;

namespace PSCPortal.CMS
{
    public delegate void LinkWebsiteDelegate(object sender, LinkWebsiteArgs args);
    [Serializable]
    public class LinkWebsiteArgs : EventArgs
    {
        private LinkWebsite _link;
        public LinkWebsite LinkWebsite
        {
            get
            {
                return _link;
            }
        }
        private bool _isEdit;
        public bool IsEdit
        {
            get
            {
                return _isEdit;
            }
        }
        public LinkWebsiteArgs(LinkWebsite link, bool isEdit)
        {
            _link = link;
            _isEdit = isEdit;
        }
    }
}
