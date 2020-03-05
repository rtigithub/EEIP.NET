using System.Text;
using System.Threading.Tasks;

namespace Sres.Net.EEIP.ObjectLibrary
{
    /// <summary>
    /// Chapter 5-3.2.2.2 Volume 2
    /// </summary>
    public struct InterfaceCapabilityFlags
    {
        #region Public Fields

        public bool BootPClient;
        public bool ConfigurationSettable;
        public bool DHCP_DNSUpdate;
        public bool DHCPClient;
        public bool DNSClient;

        #endregion Public Fields
    }

    /// <summary>
    /// Chapter 5-3.2.2.3 Volume 2
    /// </summary>
    public struct InterfaceControlFlags
    {
        #region Public Fields

        public bool EnableBootP;
        public bool EnableDHCP;
        public bool EnableDNS;
        public bool UsePreviouslyStored;

        #endregion Public Fields
    }

    /// <summary>
    /// Chapter 5-3.2.2.1 Volume 2
    /// </summary>
    public struct InterfaceStatus
    {
        #region Public Fields

        public bool McastPending;
        public bool NotConfigured;
        public bool ValidConfiguration;
        public bool ValidManualConfiguration;

        #endregion Public Fields
    }

    /// <summary>
    /// Chapter 5-3.2.2.5 Volume 2
    /// </summary>
    public struct NetworkInterfaceConfiguration
    {
        #region Public Fields

        public string DomainName;
        public uint GatewayAddress;
        public uint IPAddress;
        public uint NameServer;
        public uint NameServer2;
        public uint NetworkMask;

        #endregion Public Fields
    }

    /// <summary>
    /// Page 5.5 Volume 2
    /// </summary>
    public struct PhysicalLink
    {
        #region Public Fields

        public byte[] Path;
        public ushort PathSize;

        #endregion Public Fields
    }

    public class TcpIpInterfaceObject
    {
        #region Public Fields

        public EEIPClient eeipClient;

        #endregion Public Fields

        #region Public Constructors

        /// <summary>
        /// Constructor. </summary>
        /// <param name="eeipClient"> EEIPClient Object</param>
        public TcpIpInterfaceObject(EEIPClient eeipClient)
        {
            this.eeipClient = eeipClient;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// gets the Configuration capability / Read "TCP/IP Interface Object" Class Code 0xF5 - Attribute ID 2
        /// </summary>
        public async Task<InterfaceCapabilityFlags> GetConfigurationCapabilityAsync()
        {
            byte[] byteArray = await eeipClient.GetAttributeSingleAsync(0xF5, 1, 2);
            InterfaceCapabilityFlags configurationCapability = new InterfaceCapabilityFlags();
            if ((byteArray[0] & 0x01) != 0)
                configurationCapability.BootPClient = true;
            if ((byteArray[0] & 0x02) != 0)
                configurationCapability.DNSClient = true;
            if ((byteArray[0] & 0x04) != 0)
                configurationCapability.DHCPClient = true;
            if ((byteArray[0] & 0x08) != 0)
                configurationCapability.DHCPClient = true;
            if ((byteArray[0] & 0x10) != 0)
                configurationCapability.ConfigurationSettable = true;
            return configurationCapability;
        }

        /// <summary>
        /// gets the Path to the Physical Link object / Read "TCP/IP Interface Object" Class Code 0xF5 - Attribute ID 4
        /// </summary>
        public async Task<PhysicalLink> GetPhysicalLinkObjectAsync()
        {
            byte[] byteArray = await eeipClient.GetAttributeSingleAsync(0xF5, 1, 4);
            PhysicalLink physicalLinkObject = new PhysicalLink();
            physicalLinkObject.PathSize = (ushort)(byteArray[1] << 8 | byteArray[0]);
            if (byteArray.Length > 2)
                System.Buffer.BlockCopy(byteArray, 2, physicalLinkObject.Path, 0, byteArray.Length - 2);
            return physicalLinkObject;
        }

        /// <summary>
        /// gets the Status / Read "TCP/IP Interface Object" Class Code 0xF5 - Attribute ID 1
        /// </summary>
        public async Task<InterfaceStatus> GetStatusAsync()
        {
            byte[] byteArray = await eeipClient.GetAttributeSingleAsync(0xF5, 1, 1);
            InterfaceStatus status = new InterfaceStatus();
            if ((byteArray[0] & 0x0F) == 0)
                status.NotConfigured = true;
            if ((byteArray[0] & 0x0F) == 1)
                status.ValidConfiguration = true;
            if ((byteArray[0] & 0x0F) == 2)
                status.ValidManualConfiguration = true;
            if ((byteArray[0] & 0x10) != 0)
                status.McastPending = true;
            return status;
        }

        /// <summary>
        /// sets the TCP/IP Network interface Configuration / Write "TCP/IP Interface Object" Class Code 0xF5 - Attribute ID 5
        /// </summary>
        public Task InterfaceConfigurationAsync(NetworkInterfaceConfiguration value)
        {
            byte[] valueToWrite = new byte[68];
            valueToWrite[0] = (byte)value.IPAddress;
            valueToWrite[1] = (byte)(value.IPAddress >> 8);
            valueToWrite[2] = (byte)(value.IPAddress >> 16);
            valueToWrite[3] = (byte)(value.IPAddress >> 24);
            valueToWrite[4] = (byte)value.NetworkMask;
            valueToWrite[5] = (byte)(value.NetworkMask >> 8);
            valueToWrite[6] = (byte)(value.NetworkMask >> 16);
            valueToWrite[7] = (byte)(value.NetworkMask >> 24);
            valueToWrite[8] = (byte)value.GatewayAddress;
            valueToWrite[9] = (byte)(value.GatewayAddress >> 8);
            valueToWrite[10] = (byte)(value.GatewayAddress >> 16);
            valueToWrite[11] = (byte)(value.GatewayAddress >> 24);
            valueToWrite[12] = (byte)value.NameServer;
            valueToWrite[13] = (byte)(value.NameServer >> 8);
            valueToWrite[14] = (byte)(value.NameServer >> 16);
            valueToWrite[15] = (byte)(value.NameServer >> 24);
            valueToWrite[16] = (byte)value.NameServer2;
            valueToWrite[17] = (byte)(value.NameServer2 >> 8);
            valueToWrite[18] = (byte)(value.NameServer2 >> 16);
            valueToWrite[19] = (byte)(value.NameServer2 >> 24);
            if (value.DomainName != null)
            {
                byte[] domainName = Encoding.ASCII.GetBytes(value.DomainName);
                System.Buffer.BlockCopy(domainName, 0, valueToWrite, 20, domainName.Length);
            }
            return eeipClient.SetAttributeSingleAsync(0xF5, 1, 5, valueToWrite);
        }

        /// <summary>
        /// sets the Configuration control attribute / Write "TCP/IP Interface Object" Class Code 0xF5 - Attribute ID 3
        /// </summary>
        public Task SetConfigurationControlAsync(InterfaceControlFlags value)
        {
            byte[] valueToWrite = new byte[4];
            if (value.EnableBootP)
                valueToWrite[0] = 1;
            if (value.EnableDHCP)
                valueToWrite[0] = 2;
            if (value.EnableDNS)
                valueToWrite[0] = (byte)(valueToWrite[0] | 0x10);
            return eeipClient.SetAttributeSingleAsync(0xF5, 1, 3, valueToWrite);
        }

        #endregion Public Methods
    }
}
