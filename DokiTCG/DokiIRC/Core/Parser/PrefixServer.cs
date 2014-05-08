using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DokiIRC.Core.Parser
{
    public class PrefixServer : AbstractPrefix
    {
        private static string hostPattern = "[^{A-z}|{0-9}|{-}|{.}|{:}]";

        public string ServerName { get; private set; }

        public PrefixServer(string servername)
        {
            IsValidHost(servername);
            ServerName = servername;
        }

        public override PrefixType GetPrefixType()
        {
            return PrefixType.SERVER;
        }

        public override string ToString()
        {
            return ServerName;
        }

        /// <summary>
        /// Checks the hostname for PrefixServer
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        private static bool IsValidHost(string host)
        {
            if (host == null)
            {
                throw new ArgumentNullException("Hostname is null.");
            }

            if (host.Length <= 0)
            {
                throw new ArgumentException("Hostname length is 0.");
            }

            Match name = Regex.Match(host, hostPattern, RegexOptions.None);

            if (name.Success)
            {
                throw new ArgumentException("Hostname contains illegal characters.");
            }

            return true;
        }
    }
}