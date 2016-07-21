namespace mw_mgr
{
    partial class comp_obj
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(comp_obj));
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.pb_icon = new System.Windows.Forms.PictureBox();
            this.lbl_compName = new System.Windows.Forms.Label();
            this.lbl_ipAdd = new System.Windows.Forms.Label();
            this.lbl_status = new System.Windows.Forms.Label();
            this.btn_restart = new System.Windows.Forms.Button();
            this.btn_info = new System.Windows.Forms.Button();
            this.btn_copy = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pb_icon)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(3, 15);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // pb_icon
            // 
            this.pb_icon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pb_icon.Image = ((System.Drawing.Image)(resources.GetObject("pb_icon.Image")));
            this.pb_icon.Location = new System.Drawing.Point(24, 3);
            this.pb_icon.Name = "pb_icon";
            this.pb_icon.Size = new System.Drawing.Size(77, 46);
            this.pb_icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_icon.TabIndex = 1;
            this.pb_icon.TabStop = false;
            // 
            // lbl_compName
            // 
            this.lbl_compName.AutoSize = true;
            this.lbl_compName.Location = new System.Drawing.Point(107, 10);
            this.lbl_compName.Name = "lbl_compName";
            this.lbl_compName.Size = new System.Drawing.Size(78, 13);
            this.lbl_compName.TabIndex = 2;
            this.lbl_compName.Text = "KLIA_ARRVL1";
            // 
            // lbl_ipAdd
            // 
            this.lbl_ipAdd.AutoSize = true;
            this.lbl_ipAdd.Location = new System.Drawing.Point(107, 30);
            this.lbl_ipAdd.Name = "lbl_ipAdd";
            this.lbl_ipAdd.Size = new System.Drawing.Size(64, 13);
            this.lbl_ipAdd.TabIndex = 3;
            this.lbl_ipAdd.Text = "192.168.2.3";
            // 
            // lbl_status
            // 
            this.lbl_status.AutoSize = true;
            this.lbl_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_status.Location = new System.Drawing.Point(242, 15);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(106, 17);
            this.lbl_status.TabIndex = 4;
            this.lbl_status.Text = "Disconnected";
            // 
            // btn_restart
            // 
            this.btn_restart.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_restart.BackgroundImage")));
            this.btn_restart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_restart.Location = new System.Drawing.Point(507, 4);
            this.btn_restart.Name = "btn_restart";
            this.btn_restart.Size = new System.Drawing.Size(42, 41);
            this.btn_restart.TabIndex = 5;
            this.btn_restart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_restart.UseVisualStyleBackColor = true;
            // 
            // btn_info
            // 
            this.btn_info.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_info.BackgroundImage")));
            this.btn_info.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_info.Location = new System.Drawing.Point(411, 3);
            this.btn_info.Name = "btn_info";
            this.btn_info.Size = new System.Drawing.Size(42, 41);
            this.btn_info.TabIndex = 6;
            this.btn_info.UseVisualStyleBackColor = true;
            this.btn_info.Click += new System.EventHandler(this.btn_info_Click);
            // 
            // btn_copy
            // 
            this.btn_copy.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_copy.BackgroundImage")));
            this.btn_copy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_copy.Location = new System.Drawing.Point(459, 3);
            this.btn_copy.Name = "btn_copy";
            this.btn_copy.Size = new System.Drawing.Size(42, 42);
            this.btn_copy.TabIndex = 7;
            this.btn_copy.UseVisualStyleBackColor = true;
            // 
            // comp_obj
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_copy);
            this.Controls.Add(this.btn_info);
            this.Controls.Add(this.btn_restart);
            this.Controls.Add(this.lbl_status);
            this.Controls.Add(this.lbl_ipAdd);
            this.Controls.Add(this.lbl_compName);
            this.Controls.Add(this.pb_icon);
            this.Controls.Add(this.checkBox1);
            this.Name = "comp_obj";
            this.Size = new System.Drawing.Size(556, 52);
            ((System.ComponentModel.ISupportInitialize)(this.pb_icon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.PictureBox pb_icon;
        private System.Windows.Forms.Label lbl_compName;
        private System.Windows.Forms.Label lbl_ipAdd;
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.Button btn_restart;
        private System.Windows.Forms.Button btn_info;
        private System.Windows.Forms.Button btn_copy;
    }
}
