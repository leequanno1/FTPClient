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
using FTPClient.dto.requests;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FTPClient.ui.user_controls
{
    public partial class DashboardControl : UserControl
    {
        MainForm mainForm;

        private Stack<string> backStack = new Stack<string>();

        private Stack<string> forwardStack = new Stack<string>();

        private string currentPath = CompositeConstance.ROOT_FOLDER_NAME;

        private string clipboardPath = null;

        private string clipboardType = null; // file or folder

        private bool isCutOperation = false;

        private List<ListViewItem> originalList = new List<ListViewItem>();


        public DashboardControl(MainForm parent)
        {
            InitializeComponent();
            mainForm = parent;

            // Config TreeView
            treeViewFolder.ImageList = imgListIcons;
            LoadRootDirectoryOnTreeView();

            // Config ListView
            LoadListView();
        }

        private void DashboardControl_Load(object sender, EventArgs e) { 

        }

        private void LoadListView()
        {
            listViewFolderFileTree.View = View.Details;

            listViewFolderFileTree.Columns.Add("Name", 250);
            listViewFolderFileTree.Columns.Add("Type", 150);
            listViewFolderFileTree.Columns.Add("Date Modify", 150);

            listViewFolderFileTree.SmallImageList = imgListIcons;
            listViewFolderFileTree.ContextMenuStrip = contextMenuListView;

            LoadDirectoryOnListView("root");
        }

        // Method to load root directory
        private void LoadRootDirectoryOnTreeView()
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
                    folderNode.Nodes.Add(new TreeNode());
                    setImageForItem(folderNode, folder);
                    rootNode.Nodes.Add(folderNode);
                }

                treeViewFolder.ExpandAll();
            }
            else
            {
                MessageBox.Show("Could not load directory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method to hanle when user click to expand button
        private void treeViewFolder_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode node = e.Node;
            if (node.Nodes.Count == 1 && node.Nodes[0].Text == "" && node.Nodes[0].Tag == null)
            {
                node.Nodes.Clear();
                string path = node.Tag.ToString();

                ListRequest request = new ListRequest { FolderPath = path };
                Client.AuthenToken = MySession.MyToken;
                ListResponse response = Controller.ListDirectory(Client.ClientSocket, request);
                if (response != null)
                {
                    foreach (CompositeItemDTO folder in response.Folders)
                    {
                        TreeNode folderNode = new TreeNode(folder.ItemName) { Tag = folder.ItemPath };
                        setImageForItem(folderNode, folder);
                        folderNode.Nodes.Add(new TreeNode());
                        node.Nodes.Add(folderNode);
                    }
                }
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

        // Load directory base on path
        private void LoadDirectoryOnListView(string path)
        {
            ListRequest request = new ListRequest { FolderPath = path };
            Client.AuthenToken = MySession.MyToken;

            ListResponse response = Controller.ListDirectory(Client.ClientSocket, request);
            if (response != null)
            {
                listViewFolderFileTree.Items.Clear();
                originalList.Clear();

                foreach (CompositeItemDTO item in response.Folders)
                {
                    ListViewItem lvi = new ListViewItem(item.ItemName, 0);
                    lvi.SubItems.Add("Folder");
                    string dateFormated = DateTimeHelper.ConvertIsoDateToFormatted(item.DateModify.ToString());
                    lvi.SubItems.Add(dateFormated);
                    lvi.Tag = item.ItemPath;
                    listViewFolderFileTree.Items.Add(lvi);
                    originalList.Add((ListViewItem)lvi.Clone());
                }

                foreach (CompositeItemDTO item in response.Files)
                {
                    ListViewItem lvi = new ListViewItem(item.ItemName, 1);
                    lvi.SubItems.Add("File");
                    string dateFormated = DateTimeHelper.ConvertIsoDateToFormatted(item.DateModify.ToString());
                    lvi.SubItems.Add(dateFormated);
                    lvi.Tag = item.ItemPath;
                    listViewFolderFileTree.Items.Add(lvi);
                    originalList.Add((ListViewItem)lvi.Clone());
                }
            }

            UpdateItemCountStatus();
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
            toolStripSearchItem.Text = "";
            //Console.WriteLine($"[NavigateTo] current: {currentPath} -> new: {path}, addToHistory: {addToHistory}");
            if (path == currentPath) return;

            if (addToHistory)
            {
                backStack.Push(currentPath);
                forwardStack.Clear();
            }

            // Load to listview
            currentPath = path;
            LoadDirectoryOnListView(currentPath);

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

        // Handle when user click to the go to path button
        private void btnGoToPath_Click(object sender, EventArgs e)
        {
            string inputPath = txtCurrentPath.Text.Trim();
            if (!string.IsNullOrEmpty(inputPath))
            {
                NavigateTo(inputPath);
                SelectNodeInTreeView(inputPath);
            }
        }

        // Method to search item
        private void SearchItem()
        {
            string keyword = toolStripSearchItem.Text.Trim().ToLower();

            listViewFolderFileTree.Items.Clear();

            if (string.IsNullOrEmpty(keyword))
            {
                // Show all
                foreach (var item in originalList)
                {
                    listViewFolderFileTree.Items.Add((ListViewItem)item.Clone());
                }
            }
            else
            {
                // Find by name
                foreach (var item in originalList)
                {
                    if (item.Text.ToLower().Contains(keyword))
                    {
                        listViewFolderFileTree.Items.Add((ListViewItem)item.Clone());
                    }
                }
            }

            if (listViewFolderFileTree.Items.Count == 0)
            {
                listViewFolderFileTree.Items.Add(new ListViewItem("Không tìm thấy kết quả nào") { ForeColor = Color.Gray });
            }
        }

        // Handle when changed content of search item 
        private void toolStripSearchItem_TextChanged(object sender, EventArgs e)
        {
            SearchItem();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            SearchItem();
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
        // Reload list view and tree view
        private void ReloadListViewAndTreeView(string pathToReload)
        {
            Task.Run(() =>
            {
                try
                {
                    this.Invoke(new Action(() =>
                    {
                        LoadDirectoryOnListView(pathToReload);
                        LoadRootDirectoryOnTreeView();
                        NavigateTo(pathToReload);
                    }));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error loading directory: " + ex.Message);
                }
            });
        }

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
                    DialogHelper.ShowWarning("Please choose folder to create sub folder!");
                    return;
                }
            }

            // Open create new folder form
            CreateNerFolderForm createNerFolderForm = new CreateNerFolderForm(selectedPath);
            createNerFolderForm.OnFolderCreated = () =>
            {
                ReloadListViewAndTreeView(selectedPath);
            };

            createNerFolderForm.Show();
        }


        // Handle when user want to change name of folder or file.
        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewFolderFileTree.SelectedItems.Count == 0)
            {
                DialogHelper.ShowWarning("Please choose item to rename!");
                return;
            }

            var selectedItem = listViewFolderFileTree.SelectedItems[0];
            string selectedPath = selectedItem.Tag?.ToString();
            string itemType = selectedItem.SubItems[1].Text.ToLower();
            RenameItemForm renameItemForm = new RenameItemForm(selectedPath, itemType);

            renameItemForm.OnItemChangedName = () =>
            {
                ReloadListViewAndTreeView(currentPath);
            };

            renameItemForm.Show();
        }

        // Handle when user want to delete folder or file.
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewFolderFileTree.SelectedItems.Count == 0)
            {
                DialogHelper.ShowWarning("Please choose item to delete!");
                return;
            }

            var selectedItem = listViewFolderFileTree.SelectedItems[0];
            string selectedPath = selectedItem.Tag?.ToString();
            string itemType = selectedItem.SubItems[1].Text.ToLower();
            DeleteItemForm deleteItemForm = new DeleteItemForm(selectedPath, itemType);

            deleteItemForm.OnItemDeleted = () =>
            {
                ReloadListViewAndTreeView(currentPath);
            };

            deleteItemForm.Show();
        }

        // Handle when user click to upload file
        private void uploadFileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string selectedPath = currentPath;

            if (listViewFolderFileTree.SelectedItems.Count > 0)
            {
                var selectedItem = listViewFolderFileTree.SelectedItems[0];
                string itemType = selectedItem.SubItems[1].Text.ToLower();
                if (itemType == CompositeConstance.FILE)
                {
                    DialogHelper.ShowWarning("Please choose folder to update!");
                    return;
                }

                if (selectedItem != null)
                {
                    selectedPath = selectedItem.Tag?.ToString();
                }
            }

            UploadItemForm uploadItemForm = new UploadItemForm(selectedPath);
            uploadItemForm.OnUploadFile = () =>
            {
                //ReloadListViewAndTreeView(selectedPath);

                Task.Run(() =>
                {
                    try
                    {
                        this.Invoke(new Action(() =>
                        {
                            LoadDirectoryOnListView(selectedPath);
                            NavigateTo(selectedPath);
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
            if (listViewFolderFileTree.SelectedItems.Count == 0)
            {
                DialogHelper.ShowWarning("Please choose item to download!");
                return;
            }

            var selectedItem = listViewFolderFileTree.SelectedItems[0];
            string selectedPath = selectedItem.Tag?.ToString();
            DownloadFileForm downloadFileForm = new DownloadFileForm(selectedPath);
            downloadFileForm.Show();
        }

        // New features .....
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewFolderFileTree.SelectedItems.Count == 0)
            {
                DialogHelper.ShowWarning("Please choose item to copy!");
                return;
            }

            var selectedItem = listViewFolderFileTree.SelectedItems[0];

            clipboardPath = selectedItem.Tag?.ToString();
            clipboardType = selectedItem.SubItems[1].Text.ToLower();
            isCutOperation = false;
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewFolderFileTree.SelectedItems.Count == 0)
            {
                DialogHelper.ShowWarning("Please choose item to cut!");
                return;
            }

            var selectedItem = listViewFolderFileTree.SelectedItems[0];

            clipboardPath = selectedItem.Tag?.ToString();
            clipboardType = selectedItem.SubItems[1].Text.ToLower();
            isCutOperation = true;
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(clipboardPath) || string.IsNullOrEmpty(clipboardType))
            {
                DialogHelper.ShowWarning("Nothing to paste!");
                return;
            }

            string destinationDir = currentPath;
            object response = null;

            // If it is copy
            if (!isCutOperation)
            {
                if (clipboardType == "file")
                {
                    FileCopyRequest fileCopyRequest = new FileCopyRequest()
                    {
                        FilePath = clipboardPath,
                        FolderPath = destinationDir
                    };

                    response = Controller.CopyFile(Client.ClientSocket, fileCopyRequest);
                }
                else
                {
                    FolderCopyRequest folderCopyRequest = new FolderCopyRequest()
                    {
                        FolderPath = clipboardPath,
                        DestinationPath = destinationDir
                    };

                    response = Controller.CopyFolder(Client.ClientSocket, folderCopyRequest);
                }
            }
            else
            {
                MessageBox.Show("Type: " + clipboardType + "\nPath: " + clipboardPath + "\nDes: " + destinationDir);
                //return;
                if (clipboardType == "file")
                {
                    FileMoveRequest fileMoveRequest = new FileMoveRequest()
                    {
                        FilePath = clipboardPath,
                        FileNewPath = destinationDir
                    };

                    response = Controller.MoveFile(Client.ClientSocket, fileMoveRequest);
                }
                else
                {
                    FolderMoveRequest folderMoveRequest = new FolderMoveRequest()
                    {
                        FolderPath = clipboardPath,
                        FolderNewPath = destinationDir
                    };

                    response = Controller.MoveFolder(Client.ClientSocket, folderMoveRequest);
                }
            }

            if (response != null)
            {
                bool isError = (response as dynamic)?.Status == ResponseStatus.ERROR;
                string message = (response as dynamic)?.Message;

                if (!isError)
                {
                    LoadDirectoryOnListView(currentPath);
                    LoadRootDirectoryOnTreeView();
                    DialogHelper.ShowSuccess(message);
                } else
                {
                    DialogHelper.ShowError("Status: " + (response as dynamic)?.Status + message);
                }

                clipboardPath = null; 
                clipboardType = null;
                isCutOperation = false;
            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadDirectoryOnListView(currentPath);
            LoadRootDirectoryOnTreeView();
        }

        private void UpdateItemCountStatus()
        {
            int folderCount = 0;
            int fileCount = 0;

            foreach (ListViewItem item in listViewFolderFileTree.Items)
            {
                //if (item.ForeColor == Color.Gray) continue;
                if (item.SubItems[1].Text.ToLower() == "folder")
                    folderCount++;
                else
                    fileCount++;
            }

            //statusStripCountItems.Text = $"Folder: {folderCount} | File: {fileCount}";
            toolStripStatusLabelFollderCount.Text = $"Folder: {folderCount}";
            toolStripStatusLabelFileCount.Text = $"File: {fileCount}";
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MySession.MyToken = "";
            this.mainForm.LoadControl(new LoginControl(mainForm));
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}