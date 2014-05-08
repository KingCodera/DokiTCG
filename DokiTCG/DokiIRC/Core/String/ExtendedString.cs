using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class ExtendedString
    {
        public static string Red(this String arg)
        {
            return '\x03' + "04" + arg;
        }
    }
}