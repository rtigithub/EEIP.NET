using Sres.Net.EEIP;
using System;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    internal class Program
    {
        #region Private Methods

        private static void Main(string[] args) => MainAsync(args).GetAwaiter().GetResult();

        private static async Task MainAsync(string[] args)
        {
            EEIPClient eeipClient = new EEIPClient();
            await eeipClient.RegisterSessionAsync(new Uri("tcp://192.168.0.123"));
            byte[] response = await eeipClient.GetAttributeSingleAsync(0x66, 1, 0x325);
            Console.WriteLine("Current Value Sensor 1: " + (response[1] * 255 + response[0]).ToString());
            response = await eeipClient.GetAttributeSingleAsync(0x66, 2, 0x325);
            Console.WriteLine("Current Value Sensor 2: " + (response[1] * 255 + response[0]).ToString());
            Console.WriteLine();
            Console.Write("Enter intensity for Sensor 1 [1..100]");
            int value = int.Parse(Console.ReadLine());
            Console.WriteLine("Set Light intensity Sensor 1 to " + value + "%");
            await eeipClient.SetAttributeSingleAsync(0x66, 1, 0x389, new byte[] { (byte)value, 0 });
            Console.Write("Enter intensity for Sensor 2 [1..100]");
            value = int.Parse(Console.ReadLine());
            Console.WriteLine("Set Light intensity Sensor 2 to " + value + "%");
            await eeipClient.SetAttributeSingleAsync(0x66, 2, 0x389, new byte[] { (byte)value, 0 });
            Console.WriteLine();
            Console.WriteLine("Read Values from device to approve the value");
            response = await eeipClient.GetAttributeSingleAsync(0x66, 1, 0x389);
            Console.WriteLine("Current light Intensity Sensor 1 in %: " + (response[1] * 255 + response[0]).ToString());
            response = await eeipClient.GetAttributeSingleAsync(0x66, 2, 0x389);
            Console.WriteLine("Current light Intensity Sensor 2 in %: " + (response[1] * 255 + response[0]).ToString());
            await eeipClient.UnRegisterSessionAsync();
            Console.ReadKey();
        }

        #endregion Private Methods
    }
}
