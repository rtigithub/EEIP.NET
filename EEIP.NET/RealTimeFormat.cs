// ***********************************************************************
// Assembly         : EEIP
// Created          : 03-05-2020
// Last Modified On : 03-05-2020
// <copyright file="RealTimeFormat.cs" company="Stefan Rossmann, Nathan Brown and contributors">
//     Copyright © 2020, All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Sres.Net.EEIP
{
    /// <summary>
    /// Enum RealTimeFormat
    /// </summary>
    public enum RealTimeFormat : byte
    {
        /// <summary>
        /// The modeless
        /// </summary>
        Modeless = 0,
        /// <summary>
        /// The zero length
        /// </summary>
        ZeroLength = 1,
        /// <summary>
        /// The heartbeat
        /// </summary>
        Heartbeat = 2,
        /// <summary>
        /// The header32 bit
        /// </summary>
        Header32Bit = 3
    }
}
