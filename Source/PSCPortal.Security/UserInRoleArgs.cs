using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.Security
{
    public delegate void UserInRoleDelegate(object sender, UserInRoleArgs args);
    [Serializable]
    public class UserInRoleArgs : EventArgs
    {
        private UserInRole _userInRole;
        public UserInRole UserInRole
        {
            get
            {
                return _userInRole;
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
        public UserInRoleArgs(UserInRole userInRole, bool isEdit)
        {
            _userInRole = userInRole;
            _isEdit = isEdit;
        }
    }
}