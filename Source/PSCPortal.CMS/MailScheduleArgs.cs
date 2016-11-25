using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;

namespace PSCPortal.CMS
{
    public delegate void MailScheduleDelegate(object sender, MailScheduleArgs args);
    [Serializable]
    public class MailScheduleArgs : EventArgs
    {
        private MailSchedule _MailSchedule;
        public MailSchedule MailSchedule
        {
            get
            {
                return _MailSchedule;
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
        public MailScheduleArgs(MailSchedule Mail, bool isEdit)
        {
            _MailSchedule = Mail;
            _isEdit = isEdit;
        }
    }
}
