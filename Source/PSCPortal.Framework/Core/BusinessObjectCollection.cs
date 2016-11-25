using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace PSCPortal.Framework.Core
{
    [Serializable]
    public abstract class BusinessObjectCollection<T>: ICollection<T> where T: BusinessObject
    {
        #region Attributes
        protected List<T> _listObjects = null;
        protected string _sortExpression = string.Empty;
        #endregion

        #region Construction
        public BusinessObjectCollection()
        {
            _listObjects = new List<T>();
        }
        public BusinessObjectCollection(IEnumerable<T> list)
        {
            _listObjects = new List<T>(list);
        }
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

        #region ICollection<BusinessObject> Members

        public void Add(T item)
        {                        
            _listObjects.Add(item);
            item._collection = this;
        }
        public void AddDB(T item)
        {
            Add(item);
            Database database = new Database(ConnectionStringNameVirtual);
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = item.GetInsertCommand();
                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public T this[int i]
        {
            get
            {
                return _listObjects[i];
            }
        }

        public void Clear()
        {            
            _listObjects.Clear();
        }

        public bool Contains(T item)
        {
            return _listObjects.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _listObjects.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _listObjects.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public void Insert(int index, T item)
        {
            _listObjects.Insert(index, item);
        }
        public IEnumerable<T> GetSegment(int startIndex, int maximumRows, string sortExpressions)
        {            
            _sortExpression = sortExpressions;
            SortExpressionCollection sortExpressionList = null;
            if (sortExpressions != string.Empty)
                sortExpressionList = SortExpressionCollection.Parse(sortExpressions);
            if (sortExpressionList != null)
                return Sort(sortExpressionList).Skip(startIndex).Take(maximumRows);                        

            return _listObjects.Skip(startIndex).Take(maximumRows);
        }        
        private IOrderedEnumerable<T> Sort(SortExpressionCollection sortExpressions)
        {            
            IOrderedEnumerable<T> result = null;
            if (sortExpressions[0].Order == SortExpression.ORDER.ASC)
                result = _listObjects.OrderBy(s=> s.GetPropertySort(sortExpressions[0].PropertyName));
            else
                result = _listObjects.OrderByDescending(s => s.GetPropertySort(sortExpressions[0].PropertyName));
            for(int i = 1 ; i < sortExpressions.Count ; i++)
            {
                SortExpression sort = sortExpressions[i];
                if (sort.Order == SortExpression.ORDER.ASC)
                    result = result.ThenBy(s => s.GetPropertySort(sort.PropertyName));
                else
                    result = result.ThenByDescending(s => s.GetPropertySort(sort.PropertyName));
            }
            return result;
        }     
        public void RemoveAt(int index)
        {
            T item = _listObjects[index];            
            _listObjects.RemoveAt(index);            
        }

        public bool Remove(T item)
        {
            bool result = false;         
            result = _listObjects.Remove(item);
            return result;
        }

        public bool RemoveDB(T item)
        {
            bool result = Remove(item);
            if (result)
            {
                Database database = new Database(ConnectionStringNameVirtual);
                using (DbConnection connection = database.GetConnection())
                {
                    DbCommand command = item.GetDeleteCommand();
                    command.Connection = connection;
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return result;
        }       

        #endregion

        #region IEnumerable<BusinessObject> Members

        public IEnumerator<T> GetEnumerator()
        {
            return _listObjects.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _listObjects.GetEnumerator();
        }

        #endregion

        #region Abstract Methods
        protected internal abstract DbCommand GetSelectAllCommand();
        protected internal static string ConnectionStringName
        {
            get
            {
                return string.Empty;
            }
        }
        #endregion
    }
}
