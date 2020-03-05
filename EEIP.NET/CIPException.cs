// ***********************************************************************
// Assembly         : EEIP
// Created          : 03-05-2020
// Last Modified On : 03-05-2020
// <copyright file="CIPException.cs" company="Stefan Rossmann, Nathan Brown and contributors">
//     Copyright © 2020, All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Sres.Net.EEIP
{
    using System;

    /// <summary>
    /// Class CIPException.
    /// Implements the <see cref="System.Exception" />
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class CIPException : Exception
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CIPException"/> class.
        /// </summary>
        public CIPException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CIPException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public CIPException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CIPException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public CIPException(string message, Exception inner)
            : base(message, inner)
        {
        }

        #endregion Public Constructors
    }
}
