using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Exceptions
{
    public class LoginException : Exception
    {
        public LoginException(String error) : base(error) { }
    }
}
