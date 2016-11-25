using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;

namespace PSCPortal.Engine
{
    public delegate void SubDomainDelegate(object sender, SubDomainArgs args);
     [Serializable]
    public class SubDomainArgs : EventArgs
    {
         private SubDomain _subdomain;
         public SubDomain SubDomain
        {
            get
            {
                return _subdomain;
            }
        }
        private bool _isEdit;
        public bool IsEdit
        {
            get
            {
                return _isEdit;
            }
        }
        public SubDomainArgs(SubDomain sub, bool isEdit)
        {
            _subdomain = sub;
            _isEdit = isEdit;
        }       
    }
}
