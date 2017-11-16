using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Exceptions
{
    public class RegisterException: Exception
    {
        public RegisterException(String error) : base(error) { }
    }
}
