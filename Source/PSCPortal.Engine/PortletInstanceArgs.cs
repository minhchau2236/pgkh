using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.Engine
{
    public delegate void PortletInstanceDelegate(object sender, PortletInstanceArgs args);
    [Serializable]
    public class PortletInstanceArgs : EventArgs
    {
        private PortletInstance _portletInstance;
        public PortletInstance PortletInstance
        {
            get
            {
                return _portletInstance;
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
        public PortletInstanceArgs(PortletInstance portletInstance, bool isEdit)
        {
            _portletInstance = portletInstance;
            _isEdit = isEdit;
        }
    }
}