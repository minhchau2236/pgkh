using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.Security
{
    public delegate void UserDelegate(object sender, UserArgs args);
    [Serializable]
    public class UserArgs : EventArgs
    {
        private User _user;
        public User User
        {
            get
            {
                return _user;
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

        private bool _isAdministrator = false;

        public bool IsAdministrator
        {
            get
            {
                return _isAdministrator;
            }

            set
            {
                _isAdministrator = value;
            }
        }
        public UserArgs(User user, bool isEdit)
        {
            _user = user;
            _isEdit = isEdit;
        }
        public UserArgs(User user, bool isEdit, bool isAdministrator)
        {
            _user = user;
            _isEdit = isEdit;
            _isAdministrator = isAdministrator;
        }
    }
}