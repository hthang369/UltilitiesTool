﻿namespace SQLQQ
{
    partial class frmColumn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmColumn));
            this.cbbColumn = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cbbColumn
            // 
            this.cbbColumn.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbbColumn.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbbColumn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.cbbColumn.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.cbbColumn.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbbColumn.FormattingEnabled = true;
            this.cbbColumn.Location = new System.Drawing.Point(0, 0);
            this.cbbColumn.Name = "cbbColumn";
            this.cbbColumn.Size = new System.Drawing.Size(335, 21);
            this.cbbColumn.TabIndex = 5;
            // 
            // frmColumn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 22);
            this.Controls.Add(this.cbbColumn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmColumn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Column";
            this.Load += new System.EventHandler(this.frmColumn_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmColumn_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbbColumn;
    }
}