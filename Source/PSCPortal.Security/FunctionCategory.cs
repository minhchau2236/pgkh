using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.Security
{
    [Serializable]
    public class FunctionCategory
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
        private FunctionCollection _functionList = new FunctionCollection();
        public FunctionCollection FunctionList
        {
            get
            {
                return _functionList;
            }
        }
        #endregion

        #region Constructions
        public FunctionCategory()
            : base()
        {
        }

        public FunctionCategory(DbDataReader reader)
        {
            MappingData(reader);
        }
        #endregion       
        protected void MappingData(System.Data.Common.DbDataReader reader)
        {
            _id = (int)reader["FunctionCategoryId"];
            _name = (string)reader["FunctionCategoryName"];
        }
        

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(FunctionCategory)
                && ((FunctionCategory)obj)._id == _id
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
    }
}