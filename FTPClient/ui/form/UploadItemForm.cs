using dto.requests;
using dto.responses;
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
    public partial class UploadItemForm : Form
    {
        public Action OnUploadFile;

        private string currentPath;

        private string selectedFilePath;

        public UploadItemForm(string currentPath)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            this.currentPath = currentPath;
        }

        private void UploadItemForm_Load(object sender, EventArgs e)
        {
            TextBoxHelper.SetHint(this.txtItemName, "Please choose file");

            BeginInvoke((MethodInvoker)delegate
            {
                label3.Focus();
            });
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedFilePath) || !System.IO.File.Exists(selectedFilePath))
            {
                DialogHelper.ShowError("Please choose a valid file before uploading.");
                return;
            }

            string fileName = System.IO.Path.GetFileName(selectedFilePath);
            long fileSize = new System.IO.FileInfo(selectedFilePath).Length;

            FileAddRequest request = new FileAddRequest()
            {
                FileName = fileName,
                FolderPath = currentPath,
                IpEndPoint = ServerEndpoint.FileServer.ToString(),
                Size = 1024
            };
            Client.AuthenToken = MySession.MyToken;

            bool result = Controller.AddFile(Client.ClientSocket, request, selectedFilePath);
            if (result)
            {
                this.OnUploadFile?.Invoke();
                DialogHelper.ShowSuccess("Upload file successfully!", () => Close());
            }
            else
            {
                DialogHelper.ShowError("Failed to upload file.");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtItemName_KeyDown(object sender, KeyEventArgs e)
        {
            TextBoxHelper.preventLineBreak(e);
        }

        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "C:\\";
                openFileDialog.Filter = "All files (*.*)|*.*|Text files (*.txt)|*.txt|Image files (*.jpg;*.png)|*.jpg;*.png";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedFilePath = openFileDialog.FileName;

                    txtItemName.Text = System.IO.Path.GetFileName(selectedFilePath);
                    txtItemName.ForeColor = Color.FromArgb(70, 64, 64);
                }
            }
        }
    }
}
