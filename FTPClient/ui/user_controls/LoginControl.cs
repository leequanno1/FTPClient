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
            mainForm = parent;
        }

        private void LoginControl_Load(object sender, EventArgs e)
        {
            SetTextHint();

            BeginInvoke((MethodInvoker)delegate
            {
                label1.Focus();
            });
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            // Get data from form
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Validation form
            bool isValid = ValidateFormHelper.IsValidForm(lbErrorMessage, username, password);
            if (!isValid) return;

            // Disable the login button to prevent multiple clicks
            btnLogin.Enabled = false;
            btnLogin.ForeColor = Color.White;
            lbErrorMessage.Text = "Logging in...";
            await Task.Delay(50);

            try
            {
                // Run login process asynchronously
                var loginResponse = await Task.Run(() => Login(username, password));

                if (loginResponse == null || string.IsNullOrEmpty(loginResponse.Token))
                {
                    DialogHelper.ShowError("Wrong username or password!");
                }
                else
                {         
                    MySession.MyToken = loginResponse.Token;
                    mainForm.LoadControl(new DashboardControl(mainForm));
                }
            }
            catch (Exception ex)
            {
                DialogHelper.ShowError($"An error occurred: {ex.Message}");
                lbErrorMessage.Text = string.Empty;
            }
            finally
            {
                btnLogin.Enabled = true;
            }
        }

        // Method to handle the login process (this can be synchronous as it is called inside Task.Run)
        private LoginResponse Login(string username, string password)
        {
            // Create login request object
            LoginRequest loginRequest = new LoginRequest
            {
                Username = username,
                Password = password
            };

            // Send request to the server
            return Controller.Login(Client.ClientSocket, loginRequest);
        }


        private void linkLbSignUp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            mainForm.LoadControl(new SignUpControl(mainForm));
        }

        // Method to clear form
        public void SetTextHint()
        {
            TextBoxHelper.SetHint(txtUsername, "Please enter username");
            TextBoxHelper.SetHint(txtPassword, "Please enter password", true);
        }
    }
}
