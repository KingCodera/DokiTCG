using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Net
{
    internal class InvalidPortException : Exception
    {
        private string message;

        public InvalidPortException()
        {
        }

        public InvalidPortException(string message)
        {
            this.message = message;
        }

        public InvalidPortException(string message, System.Exception inner)
        {
            this.message = message;
        }
    }
}