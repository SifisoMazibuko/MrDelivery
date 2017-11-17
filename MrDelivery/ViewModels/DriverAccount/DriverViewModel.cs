using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MrDelivery.ViewModels.DriverAccount
{
    public class DriverViewModel
    {
        
        [Required]
        [Display(Name = "Full Name")]
        public String FullName { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public String PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Email")]
        public String Email { get; set; }
        [Required]
        [Display(Name = "Location")]
        public String Location { get; set; }
        [Required]
        [Display(Name = "Transport")]
        public String Transport { get; set; }
        [Required]
        [Display(Name = "DriverLicence")]
        public String DriverLicence { get; set; }
        [Required]
        [Display(Name = "Duration")]
        public String Duration { get; set; }
        [Required]
        [Display(Name = "Availability")]
        public String Availability { get; set; }
    }
}
