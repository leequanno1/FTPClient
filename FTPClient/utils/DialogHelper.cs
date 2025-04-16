using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FTPClient.utils
{
    public class DialogHelper
    {
        public static void ShowSuccess(string message, Action onOk = null)
        {
            DialogResult result = MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                onOk?.Invoke(); 
            }
        }

        public static void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
