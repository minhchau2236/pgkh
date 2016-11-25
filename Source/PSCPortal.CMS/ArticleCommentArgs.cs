using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;

namespace PSCPortal.CMS
{
    public delegate void ArticleCommentDelegate(object sender, ArticleCommentArgs args);
    [Serializable]
    public class ArticleCommentArgs : EventArgs
    {
        private ArticleComment _Artcomment;
        public ArticleComment FeedBack
        {
            get
            {
                return _Artcomment;
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

        //private string _content = string.Empty;
        //public string FeedBackContent
        //{
        //    get
        //    {
        //        return _content;
        //    }
        //    set
        //    {
        //        _content = value;
        //    }
        //}
        //private string _contentreplay = string.Empty;
        //public string FeedBackContentReply
        //{
        //    get
        //    {
        //        return _contentreplay;
        //    }
        //    set
        //    {
        //        _contentreplay = value;
        //    }
        //}
        public ArticleCommentArgs(ArticleComment Artcomment, bool isEdit)
        {
            _Artcomment = Artcomment;
            _isEdit = isEdit;
        }
        //public ArticleCommentArgs(ArticleComment Artcomment, string content, string replay, bool isEdit)
        //{
        //    _Artcomment = Artcomment;
        //    _content = content;
        //    _contentreplay = replay;
        //    _isEdit = isEdit;
        //}
    }
}
