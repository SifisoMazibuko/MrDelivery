using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Group
    {
        public String groupName { get; set; }
        public String members { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
