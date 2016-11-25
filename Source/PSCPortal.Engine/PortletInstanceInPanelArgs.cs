using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.Engine
{
    public delegate void PortletInstanceInPanelDelegate(object sender, PortletInstanceInPanelArgs args);
    [Serializable]
    public class PortletInstanceInPanelArgs : EventArgs
    {
        private PortletInstanceInPanel _portletInstanceInPanel;
        public PortletInstanceInPanel PortletInstanceInPanel
        {
            get
            {
                return _portletInstanceInPanel;
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
        public PortletInstanceInPanelArgs(PortletInstanceInPanel portletInstanceInPanel, bool isEdit)
        {
            _portletInstanceInPanel = portletInstanceInPanel;
            _isEdit = isEdit;
        }
    }
}