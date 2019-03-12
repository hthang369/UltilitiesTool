namespace SQLQQ
{
    partial class frmModule
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
            this.cbbModule = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cbbModule
            // 
            this.cbbModule.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbbModule.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbbModule.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cbbModule.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.cbbModule.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbbModule.FormattingEnabled = true;
            this.cbbModule.Location = new System.Drawing.Point(0, 0);
            this.cbbModule.Name = "cbbModule";
            this.cbbModule.Size = new System.Drawing.Size(284, 21);
            this.cbbModule.TabIndex = 4;
            // 
            // frmModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 21);
            this.Controls.Add(this.cbbModule);
            this.KeyPreview = true;
            this.Name = "frmModule";
            this.Text = "Module";
            this.Load += new System.EventHandler(this.frmModule_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmModule_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbbModule;
    }
}