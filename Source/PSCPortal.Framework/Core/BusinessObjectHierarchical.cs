using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace PSCPortal.Framework.Core
{
    [Serializable] 
    public abstract class BusinessObjectHierarchical : BusinessObject, System.Web.UI.IHierarchyData
    {
        #region Construction
        protected BusinessObjectHierarchical()
            :base()
        {            
        }
        protected internal BusinessObjectHierarchical(DbDataReader reader)
            :base(reader)
        {                      
        }        
        #endregion
        #region Tree Relation
        protected internal BusinessObjectHierarchical _parent;
        [System.Web.Script.Serialization.ScriptIgnore]
        public BusinessObjectHierarchical Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                Tree.ChangeNodeParent(value, this);             
            }
        }
        protected internal BusinessObjectTree _tree;
        internal BusinessObjectTree Tree
        {
            get
            {
                return _tree;
            }
            set
            {
                _tree = value;
            }
        }
        [System.Web.Script.Serialization.ScriptIgnore]
        public BusinessObjectHierarchicalCollection Childs
        {
            get
            {
                return Tree.GetChilds(this);
            }
        }
        #endregion        
        #region IHierarchyData Members

        public System.Web.UI.IHierarchicalEnumerable GetChildren()
        {
            return Tree.GetChilds(this);
        }

        public System.Web.UI.IHierarchyData GetParent()
        {
            return _parent as System.Web.UI.IHierarchyData;
        }
        [System.Web.Script.Serialization.ScriptIgnore]
        public bool HasChildren
        {
            get { return Tree.GetChilds(this).Count > 0; }
        }
        [System.Web.Script.Serialization.ScriptIgnore]
        public object Item
        {
            get { return this; }
        }
        [System.Web.Script.Serialization.ScriptIgnore]
        public virtual string Path
        {
            get
            {
                if (_parent == null)
                    return string.Empty;
                string result = string.Empty;
                int level = 0;
                BusinessObjectHierarchical parent = _parent;
                while (parent != null)
                {
                    level++;
                    parent = parent._parent;
                }
                for (int i = 0; i < level; i++)
                    //result += "....";&nbsp;
                    result += System.Web.HttpUtility.HtmlDecode("&nbsp;&nbsp;&nbsp;&nbsp;");
                result += "└─" + ToString();
                return result;
            }
        }
        [System.Web.Script.Serialization.ScriptIgnore]
        public string Type
        {
            get
            {
                return this.GetType().ToString();
            }
        }

        #endregion    
    }
}
