namespace CustomerFormStyle
{
    partial class Form1
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
            this.BottomPanel = new CustomerFormStyle.PanelZ();
            this.RightPanel = new CustomerFormStyle.PanelZ();
            this.LeftPanel = new CustomerFormStyle.PanelZ();
            this.TopPanel = new CustomerFormStyle.PanelZ();
            this.btnClose = new CustomerFormStyle.ButtonZ();
            this.btnMaximize = new CustomerFormStyle.ButtonZ();
            this.btnMinimize = new CustomerFormStyle.ButtonZ();
            this.TopPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // BottomPanel
            // 
            this.BottomPanel.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.EndColor = System.Drawing.Color.DarkBlue;
            this.BottomPanel.GradientAngle = 90;
            this.BottomPanel.Location = new System.Drawing.Point(3, 287);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(539, 3);
            this.BottomPanel.StartColor = System.Drawing.Color.SteelBlue;
            this.BottomPanel.TabIndex = 3;
            this.BottomPanel.Transparent1 = 255;
            this.BottomPanel.Transparent2 = 255;
            this.BottomPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BottomPanel_MouseDown);
            this.BottomPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BottomPanel_MouseMove);
            this.BottomPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BottomPanel_MouseUp);
            // 
            // RightPanel
            // 
            this.RightPanel.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.RightPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.RightPanel.EndColor = System.Drawing.Color.DarkBlue;
            this.RightPanel.GradientAngle = 90;
            this.RightPanel.Location = new System.Drawing.Point(542, 30);
            this.RightPanel.Name = "RightPanel";
            this.RightPanel.Size = new System.Drawing.Size(3, 260);
            this.RightPanel.StartColor = System.Drawing.Color.SteelBlue;
            this.RightPanel.TabIndex = 2;
            this.RightPanel.Transparent1 = 255;
            this.RightPanel.Transparent2 = 255;
            this.RightPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RightPanel_MouseDown);
            this.RightPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.RightPanel_MouseMove);
            this.RightPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightPanel_MouseUp);
            // 
            // LeftPanel
            // 
            this.LeftPanel.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.LeftPanel.EndColor = System.Drawing.Color.DarkBlue;
            this.LeftPanel.GradientAngle = 90;
            this.LeftPanel.Location = new System.Drawing.Point(0, 30);
            this.LeftPanel.Name = "LeftPanel";
            this.LeftPanel.Size = new System.Drawing.Size(3, 260);
            this.LeftPanel.StartColor = System.Drawing.Color.SteelBlue;
            this.LeftPanel.TabIndex = 1;
            this.LeftPanel.Transparent1 = 255;
            this.LeftPanel.Transparent2 = 255;
            this.LeftPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LeftPanel_MouseDown);
            this.LeftPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LeftPanel_MouseMove);
            this.LeftPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LeftPanel_MouseUp);
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.SystemColors.Control;
            this.TopPanel.Controls.Add(this.btnClose);
            this.TopPanel.Controls.Add(this.btnMaximize);
            this.TopPanel.Controls.Add(this.btnMinimize);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.EndColor = System.Drawing.Color.DarkBlue;
            this.TopPanel.GradientAngle = 90;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(545, 30);
            this.TopPanel.StartColor = System.Drawing.Color.SteelBlue;
            this.TopPanel.TabIndex = 0;
            this.TopPanel.Transparent1 = 255;
            this.TopPanel.Transparent2 = 255;
            this.TopPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseDown);
            this.TopPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseMove);
            this.TopPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseUp);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DisplayText = "x";
            this.btnClose.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.GradientAngle = 90;
            this.btnClose.Location = new System.Drawing.Point(502, 0);
            this.btnClose.MouseClickColor1 = System.Drawing.Color.DarkOrange;
            this.btnClose.MouseClickColor2 = System.Drawing.Color.Red;
            this.btnClose.MouseHoverColor1 = System.Drawing.Color.Red;
            this.btnClose.MouseHoverColor2 = System.Drawing.Color.Red;
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(31, 24);
            this.btnClose.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "x";
            this.btnClose.TextLocation_X = 6;
            this.btnClose.TextLocation_Y = 0;
            this.btnClose.Transparent1 = 250;
            this.btnClose.Transparent2 = 250;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.Closebutton_Click);
            // 
            // btnMaximize
            // 
            this.btnMaximize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMaximize.DisplayText = "-";
            this.btnMaximize.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnMaximize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaximize.Font = new System.Drawing.Font("Microsoft YaHei UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMaximize.ForeColor = System.Drawing.Color.White;
            this.btnMaximize.GradientAngle = 90;
            this.btnMaximize.Location = new System.Drawing.Point(465, 0);
            this.btnMaximize.MouseClickColor1 = System.Drawing.Color.DarkOrange;
            this.btnMaximize.MouseClickColor2 = System.Drawing.Color.Red;
            this.btnMaximize.MouseHoverColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.btnMaximize.MouseHoverColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.btnMaximize.Name = "btnMaximize";
            this.btnMaximize.Size = new System.Drawing.Size(31, 24);
            this.btnMaximize.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnMaximize.TabIndex = 5;
            this.btnMaximize.Text = "-";
            this.btnMaximize.TextLocation_X = 6;
            this.btnMaximize.TextLocation_Y = -8;
            this.btnMaximize.Transparent1 = 250;
            this.btnMaximize.Transparent2 = 250;
            this.btnMaximize.UseVisualStyleBackColor = true;
            this.btnMaximize.Click += new System.EventHandler(this.Maximizebutton_Click);
            // 
            // btnMinimize
            // 
            this.btnMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimize.DisplayText = "_";
            this.btnMinimize.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.Font = new System.Drawing.Font("Microsoft YaHei UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMinimize.ForeColor = System.Drawing.Color.White;
            this.btnMinimize.GradientAngle = 90;
            this.btnMinimize.Location = new System.Drawing.Point(428, 0);
            this.btnMinimize.MouseClickColor1 = System.Drawing.Color.DarkOrange;
            this.btnMinimize.MouseClickColor2 = System.Drawing.Color.Red;
            this.btnMinimize.MouseHoverColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.btnMinimize.MouseHoverColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(31, 24);
            this.btnMinimize.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnMinimize.TabIndex = 4;
            this.btnMinimize.Text = "_";
            this.btnMinimize.TextLocation_X = 6;
            this.btnMinimize.TextLocation_Y = -20;
            this.btnMinimize.Transparent1 = 250;
            this.btnMinimize.Transparent2 = 250;
            this.btnMinimize.UseVisualStyleBackColor = true;
            this.btnMinimize.Click += new System.EventHandler(this.Minimizebutton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 290);
            this.Controls.Add(this.BottomPanel);
            this.Controls.Add(this.RightPanel);
            this.Controls.Add(this.LeftPanel);
            this.Controls.Add(this.TopPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.TopPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PanelZ TopPanel;
        private PanelZ LeftPanel;
        private PanelZ RightPanel;
        private PanelZ BottomPanel;
        private ButtonZ btnMinimize;
        private ButtonZ btnClose;
        private ButtonZ btnMaximize;
    }
}

