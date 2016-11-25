using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;

namespace PSCPortal.CMS
{
    public delegate void FolderClipNewDelegate(object sender, FolderClipNewArgs args);
    [Serializable]
    public class FolderClipNewArgs : EventArgs
    {
        private FolderClipNew _folder;
        public FolderClipNew Folder
        {
            get
            {
                return _folder;
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
        public FolderClipNewArgs(FolderClipNew Album, bool isEdit)
        {
            _folder = Album;
            _isEdit = isEdit;
        }
    }
}
