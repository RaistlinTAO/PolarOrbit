#region À la vie, à la mort

// ###########################################
// 
// FILE:                      Cybernetics.cs 
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
using System.Linq;
using System.ServiceModel;

#endregion

namespace PolarOrbit
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class Cybernetics : PolarOrbit
    {
        public delegate void TransimitMessage(MessageInfo Message);

        private readonly List<MessageInfo> _Message = new List<MessageInfo>();

        public void Transimit(MessageInfo Message)
        {
            _Message.Add(Message);
            if (OnTransimitMessage != null) OnTransimitMessage(Message);
        }

        public List<MessageInfo> GetMessage()
        {
            _Message.Sort(new TimeComparer());
            var newList = _Message.Where(y => DateTime.Now.Subtract(y.Time).Hours < 2).ToList();
            return newList.Count > 30 ? newList.GetRange(newList.Count - 30, 30) : newList;
        }

        public event TransimitMessage OnTransimitMessage;

        public int GetMessageNumber()
        {
            return _Message.Count;
        }

        public void CleanMessage()
        {
            try
            {
                var newList = _Message.Where(y => DateTime.Now.Subtract(y.Time).Days <= 1).ToList();
                _Message.Clear();
                _Message.AddRange(newList);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public class TimeComparer : IComparer<MessageInfo>
    {
        public int Compare(MessageInfo x, MessageInfo y)
        {
            return x.Time.CompareTo(y.Time);
        }
    }
}