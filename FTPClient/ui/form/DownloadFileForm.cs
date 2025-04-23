using dto.requests;
using FTPClient.utils;
using lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FTPClient.ui.form
{
    public partial class DownloadFileForm : Form
    {
        public Action OnDownloadedFile;

        private string itemPath;

        public DownloadFileForm(string itemPath)
        {
            InitializeComponent();
            this.itemPath = itemPath;
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void DownloadFileForm_Load(object sender, EventArgs e)
        {
            TextBoxHelper.SetHint(this.txtItemName, "Please choose location to save file");

            BeginInvoke((MethodInvoker)delegate
            {
                label3.Focus();
            });
        }

        private void btnChooseFile_Click(object sender, EventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            FileDownloadRequest fileDownloadRequest = new FileDownloadRequest() {
                FilePath = itemPath,
                ClientEndpoint = ServerEndpoint.FileServer.ToString(),
            };

            bool result = Controller.DownloadFile(Client.ClientSocket, fileDownloadRequest, "C:\\Users\\Liliana\\Downloads\\");

            if (result)
            {
                this.OnDownloadedFile?.Invoke();
                DialogHelper.ShowSuccess("Download file successfully!", () => Close());
            }
            else
            {
                DialogHelper.ShowError("Failed to download file.");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
