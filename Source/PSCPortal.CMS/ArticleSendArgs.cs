using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.CMS
{
    public delegate void ArticleSendDelegate(object sender, ArticleSendArgs args);
    [Serializable]
    public class ArticleSendArgs : EventArgs
    {
        private Guid _topicId;
        public Guid TopicId
        {
            get
            {
                return _topicId;
            }

        }
        private Guid _pageId;
        public Guid PageId
        {
            get
            {
                return _pageId;
            }

        }
        public ArticleSendArgs(Guid topicId, Guid pageId)
        {
            _topicId = topicId;
            _pageId = pageId;
        }
        
    }
}