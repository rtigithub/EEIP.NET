using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace DescoverDevices
{
    internal class Program
    {
        #region Private Methods

        private static void Main(string[] args) => MainAsync(args).GetAwaiter().GetResult();

        private static async Task MainAsync(string[] args)
        {
            Sres.Net.EEIP.EEIPClient eipClient = new Sres.Net.EEIP.EEIPClient();
            List<Sres.Net.EEIP.Encapsulation.CIPIdentityItem> cipIdentityItem = await eipClient.ListIdentityAsync();

            for (int i = 0; i < cipIdentityItem.Count; i++)
            {
                Console.WriteLine("Ethernet/IP Device Found:");
                Console.WriteLine(cipIdentityItem[i].ProductName1);
                Console.WriteLine("IP-Address: " + new IPAddress(cipIdentityItem[i].SocketAddress.SIN_Address));
                Console.WriteLine("Port: " + cipIdentityItem[i].SocketAddress.SIN_port);
                Console.WriteLine("Vendor ID: " + cipIdentityItem[i].VendorID1);
                Console.WriteLine("Product-code: " + cipIdentityItem[i].ProductCode1);
                Console.WriteLine("Type-Code: " + cipIdentityItem[i].ItemTypeCode);
                Console.WriteLine("Serial Number: " + cipIdentityItem[i].SerialNumber1);
            }
            Console.ReadKey();
        }

        #endregion Private Methods
    }
}