// ***********************************************************************
// Assembly         : EEIP
// Created          : 03-05-2020
// Last Modified On : 03-05-2020
// <copyright file="Priority.cs" company="Stefan Rossmann, Nathan Brown and contributors">
//     Copyright © 2020, All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Sres.Net.EEIP
{
    /// <summary>
    /// Enum Priority
    /// </summary>
    public enum Priority : byte
    {
        /// <summary>
        /// The low
        /// </summary>
        Low = 0,
        /// <summary>
        /// The high
        /// </summary>
        High = 1,
        /// <summary>
        /// The scheduled
        /// </summary>
        Scheduled = 2,
        /// <summary>
        /// The urgent
        /// </summary>
        Urgent = 3
    }
}
