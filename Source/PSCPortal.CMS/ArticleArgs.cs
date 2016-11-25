using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.CMS
{
    public delegate void ArticleDelegate(object sender, ArticleArgs args);
    [Serializable]
    public class ArticleArgs : EventArgs
    {
        private Article _article;
        public Article Article
        {
            get
            {
                return _article;
            }

        }
        private string _avatar = string.Empty;
        public string Avatar
        {
            get
            {
                return _avatar;
            }
            set
            {
                _avatar = value;
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
        private string _description = string.Empty;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }
        private string _content = string.Empty;
        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
            }
        }

        private byte[] _artImage = null;
        public byte[] ArtImage
        {
            get
            {
                return _artImage;
            }
            set
            {
                _artImage = value;
            }
        }
        public ArticleArgs(Article article, string description)
        {
            _article = article;
            _description = description;
        }
        public ArticleArgs(Article article, string description, string content, bool isEdit)
        {
            _article = article;
            _isEdit = isEdit;
            _description = description;
            _content = content;
        }
    }
}