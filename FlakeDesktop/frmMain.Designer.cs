namespace FlakeDesktop
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.contextTrayMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.itemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIconTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextTrayMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextTrayMenu
            // 
            this.contextTrayMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemAbout,
            this.toolStripSeparator1,
            this.itemExit});
            this.contextTrayMenu.Name = "contextTrayMenu";
            this.contextTrayMenu.Size = new System.Drawing.Size(126, 54);
            // 
            // itemAbout
            // 
            this.itemAbout.Name = "itemAbout";
            this.itemAbout.Size = new System.Drawing.Size(125, 22);
            this.itemAbout.Text = "Giới thiệu";
            this.itemAbout.Click += new System.EventHandler(this.OnAbout);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(122, 6);
            // 
            // itemExit
            // 
            this.itemExit.Name = "itemExit";
            this.itemExit.Size = new System.Drawing.Size(125, 22);
            this.itemExit.Text = "Thoát";
            this.itemExit.Click += new System.EventHandler(this.OnExit);
            // 
            // notifyIconTray
            // 
            this.notifyIconTray.ContextMenuStrip = this.contextTrayMenu;
            this.notifyIconTray.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIconTray.Icon")));
            this.notifyIconTray.Text = "Flake Desktop";
            this.notifyIconTray.Visible = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 44);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "FrmMain";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.OnLoad);
            this.contextTrayMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextTrayMenu;
        private System.Windows.Forms.ToolStripMenuItem itemAbout;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem itemExit;
        private System.Windows.Forms.NotifyIcon notifyIconTray;
    }
}

