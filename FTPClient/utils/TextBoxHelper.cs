using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FTPClient.utils
{
    public class TextBoxHelper
    {
        // Method to set placeholder for text box
        public static void SetHint(TextBox textBox, string hintText, bool isPassword = false,  Color? hintColor = null)
        {
            Color color = hintColor ?? Color.LightGray;

            textBox.Text = hintText;
            textBox.ForeColor = color;
            textBox.PasswordChar = '\0';

            if (isPassword)
            {
                textBox.UseSystemPasswordChar = false;
            }

            textBox.Enter += (s, e) =>
            {
                if (textBox.Text == hintText)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.FromArgb(70, 64, 64);
                    textBox.PasswordChar = isPassword ? '*' : '\0';
                }
            };

            textBox.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = hintText;
                    textBox.ForeColor = color;
                    textBox.PasswordChar = '\0';
                }
            };
        }

        // Method to prevent line break
        public static void preventLineBreak(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; 
            }
        }
    }
}
