using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace PSCPortal.CMS
{
    [Serializable]
    public class MenuMasterPermission
    {
        public enum PERMISSION
        {
            MenuMaster_EditStruct = 1
        }
        private int _id;
        public int Id
        {
            get
            {
                return _id;
            }
        }

        private string _name = string.Empty;
        public string Name
        {
            get
            {
                return _name;
            }
        }

        public MenuMasterPermission()
        {
        }
        internal MenuMasterPermission(DbDataReader reader)
        {
            _id = (int)reader["MenuMasterPermissionId"];
            _name = (string)reader["MenuMasterPermissionName"];
        }
        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(MenuMasterPermission)
                && ((MenuMasterPermission)obj)._id == _id
               )
                return true;
            return false;
        }
        public override int GetHashCode()
        {
            int hashCode = 0;
            hashCode += _id.GetHashCode();
            return hashCode;
        }
        public static MenuMasterPermission Parse(PERMISSION permission)
        {
            MenuMasterPermission result = new MenuMasterPermission();
            result._id = (int)permission;
            return result;
        }
    }    
}
