// ***********************************************************************
// Assembly         : EEIP
// Created          : 03-05-2020
// Last Modified On : 03-05-2020
// <copyright file="Encapsulation.cs" company="Stefan Rossmann, Nathan Brown and contributors">
//     Copyright © 2020, All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Sres.Net.EEIP
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Class Encapsulation.
    /// </summary>
    public class Encapsulation
    {
        #region Public Fields

        /// <summary>
        /// The command specific data
        /// </summary>
        public List<byte> CommandSpecificData = new List<byte>();

        #endregion Public Fields

        #region Private Fields

        /// <summary>
        /// The options
        /// </summary>
        private readonly uint options = 0;
        /// <summary>
        /// The sender context
        /// </summary>
        private readonly byte[] senderContext = new byte[8];

        #endregion Private Fields

        #region Public Enums

        /// <summary>
        /// Table 2-3.2 Encapsulation Commands
        /// </summary>
        public enum CommandsEnum : ushort
        {
            /// <summary>
            /// The nop
            /// </summary>
            NOP = 0x0000,
            /// <summary>
            /// The list services
            /// </summary>
            ListServices = 0x0004,
            /// <summary>
            /// The list identity
            /// </summary>
            ListIdentity = 0x0063,
            /// <summary>
            /// The list interfaces
            /// </summary>
            ListInterfaces = 0x0064,
            /// <summary>
            /// The register session
            /// </summary>
            RegisterSession = 0x0065,
            /// <summary>
            /// The un register session
            /// </summary>
            UnRegisterSession = 0x0066,
            /// <summary>
            /// The send rr data
            /// </summary>
            SendRRData = 0x006F,
            /// <summary>
            /// The send unit data
            /// </summary>
            SendUnitData = 0x0070,
            /// <summary>
            /// The indicate status
            /// </summary>
            IndicateStatus = 0x0072,
            /// <summary>
            /// The cancel
            /// </summary>
            Cancel = 0x0073
        }

        /// <summary>
        /// Table 2-3.3 Error Codes
        /// </summary>
        public enum StatusEnum : uint
        {
            /// <summary>
            /// The success
            /// </summary>
            Success = 0x0000,
            /// <summary>
            /// The invalid command
            /// </summary>
            InvalidCommand = 0x0001,
            /// <summary>
            /// The insufficient memory
            /// </summary>
            InsufficientMemory = 0x0002,
            /// <summary>
            /// The incorrect data
            /// </summary>
            IncorrectData = 0x0003,
            /// <summary>
            /// The invalid session handle
            /// </summary>
            InvalidSessionHandle = 0x0064,
            /// <summary>
            /// The invalid length
            /// </summary>
            InvalidLength = 0x0065,
            /// <summary>
            /// The unsupported encapsulation protocol
            /// </summary>
            UnsupportedEncapsulationProtocol = 0x0069
        }

        #endregion Public Enums

        #region Public Properties

        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        /// <value>The command.</value>
        public CommandsEnum Command { get; set; }
        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        /// <value>The length.</value>
        public ushort Length { get; set; }
        /// <summary>
        /// Gets or sets the session handle.
        /// </summary>
        /// <value>The session handle.</value>
        public uint SessionHandle { get; set; }
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public StatusEnum Status { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Serializes to bytes.
        /// </summary>
        /// <returns>System.Byte[].</returns>
        public byte[] SerializeToBytes()
        {
            var returnValue = new byte[24 + CommandSpecificData.Count];
            returnValue[0] = (byte)Command;
            returnValue[1] = (byte)((ushort)Command >> 8);
            returnValue[2] = (byte)Length;
            returnValue[3] = (byte)(Length >> 8);
            returnValue[4] = (byte)SessionHandle;
            returnValue[5] = (byte)(SessionHandle >> 8);
            returnValue[6] = (byte)(SessionHandle >> 16);
            returnValue[7] = (byte)(SessionHandle >> 24);
            returnValue[8] = (byte)Status;
            returnValue[9] = (byte)((ushort)Status >> 8);
            returnValue[10] = (byte)((ushort)Status >> 16);
            returnValue[11] = (byte)((ushort)Status >> 24);
            returnValue[12] = senderContext[0];
            returnValue[13] = senderContext[1];
            returnValue[14] = senderContext[2];
            returnValue[15] = senderContext[3];
            returnValue[16] = senderContext[4];
            returnValue[17] = senderContext[5];
            returnValue[18] = senderContext[6];
            returnValue[19] = senderContext[7];
            returnValue[20] = (byte)options;
            returnValue[21] = (byte)((ushort)options >> 8);
            returnValue[22] = (byte)((ushort)options >> 16);
            returnValue[23] = (byte)((ushort)options >> 24);
            for (var i = 0; i < CommandSpecificData.Count; i++)
                returnValue[24 + i] = CommandSpecificData[i];
            return returnValue;
        }

        #endregion Public Methods

        #region Public Classes

        /// <summary>
        /// Table 2-4.4 CIP Identity Item
        /// </summary>
        public class CIPIdentityItem
        {
            #region Private Constructors

            /// <summary>
            /// Initializes a new instance of the <see cref="CIPIdentityItem"/> class.
            /// </summary>
            /// <param name="startingByte">The starting byte.</param>
            /// <param name="receivedData">The received data.</param>
            private CIPIdentityItem(int startingByte, byte[] receivedData)
            {
                startingByte = startingByte + 2; //Skipped ItemCount
                ItemTypeCode = Convert.ToUInt16(receivedData[0 + startingByte]
                                                | (receivedData[1 + startingByte] << 8));
                ItemLength = Convert.ToUInt16(receivedData[2 + startingByte]
                                              | (receivedData[3 + startingByte] << 8));
                EncapsulationProtocolVersion = Convert.ToUInt16(receivedData[4 + startingByte]
                                                                | (receivedData[5 + startingByte] << 8));
                SocketAddress = SocketAddress.FromBytes(receivedData, 6 + startingByte);
                VendorID1 = Convert.ToUInt16(receivedData[22 + startingByte]
                                             | (receivedData[23 + startingByte] << 8));
                DeviceType1 = Convert.ToUInt16(receivedData[24 + startingByte]
                                               | (receivedData[25 + startingByte] << 8));
                ProductCode1 = Convert.ToUInt16(receivedData[26 + startingByte]
                                                | (receivedData[27 + startingByte] << 8));
                Revision1 = new[] { receivedData[28 + startingByte], receivedData[29 + startingByte] };
                Status1 = Convert.ToUInt16(receivedData[30 + startingByte]
                                           | (receivedData[31 + startingByte] << 8));
                SerialNumber1 = (uint)(receivedData[32 + startingByte]
                                       | (receivedData[33 + startingByte] << 8)
                                       | (receivedData[34 + startingByte] << 16)
                                       | (receivedData[35 + startingByte] << 24));
                ProductNameLength = receivedData[36 + startingByte];
                ProductName1 = Encoding.ASCII.GetString(receivedData, 37 + startingByte, ProductNameLength);
                State1 = receivedData[receivedData.Length - 1];
            }

            #endregion Private Constructors

            #region Public Properties

            /// <summary>
            /// Gets the device type1.
            /// </summary>
            /// <value>The device type1.</value>
            public ushort DeviceType1 { get; }
            /// <summary>
            /// Gets the encapsulation protocol version.
            /// </summary>
            /// <value>The encapsulation protocol version.</value>
            public ushort EncapsulationProtocolVersion { get; }
            /// <summary>
            /// Gets the length of the item.
            /// </summary>
            /// <value>The length of the item.</value>
            public ushort ItemLength { get; }
            /// <summary>
            /// Gets the item type code.
            /// </summary>
            /// <value>The item type code.</value>
            public ushort ItemTypeCode { get; }                               //Code indicating item type of CIP Identity (0x0C)

            //Device Type of product
            /// <summary>
            /// Gets the product code1.
            /// </summary>
            /// <value>The product code1.</value>
            public ushort ProductCode1 { get; }

            /// <summary>
            /// Gets the product name1.
            /// </summary>
            /// <value>The product name1.</value>
            public string ProductName1 { get; }

            /// <summary>
            /// Gets the length of the product name.
            /// </summary>
            /// <value>The length of the product name.</value>
            public byte ProductNameLength { get; }

            //Product Code assigned with respect to device type
            /// <summary>
            /// Gets the revision1.
            /// </summary>
            /// <value>The revision1.</value>
            public byte[] Revision1 { get; } = new byte[2];

            /// <summary>
            /// Gets the serial number1.
            /// </summary>
            /// <value>The serial number1.</value>
            public uint SerialNumber1 { get; }

            //Number of bytes in item which follow (length varies depending on Product Name string)
            //Encapsulation Protocol Version supported (also returned with Register Sesstion reply).
            /// <summary>
            /// Gets the socket address.
            /// </summary>
            /// <value>The socket address.</value>
            public SocketAddress SocketAddress { get; } = new SocketAddress(); //Socket Address (see section 2-6.3.2)

            //Serial number of device
            //Human readable description of device
            /// <summary>
            /// Gets the state1.
            /// </summary>
            /// <value>The state1.</value>
            public byte State1 { get; }

            //Device revision
            /// <summary>
            /// Gets the status1.
            /// </summary>
            /// <value>The status1.</value>
            public ushort Status1 { get; }

            /// <summary>
            /// Gets the vendor i d1.
            /// </summary>
            /// <value>The vendor i d1.</value>
            public ushort VendorID1 { get; }

            #endregion Public Properties

            //Device manufacturers Vendor ID
            //Current status of device
            //Current state of device

            #region Public Methods

            /// <summary>
            /// Deserializes the specified starting byte.
            /// </summary>
            /// <param name="startingByte">The starting byte.</param>
            /// <param name="receivedData">The received data.</param>
            /// <returns>CIPIdentityItem.</returns>
            public static CIPIdentityItem Deserialize(int startingByte, byte[] receivedData)
            {
                return new CIPIdentityItem(startingByte, receivedData);
            }

            /// <summary>
            /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
            /// </summary>
            /// <param name="obj">The object to compare with the current object.</param>
            /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj))
                    return false;
                if (ReferenceEquals(this, obj))
                    return true;
                if (obj.GetType() != this.GetType())
                    return false;
                return Equals((CIPIdentityItem)obj);
            }

            /// <summary>
            /// Returns a hash code for this instance.
            /// </summary>
            /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = ItemTypeCode.GetHashCode();
                    hashCode = (hashCode * 397) ^ ItemLength.GetHashCode();
                    hashCode = (hashCode * 397) ^ EncapsulationProtocolVersion.GetHashCode();
                    hashCode = (hashCode * 397) ^ SocketAddress.GetHashCode();
                    hashCode = (hashCode * 397) ^ VendorID1.GetHashCode();
                    hashCode = (hashCode * 397) ^ DeviceType1.GetHashCode();
                    hashCode = (hashCode * 397) ^ ProductCode1.GetHashCode();
                    hashCode = (hashCode * 397) ^ Revision1.GetHashCode();
                    hashCode = (hashCode * 397) ^ Status1.GetHashCode();
                    hashCode = (hashCode * 397) ^ (int)SerialNumber1;
                    hashCode = (hashCode * 397) ^ ProductNameLength.GetHashCode();
                    hashCode = (hashCode * 397) ^ (ProductName1 != null ? ProductName1.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ State1.GetHashCode();
                    return hashCode;
                }
            }

            #endregion Public Methods

            #region Protected Methods

            /// <summary>
            /// Equalses the specified other.
            /// </summary>
            /// <param name="other">The other.</param>
            /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
            protected bool Equals(CIPIdentityItem other)
            {
                return ItemTypeCode == other.ItemTypeCode
                       && ItemLength == other.ItemLength
                       && EncapsulationProtocolVersion == other.EncapsulationProtocolVersion
                       && Equals(SocketAddress, other.SocketAddress)
                       && VendorID1 == other.VendorID1
                       && DeviceType1 == other.DeviceType1
                       && ProductCode1 == other.ProductCode1
                       && Equals(Revision1, other.Revision1)
                       && Status1 == other.Status1
                       && SerialNumber1 == other.SerialNumber1
                       && ProductNameLength == other.ProductNameLength
                       && ProductName1 == other.ProductName1
                       && State1 == other.State1;
            }

            #endregion Protected Methods
        }

        /// <summary>
        /// Class CommonPacketFormat.
        /// </summary>
        public class CommonPacketFormat
        {
            #region Public Properties

            /// <summary>
            /// Gets or sets the address item.
            /// </summary>
            /// <value>The address item.</value>
            public ushort AddressItem { get; set; } = 0x0000;
            /// <summary>
            /// Gets or sets the length of the address.
            /// </summary>
            /// <value>The length of the address.</value>
            public ushort AddressLength { get; set; } = 0;
            /// <summary>
            /// Gets the data.
            /// </summary>
            /// <value>The data.</value>
            public List<byte> Data { get; } = new List<byte>();
            /// <summary>
            /// Gets or sets the data item.
            /// </summary>
            /// <value>The data item.</value>
            public ushort DataItem { get; set; } = 0xB2;

            //0xB2 = Unconnected Data Item
            /// <summary>
            /// Gets or sets the length of the data.
            /// </summary>
            /// <value>The length of the data.</value>
            public ushort DataLength { get; set; } = 8;

            /// <summary>
            /// Gets or sets the item count.
            /// </summary>
            /// <value>The item count.</value>
            public ushort ItemCount { get; set; } = 2;
            /// <summary>
            /// Gets or sets the sockaddr information item o t.
            /// </summary>
            /// <value>The sockaddr information item o t.</value>
            public ushort SockaddrInfoItem_O_T { get; set; } = 0x8001; //8000 for O->T and 8001 for T->O - Volume 2 Table 2-6.9
            /// <summary>
            /// Gets or sets the length of the sockaddr information.
            /// </summary>
            /// <value>The length of the sockaddr information.</value>
            public ushort SockaddrInfoLength { get; set; } = 16;
            /// <summary>
            /// Gets or sets the socketaddr information o t.
            /// </summary>
            /// <value>The socketaddr information o t.</value>
            public SocketAddress SocketaddrInfo_O_T { get; set; } = null;

            #endregion Public Properties

            #region Public Methods

            /// <summary>
            /// Serializes to bytes.
            /// </summary>
            /// <returns>System.Byte[].</returns>
            public byte[] SerializeToBytes()
            {
                if (SocketaddrInfo_O_T != null)
                    ItemCount = 3;
                var returnValue = new byte[10 + Data.Count + (SocketaddrInfo_O_T == null ? 0 : 20)];
                returnValue[0] = (byte)ItemCount;
                returnValue[1] = (byte)(ItemCount >> 8);
                returnValue[2] = (byte)AddressItem;
                returnValue[3] = (byte)(AddressItem >> 8);
                returnValue[4] = (byte)AddressLength;
                returnValue[5] = (byte)(AddressLength >> 8);
                returnValue[6] = (byte)DataItem;
                returnValue[7] = (byte)(DataItem >> 8);
                returnValue[8] = (byte)DataLength;
                returnValue[9] = (byte)(DataLength >> 8);
                for (var i = 0; i < Data.Count; i++)
                    returnValue[10 + i] = Data[i];

                // Add Socket Address Info Item
                if (SocketaddrInfo_O_T != null)
                {
                    returnValue[10 + Data.Count + 0] = (byte)SockaddrInfoItem_O_T;
                    returnValue[10 + Data.Count + 1] = (byte)(SockaddrInfoItem_O_T >> 8);
                    returnValue[10 + Data.Count + 2] = (byte)SockaddrInfoLength;
                    returnValue[10 + Data.Count + 3] = (byte)(SockaddrInfoLength >> 8);
                    returnValue[10 + Data.Count + 5] = (byte)SocketaddrInfo_O_T.SIN_family;
                    returnValue[10 + Data.Count + 4] = (byte)(SocketaddrInfo_O_T.SIN_family >> 8);
                    returnValue[10 + Data.Count + 7] = (byte)SocketaddrInfo_O_T.SIN_port;
                    returnValue[10 + Data.Count + 6] = (byte)(SocketaddrInfo_O_T.SIN_port >> 8);
                    returnValue[10 + Data.Count + 8] = (byte)SocketaddrInfo_O_T.SIN_Address;
                    returnValue[10 + Data.Count + 9] = (byte)(SocketaddrInfo_O_T.SIN_Address >> 8);
                    returnValue[10 + Data.Count + 10] = (byte)(SocketaddrInfo_O_T.SIN_Address >> 16);
                    returnValue[10 + Data.Count + 11] = (byte)(SocketaddrInfo_O_T.SIN_Address >> 24);
                    returnValue[10 + Data.Count + 12] = SocketaddrInfo_O_T.SIN_Zero[0];
                    returnValue[10 + Data.Count + 13] = SocketaddrInfo_O_T.SIN_Zero[1];
                    returnValue[10 + Data.Count + 14] = SocketaddrInfo_O_T.SIN_Zero[2];
                    returnValue[10 + Data.Count + 15] = SocketaddrInfo_O_T.SIN_Zero[3];
                    returnValue[10 + Data.Count + 16] = SocketaddrInfo_O_T.SIN_Zero[4];
                    returnValue[10 + Data.Count + 17] = SocketaddrInfo_O_T.SIN_Zero[5];
                    returnValue[10 + Data.Count + 18] = SocketaddrInfo_O_T.SIN_Zero[6];
                    returnValue[10 + Data.Count + 19] = SocketaddrInfo_O_T.SIN_Zero[7];
                }

                return returnValue;
            }

            #endregion Public Methods
        }

        /// <summary>
        /// Socket Address (see section 2-6.3.2)
        /// </summary>
        public class SocketAddress
        {
            #region Public Constructors

            /// <summary>
            /// Initializes a new instance of the <see cref="SocketAddress"/> class.
            /// </summary>
            public SocketAddress()
            {
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="SocketAddress"/> class.
            /// </summary>
            /// <param name="sinFamily">The sin family.</param>
            /// <param name="sinPort">The sin port.</param>
            /// <param name="sinAddress">Value of IP address. The value is in big-endian format</param>
            public SocketAddress(ushort sinFamily, ushort sinPort, uint sinAddress)
            {
                SIN_family = sinFamily;
                SIN_port = sinPort;
                SIN_Address = sinAddress;
            }

            #endregion Public Constructors

            #region Public Properties

            /// <summary>
            /// Value of IP address. The value is in big-endian format
            /// </summary>
            /// <value>The sin address.</value>
            public uint SIN_Address { get; }

            /// <summary>
            /// Gets the sin family.
            /// </summary>
            /// <value>The sin family.</value>
            public ushort SIN_family { get; }
            /// <summary>
            /// Gets the sin port.
            /// </summary>
            /// <value>The sin port.</value>
            public ushort SIN_port { get; }
            /// <summary>
            /// Gets the sin zero.
            /// </summary>
            /// <value>The sin zero.</value>
            public byte[] SIN_Zero { get; } = new byte[8];

            #endregion Public Properties

            #region Public Methods

            /// <summary>
            /// Froms the bytes.
            /// </summary>
            /// <param name="data">The data.</param>
            /// <param name="startIndex">The start index.</param>
            /// <returns>SocketAddress.</returns>
            public static SocketAddress FromBytes(byte[] data, int startIndex)
            {
                var family = (ushort)(data[startIndex + 1]
                                      | (data[startIndex + 0] << 8));
                var port = (ushort)(data[startIndex + 3]
                                    | (data[startIndex + 2] << 8));
                var address = (uint)(data[startIndex + 4]
                                     | (data[startIndex + 5] << 8)
                                     | (data[startIndex + 6] << 16)
                                     | (data[startIndex + 7] << 24));

                return new SocketAddress(family, port, address);
            }

            /// <summary>
            /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
            /// </summary>
            /// <param name="obj">The object to compare with the current object.</param>
            /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj))
                    return false;
                if (ReferenceEquals(this, obj))
                    return true;
                if (obj.GetType() != this.GetType())
                    return false;
                return Equals((SocketAddress)obj);
            }

            /// <summary>
            /// Returns a hash code for this instance.
            /// </summary>
            /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = SIN_family.GetHashCode();
                    hashCode = (hashCode * 397) ^ SIN_port.GetHashCode();
                    hashCode = (hashCode * 397) ^ (int)SIN_Address;
                    return hashCode;
                }
            }

            #endregion Public Methods

            #region Protected Methods

            /// <summary>
            /// Equalses the specified other.
            /// </summary>
            /// <param name="other">The other.</param>
            /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
            protected bool Equals(SocketAddress other)
            {
                return SIN_family == other.SIN_family
                       && SIN_port == other.SIN_port
                       && SIN_Address == other.SIN_Address;
            }

            #endregion Protected Methods
        }

        #endregion Public Classes
    }
}
