using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.Engine
{
    public delegate void ModuleDelegate(object sender, ModuleArgs args);
    [Serializable]
    public class ModuleArgs : EventArgs
    {
        private Module _module;
        public Module Module
        {
            get
            {
                return _module;
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
        public ModuleArgs(Module module, bool isEdit)
        {
            _module = module;
            _isEdit = isEdit;
        }
    }
}