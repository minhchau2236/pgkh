using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSCPortal.Security
{
    public delegate void ChangeStateDelegate(object sender, ChangeStateArgs e);
    public class ChangeStateArgs : EventArgs
    {
        public enum STATE
        {
            Admin,
            Users,
            UserEdit,
            Roles,
            Topics,
            Articles

        }

        private STATE _state;
        public STATE State
        {
            get
            {
                return _state;
            }
        }
        public ChangeStateArgs(STATE state)
        {
            _state = state;
        }
    }
}
