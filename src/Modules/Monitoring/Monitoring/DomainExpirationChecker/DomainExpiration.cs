using Common.Application.AddressNetWorkHelper;
using Monitoring.Exceptions;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace Monitoring.DomainExpirationChecker
{
    internal static class DomainExpiration
    {
        private static DateTime? _ExpirationDate = null;
        public static DateTime GetExpirationDate(string domainName)
        {
            try
            {
                string whoisServer = "whois.iana.org";
                int whoisPort = 43;
                string query = domainName.ExtractAddressComponents().Hostname + "\r\n";
                byte[] data = System.Text.Encoding.ASCII.GetBytes(query);

                TcpClient whoisClient = new TcpClient();
                whoisClient.Connect(whoisServer, whoisPort);
                NetworkStream stream = whoisClient.GetStream();

                stream.Write(data, 0, data.Length);

                StreamReader reader = new StreamReader(stream);
                string response = reader.ReadToEnd();

                string whoisServerPattern = @"whois:\s*(.+)";
                Match match = Regex.Match(response, whoisServerPattern);
                string domainWhoisServer = match.Groups[1].Value.Trim();

                TcpClient domainWhoisClient = new TcpClient();
                domainWhoisClient.Connect(domainWhoisServer, whoisPort);
                NetworkStream domainStream = domainWhoisClient.GetStream();

                byte[] domainData = System.Text.Encoding.ASCII.GetBytes(query);
                domainStream.Write(domainData, 0, domainData.Length);

                StreamReader domainReader = new StreamReader(domainStream);
                string domainResponse = domainReader.ReadToEnd();

                string expirationDatePattern = @"Expiry Date:\s*(\d{4}-\d{2}-\d{2})";
                Match expirationMatch = Regex.Match(domainResponse, expirationDatePattern);
                string expirationDateString = expirationMatch.Groups[1].Value.Trim();

                DateTime expirationDate = DateTime.ParseExact(expirationDateString, "yyyy-MM-dd", null);
                _ExpirationDate = expirationDate;
                reader.Close();
                stream.Close();
                whoisClient.Close();
                domainReader.Close();
                domainStream.Close();
                domainWhoisClient.Close();

            }
            catch (IOException ex) when (ex.Message == "An existing connection was forcibly closed by the remote host.")
            {
                TcpClient whois = new TcpClient("whois.internic.net", 43);
                string query = domainName.ExtractAddressComponents().Hostname + "\r\n";
                byte[] buffer = Encoding.ASCII.GetBytes(query);
                whois.GetStream().Write(buffer, 0, buffer.Length);
                byte[] data = new byte[8192];
                int count = whois.GetStream().Read(data, 0, data.Length);
                string response = Encoding.ASCII.GetString(data, 0, count);
                Console.WriteLine("Expiration date: " + response);
                string expirationDatePattern = @"Registry Expiry Date:\s*(\d{4}-\d{2}-\d{2})";
                Match expirationMatch = Regex.Match(response, expirationDatePattern);
                string expirationDateString = expirationMatch.Groups[1].Value.Trim();
                DateTime expirationDate = DateTime.ParseExact(expirationDateString, "yyyy-MM-dd", null);
                _ExpirationDate = expirationDate;
            }
            catch (IOException ex) when (ex.Message == "Unable to read data from the transport connection: An existing connection was forcibly closed by the remote host..")
            {
                TcpClient whois = new TcpClient("whois.internic.net", 43);
                string query = domainName.ExtractAddressComponents().Hostname + "\r\n";
                byte[] buffer = Encoding.ASCII.GetBytes(query);
                whois.GetStream().Write(buffer, 0, buffer.Length);
                byte[] data = new byte[8192];
                int count = whois.GetStream().Read(data, 0, data.Length);
                string response = Encoding.ASCII.GetString(data, 0, count);
                Console.WriteLine("Expiration date: " + response);
                string expirationDatePattern = @"Registry Expiry Date:\s*(\d{4}-\d{2}-\d{2})";
                Match expirationMatch = Regex.Match(response, expirationDatePattern);
                string expirationDateString = expirationMatch.Groups[1].Value.Trim();
                DateTime expirationDate = DateTime.ParseExact(expirationDateString, "yyyy-MM-dd", null);
                _ExpirationDate = expirationDate;
            }
            catch (HttpRequestException ex) when (ex.StatusCode != HttpStatusCode.OK)
            {
                TcpClient whois = new TcpClient("whois.internic.net", 43);
                string query = domainName.ExtractAddressComponents().Hostname + "\r\n";
                byte[] buffer = Encoding.ASCII.GetBytes(query);
                whois.GetStream().Write(buffer, 0, buffer.Length);
                byte[] data = new byte[8192];
                int count = whois.GetStream().Read(data, 0, data.Length);
                string response = Encoding.ASCII.GetString(data, 0, count);
                Console.WriteLine("Expiration date: " + response);
                string expirationDatePattern = @"Registry Expiry Date:\s*(\d{4}-\d{2}-\d{2})";
                Match expirationMatch = Regex.Match(response, expirationDatePattern);
                string expirationDateString = expirationMatch.Groups[1].Value.Trim();
                DateTime expirationDate = DateTime.ParseExact(expirationDateString, "yyyy-MM-dd", null);
                _ExpirationDate = expirationDate;
            }
            finally
            {
                if (_ExpirationDate == null)
                    throw new GetExpirationDateDomainException();
            }
            if (_ExpirationDate != null)
            {
                return _ExpirationDate.Value;
            }
            throw new GetExpirationDateDomainException();

        }
    }
}
