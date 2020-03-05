using System;
using System.Text;
using System.Threading.Tasks;

namespace Sres.Net.EEIP.ObjectLibrary
{
    /// <summary>
    /// Identity Object - Class Code: 01 Hex
    /// </summary>
    /// <remarks>
    /// This object provides identification of and general information about the device. The Identity Object shall be present in all CIP products.
    /// If autonomous components of a device exist, use multiple instances of the Identity Object.
    /// </remarks>
    public class IdentityObject
    {
        public EEIPClient eeipClient;

        /// <summary>
        /// Constructor. </summary>
        /// <param name="eeipClient"> EEIPClient Object</param>
        public IdentityObject(EEIPClient eeipClient)
        {
            this.eeipClient = eeipClient;
        }

        /// <summary>
        /// gets the Vendor ID / Read "Identity Object" Class Code 0x01 - Attribute ID 1
        /// </summary>
        public async Task<ushort> GetVendorIDAsync()
        {
            byte[] byteArray = await eeipClient.GetAttributeSingleAsync(1, 1, 1);
            ushort returnValue = (ushort)(byteArray[1] << 8 | byteArray[0]);
            return returnValue;
        }

        /// <summary>
        /// gets the Device Type / Read "Identity Object" Class Code 0x01 - Attribute ID 2
        /// </summary>
        public async Task<ushort> GetDeviceTypeAsync()
        {
            byte[] byteArray = await eeipClient.GetAttributeSingleAsync(1, 1, 2);
            ushort returnValue = (ushort)(byteArray[1] << 8 | byteArray[0]);
            return returnValue;
        }


        /// <summary>
        /// gets the Product code / Read "Identity Object" Class Code 0x01 - Attribute ID 3
        /// </summary>
        public async Task<ushort> GetProductCodeAsync()
        {
            byte[] byteArray = await eeipClient.GetAttributeSingleAsync(1, 1, 3);
            ushort returnValue = (ushort)(byteArray[1] << 8 | byteArray[0]);
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

        public struct Revison
        {
            public ushort MajorRevision;
            public ushort MinorRevision;
        }

        /// <summary>
        /// gets the Status / Read "Identity Object" Class Code 0x01 - Attribute ID 5
        /// </summary>
        public async Task<ushort> GetStatusAsync()
        {
            byte[] byteArray = await eeipClient.GetAttributeSingleAsync(1, 1, 5);
            ushort returnValue = (ushort)(byteArray[1] << 8 | byteArray[0]);
            return returnValue;
        }

        /// <summary>
        /// gets the Serial number / Read "Identity Object" Class Code 0x01 - Attribute ID 6
        /// </summary>
        public async Task<uint> GetSerialNumberAsync()
        {
            byte[] byteArray = await eeipClient.GetAttributeSingleAsync(1, 1, 6);
            uint returnValue = ((uint)byteArray[3] << 24 | (uint)byteArray[2] << 16 | (uint)byteArray[1] << 8 | (uint)byteArray[0]);
            return returnValue;
        }

        /// <summary>
        /// gets the Product Name / Read "Identity Object" Class Code 0x01 - Attribute ID 7
        /// </summary>
        public async Task<string> GetProductNameAsync()
        {
            byte[] byteArray = await eeipClient.GetAttributeSingleAsync(1, 1, 7);
            string returnValue = Encoding.UTF8.GetString(byteArray);
            return returnValue;
        }

        public enum StateEnum
        {
            Nonexistent = 0,
            DeviceSelfTesting = 1,
            Standby = 2,
            Operational = 3,
            MajorRecoverableFault = 4,
            MajorUnrecoverableFault = 5,
            DefaultforGet_Attributes_All_service = 255
        }

        /// <summary>
        /// gets the State / Read "Identity Object" Class Code 0x01 - Attribute ID 8
        /// </summary>
        public async Task<StateEnum> GetStateAsync()
        {
            byte[] byteArray = await eeipClient.GetAttributeSingleAsync(1, 1, 8);
            StateEnum returnValue = (StateEnum)byteArray[0];
            return returnValue;
        }

        /// <summary>
        /// gets the State / Read "Identity Object" Class Code 0x01 - Attribute ID 9
        /// </summary>
        public async Task<ushort> GetConfigurationConsistencyValueAsync()
        {
            byte[] byteArray = await eeipClient.GetAttributeSingleAsync(1, 1, 9);
            ushort returnValue = (ushort)(byteArray[1] << 8 | byteArray[0]);
            return returnValue;
        }

        /// <summary>
        /// gets the Heartbeat intervall / Read "Identity Object" Class Code 0x01 - Attribute ID 10
        /// </summary>
        public async Task<byte> GetHeartbeatIntervalAsync()
        {
            byte[] byteArray = await eeipClient.GetAttributeSingleAsync(1, 1, 10);
            byte returnValue = (byte)byteArray[0];
            return returnValue;
        }

        /// <summary>
        /// gets the Supported Language List / Read "Identity Object" Class Code 0x01 - Attribute ID 12
        /// </summary>
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
        /// gets all class attributes
        /// </summary>
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
        /// gets all instance attributes
        /// </summary>
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


        public struct ClassAttributesStruct
        {
            public ushort Revision;
            public ushort MaxInstance;
            public ushort MaxIDNumberOfClassAttributes;
            public ushort MaxIDNumberOfInstanceAttributes;
        }

        public struct InstanceAttributesStruct
        {
            public ushort VendorID;
            public ushort DeviceType;
            public ushort ProductCode;
            public Revison Revision;
            public ushort Status;
            public uint SerialNumber;
            public string ProductName;
        }
    }
}
