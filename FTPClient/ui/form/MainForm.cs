using FTPClient.ui.user_controls;
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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            LoadControl(new LoginControl(this));

            // Event when form resize
            panelMain.Resize += (s, e) =>
            {
                if (panelMain.Controls.Count > 0)
                {
                    var uc = panelMain.Controls[0];
                    uc.Location = new Point(
                        (panelMain.Width - uc.Width) / 2,
                        (panelMain.Height - uc.Height) / 2
                    );
                }
            };
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
           
        }

        private void CenterControlInPanel(Control control, Panel container)
        {
            control.Location = new Point(
                (container.Width - control.Width) / 2,
                (container.Height - control.Height) / 2
            );
        }


        public void LoadControl(UserControl control)
        {
            panelMain.Controls.Clear();
            panelMain.Controls.Add(control);

            control.Location = new Point(
                (panelMain.Width - control.Width) / 2,
                (panelMain.Height - control.Height) / 2
            );
        }

    }
}
