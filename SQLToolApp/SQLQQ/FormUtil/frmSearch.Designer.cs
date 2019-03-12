﻿namespace SQLQQ
{
    partial class frmSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSearch));
            this.cboItems = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblCaption = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cboItems
            // 
            this.cboItems.AllowDrop = true;
            this.cboItems.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cboItems.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboItems.FormattingEnabled = true;
            this.cboItems.Location = new System.Drawing.Point(12, 3);
            this.cboItems.Name = "cboItems";
            this.cboItems.Size = new System.Drawing.Size(316, 24);
            this.cboItems.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(253, 28);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 24);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // txtInput
            // 
            this.txtInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtInput.Location = new System.Drawing.Point(12, 5);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(317, 22);
            this.txtInput.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(172, 28);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 24);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblCaption
            // 
            this.lblCaption.AutoSize = true;
            this.lblCaption.Location = new System.Drawing.Point(9, 32);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(45, 16);
            this.lblCaption.TabIndex = 9;
            this.lblCaption.Text = "label1";
            // 
            // frmSearch
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(339, 55);
            this.Controls.Add(this.lblCaption);
            this.Controls.Add(this.cboItems);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.btnOK);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "frmSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Table Name ";
            this.Load += new System.EventHandler(this.frmSearch_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSearch_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboItems;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblCaption;
    }
}