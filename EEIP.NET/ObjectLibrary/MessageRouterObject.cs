// ***********************************************************************
// Assembly         : EEIP
// Created          : 03-05-2020
// Last Modified On : 03-05-2020
// <copyright file="MessageRouterObject.cs" company="Stefan Rossmann, Nathan Brown and contributors">
//     Copyright © 2020, All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Sres.Net.EEIP.ObjectLibrary
{
    using System.Threading.Tasks;

    /// <summary>
    /// Class MessageRouterObject.
    /// </summary>
    public class MessageRouterObject
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
        public MessageRouterObject(EEIPClient eeipClient)
        {
            this.eeipClient = eeipClient;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// gets the active connections / Read "Message Router Object" Class Code 0x02 - Attribute ID 4
        /// </summary>
        /// <returns>Task&lt;System.UInt16[]&gt;.</returns>
        public async Task<ushort[]> GetActiveConnectionsAsync()
        {
            byte[] byteArray = await eeipClient.GetAttributeSingleAsync(2, 4, 1);
            ushort[] returnValue = new ushort[byteArray.Length / 2];
            for (int i = 0; i < returnValue.Length; i++)
            {
                returnValue[i] = (ushort)(byteArray[1 + 2 * i] << 8 | byteArray[0 + 2 * i]);
            }
            return returnValue;
        }

        /// <summary>
        /// gets the number of active connections / Read "Message Router Object" Class Code 0x02 - Attribute ID 3
        /// </summary>
        /// <returns>Task&lt;System.UInt16&gt;.</returns>
        public async Task<ushort> GetNumberActiveAsync()
        {
            byte[] byteArray = await eeipClient.GetAttributeSingleAsync(2, 3, 1);
            ushort returnValue;
            returnValue = (ushort)(byteArray[1] << 8 | byteArray[0]);
            return returnValue;
        }

        /// <summary>
        /// gets the Maximum of connections supported / Read "Message Router Object" Class Code 0x02 - Attribute ID 2
        /// </summary>
        /// <returns>Task&lt;System.UInt16&gt;.</returns>
        public async Task<ushort> GetNumberAvailableAsync()
        {
            byte[] byteArray = await eeipClient.GetAttributeSingleAsync(2, 2, 1);
            ushort returnValue;
            returnValue = (ushort)(byteArray[1] << 8 | byteArray[0]);
            return returnValue;
        }

        /// <summary>
        /// gets the Object List / Read "Message Router Object" Class Code 0x02 - Attribute ID 1
        /// </summary>
        /// <returns>Task&lt;ObjectListStruct&gt;.</returns>
        public async Task<ObjectListStruct> GetObjectListAsync()
        {
            byte[] byteArray = await eeipClient.GetAttributeSingleAsync(2, 1, 1);
            ObjectListStruct returnValue;
            returnValue.Number = (ushort)(byteArray[1] << 8 | byteArray[0]);
            returnValue.Classes = new ushort[returnValue.Number];
            for (int i = 0; i < returnValue.Classes.Length; i++)
            {
                returnValue.Classes[i] = (ushort)(byteArray[i * 2 + 3] << 8 | byteArray[i * 2 + 2]);
            }
            return returnValue;
        }

        #endregion Public Methods

        #region Public Structs

        /// <summary>
        /// Struct ObjectListStruct
        /// </summary>
        public struct ObjectListStruct
        {
            #region Public Fields

            /// <summary>
            /// The classes
            /// </summary>
            public ushort[] Classes;
            /// <summary>
            /// The number
            /// </summary>
            public ushort Number;

            #endregion Public Fields
        }

        #endregion Public Structs
    }
}
