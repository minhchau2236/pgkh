using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.Security
{
    public delegate void RoleDelegate(object sender, RoleArgs args);
    [Serializable]
    public class RoleArgs : EventArgs
    {
        private Role _role;
        public Role Role
        {
            get
            {
                return _role;
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
        public RoleArgs(Role role, bool isEdit)
        {
            _role = role;
            _isEdit = isEdit;
        }
    }
}