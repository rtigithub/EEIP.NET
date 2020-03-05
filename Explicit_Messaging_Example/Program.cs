﻿using Sres.Net.EEIP;
using System;
using System.Threading.Tasks;

namespace Explicit_Messaging_Example
{
    internal class Program
    {
        #region Private Methods

        private static void Main(string[] args) => MainAsync(args).GetAwaiter().GetResult();

        private static async Task MainAsync(string[] args)
        {
            EEIPClient eeipClient = new EEIPClient();

            //Register Session (Wago-Device 750-352 IP-Address: 192.168.178.66)
            //we use the Standard Port for Ethernet/IP TCP-connections 0xAF12
            await eeipClient.RegisterSessionAsync(new Uri("tcp://192.168.1.3"));

            //We write an Output to the Wago-Device; According to the Manual of the Device
            //Instance 0x66 of the Assembly Object contains the Digital Output data
            //The Documentation can be found at: http://www.wago.de/download.esm?file=%5Cdownload%5C00368362_0.pdf&name=m07500352_xxxxxxxx_0en.pdf

            //We set the first output "High"
            await eeipClient.AssemblyObject.SetInstanceAsync(0x66, new byte[] { 0x01 });

            System.Threading.Thread.Sleep(1000);

            //We set the secoond output "High"
            await eeipClient.AssemblyObject.SetInstanceAsync(0x66, new byte[] { 0x02 });

            System.Threading.Thread.Sleep(1000);

            //We set the secoond output "High"
            await eeipClient.AssemblyObject.SetInstanceAsync(0x66, new byte[] { 0x03 });

            System.Threading.Thread.Sleep(1000);

            //We reset the outputs
            await eeipClient.AssemblyObject.SetInstanceAsync(0x66, new byte[] { 0x00 });

            //When done, we unregister the session
            await eeipClient.UnRegisterSessionAsync();
        }

        #endregion Private Methods
    }
}