using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt.Data.Audit
{
    public class Entity
    {
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
