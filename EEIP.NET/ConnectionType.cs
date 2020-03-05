// ***********************************************************************
// Assembly         : EEIP
// Created          : 03-05-2020
// Last Modified On : 03-05-2020
// <copyright file="ConnectionType.cs" company="Stefan Rossmann, Nathan Brown and contributors">
//     Copyright © 2020, All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Sres.Net.EEIP
{
    /// <summary>
    /// Enum ConnectionType
    /// </summary>
    public enum ConnectionType : byte
    {
        /// <summary>
        /// The null
        /// </summary>
        Null = 0,
        /// <summary>
        /// The multicast
        /// </summary>
        Multicast = 1,
        /// <summary>
        /// The point to point
        /// </summary>
        Point_to_Point = 2
    }
}
