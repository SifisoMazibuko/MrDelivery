using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Exceptions
{
    public class ForgotPasswordException : Exception
    {
        public ForgotPasswordException(string error) : base(error) { }
    }
}
