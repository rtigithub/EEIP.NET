// ***********************************************************************
// Assembly         : EEIP
// Created          : 03-05-2020
// Last Modified On : 03-05-2020
// <copyright file="TcpIpInterfaceObject.cs" company="Stefan Rossmann, Nathan Brown and contributors">
//     Copyright © 2020, All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Sres.Net.EEIP.ObjectLibrary
{
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Chapter 5-3.2.2.2 Volume 2
    /// </summary>
    public struct InterfaceCapabilityFlags
    {
        #region Public Fields

        /// <summary>
        /// The boot p client
        /// </summary>
        public bool BootPClient;
        /// <summary>
        /// The configuration settable
        /// </summary>
        public bool ConfigurationSettable;
        /// <summary>
        /// The DHCP DNS update
        /// </summary>
        public bool DHCP_DNSUpdate;
        /// <summary>
        /// The DHCP client
        /// </summary>
        public bool DHCPClient;
        /// <summary>
        /// The DNS client
        /// </summary>
        public bool DNSClient;

        #endregion Public Fields
    }

    /// <summary>
    /// Chapter 5-3.2.2.3 Volume 2
    /// </summary>
    public struct InterfaceControlFlags
    {
        #region Public Fields

        /// <summary>
        /// The enable boot p
        /// </summary>
        public bool EnableBootP;
        /// <summary>
        /// The enable DHCP
        /// </summary>
        public bool EnableDHCP;
        /// <summary>
        /// The enable DNS
        /// </summary>
        public bool EnableDNS;
        /// <summary>
        /// The use previously stored
        /// </summary>
        public bool UsePreviouslyStored;

        #endregion Public Fields
    }

    /// <summary>
    /// Chapter 5-3.2.2.1 Volume 2
    /// </summary>
    public struct InterfaceStatus
    {
        #region Public Fields

        /// <summary>
        /// The mcast pending
        /// </summary>
        public bool McastPending;
        /// <summary>
        /// The not configured
        /// </summary>
        public bool NotConfigured;
        /// <summary>
        /// The valid configuration
        /// </summary>
        public bool ValidConfiguration;
        /// <summary>
        /// The valid manual configuration
        /// </summary>
        public bool ValidManualConfiguration;

        #endregion Public Fields
    }

    /// <summary>
    /// Chapter 5-3.2.2.5 Volume 2
    /// </summary>
    public struct NetworkInterfaceConfiguration
    {
        #region Public Fields

        /// <summary>
        /// The domain name
        /// </summary>
        public string DomainName;
        /// <summary>
        /// The gateway address
        /// </summary>
        public uint GatewayAddress;
        /// <summary>
        /// The ip address
        /// </summary>
        public uint IPAddress;
        /// <summary>
        /// The name server
        /// </summary>
        public uint NameServer;
        /// <summary>
        /// The name server2
        /// </summary>
        public uint NameServer2;
        /// <summary>
        /// The network mask
        /// </summary>
        public uint NetworkMask;

        #endregion Public Fields
    }

    /// <summary>
    /// Page 5.5 Volume 2
    /// </summary>
    public struct PhysicalLink
    {
        #region Public Fields

        /// <summary>
        /// The path
        /// </summary>
        public byte[] Path;
        /// <summary>
        /// The path size
        /// </summary>
        public ushort PathSize;

        #endregion Public Fields
    }

    /// <summary>
    /// Class TcpIpInterfaceObject.
    /// </summary>
    public class TcpIpInterfaceObject
    {
        #region Public Fields

        /// <summary>
        /// The eeip client
        /// </summary>
        public EEIPClient eeipClient;

        #endregion Public Fields

        #region Public Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="eeipClient">EEIPClient Object</param>
        public TcpIpInterfaceObject(EEIPClient eeipClient)
        {
            this.eeipClient = eeipClient;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// gets the Configuration capability / Read "TCP/IP Interface Object" Class Code 0xF5 - Attribute ID 2
        /// </summary>
        /// <returns>Task&lt;InterfaceCapabilityFlags&gt;.</returns>
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
        /// <returns>Task&lt;PhysicalLink&gt;.</returns>
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
        /// <returns>Task&lt;InterfaceStatus&gt;.</returns>
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
        /// <param name="value">The value.</param>
        /// <returns>Task.</returns>
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
        /// <param name="value">The value.</param>
        /// <returns>Task.</returns>
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
