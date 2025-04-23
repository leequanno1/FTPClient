namespace FTPClient.ui.user_controls
{
    partial class DashboardControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DashboardControl));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.createFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.uploadFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonLeft = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRight = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.txtCurrentPath = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripTextBox2 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.imgListIcons = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuListView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createFolderToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadFileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadFileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel3 = new System.Windows.Forms.Panel();
            this.treeViewFolder = new System.Windows.Forms.TreeView();
            this.listViewFolderFileTree = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.contextMenuListView.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.statusStrip1.Location = new System.Drawing.Point(0, 538);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1147, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "Đang tải dữ liệu .....";
            this.statusStrip1.Visible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripLabel1,
            this.toolStripButton2,
            this.toolStripLabel2,
            this.toolStripButton6});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.Size = new System.Drawing.Size(1147, 25);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.Visible = false;
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createFolderToolStripMenuItem,
            this.getAllToolStripMenuItem});
            this.toolStripDropDownButton1.Image = global::FTPClient.Properties.Resources.folder_3_512;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Margin = new System.Windows.Forms.Padding(4, 1, 0, 2);
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            // 
            // createFolderToolStripMenuItem
            // 
            this.createFolderToolStripMenuItem.Image = global::FTPClient.Properties.Resources.folder_3_512;
            this.createFolderToolStripMenuItem.Name = "createFolderToolStripMenuItem";
            this.createFolderToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.createFolderToolStripMenuItem.Text = "Create folder";
            // 
            // getAllToolStripMenuItem
            // 
            this.getAllToolStripMenuItem.Name = "getAllToolStripMenuItem";
            this.getAllToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.getAllToolStripMenuItem.Text = "Get All";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(40, 22);
            this.toolStripLabel1.Text = "Folder";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uploadFileToolStripMenuItem,
            this.downloadFileToolStripMenuItem});
            this.toolStripButton2.Image = global::FTPClient.Properties.Resources.blank_file_512;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Margin = new System.Windows.Forms.Padding(4, 1, 0, 2);
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(29, 22);
            this.toolStripButton2.Text = "toolStripButton2";
            // 
            // uploadFileToolStripMenuItem
            // 
            this.uploadFileToolStripMenuItem.Image = global::FTPClient.Properties.Resources.upload_3_512;
            this.uploadFileToolStripMenuItem.Name = "uploadFileToolStripMenuItem";
            this.uploadFileToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.uploadFileToolStripMenuItem.Text = "Upload file";
            // 
            // downloadFileToolStripMenuItem
            // 
            this.downloadFileToolStripMenuItem.Image = global::FTPClient.Properties.Resources.download_2_512;
            this.downloadFileToolStripMenuItem.Name = "downloadFileToolStripMenuItem";
            this.downloadFileToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.downloadFileToolStripMenuItem.Text = "Download file";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(25, 22);
            this.toolStripLabel2.Text = "File";
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton6.Text = "toolStripButton6";
            // 
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonLeft,
            this.toolStripButtonRight,
            this.toolStripSeparator1,
            this.txtCurrentPath,
            this.toolStripButton4,
            this.toolStripSeparator2,
            this.toolStripTextBox2,
            this.toolStripButton5});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(1147, 25);
            this.toolStrip2.TabIndex = 7;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButtonLeft
            // 
            this.toolStripButtonLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonLeft.Image = global::FTPClient.Properties.Resources.icons8_left_96;
            this.toolStripButtonLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLeft.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.toolStripButtonLeft.Name = "toolStripButtonLeft";
            this.toolStripButtonLeft.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonLeft.Text = "toolStripButton1";
            this.toolStripButtonLeft.Click += new System.EventHandler(this.toolStripButtonLeft_Click);
            // 
            // toolStripButtonRight
            // 
            this.toolStripButtonRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRight.Image = global::FTPClient.Properties.Resources.icons8_right_96;
            this.toolStripButtonRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRight.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.toolStripButtonRight.Name = "toolStripButtonRight";
            this.toolStripButtonRight.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonRight.Text = "toolStripButton3";
            this.toolStripButtonRight.Click += new System.EventHandler(this.toolStripButtonRight_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // txtCurrentPath
            // 
            this.txtCurrentPath.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtCurrentPath.Margin = new System.Windows.Forms.Padding(10, 0, 1, 0);
            this.txtCurrentPath.Name = "txtCurrentPath";
            this.txtCurrentPath.Size = new System.Drawing.Size(500, 25);
            this.txtCurrentPath.Text = "root";
            this.txtCurrentPath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCurrentPath_KeyDown);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = global::FTPClient.Properties.Resources.icons8_refresh_96;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "toolStripButton4";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripTextBox2
            // 
            this.toolStripTextBox2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBox2.Margin = new System.Windows.Forms.Padding(10, 0, 1, 0);
            this.toolStripTextBox2.Name = "toolStripTextBox2";
            this.toolStripTextBox2.Size = new System.Drawing.Size(300, 25);
            this.toolStripTextBox2.Text = "Search here ...";
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = global::FTPClient.Properties.Resources.search_12_5121;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton5.Text = "toolStripButton5";
            // 
            // imgListIcons
            // 
            this.imgListIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListIcons.ImageStream")));
            this.imgListIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imgListIcons.Images.SetKeyName(0, "icons8-folder-256.png");
            this.imgListIcons.Images.SetKeyName(1, "icons8-file-480.png");
            // 
            // contextMenuListView
            // 
            this.contextMenuListView.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.contextMenuListView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createFolderToolStripMenuItem1,
            this.renameToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.uploadFileToolStripMenuItem1,
            this.downloadFileToolStripMenuItem1,
            this.copyToolStripMenuItem});
            this.contextMenuListView.Name = "contextMenuListView";
            this.contextMenuListView.Size = new System.Drawing.Size(148, 136);
            // 
            // createFolderToolStripMenuItem1
            // 
            this.createFolderToolStripMenuItem1.Name = "createFolderToolStripMenuItem1";
            this.createFolderToolStripMenuItem1.Size = new System.Drawing.Size(147, 22);
            this.createFolderToolStripMenuItem1.Text = "Create Folder";
            this.createFolderToolStripMenuItem1.Click += new System.EventHandler(this.createFolderToolStripMenuItem1_Click);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // uploadFileToolStripMenuItem1
            // 
            this.uploadFileToolStripMenuItem1.Name = "uploadFileToolStripMenuItem1";
            this.uploadFileToolStripMenuItem1.Size = new System.Drawing.Size(147, 22);
            this.uploadFileToolStripMenuItem1.Text = "Upload file";
            this.uploadFileToolStripMenuItem1.Click += new System.EventHandler(this.uploadFileToolStripMenuItem1_Click);
            // 
            // downloadFileToolStripMenuItem1
            // 
            this.downloadFileToolStripMenuItem1.Name = "downloadFileToolStripMenuItem1";
            this.downloadFileToolStripMenuItem1.Size = new System.Drawing.Size(147, 22);
            this.downloadFileToolStripMenuItem1.Text = "Download file";
            this.downloadFileToolStripMenuItem1.Click += new System.EventHandler(this.downloadFileToolStripMenuItem1_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel3.Location = new System.Drawing.Point(294, 63);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(2, 551);
            this.panel3.TabIndex = 14;
            // 
            // treeViewFolder
            // 
            this.treeViewFolder.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.treeViewFolder.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeViewFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeViewFolder.Location = new System.Drawing.Point(3, 86);
            this.treeViewFolder.Name = "treeViewFolder";
            this.treeViewFolder.Size = new System.Drawing.Size(293, 528);
            this.treeViewFolder.TabIndex = 10;
            this.treeViewFolder.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewFolder_AfterSelect);
            // 
            // listViewFolderFileTree
            // 
            this.listViewFolderFileTree.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.listViewFolderFileTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewFolderFileTree.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewFolderFileTree.HideSelection = false;
            this.listViewFolderFileTree.Location = new System.Drawing.Point(331, 63);
            this.listViewFolderFileTree.Name = "listViewFolderFileTree";
            this.listViewFolderFileTree.Size = new System.Drawing.Size(816, 551);
            this.listViewFolderFileTree.TabIndex = 11;
            this.listViewFolderFileTree.UseCompatibleStateImageBehavior = false;
            this.listViewFolderFileTree.DoubleClick += new System.EventHandler(this.listViewFolderFileTree_DoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 16);
            this.label2.TabIndex = 12;
            this.label2.Text = "Tree folder";
            // 
            // DashboardControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.listViewFolderFileTree);
            this.Controls.Add(this.treeViewFolder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "DashboardControl";
            this.Size = new System.Drawing.Size(1147, 647);
            this.Load += new System.EventHandler(this.DashboardControl_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.contextMenuListView.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButtonLeft;
        private System.Windows.Forms.ToolStripButton toolStripButtonRight;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripTextBox txtCurrentPath;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox2;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripButton2;
        private System.Windows.Forms.ToolStripMenuItem uploadFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ImageList imgListIcons;
        private System.Windows.Forms.ToolStripMenuItem createFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ContextMenuStrip contextMenuListView;
        private System.Windows.Forms.ToolStripMenuItem createFolderToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uploadFileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem downloadFileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView listViewFolderFileTree;
        private System.Windows.Forms.TreeView treeViewFolder;
        private System.Windows.Forms.Panel panel3;
    }
}
