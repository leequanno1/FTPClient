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
        public String FolderName => txtFolderName.Text;

        public CreateNerFolderForm()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void CreateNerFolder_Load(object sender, EventArgs e)
        {
            TextBoxHelper.SetHint(this.txtFolderName, "Please enter folder name");

            this.BeginInvoke((MethodInvoker)delegate
            {
                label3.Focus();
            });
        }

        private void CreateNerFolderForm_Shown(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.OK;

            // Create request
            FolderAddRequest request = new FolderAddRequest();
            request.ParrentPath = CompositeConstance.ROOT_FOLDER_NAME;
            request.FolderName = txtFolderName.Text;
            Client.AuthenToken = MySession.MyToken;

            // Call API to send request
            FolderAddResponse reponse = Controller.AddFolder(Client.ClientSocket, request);

            if (reponse != null) {
                MessageBox.Show("Res: " + reponse.Status + "Message: " + reponse.Message);
            }
            else
            {
                MessageBox.Show("Co loi");
            }

            //this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
