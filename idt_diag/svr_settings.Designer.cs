namespace mw_mgr
{
    partial class svr_settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(svr_settings));
            this.cbx_transfer = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_transTime = new System.Windows.Forms.TextBox();
            this.cbx_restart = new System.Windows.Forms.CheckBox();
            this.txt_reTime = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_pass = new System.Windows.Forms.TextBox();
            this.txt_user = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_save = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chk_hit = new System.Windows.Forms.CheckBox();
            this.rd_DB = new System.Windows.Forms.RadioButton();
            this.rdDBFile = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbx_transfer
            // 
            this.cbx_transfer.AutoSize = true;
            this.cbx_transfer.Location = new System.Drawing.Point(12, 12);
            this.cbx_transfer.Name = "cbx_transfer";
            this.cbx_transfer.Size = new System.Drawing.Size(110, 17);
            this.cbx_transfer.TabIndex = 0;
            this.cbx_transfer.Text = "Transfer RD/CSV";
            this.cbx_transfer.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Transfer Time (hh:mm tt)";
            // 
            // txt_transTime
            // 
            this.txt_transTime.Location = new System.Drawing.Point(12, 52);
            this.txt_transTime.Name = "txt_transTime";
            this.txt_transTime.Size = new System.Drawing.Size(210, 20);
            this.txt_transTime.TabIndex = 2;
            // 
            // cbx_restart
            // 
            this.cbx_restart.AutoSize = true;
            this.cbx_restart.Location = new System.Drawing.Point(12, 78);
            this.cbx_restart.Name = "cbx_restart";
            this.cbx_restart.Size = new System.Drawing.Size(94, 17);
            this.cbx_restart.TabIndex = 3;
            this.cbx_restart.Text = "Restart Clients";
            this.cbx_restart.UseVisualStyleBackColor = true;
            // 
            // txt_reTime
            // 
            this.txt_reTime.Location = new System.Drawing.Point(12, 118);
            this.txt_reTime.Name = "txt_reTime";
            this.txt_reTime.Size = new System.Drawing.Size(210, 20);
            this.txt_reTime.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Restart Time (hh:mm tt)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_pass);
            this.groupBox1.Controls.Add(this.txt_user);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(470, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(264, 147);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Database";
            // 
            // txt_pass
            // 
            this.txt_pass.Location = new System.Drawing.Point(6, 97);
            this.txt_pass.Name = "txt_pass";
            this.txt_pass.PasswordChar = '*';
            this.txt_pass.Size = new System.Drawing.Size(229, 20);
            this.txt_pass.TabIndex = 7;
            // 
            // txt_user
            // 
            this.txt_user.Location = new System.Drawing.Point(6, 47);
            this.txt_user.Name = "txt_user";
            this.txt_user.Size = new System.Drawing.Size(229, 20);
            this.txt_user.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Username";
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(659, 165);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 23);
            this.btn_save.TabIndex = 7;
            this.btn_save.Text = "Save";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.rdDBFile);
            this.groupBox2.Controls.Add(this.rd_DB);
            this.groupBox2.Controls.Add(this.chk_hit);
            this.groupBox2.Location = new System.Drawing.Point(228, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(236, 147);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Hitdata";
            // 
            // chk_hit
            // 
            this.chk_hit.AutoSize = true;
            this.chk_hit.Location = new System.Drawing.Point(6, 19);
            this.chk_hit.Name = "chk_hit";
            this.chk_hit.Size = new System.Drawing.Size(137, 17);
            this.chk_hit.TabIndex = 1;
            this.chk_hit.Text = "Transfer Hitdata To HQ";
            this.chk_hit.UseVisualStyleBackColor = true;
            // 
            // rd_DB
            // 
            this.rd_DB.AutoSize = true;
            this.rd_DB.Location = new System.Drawing.Point(58, 45);
            this.rd_DB.Name = "rd_DB";
            this.rd_DB.Size = new System.Drawing.Size(64, 17);
            this.rd_DB.TabIndex = 2;
            this.rd_DB.TabStop = true;
            this.rd_DB.Text = "DB Only";
            this.rd_DB.UseVisualStyleBackColor = true;
            // 
            // rdDBFile
            // 
            this.rdDBFile.AutoSize = true;
            this.rdDBFile.Location = new System.Drawing.Point(128, 45);
            this.rdDBFile.Name = "rdDBFile";
            this.rdDBFile.Size = new System.Drawing.Size(62, 17);
            this.rdDBFile.TabIndex = 3;
            this.rdDBFile.TabStop = true;
            this.rdDBFile.Text = "DB+File";
            this.rdDBFile.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Options:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(121, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Transfer Time (hh:mm tt)";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 90);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(210, 20);
            this.textBox1.TabIndex = 9;
            // 
            // svr_settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 200);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_reTime);
            this.Controls.Add(this.cbx_restart);
            this.Controls.Add(this.txt_transTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbx_transfer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "svr_settings";
            this.Text = "Server Settings";
            this.Load += new System.EventHandler(this.svr_settings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbx_transfer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_transTime;
        private System.Windows.Forms.CheckBox cbx_restart;
        private System.Windows.Forms.TextBox txt_reTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_pass;
        private System.Windows.Forms.TextBox txt_user;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rdDBFile;
        private System.Windows.Forms.RadioButton rd_DB;
        private System.Windows.Forms.CheckBox chk_hit;
    }
}