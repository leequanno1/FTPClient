using dto.requests;
using dto.responses;
using FTPClient.dto.requests;
using FTPClient.dto.responses;
using lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FTPClient
{
    public static class Controller
    {
        public static bool AddFile(Socket socket, FileAddRequest request, string realiticFilePath, Func<long, long> statusHandler = null)
        {
            // kết nối file server
            Client.ConnectFileSocket(ServerEndpoint.FileServer);
            request.IpEndPoint = Client.FileSocket.LocalEndPoint.ToString();
            // gửi request
            TcpProtocol.Send<GlobalRequest>(socket, new GlobalRequest()
            {
                Route = "/file-add",
                AuthentToken = Client.AuthenToken,
                RequestObject = request
            }
            );
            // nhận response
            GlobalResponse glResponse;
            if (TcpProtocol.Receive<GlobalResponse>(socket, out glResponse))
            {
                FileAddResponse response = ConverTo<FileAddResponse>(glResponse.RequestObject);
                if (response.Status == ResponseStatus.READY)
                {
                    // bắt đầu gửi file
                    try
                    {
                        Console.WriteLine(Client.FileSocket.RemoteEndPoint.ToString());
                        Console.WriteLine(Client.FileSocket.LocalEndPoint.ToString());
                        FileTransferHelper.SendFileTo(Client.FileSocket, realiticFilePath, statusHandler);
                        while (Controller.IsSocketConnected(Client.FileSocket))
                        {
                            Console.WriteLine("ádasd");
                            Thread.Sleep(100);
                        }
                        Client.FileSocket.Close();
                        Client.FileSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool DownloadFile(Socket socket, FileDownloadRequest request, string realicticSaveFolderPath, Func<long, long> statusHandler = null)
        {
            Client.ConnectFileSocket(ServerEndpoint.FileServer);
            request.ClientEndpoint = Client.FileSocket.LocalEndPoint.ToString();
            // gửi request
            TcpProtocol.Send<GlobalRequest>(socket, new GlobalRequest()
            {
                Route = "/file-download",
                AuthentToken = Client.AuthenToken,
                RequestObject = request
            }
            );
            // nhận response
            GlobalResponse glResponse;
            if (TcpProtocol.Receive<GlobalResponse>(socket, out glResponse))
            {
                FileDowloadResponse response = ConverTo<FileDowloadResponse>(glResponse.RequestObject);
                if (response.Status == ResponseStatus.READY)
                {
                    // bắt đầu nhận file file
                    try
                    {
                        string fileName = request.FilePath.Split('/').Last();
                        Console.WriteLine(socket.RemoteEndPoint.ToString());
                        Console.WriteLine(socket.LocalEndPoint.ToString());
                        FileTransferHelper.ReceiveFileFrom(Client.FileSocket, realicticSaveFolderPath, fileName, statusHandler);
                        Client.FileSocket.Shutdown(SocketShutdown.Both);

                        Client.FileSocket.Close();
                        Client.FileSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        public static FileDeleteResponse DeleteFile(Socket socket, FileDeleteRequest request)
        {
            TcpProtocol.Send<GlobalRequest>(socket, new GlobalRequest()
            {
                Route = "/file-delete",
                AuthentToken = Client.AuthenToken,
                RequestObject = request
            }
            );
            GlobalResponse response;
            if (TcpProtocol.Receive<GlobalResponse>(socket, out response))
            {
                return ConverTo<FileDeleteResponse>(response.RequestObject);
            }
            return null;
        }

        public static FileUpdateResponse UpdateFile(Socket socket, FileUpdateRequest request)
        {
            TcpProtocol.Send<GlobalRequest>(socket, new GlobalRequest()
            {
                Route = "/file-update",
                AuthentToken = Client.AuthenToken,
                RequestObject = request
            }
            );
            GlobalResponse response;
            if (TcpProtocol.Receive<GlobalResponse>(socket, out response))
            {
                return ConverTo<FileUpdateResponse>(response.RequestObject);
            }
            return null;
        }

        public static FileMoveResponse MoveFile(Socket socket, FileMoveRequest request)
        {
            TcpProtocol.Send<GlobalRequest>(socket, new GlobalRequest()
            {
                Route = "/file-move",
                AuthentToken = Client.AuthenToken,
                RequestObject = request
            }
            );
            GlobalResponse response;
            if (TcpProtocol.Receive<GlobalResponse>(socket, out response))
            {
                return ConverTo<FileMoveResponse>(response.RequestObject);
            }
            return null;
        }

        public static FileCopyResponse CopyFile(Socket socket, FileCopyRequest request)
        {
            TcpProtocol.Send<GlobalRequest>(socket, new GlobalRequest()
            {
                Route = "/file-copy",
                AuthentToken = Client.AuthenToken,
                RequestObject = request
            }
            );
            GlobalResponse response;
            if (TcpProtocol.Receive<GlobalResponse>(socket, out response))
            {
                return ConverTo<FileCopyResponse>(response.RequestObject);
            }
            return null;
        }

        public static FolderAddResponse AddFolder(Socket socket, FolderAddRequest request)
        {
            TcpProtocol.Send<GlobalRequest>(socket, new GlobalRequest()
            {
                Route = "/folder-add",
                AuthentToken = Client.AuthenToken,
                RequestObject = request
            }
            );
            GlobalResponse response;
            if (TcpProtocol.Receive<GlobalResponse>(socket, out response))
            {
                return ConverTo<FolderAddResponse>(response.RequestObject);
            }
            return null;
        }

        public static FolderDeleteResponse DeleteFolder(Socket socket, FolderDeleteRequest request)
        {
            TcpProtocol.Send<GlobalRequest>(socket, new GlobalRequest()
            {
                Route = "/folder-delete",
                AuthentToken = Client.AuthenToken,
                RequestObject = request
            }
            );
            GlobalResponse response;
            if (TcpProtocol.Receive<GlobalResponse>(socket, out response))
            {
                return ConverTo<FolderDeleteResponse>(response.RequestObject);
            }
            return null;
        }

        public static FolderUpdateResponse UpdateFolder(Socket socket, FolderUpdateRequest request)
        {
            TcpProtocol.Send<GlobalRequest>(socket, new GlobalRequest()
            {
                Route = "/folder-update",
                AuthentToken = Client.AuthenToken,
                RequestObject = request
            }
            );
            GlobalResponse response;
            if (TcpProtocol.Receive<GlobalResponse>(socket, out response))
            {
                return ConverTo<FolderUpdateResponse>(response.RequestObject);
            }
            return null;
        }

        public static FolderMoveResponse MoveFolder(Socket socket, FolderMoveRequest request)
        {
            TcpProtocol.Send<GlobalRequest>(socket, new GlobalRequest()
            {
                Route = "/folder-move",
                AuthentToken = Client.AuthenToken,
                RequestObject = request
            }
            );
            GlobalResponse response;
            if (TcpProtocol.Receive<GlobalResponse>(socket, out response))
            {
                return ConverTo<FolderMoveResponse>(response.RequestObject);
            }
            return null;
        }

        public static FolderCopyResponse CopyFolder(Socket socket, FolderCopyRequest request)
        {
            TcpProtocol.Send<GlobalRequest>(socket, new GlobalRequest()
            {
                Route = "/folder-copy",
                AuthentToken = Client.AuthenToken,
                RequestObject = request
            }
            );
            GlobalResponse response;
            if (TcpProtocol.Receive<GlobalResponse>(socket, out response))
            {
                return ConverTo<FolderCopyResponse>(response.RequestObject);
            }
            return null;
        }

        public static ListResponse ListDirectory(Socket socket, ListRequest request)
        {
            TcpProtocol.Send<GlobalRequest>(socket, new GlobalRequest()
            {
                Route = "/list",
                AuthentToken = Client.AuthenToken,
                RequestObject = request
            }
            );
            GlobalResponse response;
            if (TcpProtocol.Receive<GlobalResponse>(socket, out response))
            {
                return ConverTo<ListResponse>(response.RequestObject);
            }
            return null;
        }

        public static LoginResponse Login(Socket socket, LoginRequest request)
        {
            TcpProtocol.Send<GlobalRequest>(socket, new GlobalRequest()
            {
                Route = "/login",
                AuthentToken = Client.AuthenToken,
                RequestObject = request
            }
            );
            GlobalResponse response;
            if (TcpProtocol.Receive<GlobalResponse>(socket, out response)) {
                return ConverTo<LoginResponse>(response.RequestObject);
            }
            return null;
        }

        public static SignupResponse Signup(Socket socket, SignupRequest request)
        {
            //MessageBox.Show(request.Username + " | " + request.Password);
            TcpProtocol.Send<GlobalRequest>(socket, new GlobalRequest()
            {
                Route = "/signup",
                AuthentToken = Client.AuthenToken,
                RequestObject = request
            }
            );
            GlobalResponse response;
            if (TcpProtocol.Receive<GlobalResponse>(socket, out response))
            {
                return ConverTo<SignupResponse>(response.RequestObject);
            }
            return null;
        }

        private static T ConverTo<T>(object value)
        {
            string json = JsonSerializer.Serialize(value);
            Console.WriteLine(json);
            T request = JsonSerializer.Deserialize<T>(json);
            return request;
        }

        private static bool IsSocketConnected(Socket socket)
        {
            try
            {
                return !(socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0);
            }
            catch (SocketException)
            {
                return false;
            }
        }
    }
}
