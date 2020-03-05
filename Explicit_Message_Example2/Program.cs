namespace Explicit_Message_Example2
{
    using Sres.Net.EEIP;
    using System;
    using System.Threading.Tasks;

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

            //Get the State of a digital Input According to the Manual
            //Instance 0x6C of the Assembly Object contains the Digital Input data
            //The Documentation can be found at: http://www.wago.de/download.esm?file=%5Cdownload%5C00368362_0.pdf&name=m07500352_xxxxxxxx_0en.pdf
            byte[] digitalInputs = await eeipClient.AssemblyObject.GetInstanceAsync(0x6c);

            Console.WriteLine("State of Digital Input 1: " + (EEIPClient.ToBool(digitalInputs[0], 0)));
            Console.WriteLine("State of Digital Input 2: " + (EEIPClient.ToBool(digitalInputs[0], 1)));
            Console.WriteLine("State of Digital Input 3: " + (EEIPClient.ToBool(digitalInputs[0], 2)));
            Console.WriteLine("State of Digital Input 4: " + (EEIPClient.ToBool(digitalInputs[0], 3)));

            //When done, we unregister the session
            await eeipClient.UnRegisterSessionAsync();
            Console.ReadKey();
        }

        #endregion Private Methods
    }
}