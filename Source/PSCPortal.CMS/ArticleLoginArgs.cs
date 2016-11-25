using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSCPortal.CMS
{
    public delegate void ArticleLoginDelegate(object sender, TopicLoginArgs args);
    [Serializable]
    public class ArticleLoginArgs
    {
        private ArticleLogin _articleLogin;
        public ArticleLogin ArticleLogin
        {
            get { return _articleLogin; }            
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
        public ArticleLoginArgs(ArticleLogin item, bool isEdit)
        {
            _articleLogin = item;
            _isEdit = isEdit;
        }
    }
}
