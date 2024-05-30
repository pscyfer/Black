using Common.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common.Application.AddressNetWorkHelper
{
    public static class AddressNetworkHelper
    {
        public static bool IsIpAddressOrHttpAddress(string Address)
        {
            if (string.IsNullOrWhiteSpace(Address)) throw new CantConvertIpAddressAndHttpAdderssException(Address);
            bool isIPAddress = Regex.IsMatch(Address, @"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$");
            bool isHTTPAddress = Regex.IsMatch(Address, @"^https?://");
            bool result = false;
            if (isIPAddress && !isHTTPAddress)
            {
                result = false;
                Console.WriteLine("The value represents an IP address.");
            }
            else if (isHTTPAddress)
            {
                result = true;
                Console.WriteLine("The value represents an HTTP address (URL).");
            }
            else
            {
                throw new CantConvertIpAddressAndHttpAdderssException($"The value does not match an IP address or an HTTP address. value={Address}");
            }

            return result;
        }
        public static AddressComponents ExtractAddressComponents(this string address)
        {
            AddressComponents components = new AddressComponents();
            string pattern = @"^(?<protocol>https?:\/\/)?(?<hostname>[^:/\s]+)(:(?<port>\d+))?(?<path>\/[^?\s]*)?(\?(?<searchwords>[^\s]+))?$";
            Match match = Regex.Match(address, pattern);

            if (match.Success)
            {
                components.Protocol = match.Groups["protocol"].Value;
                components.Hostname = match.Groups["hostname"].Value;
                components.Port = match.Groups["port"].Value;
                components.Path = match.Groups["path"].Value;
                components.SearchWords = match.Groups["searchwords"].Value;
            }

            return components;
        }
        public class AddressComponents
        {
            public string Protocol { get; set; }
            public string Hostname { get; set; }
            public string Port { get; set; }
            public string Path { get; set; }
            public string SearchWords { get; set; }
        }
    }
}
