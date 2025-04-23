using FTPClient.ui.form;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using dto.requests;
using lib;
using dto.responses;
using dto.dbdto;
using dto;
using FTPClient.utils;
using System.Threading.Tasks;

namespace FTPClient.ui.user_controls
{
    public partial class DashboardControl : UserControl
    {
        MainForm mainForm;

        private Stack<string> backStack = new Stack<string>();

        private Stack<string> forwardStack = new Stack<string>();

        private string currentPath = CompositeConstance.ROOT_FOLDER_NAME;

        public DashboardControl(MainForm parent)
        {
            InitializeComponent();
            mainForm = parent;

            // Config TreeView
            treeViewFolder.ImageList = imgListIcons;

            // Config ListView
            listViewFolderFileTree.View = View.Details;

            listViewFolderFileTree.Columns.Add("Name", 250);
            listViewFolderFileTree.Columns.Add("Type", 150);
            listViewFolderFileTree.Columns.Add("Date Modify", 150);

            listViewFolderFileTree.SmallImageList = imgListIcons;
            listViewFolderFileTree.ContextMenuStrip = contextMenuListView;
            contextMenuListView.ShowImageMargin = false;

            LoadRootDirectory();
            LoadDirectory("root");
        }

        private void DashboardControl_Load(object sender, EventArgs e) { }

        // Method to load root directory
        private void LoadRootDirectory()
        {
            ListRequest request = new ListRequest
            {
                FolderPath = CompositeConstance.ROOT_FOLDER_NAME
            };
            Client.AuthenToken = MySession.MyToken;

            ListResponse response = Controller.ListDirectory(Client.ClientSocket, request);
            if (response != null)
            {
                List<CompositeItemDTO> folders = response.Folders;
                List<CompositeItemDTO> files = response.Files;

                treeViewFolder.Nodes.Clear();
                TreeNode rootNode = new TreeNode("root") { Tag = "root" };
                treeViewFolder.Nodes.Add(rootNode);

                foreach (CompositeItemDTO folder in folders)
                {
                    TreeNode folderNode = new TreeNode(folder.ItemName) { Tag = folder.ItemPath };
                    setImageForItem(folderNode, folder);
                    rootNode.Nodes.Add(folderNode);
                }

                foreach (CompositeItemDTO file in files)
                {
                    TreeNode fileNode = new TreeNode(file.ItemName) { Tag = file.ItemPath };
                    setImageForItem(fileNode, file);
                    rootNode.Nodes.Add(fileNode);
                }

                treeViewFolder.ExpandAll();
            }
            else
            {
                MessageBox.Show("Could not load directory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method to set image for folder and item.
        private void setImageForItem(TreeNode treeNode, CompositeItemDTO item)
        {
            if (item.ItemType == "folder")
            {
                treeNode.ImageIndex = 0;
                treeNode.SelectedImageIndex = 0;
            }
            else if (item.ItemType == "file")
            {
                treeNode.ImageIndex = 1;
                treeNode.SelectedImageIndex = 1;
            }
        }

        private void ReloadCurrentDirectory()
        {
            TreeNode selected = treeViewFolder.SelectedNode;
            if (selected != null)
            {
                string path = selected.Tag.ToString();
                LoadDirectory(path);
            }
            else
            {
                LoadRootDirectory();
            }
        }

        // Load directory base on path
        private void LoadDirectory(string path)
        {
            ListRequest request = new ListRequest { FolderPath = path };
            Client.AuthenToken = MySession.MyToken;

            ListResponse response = Controller.ListDirectory(Client.ClientSocket, request);
            if (response != null)
            {
                listViewFolderFileTree.Items.Clear();

                foreach (CompositeItemDTO item in response.Folders)
                {
                    ListViewItem lvi = new ListViewItem(item.ItemName, 0);
                    lvi.SubItems.Add("Folder");
                    string dateFormated = DateTimeHelper.ConvertIsoDateToFormatted(item.DateModify.ToString());
                    lvi.SubItems.Add(dateFormated);
                    lvi.Tag = item.ItemPath;
                    listViewFolderFileTree.Items.Add(lvi);
                }

                foreach (CompositeItemDTO item in response.Files)
                {
                    ListViewItem lvi = new ListViewItem(item.ItemName, 1);
                    lvi.SubItems.Add("File");
                    string dateFormated = DateTimeHelper.ConvertIsoDateToFormatted(item.DateModify.ToString());
                    lvi.SubItems.Add(dateFormated);
                    lvi.Tag = item.ItemPath;
                    listViewFolderFileTree.Items.Add(lvi);
                }
            }
        }

        // Handle when user click to tree node
        private void treeViewFolder_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string selectedPath = e.Node.Tag?.ToString();
            if (!string.IsNullOrEmpty(selectedPath))
            {
                NavigateTo(selectedPath);
            }
        }

        // Method to navigate to path
        private void NavigateTo(string path, bool addToHistory = true)
        {
            if (addToHistory)
            {
                backStack.Push(currentPath);
                forwardStack.Clear();
            }


            // Load to listview
            currentPath = path;
            LoadDirectory(currentPath);

            // Update textbox path
            txtCurrentPath.Text = currentPath;
        }

        // Handle when user click to back
        private void toolStripButtonLeft_Click(object sender, EventArgs e)
        {
            if (backStack.Count > 0)
            {
                forwardStack.Push(currentPath);
                string previousPath = backStack.Pop();
                NavigateTo(previousPath, addToHistory: false);
            }
        }


        // Handle when user click to next
        private void toolStripButtonRight_Click(object sender, EventArgs e)
        {
            if (forwardStack.Count > 0)
            {
                backStack.Push(currentPath);
                string nextPath = forwardStack.Pop();
                NavigateTo(nextPath, addToHistory: false);
            }
        }

        // Update textbox path
        private void txtCurrentPath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string inputPath = txtCurrentPath.Text.Trim();
                if (!string.IsNullOrEmpty(inputPath))
                {
                    NavigateTo(inputPath);
                    SelectNodeInTreeView(inputPath);
                }
            }
        }

        // Method to select node in tree view
        private void SelectNodeInTreeView(string path)
        {
            foreach (TreeNode node in treeViewFolder.Nodes)
            {
                TreeNode found = FindNodeByPath(node, path);
                if (found != null)
                {
                    treeViewFolder.SelectedNode = found;
                    found.EnsureVisible();
                    break;
                }
            }
        }

        // Method to find node by path
        private TreeNode FindNodeByPath(TreeNode node, string targetPath)
        {
            if (node.Tag != null && node.Tag.ToString() == targetPath)
            {
                return node;
            }

            foreach (TreeNode child in node.Nodes)
            {
                TreeNode found = FindNodeByPath(child, targetPath);
                if (found != null)
                    return found;
            }

            return null;
        }

        // Hanle when user double click to item on list view
        private void listViewFolderFileTree_DoubleClick(object sender, EventArgs e)
        {
            if (listViewFolderFileTree.SelectedItems.Count > 0)
            {
                var item = listViewFolderFileTree.SelectedItems[0];
                string path = item.Tag?.ToString();
                if (path != null && item.SubItems[1].Text == "Folder")
                {
                    NavigateTo(path);
                    SelectNodeInTreeView(path);
                }
            }
        }

        // ========================= Context Menu =========================
        // Handle when user click to the create new folder.
        private void createFolderToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string selectedPath = currentPath; 

            // If have item is selected
            if (listViewFolderFileTree.SelectedItems.Count > 0)
            {
                var selectedItem = listViewFolderFileTree.SelectedItems[0];

                // If item is folder then update selectPath
                if (selectedItem.SubItems[1].Text == "Folder")
                {
                    selectedPath = selectedItem.Tag?.ToString();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn thư mục để tạo thư mục con.");
                    return;
                }
            }

            // Open create new folder form
            CreateNerFolderForm createNerFolderForm = new CreateNerFolderForm(selectedPath);
            createNerFolderForm.OnFolderCreated = () =>
            {
                Task.Run(() =>
                {
                    try
                    {
                        this.Invoke(new Action(() =>
                        {
                            LoadDirectory(selectedPath);
                        }));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error loading directory: " + ex.Message);
                    }
                });
            };

            createNerFolderForm.Show();
        }


        // Handle when user want to change name of folder or file.
        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedItem = listViewFolderFileTree.SelectedItems[0];
            string selectedPath = selectedItem.Tag?.ToString();

            string itemType = selectedItem.SubItems[1].Text.ToLower();
            Console.WriteLine("Item type: " + itemType);
            Console.WriteLine("Item path: " + selectedPath);
            RenameItemForm renameItemForm = new RenameItemForm(selectedPath, itemType);

            renameItemForm.OnItemChangedName = () =>
            {
                Task.Run(() =>
                {
                    try
                    {
                        this.Invoke(new Action(() =>
                        {
                            LoadDirectory(currentPath);
                        }));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error loading directory: " + ex.Message);
                    }
                });
            };

            renameItemForm.Show();
        }

        // Handle when user want to delete folder or file.
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedItem = listViewFolderFileTree.SelectedItems[0];
            string selectedPath = selectedItem.Tag?.ToString();

            string itemType = selectedItem.SubItems[1].Text.ToLower();
            Console.WriteLine("Item type: " + itemType);
            Console.WriteLine("Item path: " + selectedPath);
            DeleteItemForm deleteItemForm = new DeleteItemForm(selectedPath, itemType);

            deleteItemForm.OnItemDeleted = () =>
            {
                Task.Run(() =>
                {
                    try
                    {
                        this.Invoke(new Action(() =>
                        {
                            LoadDirectory(currentPath);
                        }));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error loading directory: " + ex.Message);
                    }
                });
            };

            deleteItemForm.Show();
        }

        // Handle when user click to upload file
        private void uploadFileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            UploadItemForm uploadItemForm = new UploadItemForm(currentPath);
            uploadItemForm.OnUploadFile = () =>
            {
                Task.Run(() =>
                {
                    try
                    {
                        this.Invoke(new Action(() =>
                        {
                            Console.WriteLine("Current path: " + currentPath);
                            LoadDirectory(currentPath);
                        }));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error loading directory: " + ex.Message);
                    }
                });
            };

            uploadItemForm.Show();
        }

        // Handle when user click to download file
        private void downloadFileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var selectedItem = listViewFolderFileTree.SelectedItems[0];
            string selectedPath = selectedItem.Tag?.ToString();
            DownloadFileForm downloadFileForm = new DownloadFileForm(selectedPath);
            downloadFileForm.OnDownloadedFile = () =>
            {
                Task.Run(() =>
                {
                    try
                    {
                        this.Invoke(new Action(() =>
                        {
                            Console.WriteLine("Current path: " + currentPath);
                            LoadDirectory(currentPath);
                        }));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error loading directory: " + ex.Message);
                    }
                });
            };
            downloadFileForm.Show();
        }
    }
}
