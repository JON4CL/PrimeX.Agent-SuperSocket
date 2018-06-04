using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace PrimeX.Base.MachineInfo
{
    public static class MachineInfo
    {
        public static string GetIP(bool withDots = true)
        {
            return string.Join((withDots ? "." : ""), Dns.GetHostEntry(Dns.GetHostName())
                .AddressList.FirstOrDefault(address => address.AddressFamily == AddressFamily.InterNetwork)
                .GetAddressBytes()
                .Select(a => a.ToString("d3")));
        }

        public static string GetName()
        {
            return Dns.GetHostName();
        }
    }
}
