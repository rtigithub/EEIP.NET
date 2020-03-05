// ***********************************************************************
// Assembly         : EEIP
// Created          : 03-05-2020
// Last Modified On : 03-05-2020
// <copyright file="CIPCommonServices.cs" company="Stefan Rossmann, Nathan Brown and contributors">
//     Copyright © 2020, All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Sres.Net.EEIP
{
    /// <summary>
    /// Table A-3.1 Volume 1 Chapter A-3
    /// </summary>
    public enum CIPCommonServices : byte
    {
        /// <summary>
        /// The get attributes all
        /// </summary>
        Get_Attributes_All = 0x01,
        /// <summary>
        /// The set attributes all request
        /// </summary>
        Set_Attributes_All_Request = 0x02,
        /// <summary>
        /// The get attribute list
        /// </summary>
        Get_Attribute_List = 0x03,
        /// <summary>
        /// The set attribute list
        /// </summary>
        Set_Attribute_List = 0x04,
        /// <summary>
        /// The reset
        /// </summary>
        Reset = 0x05,
        /// <summary>
        /// The start
        /// </summary>
        Start = 0x06,
        /// <summary>
        /// The stop
        /// </summary>
        Stop = 0x07,
        /// <summary>
        /// The create
        /// </summary>
        Create = 0x08,
        /// <summary>
        /// The delete
        /// </summary>
        Delete = 0x09,
        /// <summary>
        /// The multiple service packet
        /// </summary>
        Multiple_Service_Packet = 0x0A,
        /// <summary>
        /// The apply attributes
        /// </summary>
        Apply_Attributes = 0x0D,
        /// <summary>
        /// The get attribute single
        /// </summary>
        Get_Attribute_Single = 0x0E,
        /// <summary>
        /// The set attribute single
        /// </summary>
        Set_Attribute_Single = 0x10,
        /// <summary>
        /// The find next object instance
        /// </summary>
        Find_Next_Object_Instance = 0x11,
        /// <summary>
        /// The error response
        /// </summary>
        Error_Response = 0x14,
        /// <summary>
        /// The restore
        /// </summary>
        Restore = 0x15,
        /// <summary>
        /// The save
        /// </summary>
        Save = 0x16,
        /// <summary>
        /// The nop
        /// </summary>
        NOP = 0x17,
        /// <summary>
        /// The get member
        /// </summary>
        Get_Member = 0x18,
        /// <summary>
        /// The set member
        /// </summary>
        Set_Member = 0x19,
        /// <summary>
        /// The insert member
        /// </summary>
        Insert_Member = 0x1A,
        /// <summary>
        /// The remove member
        /// </summary>
        Remove_Member = 0x1B,
        /// <summary>
        /// The group synchronize
        /// </summary>
        GroupSync = 0x1C
    }
}
