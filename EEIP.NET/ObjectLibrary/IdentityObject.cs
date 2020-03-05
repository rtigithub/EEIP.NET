// ***********************************************************************
// Assembly         : EEIP
// Created          : 03-05-2020
// Last Modified On : 03-05-2020
// <copyright file="IdentityObject.cs" company="Stefan Rossmann, Nathan Brown and contributors">
//     Copyright © 2020, All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Sres.Net.EEIP.ObjectLibrary
{
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Identity Object - Class Code: 01 Hex
    /// </summary>
    /// <remarks>
    /// This object provides identification of and general information about the device. The Identity Object shall be present in all CIP products.
    /// If autonomous components of a device exist, use multiple instances of the Identity Object.
    /// </remarks>
    /// <summary>
    /// Class IdentityObject.
    /// </summary>
    public class IdentityObject
    {
        #region Public Fields

        /// <summary>
        /// The eeip client
        /// </summary>
        public EEIPClient eeipClient;

        #endregion Public Fields

        #region Public Constructors

        /// <summary>
        /// Constructor. </summary>
        /// <param name="eeipClient"> EEIPClient Object</param>
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityObject"/> class.
        /// </summary>
        /// <param name="eeipClient">The eeip client.</param>
        public IdentityObject(EEIPClient eeipClient)
        {
            this.eeipClient = eeipClient;
        }

        #endregion Public Constructors

        #region Public Enums

        /// <summary>
        /// Enum StateEnum
        /// </summary>
        public enum StateEnum
        {
            /// <summary>
            /// The nonexistent
            /// </summary>
            Nonexistent = 0,
            /// <summary>
            /// The device self testing
            /// </summary>
            DeviceSelfTesting = 1,
            /// <summary>
            /// The standby
            /// </summary>
            Standby = 2,
            /// <summary>
            /// The operational
            /// </summary>
            Operational = 3,
            /// <summary>
            /// The major recoverable fault
            /// </summary>
            MajorRecoverableFault = 4,
            /// <summary>
            /// The major unrecoverable fault
            /// </summary>
            MajorUnrecoverableFault = 5,
            /// <summary>
            /// The defaultfor get attributes all service
            /// </summary>
            DefaultforGet_Attributes_All_service = 255
        }

        #endregion Public Enums

        #region Public Methods

        /// <summary>
        /// gets all class attributes
        /// </summary>
        /// <returns>Task&lt;ClassAttributesStruct&gt;.</returns>
        public async Task<ClassAttributesStruct> GetClassAttributesAsync()
        {
            byte[] byteArray = await eeipClient.GetAttributeAllAsync(1, 0);
            ClassAttributesStruct returnValue;
            returnValue.Revision = (ushort)(byteArray[1] << 8 | byteArray[0]);
            returnValue.MaxInstance = (ushort)(byteArray[3] << 8 | byteArray[2]);
            returnValue.MaxIDNumberOfClassAttributes = (ushort)(byteArray[5] << 8 | byteArray[4]);
            returnValue.MaxIDNumberOfInstanceAttributes = (ushort)(byteArray[7] << 8 | byteArray[6]);
            return returnValue;
        }

        /// <summary>
        /// gets the State / Read "Identity Object" Class Code 0x01 - Attribute ID 9
        /// </summary>
        /// <returns>Task&lt;System.UInt16&gt;.</returns>
        public async Task<ushort> GetConfigurationConsistencyValueAsync()
        {
            byte[] byteArray = await eeipClient.GetAttributeSingleAsync(1, 1, 9);
            ushort returnValue = (ushort)(byteArray[1] << 8 | byteArray[0]);
            return returnValue;
        }

        /// <summary>
        /// gets the Device Type / Read "Identity Object" Class Code 0x01 - Attribute ID 2
        /// </summary>
        /// <returns>Task&lt;System.UInt16&gt;.</returns>
        public async Task<ushort> GetDeviceTypeAsync()
        {
            byte[] byteArray = await eeipClient.GetAttributeSingleAsync(1, 1, 2);
            ushort returnValue = (ushort)(byteArray[1] << 8 | byteArray[0]);
            return returnValue;
        }

        /// <summary>
        /// gets the Heartbeat intervall / Read "Identity Object" Class Code 0x01 - Attribute ID 10
        /// </summary>
        /// <returns>Task&lt;System.Byte&gt;.</returns>
        public async Task<byte> GetHeartbeatIntervalAsync()
        {
            byte[] byteArray = await eeipClient.GetAttributeSingleAsync(1, 1, 10);
            byte returnValue = (byte)byteArray[0];
            return returnValue;
        }

        /// <summary>
        /// gets all instance attributes
        /// </summary>
        /// <returns>Task&lt;InstanceAttributesStruct&gt;.</returns>
        public async Task<InstanceAttributesStruct> GetInstanceAttributesAsync()
        {
            byte[] byteArray = await eeipClient.GetAttributeAllAsync(1, 1);
            InstanceAttributesStruct returnValue;
            returnValue.VendorID = (ushort)(byteArray[1] << 8 | byteArray[0]);
            returnValue.DeviceType = (ushort)(byteArray[3] << 8 | byteArray[2]);
            returnValue.ProductCode = (ushort)(byteArray[5] << 8 | byteArray[4]);
            returnValue.Revision.MajorRevision = byteArray[6];
            returnValue.Revision.MinorRevision = byteArray[7];
            returnValue.Status = (ushort)(byteArray[9] << 8 | byteArray[8]);
            returnValue.SerialNumber = ((uint)byteArray[13] << 24 | (uint)byteArray[12] << 16 | (uint)byteArray[11] << 8 | (uint)byteArray[10]);
            byte[] productName = new byte[byteArray[14]];
            System.Buffer.BlockCopy(byteArray, 15, productName, 0, productName.Length);
            returnValue.ProductName = Encoding.UTF8.GetString(productName);
            return returnValue;
        }

        /// <summary>
        /// gets the Product code / Read "Identity Object" Class Code 0x01 - Attribute ID 3
        /// </summary>
        /// <returns>Task&lt;System.UInt16&gt;.</returns>
        public async Task<ushort> GetProductCodeAsync()
        {
            byte[] byteArray = await eeipClient.GetAttributeSingleAsync(1, 1, 3);
            ushort returnValue = (ushort)(byteArray[1] << 8 | byteArray[0]);
            return returnValue;
        }

        /// <summary>
        /// gets the Product Name / Read "Identity Object" Class Code 0x01 - Attribute ID 7
        /// </summary>
        /// <returns>Task&lt;System.String&gt;.</returns>
        public async Task<string> GetProductNameAsync()
        {
            byte[] byteArray = await eeipClient.GetAttributeSingleAsync(1, 1, 7);
            string returnValue = Encoding.UTF8.GetString(byteArray);
            return returnValue;
        }

        /// <summary>
        /// gets the Revision / Read "Identity Object" Class Code 0x01 - Attribute ID 4
        /// </summary>
        /// <returns>Revision</returns>
        public async Task<Revison> GetRevisionAsync()
        {
            byte[] byteArray = await eeipClient.GetAttributeSingleAsync(1, 1, 4);
            Revison returnValue = new Revison();
            returnValue.MajorRevision = (ushort)(byteArray[0]);
            returnValue.MinorRevision = (ushort)(byteArray[1]);
            return returnValue;
        }

        /// <summary>
        /// gets the Serial number / Read "Identity Object" Class Code 0x01 - Attribute ID 6
        /// </summary>
        /// <returns>Task&lt;System.UInt32&gt;.</returns>
        public async Task<uint> GetSerialNumberAsync()
        {
            byte[] byteArray = await eeipClient.GetAttributeSingleAsync(1, 1, 6);
            uint returnValue = ((uint)byteArray[3] << 24 | (uint)byteArray[2] << 16 | (uint)byteArray[1] << 8 | (uint)byteArray[0]);
            return returnValue;
        }

        /// <summary>
        /// gets the State / Read "Identity Object" Class Code 0x01 - Attribute ID 8
        /// </summary>
        /// <returns>Task&lt;StateEnum&gt;.</returns>
        public async Task<StateEnum> GetStateAsync()
        {
            byte[] byteArray = await eeipClient.GetAttributeSingleAsync(1, 1, 8);
            StateEnum returnValue = (StateEnum)byteArray[0];
            return returnValue;
        }

        /// <summary>
        /// gets the Status / Read "Identity Object" Class Code 0x01 - Attribute ID 5
        /// </summary>
        /// <returns>Task&lt;System.UInt16&gt;.</returns>
        public async Task<ushort> GetStatusAsync()
        {
            byte[] byteArray = await eeipClient.GetAttributeSingleAsync(1, 1, 5);
            ushort returnValue = (ushort)(byteArray[1] << 8 | byteArray[0]);
            return returnValue;
        }

        /// <summary>
        /// gets the Supported Language List / Read "Identity Object" Class Code 0x01 - Attribute ID 12
        /// </summary>
        /// <returns>Task&lt;System.String[]&gt;.</returns>
        public async Task<string[]> GetSupportedLanguageListAsync()
        {
            byte[] byteArray = await eeipClient.GetAttributeSingleAsync(1, 1, 12);
            string[] returnValue = new string[byteArray.Length / 3];
            for (int i = 0; i < returnValue.Length; i++)
            {
                byte[] byteArray2 = new byte[3];
                System.Buffer.BlockCopy(byteArray, i * 3, byteArray2, 0, 3);
                returnValue[i] = Encoding.UTF8.GetString(byteArray2);
            }
            return returnValue;
        }

        /// <summary>
        /// gets the Vendor ID / Read "Identity Object" Class Code 0x01 - Attribute ID 1
        /// </summary>
        /// <returns>Task&lt;System.UInt16&gt;.</returns>
        public async Task<ushort> GetVendorIDAsync()
        {
            byte[] byteArray = await eeipClient.GetAttributeSingleAsync(1, 1, 1);
            ushort returnValue = (ushort)(byteArray[1] << 8 | byteArray[0]);
            return returnValue;
        }

        #endregion Public Methods

        #region Public Structs

        /// <summary>
        /// Struct ClassAttributesStruct
        /// </summary>
        public struct ClassAttributesStruct
        {
            #region Public Fields

            /// <summary>
            /// The maximum identifier number of class attributes
            /// </summary>
            public ushort MaxIDNumberOfClassAttributes;
            /// <summary>
            /// The maximum identifier number of instance attributes
            /// </summary>
            public ushort MaxIDNumberOfInstanceAttributes;
            /// <summary>
            /// The maximum instance
            /// </summary>
            public ushort MaxInstance;
            /// <summary>
            /// The revision
            /// </summary>
            public ushort Revision;

            #endregion Public Fields
        }

        /// <summary>
        /// Struct InstanceAttributesStruct
        /// </summary>
        public struct InstanceAttributesStruct
        {
            #region Public Fields

            /// <summary>
            /// The device type
            /// </summary>
            public ushort DeviceType;
            /// <summary>
            /// The product code
            /// </summary>
            public ushort ProductCode;
            /// <summary>
            /// The product name
            /// </summary>
            public string ProductName;
            /// <summary>
            /// The revision
            /// </summary>
            public Revison Revision;
            /// <summary>
            /// The serial number
            /// </summary>
            public uint SerialNumber;
            /// <summary>
            /// The status
            /// </summary>
            public ushort Status;
            /// <summary>
            /// The vendor identifier
            /// </summary>
            public ushort VendorID;

            #endregion Public Fields
        }

        /// <summary>
        /// Struct Revison
        /// </summary>
        public struct Revison
        {
            #region Public Fields

            /// <summary>
            /// The major revision
            /// </summary>
            public ushort MajorRevision;
            /// <summary>
            /// The minor revision
            /// </summary>
            public ushort MinorRevision;

            #endregion Public Fields
        }

        #endregion Public Structs
    }
}
