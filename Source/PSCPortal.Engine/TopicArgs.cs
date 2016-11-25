using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSCPortal.Engine
{
    public class TopicArgs:IPanelArgs
    {
        #region IPanelArgs Members

        private Guid _pageId { get; set; }
        public TopicArgs(Guid pageId)
        {
            _pageId = pageId;
        }
        public string Path
        {
           
            get {
                if (_pageId != Guid.Empty) // Khong phai trang chu
                    return "~/Modules/CMS/TopicDisplay2.ascx"; 
                else
                    return "~/Modules/CMS/TopicDisplay.ascx"; 
            
            }
        }

        #endregion
    }
}
