namespace idt_diag
{
    partial class svr_uc
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
            this.dgv_svr = new System.Windows.Forms.DataGridView();
            this.cbx_chk = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.col_pc_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_ip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_mw = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_client = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_count = new System.Windows.Forms.Label();
            this.lbl_status = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_date = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabpage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txt_log = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_svr)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabpage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_svr
            // 
            this.dgv_svr.AllowUserToAddRows = false;
            this.dgv_svr.AllowUserToDeleteRows = false;
            this.dgv_svr.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_svr.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_svr.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_svr.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cbx_chk,
            this.col_pc_name,
            this.col_ip,
            this.col_mw,
            this.col_status,
            this.col_client,
            this.col_date});
            this.dgv_svr.Location = new System.Drawing.Point(3, 6);
            this.dgv_svr.Name = "dgv_svr";
            this.dgv_svr.RowHeadersVisible = false;
            this.dgv_svr.Size = new System.Drawing.Size(603, 505);
            this.dgv_svr.TabIndex = 0;
            this.dgv_svr.TabStop = false;
            this.dgv_svr.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_svr_CellContentClick);
            // 
            // cbx_chk
            // 
            this.cbx_chk.FillWeight = 30.45685F;
            this.cbx_chk.HeaderText = "";
            this.cbx_chk.Name = "cbx_chk";
            // 
            // col_pc_name
            // 
            this.col_pc_name.FillWeight = 113.9086F;
            this.col_pc_name.HeaderText = "PC Name";
            this.col_pc_name.Name = "col_pc_name";
            this.col_pc_name.ReadOnly = true;
            // 
            // col_ip
            // 
            this.col_ip.FillWeight = 113.9086F;
            this.col_ip.HeaderText = "IP Address";
            this.col_ip.Name = "col_ip";
            // 
            // col_mw
            // 
            this.col_mw.FillWeight = 113.9086F;
            this.col_mw.HeaderText = "MW Version";
            this.col_mw.Name = "col_mw";
            this.col_mw.ReadOnly = true;
            // 
            // col_status
            // 
            this.col_status.FillWeight = 113.9086F;
            this.col_status.HeaderText = "Status";
            this.col_status.Name = "col_status";
            this.col_status.ReadOnly = true;
            // 
            // col_client
            // 
            this.col_client.HeaderText = "Client No";
            this.col_client.Name = "col_client";
            this.col_client.ReadOnly = true;
            this.col_client.Visible = false;
            // 
            // col_date
            // 
            this.col_date.FillWeight = 113.9086F;
            this.col_date.HeaderText = "Date/Time Connected";
            this.col_date.Name = "col_date";
            this.col_date.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 514);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Clients Connected:";
            // 
            // lbl_count
            // 
            this.lbl_count.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_count.AutoSize = true;
            this.lbl_count.Location = new System.Drawing.Point(108, 514);
            this.lbl_count.Name = "lbl_count";
            this.lbl_count.Size = new System.Drawing.Size(13, 13);
            this.lbl_count.TabIndex = 4;
            this.lbl_count.Text = "0";
            // 
            // lbl_status
            // 
            this.lbl_status.AutoSize = true;
            this.lbl_status.Location = new System.Drawing.Point(441, 514);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(0, 13);
            this.lbl_status.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(155, 514);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Last Transfer:";
            // 
            // lbl_date
            // 
            this.lbl_date.AutoSize = true;
            this.lbl_date.Location = new System.Drawing.Point(233, 514);
            this.lbl_date.Name = "lbl_date";
            this.lbl_date.Size = new System.Drawing.Size(0, 13);
            this.lbl_date.TabIndex = 7;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabpage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(6, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(621, 556);
            this.tabControl1.TabIndex = 8;
            // 
            // tabpage1
            // 
            this.tabpage1.Controls.Add(this.dgv_svr);
            this.tabpage1.Controls.Add(this.lbl_date);
            this.tabpage1.Controls.Add(this.label1);
            this.tabpage1.Controls.Add(this.lbl_count);
            this.tabpage1.Controls.Add(this.lbl_status);
            this.tabpage1.Controls.Add(this.label2);
            this.tabpage1.Location = new System.Drawing.Point(4, 22);
            this.tabpage1.Name = "tabpage1";
            this.tabpage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabpage1.Size = new System.Drawing.Size(613, 530);
            this.tabpage1.TabIndex = 0;
            this.tabpage1.Text = "Main";
            this.tabpage1.UseVisualStyleBackColor = true;
            this.tabpage1.Click += new System.EventHandler(this.tabpage1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txt_log);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(613, 530);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Logs";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txt_log
            // 
            this.txt_log.Location = new System.Drawing.Point(6, 6);
            this.txt_log.Multiline = true;
            this.txt_log.Name = "txt_log";
            this.txt_log.ReadOnly = true;
            this.txt_log.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_log.Size = new System.Drawing.Size(601, 518);
            this.txt_log.TabIndex = 0;
            // 
            // svr_uc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.tabControl1);
            this.Name = "svr_uc";
            this.Size = new System.Drawing.Size(630, 712);
            this.Load += new System.EventHandler(this.svr_uc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_svr)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabpage1.ResumeLayout(false);
            this.tabpage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_svr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_count;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cbx_chk;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_pc_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_ip;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_mw;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_status;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_client;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_date;
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_date;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabpage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txt_log;
    }
}
