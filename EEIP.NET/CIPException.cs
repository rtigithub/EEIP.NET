using System;

namespace Sres.Net.EEIP
{
    public class CIPException : Exception
    {
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
    }
}
