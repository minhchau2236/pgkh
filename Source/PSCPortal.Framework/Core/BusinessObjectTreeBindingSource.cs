using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace PSCPortal.Framework.Core
{
    [Serializable] 
    public class BusinessObjectTreeBindingSource : System.Web.UI.IHierarchicalEnumerable
    {
        private BusinessObjectHierarchical[] _list;
        internal BusinessObjectTreeBindingSource(BusinessObjectHierarchical[] list)
        {
            _list = list;
        }
        #region IHierarchicalEnumerable Members

        public System.Web.UI.IHierarchyData GetHierarchyData(object enumeratedItem)
        {
            return enumeratedItem as System.Web.UI.IHierarchyData;
        }

        #endregion

        #region IEnumerable Members

        public System.Collections.IEnumerator GetEnumerator()
        {
            return new Enumerator(_list);
        }

        #endregion
        public class Enumerator : IEnumerator
        {
            private BusinessObjectHierarchical[] _items;
            private int _pos = -1;
            internal Enumerator(BusinessObjectHierarchical[] items)
            {
                _items = items;
            }

            #region IEnumerator Members

            public object Current
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
    }
}
