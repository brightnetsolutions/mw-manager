﻿namespace idt_diag
{
    partial class frm_slct
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_mode = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Manager mode :";
            // 
            // cmb_mode
            // 
            this.cmb_mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_mode.FormattingEnabled = true;
            this.cmb_mode.Items.AddRange(new object[] {
            "Server Mode",
            "Client Mode"});
            this.cmb_mode.Location = new System.Drawing.Point(102, 6);
            this.cmb_mode.Name = "cmb_mode";
            this.cmb_mode.Size = new System.Drawing.Size(167, 21);
            this.cmb_mode.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(192, 33);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frm_slct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(281, 63);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmb_mode);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_slct";
            this.Text = "Select Manager Mode";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_mode;
        private System.Windows.Forms.Button button1;
    }
}