namespace SQLQQ
{
    partial class frmSearchColumByTest
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
            this.cbbViewColumnByText = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cbbViewColumnByText
            // 
            this.cbbViewColumnByText.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbbViewColumnByText.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbbViewColumnByText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.cbbViewColumnByText.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.cbbViewColumnByText.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbbViewColumnByText.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbViewColumnByText.FormattingEnabled = true;
            this.cbbViewColumnByText.Location = new System.Drawing.Point(0, 0);
            this.cbbViewColumnByText.Name = "cbbViewColumnByText";
            this.cbbViewColumnByText.Size = new System.Drawing.Size(346, 23);
            this.cbbViewColumnByText.TabIndex = 6;
            // 
            // frmSearchColumByTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Pink;
            this.ClientSize = new System.Drawing.Size(346, 23);
            this.Controls.Add(this.cbbViewColumnByText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmSearchColumByTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View column by text";
            this.Load += new System.EventHandler(this.frmSearchColumByTest_Load_1);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbbViewColumnByText;

    }
}