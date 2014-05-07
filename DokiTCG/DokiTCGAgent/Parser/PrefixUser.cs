using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DokiTCGIRC.Parser
{
    public class PrefixUser : AbstractPrefix
    {
        /// <summary>
        /// First letter of nickname can only contain a letter or a special character.
        /// Special characters: "[", "]", "\", "`", "_", "^", "{", "|", "}"
        /// </summary>
        private static string nickFirstPattern = "[^{A-z}|{\x5B-\x60}|{\x7B-\x7D}]";

        /// <summary>
        /// Nickname can only contain letters, digits, special characters, or a "-".
        /// Special characters: "[", "]", "\", "`", "_", "^", "{", "|", "}"
        /// </summary>
        private static string nickPattern = "[^{A-z}|{0-9}|{\x5B-\x60}|{\x7B-\x7D}|-]";

        /// <summary>
        /// Username can contain all octets except NUL, CR, LF, " " (Space), or "@".
        /// </summary>
        private static string userPattern = "[^{\x01-\x09}|{\x0B-\x0C}|{\x0E-\x1F}|{\x21-\x3F}|{\x41-\xFF}]";

        /// <summary>
        /// Hostname can only contain letters, digits, a "-", or a ".".
        /// TODO: Rizon servers seem to accept more characters in hostnames, namely Control Codes.
        /// </summary>
        private static string hostPattern = "[^{A-z}|{0-9}|-|.|:]";

        /// <summary>
        /// Nickname of the user.
        /// </summary>
        public string Nickname { get; private set; }

        /// <summary>
        /// Username of the user.
        /// </summary>
        public string Username { get; private set; }

        /// <summary>
        /// Hostname of the user.
        /// </summary>
        public string Hostname { get; private set; }

        /// <summary>
        /// Constructs a PrefixUser with only a Nickname as identifier.
        /// </summary>
        /// <param name="nickname"> Nickname of user. </param>
        public PrefixUser(string nickname)
        {
            // Check if nickname is correct.
            if (nickname == null)
            {
                throw new ArgumentNullException("Nickname can't be null.");
            }
            if (!IsValidNick(nickname))
            {
                throw new ArgumentException("Nickname contains illegal characters or is empty.");
            }
            Nickname = nickname;
            Username = null;
            Hostname = null;
        }

        /// <summary>
        /// Constructs
        /// </summary>
        /// <param name="nickname"></param>
        /// <param name="username"></param>
        /// <param name="hostname"></param>
        public PrefixUser(string nickname, string username, string hostname)
        {
            // Check if nickname is correct.
            if (nickname == null)
            {
                throw new ArgumentNullException("Nickname can't be null.");
            }

            if (!IsValidNick(nickname))
            {
                throw new ArgumentException("Nickname contains illegal characters or is empty.");
            }

            Nickname = nickname;

            // Check if username is correct.
            if (username == null)
            {
                throw new ArgumentNullException("Username can't be null, use Prefix(string nickname) to create a Prefix without Username.");
            }

            if (!IsValidUser(username))
            {
                throw new ArgumentException("Username contains illegal characters or is empty.");
            }

            Username = username;

            // Check if hostname is correct.
            if (hostname == null)
            {
                throw new ArgumentNullException("Hostname can't be null, use Prefix(string nickname) to create a Prefix without Hostname.");
            }

            if (!IsValidHost(hostname))
            {
                throw new ArgumentException("Hostname contains illegal characters or is empty.");
            }

            Hostname = hostname;
        }

        /// <summary>
        /// Returns the type of Prefix.
        /// </summary>
        /// <returns> PrefixType.USER. </returns>
        public override PrefixType GetPrefixType()
        {
            return PrefixType.USER;
        }

        /// <summary>
        /// Returns a string that represents the entire mask of the user in this prefix.
        /// </summary>
        /// <returns> Mask string. </returns>
        public string Mask()
        {
            return Nickname + "!" + Username + "@" + Hostname;
        }

        /// <summary>
        /// Returns a string that can be used to send a message to the user in this prefix.
        /// </summary>
        /// <returns> Server valid string. </returns>
        public override string ToString()
        {
            return Nickname;
        }

        /// <summary>
        /// Checks if the nickname does not contain any invalid characters.
        /// </summary>
        /// <param name="nick"> String to check. </param>
        /// <returns> True or False. </returns>
        private static bool IsValidNick(string nick)
        {
            // Don't ask me how a string can have a length less than 0.
            if (nick.Length <= 0)
            {
                return false;
            }

            Match first = Regex.Match(nick[0].ToString(), nickFirstPattern, RegexOptions.None);
            Match name = Regex.Match(nick, nickPattern, RegexOptions.None);

            // If we find an illegal character, return false, else true.
            return first.Success || name.Success ? false : true;
        }

        /// <summary>
        /// Checks if the username does not contain any invalid characters.
        /// </summary>
        /// <param name="user"> String to check. </param>
        /// <returns> True or False. </returns>
        private static bool IsValidUser(string user)
        {
            if (user.Length <= 0)
            {
                return false;
            }

            Match name = Regex.Match(user, userPattern, RegexOptions.None);

            // If we find an illegal character, return false, else true.
            return name.Success ? false : true;
        }

        /// <summary>
        /// Checks if the hostname does not contain any invalid characters.
        /// </summary>
        /// <param name="host"> String to check. </param>
        /// <returns> True or False. </returns>
        private static bool IsValidHost(string host)
        {
            if (host.Length <= 0)
            {
                return false;
            }

            Match first = Regex.Match(host[0].ToString(), nickFirstPattern, RegexOptions.None);
            Match last = Regex.Match(host[host.Length - 1].ToString(), nickFirstPattern, RegexOptions.None);
            Match name = Regex.Match(host, hostPattern, RegexOptions.None);

            // If we find an illegal character, return false, else true.
            return first.Success || last.Success || name.Success ? false : true;
        }
    }
}