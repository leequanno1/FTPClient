using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace lib
{
    public class Client
    {
        private static Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static Socket fileSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static string authenToken = String.Empty;
        public static Socket ClientSocket { get => clientSocket;}
        public static Socket FileSocket { get => fileSocket;}
        public static string AuthenToken { get => authenToken; set => authenToken = value; }

        public static void Connect(IPEndPoint serverEndpoint)
        {
            clientSocket.Connect(serverEndpoint);
            //TcpProtocol.Send<GlobalRequest>(ClientSocket, new GlobalRequest() { Route = "/login", AuthentToken = null, RequestObject = null });
            //TcpProtocol.Send<GlobalRequest>(ClientSocket, new GlobalRequest() { Route = "/signup", AuthentToken = null, RequestObject = null });
            //TcpProtocol.Send<GlobalRequest>(ClientSocket, new GlobalRequest() { Route = "/list", AuthentToken = null, RequestObject = null });
            //TcpProtocol.Send<GlobalRequest>(ClientSocket, new GlobalRequest() { Route = "/folder-add", AuthentToken = null, RequestObject = null });
            //TcpProtocol.Send<GlobalRequest>(ClientSocket, new GlobalRequest() { Route = "/folder-update", AuthentToken = null, RequestObject = null });
            //TcpProtocol.Send<GlobalRequest>(ClientSocket, new GlobalRequest() { Route = "/folder-delete", AuthentToken = null, RequestObject = null });
            //TcpProtocol.Send<GlobalRequest>(ClientSocket, new GlobalRequest() { Route = "/file-add", AuthentToken = null, RequestObject = null });
            //TcpProtocol.Send<GlobalRequest>(ClientSocket, new GlobalRequest() { Route = "/file-add", AuthentToken = null, RequestObject = null });
            //TcpProtocol.Send<GlobalRequest>(ClientSocket, new GlobalRequest() { Route = "/file-update", AuthentToken = null, RequestObject = null });
            //TcpProtocol.Send<GlobalRequest>(ClientSocket, new GlobalRequest() { Route = "/file-delete", AuthentToken = null, RequestObject = null });
        }

        public static void ConnectFileSocket(IPEndPoint fileServerEndpoint)
        {
            fileSocket.Connect(fileServerEndpoint);
        }
    }
}
