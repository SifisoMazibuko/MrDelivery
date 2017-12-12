using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MrDelivery.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Firstname")]
        public String firstName { get; set; }

        [Required]
        [Display(Name = "lastname")]
        public String lastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public String email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public String password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "The password and confirmation password do not match.")]
        public String confirmPassword { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone number")]
        public String phoneNumber { get; set; }

        public bool IsEmailConfirmed { get; set; }
        public string StatusMessage { get; set; }
    }
}
