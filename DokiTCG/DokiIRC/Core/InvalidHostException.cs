using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DokiIRC.Core
{
    internal class InvalidHostException : Exception
    {
        private string message;

        public InvalidHostException()
        {
        }

        public InvalidHostException(string message)
        {
            this.message = message;
        }

        public InvalidHostException(string message, System.Exception inner)
        {
            this.message = message;
        }
    }
}