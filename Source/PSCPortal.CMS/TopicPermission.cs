using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;

namespace PSCPortal.CMS
{
    [Serializable]
    public class TopicPermission
    {
        public enum PERMISSION
        {
            PERMISSION = 1,
            ARTICLE_EDIT,
            ARTICLE_DELETE,
            ARTICLE_NEW,
            ARTICLE_APROVE
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

        public TopicPermission()
        {
        }
        internal TopicPermission(DbDataReader reader)
        {
            _id = (int)reader["TopicPermissionId"];
            _name = (string)reader["TopicPermissionName"];
        }
        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(TopicPermission)
                && ((TopicPermission)obj)._id == _id
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
        public static TopicPermission Parse(PERMISSION permission)
        {
            TopicPermission result = new TopicPermission();
            result._id = (int)permission;
            return result;
        }
    }    
}
