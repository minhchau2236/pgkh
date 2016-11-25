using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.CMS
{
    public delegate void MenuDelegate(object sender, MenuArgs args);
    [Serializable]
    public class MenuArgs : EventArgs
    {
        private Menu _menu;
        public Menu Menu
        {
            get
            {
                return _menu;
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
        public MenuArgs(Menu menu, bool isEdit)
        {
            _menu = menu;
            _isEdit = isEdit;
        }
    }
}