using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MrDelivery.ViewModels
{
    public class IndexViewModel
    {
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string phoneNumber { get; set; }

        public string StatusMessage { get; set; }
    }
}
