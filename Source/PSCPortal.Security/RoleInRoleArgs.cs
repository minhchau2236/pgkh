using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.Security
{
    public delegate void RoleInRoleDelegate(object sender, RoleInRoleArgs args);
    [Serializable]
    public class RoleInRoleArgs : EventArgs
    {
        private RoleInRole _roleInRole;
        public RoleInRole RoleInRole
        {
            get
            {
                return _roleInRole;
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
        public RoleInRoleArgs(RoleInRole roleInRole, bool isEdit)
        {
            _roleInRole = roleInRole;
            _isEdit = isEdit;
        }
    }
}