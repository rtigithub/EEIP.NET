namespace Sres.Net.EEIP
{
    using System;

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
