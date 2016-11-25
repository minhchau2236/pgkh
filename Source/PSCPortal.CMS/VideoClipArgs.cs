using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;

namespace PSCPortal.CMS
{
    public delegate void VideoClipDelegate(object sender, VideoClipArgs args);
    [Serializable]
    public class VideoClipArgs : EventArgs
    {
        private VideoClip _videoClip;
        public VideoClip VideoClip
        {
            get
            {
                return _videoClip;
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
        public VideoClipArgs(VideoClip videoClip, bool isEdit)
        {
            _videoClip = videoClip;
            _isEdit = isEdit;
        }
    }
}
