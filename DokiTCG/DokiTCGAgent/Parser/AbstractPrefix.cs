using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DokiTCGIRC.Parser
{
    public abstract class AbstractPrefix : IPrefix
    {
        /// <summary>
        /// GetPrefixType returns the type of prefix.
        /// </summary>
        /// <returns> Type of prefix. </returns>
        public virtual PrefixType GetPrefixType()
        {
            return PrefixType.UNKNOWN;
        }

        /// <summary>
        /// AbstractPrefix ToString functions should return a name that can be used by the server.
        /// </summary>
        /// <returns> Prefix that can be used by the server. </returns>
        public override string ToString()
        {
            return "UNKNOWN";
        }
    }
}