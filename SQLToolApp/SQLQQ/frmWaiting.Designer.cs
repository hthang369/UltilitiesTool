﻿namespace SQLQQ
{
    partial class frmWaiting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWaiting));
            this.proBarWating = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // proBarWating
            // 
            this.proBarWating.Dock = System.Windows.Forms.DockStyle.Top;
            this.proBarWating.Location = new System.Drawing.Point(0, 0);
            this.proBarWating.MarqueeAnimationSpeed = 30;
            this.proBarWating.Name = "proBarWating";
            this.proBarWating.Size = new System.Drawing.Size(293, 22);
            this.proBarWating.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.proBarWating.TabIndex = 0;
            // 
            // frmWaiting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(293, 22);
            this.ControlBox = false;
            this.Controls.Add(this.proBarWating);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmWaiting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Loading...";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar proBarWating;
    }
}