namespace idt_diag
{
    partial class frm_clt
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_clt));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.taskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reginalServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateClientsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.restartClientsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeDBDuplicatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.retrieveCSVFromClientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveMWLogsFromClientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.retrieveRDImagesFromClientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkDBLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hQServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transferHitdataDBOnlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transferHitdataDBAndFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transferZipDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.purgeDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rollbackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usr_cont = new System.Windows.Forms.UserControl();
            this.notify_01 = new System.Windows.Forms.NotifyIcon(this.components);
            this.verifyHitdataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.reconnectToolStripMenuItem,
            this.taskToolStripMenuItem,
            this.updateToolStripMenuItem,
            this.rollbackToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(631, 24);
            this.menuStrip1.TabIndex = 0;
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // reconnectToolStripMenuItem
            // 
            this.reconnectToolStripMenuItem.Name = "reconnectToolStripMenuItem";
            this.reconnectToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.reconnectToolStripMenuItem.Text = "Reconnect";
            this.reconnectToolStripMenuItem.Click += new System.EventHandler(this.reconnectToolStripMenuItem_Click);
            // 
            // taskToolStripMenuItem
            // 
            this.taskToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reginalServerToolStripMenuItem,
            this.hQServerToolStripMenuItem});
            this.taskToolStripMenuItem.Name = "taskToolStripMenuItem";
            this.taskToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.taskToolStripMenuItem.Text = "Task";
            // 
            // reginalServerToolStripMenuItem
            // 
            this.reginalServerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateClientsToolStripMenuItem1,
            this.restartClientsToolStripMenuItem,
            this.removeDBDuplicatesToolStripMenuItem,
            this.retrieveCSVFromClientToolStripMenuItem,
            this.moveMWLogsFromClientToolStripMenuItem,
            this.retrieveRDImagesFromClientToolStripMenuItem,
            this.checkDBLocationToolStripMenuItem,
            this.verifyHitdataToolStripMenuItem});
            this.reginalServerToolStripMenuItem.Name = "reginalServerToolStripMenuItem";
            this.reginalServerToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.reginalServerToolStripMenuItem.Text = "Regional Server";
            // 
            // updateClientsToolStripMenuItem1
            // 
            this.updateClientsToolStripMenuItem1.Name = "updateClientsToolStripMenuItem1";
            this.updateClientsToolStripMenuItem1.Size = new System.Drawing.Size(234, 22);
            this.updateClientsToolStripMenuItem1.Text = "Update Clients";
            this.updateClientsToolStripMenuItem1.Click += new System.EventHandler(this.updateClientsToolStripMenuItem1_Click);
            // 
            // restartClientsToolStripMenuItem
            // 
            this.restartClientsToolStripMenuItem.Name = "restartClientsToolStripMenuItem";
            this.restartClientsToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.restartClientsToolStripMenuItem.Text = "Restart Clients";
            this.restartClientsToolStripMenuItem.Click += new System.EventHandler(this.restartClientsToolStripMenuItem_Click);
            // 
            // removeDBDuplicatesToolStripMenuItem
            // 
            this.removeDBDuplicatesToolStripMenuItem.Name = "removeDBDuplicatesToolStripMenuItem";
            this.removeDBDuplicatesToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.removeDBDuplicatesToolStripMenuItem.Text = "Remove DB Duplicates";
            this.removeDBDuplicatesToolStripMenuItem.Click += new System.EventHandler(this.removeDBDuplicatesToolStripMenuItem_Click);
            // 
            // retrieveCSVFromClientToolStripMenuItem
            // 
            this.retrieveCSVFromClientToolStripMenuItem.Name = "retrieveCSVFromClientToolStripMenuItem";
            this.retrieveCSVFromClientToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.retrieveCSVFromClientToolStripMenuItem.Text = "Retrieve CSV From Client";
            this.retrieveCSVFromClientToolStripMenuItem.Click += new System.EventHandler(this.retrieveCSVFromClientToolStripMenuItem_Click);
            // 
            // moveMWLogsFromClientToolStripMenuItem
            // 
            this.moveMWLogsFromClientToolStripMenuItem.Name = "moveMWLogsFromClientToolStripMenuItem";
            this.moveMWLogsFromClientToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.moveMWLogsFromClientToolStripMenuItem.Text = "Retrieve MW Logs From Client";
            this.moveMWLogsFromClientToolStripMenuItem.Click += new System.EventHandler(this.moveMWLogsFromClientToolStripMenuItem_Click);
            // 
            // retrieveRDImagesFromClientToolStripMenuItem
            // 
            this.retrieveRDImagesFromClientToolStripMenuItem.Name = "retrieveRDImagesFromClientToolStripMenuItem";
            this.retrieveRDImagesFromClientToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.retrieveRDImagesFromClientToolStripMenuItem.Text = "Retrieve RD From Client";
            this.retrieveRDImagesFromClientToolStripMenuItem.Click += new System.EventHandler(this.retrieveRDImagesFromClientToolStripMenuItem_Click);
            // 
            // checkDBLocationToolStripMenuItem
            // 
            this.checkDBLocationToolStripMenuItem.Name = "checkDBLocationToolStripMenuItem";
            this.checkDBLocationToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.checkDBLocationToolStripMenuItem.Text = "Check DB Location";
            this.checkDBLocationToolStripMenuItem.Click += new System.EventHandler(this.checkDBLocationToolStripMenuItem_Click);
            // 
            // hQServerToolStripMenuItem
            // 
            this.hQServerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.transferHitdataDBOnlyToolStripMenuItem,
            this.transferHitdataDBAndFileToolStripMenuItem,
            this.transferZipDataToolStripMenuItem,
            this.purgeDataToolStripMenuItem});
            this.hQServerToolStripMenuItem.Name = "hQServerToolStripMenuItem";
            this.hQServerToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.hQServerToolStripMenuItem.Text = "HQ Server";
            // 
            // transferHitdataDBOnlyToolStripMenuItem
            // 
            this.transferHitdataDBOnlyToolStripMenuItem.Name = "transferHitdataDBOnlyToolStripMenuItem";
            this.transferHitdataDBOnlyToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.transferHitdataDBOnlyToolStripMenuItem.Text = "Transfer Hitdata (DB Only)";
            this.transferHitdataDBOnlyToolStripMenuItem.Click += new System.EventHandler(this.transferHitdataDBOnlyToolStripMenuItem_Click);
            // 
            // transferHitdataDBAndFileToolStripMenuItem
            // 
            this.transferHitdataDBAndFileToolStripMenuItem.Name = "transferHitdataDBAndFileToolStripMenuItem";
            this.transferHitdataDBAndFileToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.transferHitdataDBAndFileToolStripMenuItem.Text = "Transfer Hitdata (DB and File)";
            this.transferHitdataDBAndFileToolStripMenuItem.Click += new System.EventHandler(this.transferHitdataDBAndFileToolStripMenuItem_Click);
            // 
            // transferZipDataToolStripMenuItem
            // 
            this.transferZipDataToolStripMenuItem.Name = "transferZipDataToolStripMenuItem";
            this.transferZipDataToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.transferZipDataToolStripMenuItem.Text = "Transfer Zip Data";
            this.transferZipDataToolStripMenuItem.Click += new System.EventHandler(this.transferZipDataToolStripMenuItem_Click);
            // 
            // purgeDataToolStripMenuItem
            // 
            this.purgeDataToolStripMenuItem.Name = "purgeDataToolStripMenuItem";
            this.purgeDataToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.purgeDataToolStripMenuItem.Text = "Purge Hitdata";
            this.purgeDataToolStripMenuItem.Click += new System.EventHandler(this.purgeDataToolStripMenuItem_Click);
            // 
            // updateToolStripMenuItem
            // 
            this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            this.updateToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.updateToolStripMenuItem.Text = "Update";
            this.updateToolStripMenuItem.Click += new System.EventHandler(this.updateToolStripMenuItem_Click);
            // 
            // rollbackToolStripMenuItem
            // 
            this.rollbackToolStripMenuItem.Name = "rollbackToolStripMenuItem";
            this.rollbackToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.rollbackToolStripMenuItem.Text = "Rollback";
            this.rollbackToolStripMenuItem.Click += new System.EventHandler(this.rollbackToolStripMenuItem_Click_1);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // usr_cont
            // 
            this.usr_cont.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.usr_cont.Location = new System.Drawing.Point(0, 27);
            this.usr_cont.Name = "usr_cont";
            this.usr_cont.Size = new System.Drawing.Size(631, 565);
            this.usr_cont.TabIndex = 4;
            // 
            // notify_01
            // 
            this.notify_01.Icon = ((System.Drawing.Icon)(resources.GetObject("notify_01.Icon")));
            this.notify_01.Text = "MW Manager";
            this.notify_01.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notify_01_MouseDoubleClick);
            // 
            // verifyHitdataToolStripMenuItem
            // 
            this.verifyHitdataToolStripMenuItem.Name = "verifyHitdataToolStripMenuItem";
            this.verifyHitdataToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.verifyHitdataToolStripMenuItem.Text = "Verify Hitdata";
            this.verifyHitdataToolStripMenuItem.Click += new System.EventHandler(this.verifyHitdataToolStripMenuItem_Click);
            // 
            // frm_clt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 591);
            this.Controls.Add(this.usr_cont);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "frm_clt";
            this.ShowInTaskbar = false;
            this.Text = "Form";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.frm_clt_Load);
            this.Resize += new System.EventHandler(this.Form_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.UserControl usr_cont;
        private System.Windows.Forms.ToolStripMenuItem taskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notify_01;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reconnectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rollbackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reginalServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hQServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem retrieveRDImagesFromClientToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transferHitdataDBOnlyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transferHitdataDBAndFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restartClientsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem retrieveCSVFromClientToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveMWLogsFromClientToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateClientsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem removeDBDuplicatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkDBLocationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transferZipDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem purgeDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verifyHitdataToolStripMenuItem;
    }
}

