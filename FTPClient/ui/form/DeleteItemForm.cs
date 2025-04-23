using dto.requests;
using dto;
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
    public partial class DeleteItemForm : Form
    {
        public Action OnItemDeleted;

        private string itemPath;

        private string itemType;

        public DeleteItemForm(string itemPath, string itemType)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            this.itemPath = itemPath;
            this.itemType = itemType;
        }

        private void DeleteItemForm_Load(object sender, EventArgs e)
        {
            if (itemType == "file")
            {
                this.Text = "Delete File Form";
                this.label3.Text = "Are you want to delete this file?";
            } else
            {
                this.Text = "Delete Folder Form";
                this.label3.Text = "Are you want to delete this folder?";
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
            object response = null;
            // Create request
            Client.AuthenToken = MySession.MyToken;
            if (itemType.ToLower() == CompositeConstance.FOLDER)
            {
                FolderDeleteRequest request = new FolderDeleteRequest()
                {
                    FolderPath = itemPath
                };

                response = Controller.DeleteFolder(Client.ClientSocket, request);
            }
            else
            {
                FileDeleteRequest request = new FileDeleteRequest()
                {
                    FilePath = itemPath
                };
                response = Controller.DeleteFile(Client.ClientSocket, request);
            }


            // Call API to send delete request
            if (response != null)
            {
                Console.WriteLine("Delete: " + (response as dynamic)?.Message);
                OnItemDeleted?.Invoke();
                Close();
            }
            else
            {
                DialogHelper.ShowError("Error when deleting item: " + (response as dynamic)?.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
