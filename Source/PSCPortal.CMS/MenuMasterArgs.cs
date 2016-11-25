using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.CMS
{
    public delegate void MenuMasterDelegate(object sender, MenuMasterArgs args);
    [Serializable]
    public class MenuMasterArgs : EventArgs
    {
        private MenuMaster _menuMaster;
        public MenuMaster MenuMaster
        {
            get
            {
                return _menuMaster;
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
        private Guid _pageId;
        public Guid PageId
        {
            get
            {
                return _pageId;
            }
            set
            {
                _pageId = value;
            }
        }
        public MenuMasterArgs(MenuMaster menuMaster, bool isEdit)
        {
            _menuMaster = menuMaster;
            _isEdit = isEdit;
        }
        public MenuMasterArgs(MenuMaster menuMaster, Guid pageId)
        {
            _menuMaster = menuMaster;
            _pageId = pageId;
        }
    }
}