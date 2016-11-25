using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace PSCPortal.Engine
{
    [Serializable]
    public class PagePermission
    {
        
        public enum PERMISSION
        {
            Page_EditStruct = 1
        }
        private int _id;
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        private string _name = string.Empty;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public PagePermission()
        {
        }
        internal PagePermission(DbDataReader reader)
        {
            _id = (int)reader["PagePermissionId"];
            _name = (string)reader["PagePermissionName"];
        }
        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(PagePermission)
                && ((PagePermission)obj)._id == _id
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
        public static PagePermission Parse(PERMISSION permission)
        {
            PagePermission result = new PagePermission();
            result._id = (int)permission;
            return result;
        }
    }
}
