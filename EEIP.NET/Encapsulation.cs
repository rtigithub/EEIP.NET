using System;
using System.Collections.Generic;
using System.Text;

namespace Sres.Net.EEIP
{
    public class Encapsulation
    {
        public CommandsEnum Command { get; set; }
        public ushort Length { get; set; }
        public uint SessionHandle { get; set; }
        public StatusEnum Status { get; }
        private readonly byte[] senderContext = new byte[8];
        private readonly uint options = 0;
        public List<byte> CommandSpecificData = new List<byte>();

        /// <summary>
        ///     Table 2-3.3 Error Codes
        /// </summary>
        public enum StatusEnum : uint
        {
            Success = 0x0000,
            InvalidCommand = 0x0001,
            InsufficientMemory = 0x0002,
            IncorrectData = 0x0003,
            InvalidSessionHandle = 0x0064,
            InvalidLength = 0x0065,
            UnsupportedEncapsulationProtocol = 0x0069
        }

        /// <summary>
        ///     Table 2-3.2 Encapsulation Commands
        /// </summary>
        public enum CommandsEnum : ushort
        {
            NOP = 0x0000,
            ListServices = 0x0004,
            ListIdentity = 0x0063,
            ListInterfaces = 0x0064,
            RegisterSession = 0x0065,
            UnRegisterSession = 0x0066,
            SendRRData = 0x006F,
            SendUnitData = 0x0070,
            IndicateStatus = 0x0072,
            Cancel = 0x0073
        }

        public byte[] toBytes()
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


        /// <summary>
        ///     Table 2-4.4 CIP Identity Item
        /// </summary>
        public class CIPIdentityItem
        {
            public ushort ItemTypeCode;                               //Code indicating item type of CIP Identity (0x0C)
            public ushort ItemLength;                                 //Number of bytes in item which follow (length varies depending on Product Name string)
            public ushort EncapsulationProtocolVersion;               //Encapsulation Protocol Version supported (also returned with Register Sesstion reply).
            public SocketAddress SocketAddress = new SocketAddress(); //Socket Address (see section 2-6.3.2)
            public ushort VendorID1;                                  //Device manufacturers Vendor ID
            public ushort DeviceType1;                                //Device Type of product
            public ushort ProductCode1;                               //Product Code assigned with respect to device type
            public byte[] Revision1 = new byte[2];                    //Device revision
            public ushort Status1;                                    //Current status of device
            public uint SerialNumber1;                                //Serial number of device
            public byte ProductNameLength;
            public string ProductName1; //Human readable description of device
            public byte State1;         //Current state of device


            public static CIPIdentityItem getCIPIdentityItem(int startingByte, byte[] receivedData)
            {
                startingByte = startingByte + 2; //Skipped ItemCount
                var cipIdentityItem = new CIPIdentityItem();
                cipIdentityItem.ItemTypeCode = Convert.ToUInt16(receivedData[0 + startingByte]
                                                                | (receivedData[1 + startingByte] << 8));
                cipIdentityItem.ItemLength = Convert.ToUInt16(receivedData[2 + startingByte]
                                                              | (receivedData[3 + startingByte] << 8));
                cipIdentityItem.EncapsulationProtocolVersion = Convert.ToUInt16(receivedData[4 + startingByte]
                                                                                | (receivedData[5 + startingByte] << 8));
                cipIdentityItem.SocketAddress.SIN_family = Convert.ToUInt16(receivedData[7 + startingByte]
                                                                            | (receivedData[6 + startingByte] << 8));
                cipIdentityItem.SocketAddress.SIN_port = Convert.ToUInt16(receivedData[9 + startingByte]
                                                                          | (receivedData[8 + startingByte] << 8));
                cipIdentityItem.SocketAddress.SIN_Address = (uint)(receivedData[13 + startingByte]
                                                                   | (receivedData[12 + startingByte] << 8)
                                                                   | (receivedData[11 + startingByte] << 16)
                                                                   | (receivedData[10 + startingByte] << 24)
                    );
                cipIdentityItem.VendorID1 = Convert.ToUInt16(receivedData[22 + startingByte]
                                                             | (receivedData[23 + startingByte] << 8));
                cipIdentityItem.DeviceType1 = Convert.ToUInt16(receivedData[24 + startingByte]
                                                               | (receivedData[25 + startingByte] << 8));
                cipIdentityItem.ProductCode1 = Convert.ToUInt16(receivedData[26 + startingByte]
                                                                | (receivedData[27 + startingByte] << 8));
                cipIdentityItem.Revision1[0] = receivedData[28 + startingByte];
                cipIdentityItem.Revision1[1] = receivedData[29 + startingByte];
                cipIdentityItem.Status1 = Convert.ToUInt16(receivedData[30 + startingByte]
                                                           | (receivedData[31 + startingByte] << 8));
                cipIdentityItem.SerialNumber1 = (uint)(receivedData[32 + startingByte]
                                                       | (receivedData[33 + startingByte] << 8)
                                                       | (receivedData[34 + startingByte] << 16)
                                                       | (receivedData[35 + startingByte] << 24));
                cipIdentityItem.ProductNameLength = receivedData[36 + startingByte];
                cipIdentityItem.ProductName1 = Encoding.ASCII.GetString(receivedData, 37 + startingByte, cipIdentityItem.ProductNameLength);
                cipIdentityItem.State1 = receivedData[receivedData.Length - 1];
                return cipIdentityItem;
            }
            /// <summary>
            ///     Converts an IP-Address in UIint32 Format (Received by Device)
            /// </summary>
            public static string getIPAddress(uint address)
            {
                return (byte)(address >> 24) + "." + (byte)(address >> 16) + "." + (byte)(address >> 8) + "." + (byte)address;
            }


        }

        /// <summary>
        ///     Socket Address (see section 2-6.3.2)
        /// </summary>
        public class SocketAddress
        {
            public ushort SIN_family;
            public ushort SIN_port;
            public uint SIN_Address;
            public byte[] SIN_Zero = new byte[8];
        }

        public class CommonPacketFormat
        {
            public ushort ItemCount = 2;
            public ushort AddressItem = 0x0000;
            public ushort AddressLength = 0;
            public ushort DataItem = 0xB2; //0xB2 = Unconnected Data Item
            public ushort DataLength = 8;
            public List<byte> Data = new List<byte>();
            public ushort SockaddrInfoItem_O_T = 0x8001; //8000 for O->T and 8001 for T->O - Volume 2 Table 2-6.9
            public ushort SockaddrInfoLength = 16;
            public SocketAddress SocketaddrInfo_O_T = null;


            public byte[] toBytes()
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
                    returnValue[10 + Data.Count + 11] = (byte)SocketaddrInfo_O_T.SIN_Address;
                    returnValue[10 + Data.Count + 10] = (byte)(SocketaddrInfo_O_T.SIN_Address >> 8);
                    returnValue[10 + Data.Count + 9] = (byte)(SocketaddrInfo_O_T.SIN_Address >> 16);
                    returnValue[10 + Data.Count + 8] = (byte)(SocketaddrInfo_O_T.SIN_Address >> 24);
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
        }
    }
}