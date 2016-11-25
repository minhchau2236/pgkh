using System;

namespace PSCPortal.Portlets.TabTopicDisplay.Lib
{
    public delegate void TabTopicDisplayDelegate(object sender, TabTopicDisplayArgs args);
    [Serializable]
    public class TabTopicDisplayArgs : EventArgs
    {
        private readonly TabTopicDisplay _tabTopicDisplay;
        public TabTopicDisplay TabTopicDisplay
        {
            get
            {
                return _tabTopicDisplay;
            }
        }
        private readonly bool _isEdit;
        public bool IsEdit
        {
            get
            {
                return _isEdit;
            }
        }
        public TabTopicDisplayArgs(TabTopicDisplay tabTopicDisplay, bool isEdit)
        {
            _tabTopicDisplay = tabTopicDisplay;
            _isEdit = isEdit;
        }
    }
}