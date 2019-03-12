namespace SQLQQ
{
    partial class frmNotifycation
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
            this.lblNotifycation = new System.Windows.Forms.Label();
            this.timerNotifycation = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lblNotifycation
            // 
            this.lblNotifycation.AutoSize = true;
            this.lblNotifycation.BackColor = System.Drawing.Color.Maroon;
            this.lblNotifycation.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotifycation.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblNotifycation.Location = new System.Drawing.Point(-2, 0);
            this.lblNotifycation.Name = "lblNotifycation";
            this.lblNotifycation.Size = new System.Drawing.Size(168, 22);
            this.lblNotifycation.TabIndex = 0;
            this.lblNotifycation.Text = "Notifycation............";
            // 
            // frmNotifycation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(292, 115);
            this.Controls.Add(this.lblNotifycation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmNotifycation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmNotifycation";
            this.TransparencyKey = System.Drawing.Color.White;
            this.Load += new System.EventHandler(this.frmNotifycation_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmNotifycation_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNotifycation;
        private System.Windows.Forms.Timer timerNotifycation;
    }
}