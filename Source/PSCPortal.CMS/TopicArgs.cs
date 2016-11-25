using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.CMS
{
    public delegate void TopicDelegate(object sender, TopicArgs args);
    [Serializable]
    public class TopicArgs : EventArgs
    {
        private Topic _topic;
        public Topic Topic
        {
            get
            {
                return _topic;
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
        public TopicArgs(Topic topic, bool isEdit)
        {
            _topic = topic;
            _isEdit = isEdit;
        }
    }
}