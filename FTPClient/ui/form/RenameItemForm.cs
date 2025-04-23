using dto;
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
    public partial class RenameItemForm : Form
    {
        public Action OnItemChangedName;

        public string FolderName => txtItemName.Text;

        private string itemPath;

        private string typeItem;

        public RenameItemForm(string itemPath, string typeItem)
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;
            this.itemPath = itemPath;
            this.typeItem = typeItem;
        }

        private void RenameItemForm_Load(object sender, EventArgs e)
        {
            if (typeItem.ToLower() == CompositeConstance.FOLDER)
            {
                TextBoxHelper.SetHint(this.txtItemName, "Please enter new folder name");
                this.Text = "Rename folder form";
                this.label3.Text = "Change name of folder";
            }
            else
            {
                TextBoxHelper.SetHint(this.txtItemName, "Please enter new file name");
                this.Text = "Rename folder form";
                this.label3.Text = "Change name of item";
            }   

            this.txtItemName.Text = PathHelper.GetLastPartOfPath(this.itemPath);
            this.txtItemName.ForeColor = Color.FromArgb(70, 64, 64);

            if (itemPath == "")
            {
                BeginInvoke((MethodInvoker)delegate
                {
                    label3.Focus();
                });
            }
        }

        private void RenameItemForm_Shown(object sender, EventArgs e)
        {
            ActiveControl = null;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            object response = null;
            // Create request
            Client.AuthenToken = MySession.MyToken;
            if (typeItem.ToLower() == CompositeConstance.FOLDER)
            {
                FolderUpdateRequest request = new FolderUpdateRequest()
                {
                    FolderPath = itemPath,
                    FolderName = txtItemName.Text,
                };

                response = Controller.UpdateFolder(Client.ClientSocket, request);
            }
            else
            {
                FileUpdateRequest request = new FileUpdateRequest()
                {
                    FilePath = itemPath,
                    FileName = txtItemName.Text
                };
                response = Controller.UpdateFile(Client.ClientSocket, request);
            }


            // Call API to send request
            if (response != null)
            {
                OnItemChangedName?.Invoke();
                Close();
            }
            else
            {
                DialogHelper.ShowError("Error when change name of item: " + (response as dynamic)?.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Catch event enter "Enter" on keyborad on login control.
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                btnOk.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void txtFolderName_KeyDown(object sender, KeyEventArgs e)
        {
            TextBoxHelper.preventLineBreak(e);
        }

    }
}
