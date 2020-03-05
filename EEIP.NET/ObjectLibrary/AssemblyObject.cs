// ***********************************************************************
// Assembly         : EEIP
// Created          : 03-05-2020
// Last Modified On : 03-05-2020
// <copyright file="AssemblyObject.cs" company="Stefan Rossmann, Nathan Brown and contributors">
//     Copyright © 2020, All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Sres.Net.EEIP.ObjectLibrary
{
    using System.Threading.Tasks;

    /// <summary>
    /// Class AssemblyObject.
    /// </summary>
    public class AssemblyObject
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
        public AssemblyObject(EEIPClient eeipClient)
        {
            this.eeipClient = eeipClient;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Reads the Instance of the Assembly Object (Instance 101 returns the bytes of the class ID 101)
        /// </summary>
        /// <param name="instanceNo">Instance number to be returned</param>
        /// <returns>bytes of the Instance</returns>
        public Task<byte[]> GetInstanceAsync(int instanceNo)
        {
            return this.eeipClient.GetAttributeSingleAsync(4, instanceNo, 3);
        }

        /// <summary>
        /// Sets an Instance of the Assembly Object
        /// </summary>
        /// <param name="instanceNo">Instance number to be returned</param>
        /// <param name="value">The value.</param>
        /// <returns>bytes of the Instance</returns>
        public Task SetInstanceAsync(int instanceNo, byte[] value)
        {
            return eeipClient.SetAttributeSingleAsync(4, instanceNo, 3, value);
        }

        #endregion Public Methods
    }
}
