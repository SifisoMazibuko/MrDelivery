using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MrDelivery.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }
    }
}
