using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MrDelivery.ViewModels
{
    public class AddressViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Street No")]
        public string StreetNo { get; set; }
        [Required]
        [Display(Name = "Building Type")]
        public string BuildingType { get; set; }
        [Required]
        [Display(Name = "Unit No")]
        public string UnitNo { get; set; }
        [Required]
        [Display(Name = "Complex Name")]
        public string ComplexName { get; set; }
    }
}
