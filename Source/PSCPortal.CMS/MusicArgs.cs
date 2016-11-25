using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;

namespace PSCPortal.CMS
{
    public delegate void MusicDelegate(object sender, MusicArgs args);
    [Serializable]
    public class MusicArgs : EventArgs
    {
        private Music _music;
        public Music Music
        {
            get
            {
                return _music;
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
        public MusicArgs(Music music, bool isEdit)
        {
            _music = music;
            _isEdit = isEdit;
        }
    }
}
