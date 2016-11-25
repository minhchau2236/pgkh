using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.Security
{
    [Serializable]
    public class Function
    {
        #region Properties
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


        #endregion

        #region Constructions
        public Function(DbDataReader reader)
        {
            MappingData(reader);
        }
        #endregion
        protected void MappingData(System.Data.Common.DbDataReader reader)
        {
            _id = (int)reader["FunctionId"];
            _name = (string)reader["FunctionName"];
        }
        public override int GetHashCode()
        {
            int hashCode = 0;
            hashCode += _id.GetHashCode();
            return hashCode;
        }
        public static FUNCTIONS Parse(int id)
        {
            return (FUNCTIONS) Enum.ToObject(typeof(FUNCTIONS), id);
        }
    }
}