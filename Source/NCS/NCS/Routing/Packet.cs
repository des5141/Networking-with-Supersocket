using System;
using System.Collections.Generic;
using CGD;
using Ncs.Routing;
using Ncs.Server;
using SuperSocket.SocketBase;

namespace Ncs
{
    public class Packet<T> where T : AppSession<T, NcsRequestInfo>, new()
    {
        public Action<T, NcsRequestInfo> this[dynamic i]
        {
            set
            {
                Console.WriteLine(i);
                this.Add(i, value);
            }
        }

        internal static Dictionary<dynamic, Action<T, NcsRequestInfo>> BufferDictionary = new Dictionary<dynamic, Action<T, NcsRequestInfo>>();

        public void Add(dynamic type, Action<T, NcsRequestInfo> action)
        {
            BufferDictionary.Add(type, action);
        }
    }
}