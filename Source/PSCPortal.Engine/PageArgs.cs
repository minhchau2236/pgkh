using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.Engine
{
    public delegate void PageDelegate(object sender, PageArgs args);
    [Serializable]
    public class PageArgs : EventArgs
    {
        private Page _page;
        public Page Page
        {
            get
            {
                return _page;
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
 
        public PageArgs(Page page, bool isEdit)
        {
            _page = page;
            _isEdit = isEdit;
        }
    }
}