using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.Security
{
    public delegate void UserInSubDomainDelegate(object sender, UserInRoleArgs args);
    [Serializable]
    public class UserInSubDomainArgs : EventArgs
    {
        private UserInSubDomain _userInSubDomain;
        public UserInSubDomain UserInSubDomain
        {
            get
            {
                return _userInSubDomain;
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
        public UserInSubDomainArgs(UserInSubDomain userInRole, bool isEdit)
        {
            _userInSubDomain = userInRole;
            _isEdit = isEdit;
        }
    }
}