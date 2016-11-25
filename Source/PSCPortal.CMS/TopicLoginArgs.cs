using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSCPortal.CMS
{
    public delegate void TopicLoginDelegate(object sender, TopicLoginArgs args);
    [Serializable]
    public class TopicLoginArgs : EventArgs
    {
        private TopicLogin _topicLogin;
        public TopicLogin TopicLogin
        {
            get { return _topicLogin; }            
        }
        private Topic _topic;
        public Topic Topic
        {
            get { return _topic; }            
        }
        private bool _isEdit;

        public bool IsEdit
        {
            get { return _isEdit; }            
        }
        public TopicLoginArgs(TopicLogin item, bool isEdit)
        {
            _topicLogin = item;
            _isEdit = isEdit;
        }
    }
}
