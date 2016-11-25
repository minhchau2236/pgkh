using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.Engine
{
    public delegate void PortletDelegate(object sender, PortletArgs args);
    [Serializable]
    public class PortletArgs : EventArgs
    {
        private Portlet _portlet;
        public Portlet Portlet
        {
            get
            {
                return _portlet;
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
        public PortletArgs(Portlet portlet, bool isEdit)
        {
            _portlet = portlet;
            _isEdit = isEdit;
        }
    }
}