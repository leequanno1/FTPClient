using dto.requests;
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
    public partial class SignUpControl : UserControl
    {
        private MainForm mainForm;

        public SignUpControl(MainForm parent)
        {
            InitializeComponent();

            // Indentitfy parent form, with purpose is render control into that.
            this.mainForm = parent;
        }

        private void SignUpControl_Load(object sender, EventArgs e)
        {
            // Set placeholder for form.
            SetTextHint();

            // Ensure form is loaded.
            this.BeginInvoke((MethodInvoker)delegate
            {
                label1.Focus();
            });
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            // Get data from form.
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string repassword = txtRepassword.Text.Trim();

            // Validation form.
            bool isValid = ValidateFormHelper.IsValidForm(lbErrorMessage, username, password, repassword);
            if (!isValid) return;

            // Create signup request.
            SignupRequest signupRequest = new SignupRequest();
            signupRequest.Username = username;
            signupRequest.Password = password;

            //MessageBox.Show(signupRequest.Username + " | " + signupRequest.Password);

            // Send request to the server.
            SignupResponse signupResponse = Controller.Signup(Client.ClientSocket, signupRequest);

            // If account already exists.
            if (signupResponse.Status == ResponseStatus.ACCOUNT_ALREADY_EXISTS)
            {
                DialogHelper.ShowError(ResponseStatus.ACCOUNT_ALREADY_EXISTS_MESSAGE);
                return;
            }

            // Signup succesfully.
            clearForm();
            DialogHelper.ShowSuccess(ResponseStatus.SUCCESS_MESSAGE, () =>
            {
                this.mainForm.LoadControl(new LoginControl(mainForm));
            });
        }


        // Event to redirect to Login Form
        private void linkLbLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.mainForm.LoadControl(new LoginControl(mainForm));
        }

        // Method to clear form
        public void clearForm()
        {
            this.txtUsername.Clear();
            this.txtPassword.Clear();
            this.txtRepassword.Clear();
        }

        // Method to set text hint
        public void SetTextHint()
        {
            TextBoxHelper.SetHint(this.txtUsername, "Please enter username");
            TextBoxHelper.SetHint(this.txtPassword, "Please enter password", true);
            TextBoxHelper.SetHint(this.txtRepassword, "Please enter re-password", true);
        }

    }
}
