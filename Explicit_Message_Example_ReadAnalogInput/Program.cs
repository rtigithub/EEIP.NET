namespace Explicit_Message_Example_ReadAnalogInput
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

            //Get the State of Analog Inputs According to the Manual
            //Instance 0x6D of the Assembly Object contains the Analog Input data
            //The Documentation can be found at: http://www.wago.de/download.esm?file=%5Cdownload%5C00368362_0.pdf&name=m07500352_xxxxxxxx_0en.pdf
            //Page 202 shows the documentation for instance 6D hex
            byte[] analogInputs = await eeipClient.AssemblyObject.GetInstanceAsync(0x6D);

            Console.WriteLine("Temperature of Analog Input 1: " + (EEIPClient.ToUshort(new byte[] { analogInputs[0], analogInputs[1] }) / 10.0) + "°C");
            Console.WriteLine("Temperature of Analog Input 2: " + (EEIPClient.ToUshort(new byte[] { analogInputs[2], analogInputs[3] }) / 10.0) + "°C");
            //When done, we unregister the session
            await eeipClient.UnRegisterSessionAsync();
            Console.ReadKey();
        }

        #endregion Private Methods
    }
}