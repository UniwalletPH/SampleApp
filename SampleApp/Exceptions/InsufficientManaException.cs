using System;
using System.Collections.Generic;
using System.Text;

namespace SampleApp.Exceptions
{
    public class InsufficientManaException : Exception
    {
        public InsufficientManaException() : base("Insufficient Mana")
        {

        }
    }
}
