using FTPClient.utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using lib;
using dto.requests;
using dto.responses;
using dto;

namespace FTPClient.ui.form
{
    public partial class CreateNerFolderForm : Form
    {
        public Action OnFolderCreated;

        public String FolderName => txtFolderName.Text;

        private String currentPath;

        public bool IsFolderCreated { get; private set; } = false;

        public CreateNerFolderForm(string currentPath)
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;
            this.currentPath = currentPath;
        }

        private void CreateNerFolder_Load(object sender, EventArgs e)
        {
            TextBoxHelper.SetHint(this.txtFolderName, "Please enter folder name");

            BeginInvoke((MethodInvoker)delegate
            {
                label3.Focus();
            });
        }

        private void CreateNerFolderForm_Shown(object sender, EventArgs e)
        {
            ActiveControl = null;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // Create request
            FolderAddRequest request = new FolderAddRequest();
            request.ParrentPath = currentPath;
            request.FolderName = txtFolderName.Text;
            Client.AuthenToken = MySession.MyToken;

            // Call API to send request
            FolderAddResponse reponse = Controller.AddFolder(Client.ClientSocket, request);

            if (reponse != null)
            {
                OnFolderCreated?.Invoke();
                Close();
            }
            else
            {
                DialogHelper.ShowError("Error when create folder: " + reponse.Message.ToString());
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

        private void txtFolderName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
