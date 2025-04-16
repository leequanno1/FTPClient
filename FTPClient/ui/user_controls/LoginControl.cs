using dto.requests;
using dto.responses;
using FTPClient.ui.form;
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

namespace FTPClient.ui.user_controls
{
    public partial class LoginControl : UserControl
    {
        MainForm mainForm;

        public LoginControl(MainForm parent)
        {
            InitializeComponent();
            this.mainForm = parent;
        }

        private void LoginControl_Load(object sender, EventArgs e)
        {
            SetTextHint();

            this.BeginInvoke((MethodInvoker)delegate
            {
                label1.Focus();
            });
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Get data from form
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Validation form
            bool isValid = ValidateFormHelper.IsValidForm(lbErrorMessage, username, password);
            if (!isValid) return;

            // Create login reuqet object
            LoginRequest loginRequest = new LoginRequest();
            loginRequest.Username = username;
            loginRequest.Password = password;

            // Send reuquest to the server
            LoginResponse loginResponse = Controller.Login(Client.FileSocket, loginRequest);
            if (loginResponse.Token == String.Empty)
            {
                DialogHelper.ShowError("Wrong username or password!");
            }

            // Login successfully
            SetTextHint();
            DialogHelper.ShowSuccess($"Login successfully! \nToken: {loginResponse.Token}");
        }


        private void linkLbSignUp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.mainForm.LoadControl(new SignUpControl(mainForm));
        }

        // Method to clear form
        public void SetTextHint()
        {
            TextBoxHelper.SetHint(this.txtUsername, "Please enter username");
            TextBoxHelper.SetHint(this.txtPassword, "Please enter password", true);
        }
    }
}
