using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Payment : BaseEntity
    {
        public string Name { get; set; }
        public string cardNumber { get; set; }
        public string month { get; set; }
        public string year { get; set; }
        public string securityCode { get; set; }
        public decimal Amount { get; set; }
    }
}
