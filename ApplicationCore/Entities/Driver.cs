using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApplicationCore.Entities
{
    [Table("Driver")]
    public class Driver
    {
        [Key]
        public int Id { get; set; }
        public String FullName { get; set; }
        public String PhoneNumber { get; set; }
        public String Email { get; set; }
        public String Location { get; set; }
        public String Transport { get; set; }
        public String DriverLicence { get; set; }
        public String Duration { get; set; }
        public String Availability { get; set; }
    }
}
