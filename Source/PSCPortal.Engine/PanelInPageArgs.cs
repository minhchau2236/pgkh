using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.Engine
{
    public delegate void PanelInPageDelegate(object sender, PanelInPageArgs args);
    [Serializable]
    public class PanelInPageArgs : EventArgs
    {
        private PanelInPage _panelInPage;
        public PanelInPage PanelInPage
        {
            get
            {
                return _panelInPage;
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
        public PanelInPageArgs(PanelInPage panelInPage, bool isEdit)
        {
            _panelInPage = panelInPage;
            _isEdit = isEdit;
        }
    }
}