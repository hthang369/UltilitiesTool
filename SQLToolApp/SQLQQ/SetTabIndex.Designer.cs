﻿namespace SQLQQ
{
    partial class SetTabIndex
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDefault = new System.Windows.Forms.Button();
            this.btnTabIndex = new System.Windows.Forms.Button();
            this.cboAlignType = new System.Windows.Forms.ComboBox();
            this.cboGroup = new System.Windows.Forms.ComboBox();
            this.cboScreens = new System.Windows.Forms.ComboBox();
            this.cboUserGroup = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboModules = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this._worker = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDefault);
            this.panel1.Controls.Add(this.btnTabIndex);
            this.panel1.Controls.Add(this.cboAlignType);
            this.panel1.Controls.Add(this.cboGroup);
            this.panel1.Controls.Add(this.cboScreens);
            this.panel1.Controls.Add(this.cboUserGroup);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cboModules);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(721, 73);
            this.panel1.TabIndex = 0;
            // 
            // btnDefault
            // 
            this.btnDefault.Location = new System.Drawing.Point(620, 41);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(89, 22);
            this.btnDefault.TabIndex = 10;
            this.btnDefault.Text = "Set Default";
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new System.EventHandler(this.btnSetTabIndex_Click);
            // 
            // btnTabIndex
            // 
            this.btnTabIndex.Location = new System.Drawing.Point(620, 15);
            this.btnTabIndex.Name = "btnTabIndex";
            this.btnTabIndex.Size = new System.Drawing.Size(89, 22);
            this.btnTabIndex.TabIndex = 10;
            this.btnTabIndex.Text = "Set TabIndex";
            this.btnTabIndex.UseVisualStyleBackColor = true;
            this.btnTabIndex.Click += new System.EventHandler(this.btnSetTabIndex_Click);
            // 
            // cboAlignType
            // 
            this.cboAlignType.FormattingEnabled = true;
            this.cboAlignType.Items.AddRange(new object[] {
            "TopLeft",
            "LeftTop"});
            this.cboAlignType.Location = new System.Drawing.Point(503, 17);
            this.cboAlignType.Name = "cboAlignType";
            this.cboAlignType.Size = new System.Drawing.Size(93, 21);
            this.cboAlignType.TabIndex = 9;
            // 
            // cboGroup
            // 
            this.cboGroup.FormattingEnabled = true;
            this.cboGroup.Location = new System.Drawing.Point(293, 43);
            this.cboGroup.Name = "cboGroup";
            this.cboGroup.Size = new System.Drawing.Size(121, 21);
            this.cboGroup.TabIndex = 8;
            this.cboGroup.SelectedIndexChanged += new System.EventHandler(this.cboControl_SelectedIndexChanged);
            // 
            // cboScreens
            // 
            this.cboScreens.FormattingEnabled = true;
            this.cboScreens.Location = new System.Drawing.Point(293, 17);
            this.cboScreens.Name = "cboScreens";
            this.cboScreens.Size = new System.Drawing.Size(121, 21);
            this.cboScreens.TabIndex = 7;
            this.cboScreens.SelectedIndexChanged += new System.EventHandler(this.cboControl_SelectedIndexChanged);
            // 
            // cboUserGroup
            // 
            this.cboUserGroup.FormattingEnabled = true;
            this.cboUserGroup.Location = new System.Drawing.Point(85, 43);
            this.cboUserGroup.Name = "cboUserGroup";
            this.cboUserGroup.Size = new System.Drawing.Size(121, 21);
            this.cboUserGroup.TabIndex = 6;
            this.cboUserGroup.SelectedIndexChanged += new System.EventHandler(this.cboControl_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(440, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Align Type";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(236, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Group";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(236, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Screen";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "User Group";
            // 
            // cboModules
            // 
            this.cboModules.FormattingEnabled = true;
            this.cboModules.Location = new System.Drawing.Point(85, 17);
            this.cboModules.Name = "cboModules";
            this.cboModules.Size = new System.Drawing.Size(121, 21);
            this.cboModules.TabIndex = 1;
            this.cboModules.SelectedIndexChanged += new System.EventHandler(this.cboControl_SelectedIndexChanged);
            this.cboModules.Enter += new System.EventHandler(this.cboControl_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Module";
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvData.Location = new System.Drawing.Point(0, 73);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.Size = new System.Drawing.Size(721, 229);
            this.dgvData.TabIndex = 1;
            // 
            // _worker
            // 
            this._worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Action_DoWork);
            this._worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this._worker_RunWorkerCompleted);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(248, 279);
            this.progressBar1.MarqueeAnimationSpeed = 70;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(200, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 2;
            this.progressBar1.Visible = false;
            // 
            // SetTabIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 302);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.panel1);
            this.Name = "SetTabIndex";
            this.Text = "SetTabIndex";
            this.Load += new System.EventHandler(this.SetTabIndex_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDefault;
        private System.Windows.Forms.Button btnTabIndex;
        private System.Windows.Forms.ComboBox cboAlignType;
        private System.Windows.Forms.ComboBox cboGroup;
        private System.Windows.Forms.ComboBox cboScreens;
        private System.Windows.Forms.ComboBox cboUserGroup;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboModules;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvData;
        private System.ComponentModel.BackgroundWorker _worker;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}