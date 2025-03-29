using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FTPClient
{
    public static class ServerEndpoint
    {
        private static IPEndPoint _messageServer = new IPEndPoint(IPAddress.Loopback, 9000);
        private static IPEndPoint _fileServer = new IPEndPoint(IPAddress.Loopback, 9001);

        public static IPEndPoint MessageServer { get => _messageServer;}
        public static IPEndPoint FileServer { get => _fileServer;}
    }
}
