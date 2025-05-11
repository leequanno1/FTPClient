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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

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
            // Set placeholder for form.
            SetTextHint();

            // Fouus to label.
            BeginInvoke((MethodInvoker)delegate
            {
                label1.Focus();
            });

            // Check user use remember me feature.
            if (Properties.Settings.Default.RememberLogin)
            {
                // Set UX
                txtUsername.ForeColor = Color.FromArgb(70, 64, 64);
                txtPassword.ForeColor = Color.FromArgb(70, 64, 64);
                txtPassword.PasswordChar = '*';
                setAccountInfo();
            }
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            // Get data from form.
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Validation form.
            bool isValid = ValidateFormHelper.IsValidForm(lbErrorMessage, username, password);
            if (!isValid) return;

            // Disable the login button to prevent multiple clicks.
            btnLogin.Enabled = false;
            btnLogin.ForeColor = Color.White;
            //lbErrorMessage.Text = "Logging in...";
            //Application.DoEvents();
            //await Task.Delay(50);

            try
            {
                // Run login process asynchronously.
                var loginResponse = await Task.Run(() => Login(username, password));

                if (loginResponse == null || string.IsNullOrEmpty(loginResponse.Token))
                {
                    DialogHelper.ShowError("Wrong username or password!");
                }
                else
                {
                    MySession.MyToken = loginResponse.Token;
                    // Remember password or not.
                    rememberUser(username, password);
                    mainForm.LoadControl(new DashboardControl(mainForm), isFullScreen: true);
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

        // Method to remember user.
        private void rememberUser(string username, string password)
        {
            // If check box is checked.
            if (chkRememberMe.Checked)
            {
                Properties.Settings.Default.SavedUsername = username;
                Properties.Settings.Default.SavedPassword = password;
                Properties.Settings.Default.RememberLogin = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.SavedUsername = "";
                Properties.Settings.Default.SavedPassword = "";
                Properties.Settings.Default.RememberLogin = false;
                Properties.Settings.Default.Save();
            }
        }

        // Method to set username and password for textbox.
        private void setAccountInfo()
        {
            txtUsername.Text = Properties.Settings.Default.SavedUsername;
            txtPassword.Text = Properties.Settings.Default.SavedPassword;
            chkRememberMe.Checked = true;
        }

        // Method to handle the login process (this can be synchronous as it is called inside Task.Run).
        private LoginResponse Login(string username, string password)
        {
            // Create login request object.
            LoginRequest loginRequest = new LoginRequest
            {
                Username = username,
                Password = password
            };

            // Send request to the server.
            return Controller.Login(Client.ClientSocket, loginRequest);
        }


        private void linkLbSignUp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            mainForm.LoadControl(new SignUpControl(mainForm));
        }

        // Method to clear form.
        public void SetTextHint()
        {
            TextBoxHelper.SetHint(txtUsername, "Please enter username");
            TextBoxHelper.SetHint(txtPassword, "Please enter password", true);
        }

        // Prevent line break of username textbox.
        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            TextBoxHelper.preventLineBreak(e);
        }

        // Prevent line break of password textbox.
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            TextBoxHelper.preventLineBreak(e);
        }

        // Catch event enter "Enter" on keyborad on login control.
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                btnLogin.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
