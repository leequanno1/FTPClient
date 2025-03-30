using dto.requests;
using dto.responses;
using lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FTPClient
{
    public static class Controller
    {
        public static bool AddFile(Socket socket, FileAddRequest request, string realiticFilePath, Func<int, int> statusHandler = null)
        {
            // gửi request
            TcpProtocol.Send<GlobalRequest>(socket, new GlobalRequest() { 
                Route = "/file-add", 
                AuthentToken = Client.AuthenToken, 
                RequestObject = request }
            );
            // kết nối file server
            Client.ConnectFileSocket(ServerEndpoint.FileServer);
            // nhận response
            GlobalResponse glResponse;
            if (TcpProtocol.Receive<GlobalResponse>(socket, out glResponse)){
                FileAddResponse response =  glResponse.RequestObject as FileAddResponse;
                if(response.Status == ResponseStatus.READY)
                {
                    // bắt đầu gửi file
                    try
                    {
                        FileTranferHelper.SendFileTo(Client.FileSocket, realiticFilePath, statusHandler);
                        while (Client.FileSocket.Connected) { Thread.Sleep(100); }
                    } catch (Exception)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool DownloadFile(Socket socket, FileDownloadRequest request, string realicticSaveFolderPath, Func<int, int> statusHandler = null)
        {
            // gửi request
            TcpProtocol.Send<GlobalRequest>(socket, new GlobalRequest() { 
                Route = "/file-download", 
                AuthentToken = Client.AuthenToken, 
                RequestObject = request }
            );
            // kết nối file server
            Client.ConnectFileSocket(ServerEndpoint.FileServer);
            // nhận response
            GlobalResponse glResponse;
            if (TcpProtocol.Receive<GlobalResponse>(socket, out glResponse))
            {
                FileDowloadResponse response = glResponse.RequestObject as FileDowloadResponse;
                if (response.Status == ResponseStatus.READY)
                {
                    // bắt đầu nhận file file
                    try
                    {
                        string fileName = request.FilePath.Split('/').Last();
                        FileTranferHelper.ReceiveFileFrom(socket, realicticSaveFolderPath, fileName, statusHandler);
                        Client.FileSocket.Close();
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
            TcpProtocol.Send<GlobalRequest>(socket, new GlobalRequest() { 
                Route = "/file-delete", 
                AuthentToken = Client.AuthenToken, 
                RequestObject = request }
            );
            GlobalResponse response;
            if (TcpProtocol.Receive<GlobalResponse>(socket, out response)){
                return response.RequestObject as FileDeleteResponse;
            }
            return null;
        }

        public static FileUpdateResponse UpdateFile(Socket socket, FileUpdateRequest request)
        {
            TcpProtocol.Send<GlobalRequest>(socket, new GlobalRequest() { 
                Route = "/file-update", 
                AuthentToken = Client.AuthenToken, 
                RequestObject = request }
            );
            GlobalResponse response;
            if (TcpProtocol.Receive<GlobalResponse>(socket, out response)){
                return response.RequestObject as FileUpdateResponse;
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
                return response.RequestObject as FileMoveResponse;
            }
            return null;
        }

        public static FolderAddResponse AddFolder(Socket socket, FolderAddRequest request)
        {
            TcpProtocol.Send<GlobalRequest>(socket, new GlobalRequest() { 
                Route = "/folder-add", 
                AuthentToken = Client.AuthenToken, 
                RequestObject = request }
            );
            GlobalResponse response;
            if (TcpProtocol.Receive<GlobalResponse>(socket, out response)){
                return response.RequestObject as FolderAddResponse;
            }
            return null;
        }

        public static FolderDeleteResponse DeleteFolder(Socket socket, FolderDeleteRequest request)
        {
            TcpProtocol.Send<GlobalRequest>(socket, new GlobalRequest() { 
                Route = "/folder-delete", 
                AuthentToken = Client.AuthenToken, 
                RequestObject = request }
            );
            GlobalResponse response;
            if (TcpProtocol.Receive<GlobalResponse>(socket, out response)){
                return response.RequestObject as FolderDeleteResponse;
            }
            return null;
        }

        public static FolderUpdateResponse UpdateFolder(Socket socket, FolderUpdateRequest request)
        {
            TcpProtocol.Send<GlobalRequest>(socket, new GlobalRequest() { 
                Route = "/folder-update", 
                AuthentToken = Client.AuthenToken, 
                RequestObject = request }
            );
            GlobalResponse response;
            if (TcpProtocol.Receive<GlobalResponse>(socket, out response)){
                return response.RequestObject as FolderUpdateResponse;
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
                return response.RequestObject as FolderMoveResponse;
            }
            return null;
        }

        public static ListResponse ListDirectory(Socket socket, ListRequest request)
        {
            TcpProtocol.Send<GlobalRequest>(socket, new GlobalRequest() { 
                Route = "/list", 
                AuthentToken = Client.AuthenToken, 
                RequestObject = request }
            );
            GlobalResponse response;
            if (TcpProtocol.Receive<GlobalResponse>(socket, out response)){
                return response.RequestObject as ListResponse;
            }
            return null;
        }

        public static LoginResponse Login(Socket socket, LoginRequest request)
        {
            TcpProtocol.Send<GlobalRequest>(socket, new GlobalRequest() { 
                Route = "/login", 
                AuthentToken = Client.AuthenToken, 
                RequestObject = request }
            );
            GlobalResponse response;
            if (TcpProtocol.Receive<GlobalResponse>(socket, out response)){
                return response.RequestObject as LoginResponse;
            }
            return null;
        }

        public static SignupResponse Signup(Socket socket, SignupRequest request)
        {
            TcpProtocol.Send<GlobalRequest>(socket, new GlobalRequest() { 
                Route = "/signup", 
                AuthentToken = Client.AuthenToken, 
                RequestObject = request }
            );
            GlobalResponse response;
            if (TcpProtocol.Receive<GlobalResponse>(socket, out response)){
                return response.RequestObject as SignupResponse;
            }
            return null;
        }
    }
}
