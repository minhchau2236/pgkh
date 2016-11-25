#region ClipNewArgs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.CMS
{
    public delegate void ClipNewDelegate(object sender, ClipNewArgs args);
    [Serializable]
    public class ClipNewArgs : EventArgs
    {
        private ClipNew _ClipNew;
        public ClipNew ClipNew
        {
            get
            {
                return _ClipNew;
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
        public ClipNewArgs(ClipNew ClipNew, bool isEdit)
        {
            _ClipNew = ClipNew;
            _isEdit = isEdit;
        }
    }
}
#endregion