using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Exceptions
{
    public class DriverException : Exception
    {
        public DriverException(String error) : base(error) { }
    }
}
