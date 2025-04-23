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
        private string itemPath;

        private string selectedSavePath = "";

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
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select a folder to save the downloaded file";
                dialog.ShowNewFolderButton = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtItemName.Text = dialog.SelectedPath; 
                    txtItemName.ForeColor = Color.FromArgb(70, 64, 64);
                    
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                btnOk.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            FileDownloadRequest fileDownloadRequest = new FileDownloadRequest() {
                FilePath = itemPath,
                ClientEndpoint = ServerEndpoint.FileServer.ToString(),
            };

            selectedSavePath = txtItemName.Text;
            bool result = Controller.DownloadFile(Client.ClientSocket, fileDownloadRequest, selectedSavePath);

            if (result)
            {
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
