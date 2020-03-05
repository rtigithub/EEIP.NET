using System;

namespace Sres.Net.EEIP
{
    public class CIPException : Exception
    {
        #region Public Constructors

        public CIPException()
        {
        }

        public CIPException(string message)
            : base(message)
        {
        }

        public CIPException(string message, Exception inner)
            : base(message, inner)
        {
        }

        #endregion Public Constructors
    }
}
