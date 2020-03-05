// ***********************************************************************
// Assembly         : EEIP
// Created          : 03-05-2020
// Last Modified On : 03-05-2020
// <copyright file="ImplicitMessageReceivedArgs.cs" company="Stefan Rossmann, Nathan Brown and contributors">
//     Copyright © 2020, All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Sres.Net.EEIP
{
    using System;

    /// <summary>
    /// Class ImplicitMessageReceivedArgs.
    /// Implements the <see cref="System.EventArgs" />
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class ImplicitMessageReceivedArgs : EventArgs
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ImplicitMessageReceivedArgs"/> class.
        /// </summary>
        /// <param name="connectionId">The connection identifier.</param>
        public ImplicitMessageReceivedArgs(uint connectionId)
        {
            ConnectionId = connectionId;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets the connection identifier.
        /// </summary>
        /// <value>The connection identifier.</value>
        public uint ConnectionId { get; }

        #endregion Public Properties
    }
}
