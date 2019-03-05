#region À la vie, à la mort

// ###########################################
// 
// FILE:                      PolarOrbit.cs 
// PROJECT:              PolarOrbit
// SOLUTION:           PolarOrbit
// 
// CREATED BY RAISTLIN TAO
// DEMONVK@GMAIL.COM
// 
// FILE CREATION: 5:44 PM  05/12/2015
// 
// ###########################################

#endregion

#region Embrace the code

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

#endregion

namespace PolarOrbit
{
    [ServiceContract]
    public interface PolarOrbit
    {
        [OperationContract]
        void Transimit(MessageInfo Message);

        [OperationContract]
        List<MessageInfo> GetMessage();
    }

    [DataContract]
    public class MessageInfo
    {
        [DataMember] public string User { get; set; }

        [DataMember] public string ToUser { get; set; }

        [DataMember] public string Message { get; set; }

        [DataMember] public DateTime Time { get; set; }
    }
}