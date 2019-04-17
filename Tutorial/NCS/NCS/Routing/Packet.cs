using System;
using System.Collections.Generic;
using CGD;
using Ncs.Routing;
using Ncs.Server;

namespace Ncs
{
    public class Packet
    {
        public Action<NcsUser, NcsRequestInfo> this[dynamic i]
        {
            set
            {
                Console.WriteLine(i);
                this.Add(i, value);
            }
        }

        internal static Dictionary<dynamic, Action<NcsUser, NcsRequestInfo>> BufferDictionary = new Dictionary<dynamic, Action<NcsUser, NcsRequestInfo>>();

        public void Add(dynamic type, Action<NcsUser, NcsRequestInfo> action)
        {
            BufferDictionary.Add(type, action);
        }
    }
}