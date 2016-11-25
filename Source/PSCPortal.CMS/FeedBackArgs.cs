using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.CMS
{
    public delegate void FeedBackDelegate(object sender, FeedBackArgs args);
    [Serializable]
    public class FeedBackArgs : EventArgs
    {
        private FeedBack _feedBack;
        public FeedBack FeedBack
        {
            get
            {
                return _feedBack;
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
        public FeedBackArgs(FeedBack feedBack, bool isEdit)
        {
            _feedBack = feedBack;
            _isEdit = isEdit;
        }
    }
}