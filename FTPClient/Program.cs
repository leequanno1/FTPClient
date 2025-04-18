using lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;
using FTPClient.ui.form;

namespace FTPClient
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                Client.Connect(ServerEndpoint.MessageServer);

                // Will connect if login successfully
                //Client.Connect(ServerEndpoint.FileServer);
            }
            catch (SocketException ex)
            {
                MessageBox.Show("Can not connect to the server. Please try again!" + ex.ToString(), "Failed connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Application.Run(new MainForm());
        }
    }
}
