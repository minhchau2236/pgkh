using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace PSCPortal.Framework.Core
{
    [Serializable]
    public abstract class BusinessObject
    {
        #region Abstract Methods
        protected internal abstract void MappingData(DbDataReader reader);
        protected internal abstract DbCommand GetInsertCommand();
        protected internal abstract DbCommand GetUpdateCommand();
        protected internal abstract DbCommand GetDeleteCommand();
        protected internal static string ConnectionStringName
        {
            get
            {
                return string.Empty;
            }
        }
        #region Attributes
        protected internal object _collection;
        #endregion
        #region Properties
        private string ConnectionStringNameVirtual
        {
            get
            {
                return (string)this.GetType().GetProperty("ConnectionStringName", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic).GetValue(this, null);
            }
        }
        #endregion
        #endregion

        #region Construction
        protected BusinessObject()
        {            
        }
        protected internal BusinessObject(DbDataReader reader)
        {
            MappingData(reader);            
        }        
        #endregion
        #region Methods
        public IComparable GetPropertySort(string propertyName)
        {
            IComparable result = (IComparable)GetType().GetProperty(propertyName).GetValue(this, null);
            return result;
        }
        public void Update()
        {
            Database database = new Database(ConnectionStringNameVirtual);
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = GetUpdateCommand();
                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }       
        #endregion
    }
}
