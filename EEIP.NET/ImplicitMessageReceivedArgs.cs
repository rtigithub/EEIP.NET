using System;

namespace Sres.Net.EEIP
{
    public class ImplicitMessageReceivedArgs : EventArgs
    {
        #region Public Constructors

        public ImplicitMessageReceivedArgs(uint connectionId)
        {
            ConnectionId = connectionId;
        }

        #endregion Public Constructors

        #region Public Properties

        public uint ConnectionId { get; }

        #endregion Public Properties
    }
}
