using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Common;

namespace PSCPortal.Framework.Core
{
    [Serializable] 
    public abstract class BusinessObjectTree : IEnumerable
    {
        #region Properties
        private string ConnectionStringNameVirtual
        {
            get
            {
                return (string)this.GetType().GetProperty("ConnectionStringName", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic).GetValue(this, null);
            }
        }
        #endregion
        protected BusinessObjectHierarchical _root;        
        private Dictionary<BusinessObjectHierarchical, BusinessObjectHierarchicalCollection> _list;
        protected BusinessObjectTree(BusinessObjectHierarchical root)
        {            
            _root = root;
            
            _root.Tree = this;
            _list = new Dictionary<BusinessObjectHierarchical, BusinessObjectHierarchicalCollection>();
            _list.Add(_root, new BusinessObjectHierarchicalCollection());            
        }        
        protected internal BusinessObjectHierarchicalCollection GetChilds(BusinessObjectHierarchical item)
        {           
            return _list[item];
        }
        protected internal void ChangeNodeParent(BusinessObjectHierarchical parent, BusinessObjectHierarchical item)
        {
            _list[item._parent].Remove(item);
            _list[parent].Add(item);
            item._parent = parent;
        }
        public virtual void Add(BusinessObjectHierarchical parent, BusinessObjectHierarchical item)
        {                       
            _list[parent].Add(item);
            _list.Add(item, new BusinessObjectHierarchicalCollection());
            item._parent = parent;
            item._tree = this;
        }
        public void Add(BusinessObjectHierarchical item)
        {
            Add(_root, item);
        }
        public void AddDB(BusinessObjectHierarchical parent, BusinessObjectHierarchical item)
        {
            Add(parent, item);

            Database database = new Database(ConnectionStringNameVirtual);
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = item.GetInsertCommand();
                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void AddDB(BusinessObjectHierarchical item)
        {
            Add(_root, item);

            Database database = new Database(ConnectionStringNameVirtual);
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = item.GetInsertCommand();
                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public BusinessObjectHierarchical Search(Predicate<BusinessObjectHierarchical> method)
        {
            BusinessObjectHierarchical result = null;
            System.Collections.Generic.Stack<BusinessObjectHierarchical> stack = new Stack<BusinessObjectHierarchical>();
            stack.Push(_root);
            while (stack.Count > 0)
            {
                BusinessObjectHierarchical temp = stack.Pop();
                if (method.Invoke(temp))
                {
                    result = temp;
                }
                for (int j = _list[temp].Count - 1; j >= 0; j--)
                    stack.Push(_list[temp][j]);                
            }
            return result;            
        }
        public virtual void Remove(BusinessObjectHierarchical item)
        {
            System.Collections.Generic.Queue<BusinessObjectHierarchical> queue = new Queue<BusinessObjectHierarchical>();
            queue.Enqueue(item);
            while (queue.Count > 0)
            {
                BusinessObjectHierarchical temp = queue.Dequeue();
                Dictionary<BusinessObjectHierarchical, BusinessObjectHierarchicalCollection>.Enumerator it = _list.GetEnumerator();
                while (it.MoveNext())
                    if (it.Current.Value.Remove(temp))
                        break;
                foreach (BusinessObjectHierarchical child in _list[temp])
                    queue.Enqueue(child);
                _list.Remove(temp);             
            }
        }
        public void RemoveDB(BusinessObjectHierarchical item)
        {
            Remove(item);
            Database database = new Database(ConnectionStringNameVirtual);
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = item.GetDeleteCommand();
                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public class Enumerator : IEnumerator<BusinessObjectHierarchical>
        {
            private BusinessObjectHierarchical[] _items;
            private int _pos = -1;
            internal Enumerator(BusinessObjectHierarchical[] items)
            {
                _items = items;
            }
            #region IEnumerator<BusinessObjectHierarchical> Members

            public BusinessObjectHierarchical Current
            {
                get { return _items[_pos]; }
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {                
            }

            #endregion

            #region IEnumerator Members

            object System.Collections.IEnumerator.Current
            {
                get { return _items[_pos]; }
            }

            public bool MoveNext()
            {
                _pos++;
                return _pos < _items.Length;
            }

            public void Reset()
            {
                _pos = -1;
            }

            #endregion
        }

        #region IEnumerable<T> Members

        public IEnumerator<BusinessObjectHierarchical> GetEnumerator()
        {
            BusinessObjectTree.Enumerator it = new BusinessObjectTree.Enumerator(GetArray());
            return it;
        }

        #endregion

        private BusinessObjectHierarchical[] GetArray()
        {
            int count = _list.Count;
            BusinessObjectHierarchical[] result = new BusinessObjectHierarchical[count];
            int i = 0;
            System.Collections.Generic.Stack<BusinessObjectHierarchical> stack = new Stack<BusinessObjectHierarchical>();
            stack.Push(_root);
            while (stack.Count > 0)
            {
                BusinessObjectHierarchical temp = stack.Pop();
                for (int j = _list[temp].Count - 1; j >= 0; j--)
                    stack.Push(_list[temp][j]);              
                result[i++] = temp;
            }
            return result;
        }
        public List<BusinessObjectHierarchical> GetAll(BusinessObjectHierarchical node)
        {
            List<BusinessObjectHierarchical> result = new List<BusinessObjectHierarchical>();
            System.Collections.Generic.Stack<BusinessObjectHierarchical> stack = new Stack<BusinessObjectHierarchical>();
            stack.Push(node);
            while (stack.Count > 0)
            {
                BusinessObjectHierarchical temp = stack.Pop();
                for (int j = _list[temp].Count - 1; j >= 0; j--)
                    stack.Push(_list[temp][j]);
                result.Add(temp);
            }
            return result;
        }

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            BusinessObjectTree.Enumerator it = new BusinessObjectTree.Enumerator(GetArray());
            return it;
        }

        #endregion

        public BusinessObjectTreeBindingSource GetBindingSource()
        {
            BusinessObjectHierarchical[] list = new BusinessObjectHierarchical[_list[_root].Count];
            int i = 0;
            foreach (BusinessObjectHierarchical item in _list[_root])
                list.SetValue(item, i++);
            return new BusinessObjectTreeBindingSource(list);
        }

        #region Access Database     
        protected abstract DbCommand GetSelectAllCommand();
        #endregion
        protected static string ConnectionStringName
        {
            get
            {
                return string.Empty;
            }
        }
    }
}
