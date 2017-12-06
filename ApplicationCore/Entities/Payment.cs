using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Payment : BaseEntity
    {
        public String PaymentOption { get; set; }
        public String BillingAddress { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public int ZipCode { get; set; }
        public String CardName { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryDate { get; set; }
    }
}
