using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;

namespace PSCPortal.DB.Entities
{
    public class Topic
    {
        public virtual Guid TopicId { get; set; }
        public virtual string TopicName { get; set; }
        public virtual string TopicDescription { get; set; }
        public virtual Guid? TopicParent { get; set; }
        public virtual Guid PageId { get; set; }
        public virtual int TopicOrder { get; set; }
        public virtual bool Rss { get; set; }
    }
}
