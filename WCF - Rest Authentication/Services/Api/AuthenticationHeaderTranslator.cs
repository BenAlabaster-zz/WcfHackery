using System;
using System.Text;
using System.Text.RegularExpressions;

namespace WcfRestAuthentication.Services.Api
{
    internal class BasicAuthenticationHeaderTranslator
    {
        public string AuthenticationType { get; protected set; }
        public string Username { get; protected set; }
        public string Password { get; protected set; }

        private static Encoding Encoding { get { return Encoding.GetEncoding("iso-8859-1"); } }

        public override string ToString()
        {
            return string.Format("{0} {1}", AuthenticationType,
                EncodeCredentials(Username, Password));
        }

        public BasicAuthenticationHeaderTranslator(string authenticationType, string username, string password)
        {
            AuthenticationType = authenticationType;
            Username = username;
            Password = password;
        }

        private BasicAuthenticationHeaderTranslator(string httpHeader)
        {
            const RegexOptions regexOpts = RegexOptions.Compiled | RegexOptions.IgnoreCase;
            var match = Regex.Match(httpHeader, @"^(.*?)\s+?(.*)$", regexOpts);
            AuthenticationType = match.Groups[1].Value;

            var encoded = match.Groups[2].Value;
            var decoded = Encoding.GetString(Convert.FromBase64String(encoded));

            match = Regex.Match(decoded, @"^(.*?):(.*?)$", regexOpts);
            Username = match.Groups[1].Value;
            Password = match.Groups[2].Value;
        }

        static string EncodeCredentials(string username, string password)
        {
            string authString = string.Format("{0}:{1}", username, password);
            var bytes = Encoding.GetBytes(authString);
            return Convert.ToBase64String(bytes);
        }

        public static BasicAuthenticationHeaderTranslator Decode(string httpHeader)
        {
            if (string.IsNullOrWhiteSpace(httpHeader))
                return null;

            return new BasicAuthenticationHeaderTranslator(httpHeader);
        }

        public static bool TryDecode(string httpHeader, out BasicAuthenticationHeaderTranslator decoded)
        {
            decoded = null;
            if (string.IsNullOrWhiteSpace(httpHeader))
                return false;

            try
            {
                decoded = new BasicAuthenticationHeaderTranslator(httpHeader);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}