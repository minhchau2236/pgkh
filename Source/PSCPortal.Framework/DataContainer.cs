using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSCPortal.Framework
{
    [Serializable]
    public class DataContainer
    {
        private Dictionary<string, object> _data = new Dictionary<string,object>();
        public object this[string key]
        {
            get
            {
                if (_data.ContainsKey(key))
                    return _data[key];
                return null;
            }
            set
            {
                if (!_data.ContainsKey(key))
                    _data.Add(key, null);
                _data[key] = value;
            }
        }

        private Dictionary<string, object> _dataChilds = null;
        internal Dictionary<string, object> DataChilds
        {
            get
            {
                if (_dataChilds == null)
                    _dataChilds = new Dictionary<string, object>();
                return _dataChilds;
            }
        }
        public DataContainer()
        {
        }
    }
}
