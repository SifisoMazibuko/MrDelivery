using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MrDelivery.ViewModels
{
    public class PaymentViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Card Number")]
        public string cardNumber { get; set; }
       
        [Display(Name = "Month")]
        [StringLength(2, ErrorMessage ="Required")]
        public string month { get; set; }
   
        [Display(Name = "Year")]
        [StringLength(2, ErrorMessage = "Required")]
        public string year { get; set; }
      
        [Display(Name = "Security Code")]
        [StringLength(3, ErrorMessage = "Required")]
        public string securityCode { get; set; }

        public string Amount { get; set; }
    }
}
