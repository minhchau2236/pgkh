using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSCPortal.CMS
{
    [Serializable]
    public class LayoutArgs : EventArgs
    {
        private Layout _layout;
        public Layout Layout    
        {
            get
            {
                return _layout;
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
        private string _name = string.Empty;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        private string _navigationURL = string.Empty;
        public string NavigationURL
        {
            get
            {
                return _navigationURL;
            }
            set
            {
                _navigationURL = value;
            }
        }

      
        public LayoutArgs(Layout layout)
        {
            _layout = layout;
        }
        public LayoutArgs(Layout layout, bool isEdit)
        {
            _layout = layout;
            _isEdit = isEdit;
        }

    }
}
