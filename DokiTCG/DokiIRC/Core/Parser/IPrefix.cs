using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DokiIRC.Core.Parser
{
    public enum PrefixType
    {
        SERVER,
        USER,
        UNKNOWN
    }

    public interface IPrefix
    {
        PrefixType GetPrefixType();

        string ToString();
    }
}