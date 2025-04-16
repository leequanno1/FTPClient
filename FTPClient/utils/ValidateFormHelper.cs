using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FTPClient.utils
{
    public class ValidateFormHelper
    {

        // Method to validate form
        public static bool IsValidForm(Label lbErrorMessage, string username, string password, string repassword = null)
        {
            if (username == "" || password == "" || username == "Please enter username" || password == "Please enter password")
            {
                ShowErrorMessage(lbErrorMessage, "Please enter complete information!");
                return false;
            }

            // If that is sign up form
            if (repassword != null)
            {
                if (repassword == "" || repassword == "Please enter re-password")
                {
                    ShowErrorMessage(lbErrorMessage, "Please enter complete information!");
                    return false;
                }

                if (password != repassword)
                {
                    ShowErrorMessage(lbErrorMessage, "Password does not match!");
                    return false;
                }
            }

            lbErrorMessage.Visible = false;
            return true;
        }

        // Method to show error message
        private static void ShowErrorMessage(Label lbErrorMessage, string message)
        {
            lbErrorMessage.Text = message;
            lbErrorMessage.Visible = true;
        }
    }
}
