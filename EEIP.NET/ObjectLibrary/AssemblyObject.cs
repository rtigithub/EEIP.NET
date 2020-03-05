using System.Threading.Tasks;

namespace Sres.Net.EEIP.ObjectLibrary
{
    public class AssemblyObject
    {
        public EEIPClient eeipClient;

        /// <summary>
        /// Constructor. </summary>
        /// <param name="eeipClient"> EEIPClient Object</param>
        public AssemblyObject(EEIPClient eeipClient)
        {
            this.eeipClient = eeipClient;
        }

        /// <summary>
        /// Reads the Instance of the Assembly Object (Instance 101 returns the bytes of the class ID 101)
        /// </summary>
        /// <param name="instanceNo"> Instance number to be returned</param>
        /// <returns>bytes of the Instance</returns>
        public Task<byte[]> GetInstanceAsync(int instanceNo)
        {
            return this.eeipClient.GetAttributeSingleAsync(4, instanceNo, 3);
        }

        /// <summary>
        /// Sets an Instance of the Assembly Object
        /// </summary>
        /// <param name="instanceNo"> Instance number to be returned</param>
        /// <returns>bytes of the Instance</returns>
        public Task SetInstanceAsync(int instanceNo, byte[] value)
        {
            return eeipClient.SetAttributeSingleAsync(4, instanceNo, 3, value);
        }
    }
}
