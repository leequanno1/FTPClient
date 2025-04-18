using FTPClient.ui.form;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FTPClient.ui.form;
using dto.requests;
using lib;
using dto.responses;
using dto.dbdto;

namespace FTPClient.ui.user_controls
{
    public partial class DashboardControl : UserControl
    {
        MainForm mainForm;

        public DashboardControl(MainForm parent)
        {
            InitializeComponent();
            this.mainForm = parent;
            treeViewFolder.ImageList = imgListIcons;
            listView1.View = View.Details;

            // Tạo cột để hiển thị thông tin
            listView1.Columns.Add("Tên", 200);
            listView1.Columns.Add("Loại", 100);
            listView1.Columns.Add("Kích thước", 150);
            // Gán ImageList vào ListView
            listView1.SmallImageList = imgListIcons;

            LoadRootDirectory("D:\\");
        }

        private void LoadRootDirectory(string path)
        {
            TreeNode root = new TreeNode(path);
            root.Tag = path;
            root.ImageIndex = 0; // folder icon
            root.SelectedImageIndex = 0;
            root.Nodes.Add("Loading..."); // Placeholder
            treeViewFolder.Nodes.Add(root);
        }


        private void DashboardControl_Load(object sender, EventArgs e)
        {

        }

        private void treeViewFolder_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode node = e.Node;
            if (node.Nodes[0].Text == "Loading...")
            {
                node.Nodes.Clear(); // Xóa placeholder
                LoadSubDirectories(node);
            }
        }

        private void LoadSubDirectories(TreeNode parentNode)
        {
            string path = parentNode.Tag.ToString();
            try
            {
                string[] dirs = Directory.GetDirectories(path);
                foreach (string dir in dirs)
                {
                    TreeNode node = new TreeNode(Path.GetFileName(dir));
                    node.Tag = dir;
                    node.ImageIndex = 0; // Folder icon
                    node.SelectedImageIndex = 0;
                    node.Nodes.Add("Loading...");
                    parentNode.Nodes.Add(node);
                }

                string[] files = Directory.GetFiles(path);
                foreach (string file in files)
                {
                    TreeNode fileNode = new TreeNode(Path.GetFileName(file));
                    fileNode.Tag = file;
                    fileNode.ImageIndex = 1; // File icon
                    fileNode.SelectedImageIndex = 1;
                    parentNode.Nodes.Add(fileNode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
            }
        }


        private void treeViewFolder_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string selectedPath = e.Node.Tag.ToString();
            LoadFilesAndDirectories(selectedPath);
        }

        private void LoadFilesAndDirectories(string path)
        {
            listView1.Items.Clear();  // Xóa các mục cũ

            try
            {
                // Load thư mục con
                string[] dirs = Directory.GetDirectories(path);
                foreach (string dir in dirs)
                {
                    ListViewItem item = new ListViewItem(Path.GetFileName(dir));
                    item.SubItems.Add("Thư mục");
                    item.SubItems.Add("");  // Kích thước thư mục (có thể bỏ qua)
                    item.ImageIndex = 0;  // Icon thư mục
                    item.Tag = dir; // Lưu đường dẫn của thư mục vào Tag
                    listView1.Items.Add(item);
                }

                // Load file
                string[] files = Directory.GetFiles(path);
                foreach (string file in files)
                {
                    ListViewItem item = new ListViewItem(Path.GetFileName(file));
                    item.SubItems.Add("File");
                    item.SubItems.Add(new FileInfo(file).Length.ToString());  // Kích thước file
                    item.ImageIndex = 1;  // Icon file
                    item.Tag = file; // Lưu đường dẫn của file vào Tag
                    listView1.Items.Add(item);
                }

                // Cập nhật StatusStrip
                statusStrip1.Items.Clear();
                statusStrip1.Items.Add($"Thư mục: {dirs.Length} | File: {files.Length}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }


        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Lấy item được chọn
            ListViewItem selectedItem = listView1.SelectedItems[0];

            // Nếu đó là thư mục, tải thư mục con
            if (selectedItem.SubItems[1].Text == "Thư mục")
            {
                string path = Path.Combine("D:\\", selectedItem.Text); // Hoặc lấy path từ Tag của item
                LoadFilesAndDirectories(path);  // Tải các thư mục và file trong thư mục con
            }
        }

        private void createFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNerFolderForm form = new CreateNerFolderForm();
            form.ShowDialog();

            //if (form.ShowDialog() == DialogResult.OK)
            //{
            //    string newFolderName = form.FolderName;
            //    MessageBox.Show("Folder name is: " + newFolderName);
            //}
        }

        private void getAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListRequest request = new ListRequest();
            request.FolderPath = "root";
            Client.AuthenToken = MySession.MyToken;

            ListResponse response = Controller.ListDirectory(Client.ClientSocket, request);
            if (response != null)
            {
                List<CompositeItemDTO> folders = response.Folders;
                List<CompositeItemDTO> files = response.Files;

                //if (folders != null)
                //{
                //    MessageBox.Show("Folder: " + folders.Count);
                //}

                //if (folders == null)
                //{
                //    MessageBox.Show("Folder is null");
                //}

                MessageBox.Show("response is " + (response == null ? "null" : "not null"));
                MessageBox.Show("response.Folders is " + (response.Folders == null ? "null" : "not null"));


                //MessageBox.Show("Folder: " + folders.Count + "Files: " + files.Count);

                //StringBuilder sb = new StringBuilder();
                //sb.AppendLine("📁 Folders:");
                //foreach (var folder in folders)
                //{
                //    sb.AppendLine(" - " + folder.ItemName);
                //}

                //sb.AppendLine();
                //sb.AppendLine("📄 Files:");
                //foreach (var file in files)
                //{
                //    sb.AppendLine(" - " + file.ItemName);
                //}

                //MessageBox.Show(sb.ToString(), "Danh sách thư mục và tập tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Không thể lấy danh sách thư mục.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
