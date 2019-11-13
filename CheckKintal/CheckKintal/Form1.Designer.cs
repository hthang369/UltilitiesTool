namespace CheckKintal
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblUrl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnItemShowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnItemCloseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.txtCheckInStart = new System.Windows.Forms.DateTimePicker();
            this.txtCheckInEnd = new System.Windows.Forms.DateTimePicker();
            this.txtRecheck = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCheckOutEnd = new System.Windows.Forms.DateTimePicker();
            this.txtCheckOutStart = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.chkAutoSystem = new System.Windows.Forms.CheckBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblTime = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRecheck)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUrl.Location = new System.Drawing.Point(14, 15);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(42, 20);
            this.lblUrl.TabIndex = 0;
            this.lblUrl.Text = "URL";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Thời gian Check In";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnItemShowToolStripMenuItem,
            this.toolStripSeparator1,
            this.mnItemCloseToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(169, 54);
            // 
            // mnItemShowToolStripMenuItem
            // 
            this.mnItemShowToolStripMenuItem.Name = "mnItemShowToolStripMenuItem";
            this.mnItemShowToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.mnItemShowToolStripMenuItem.Text = "Hiện thị giao diện";
            this.mnItemShowToolStripMenuItem.Click += new System.EventHandler(this.mnItemShowToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(165, 6);
            // 
            // mnItemCloseToolStripMenuItem
            // 
            this.mnItemCloseToolStripMenuItem.Name = "mnItemCloseToolStripMenuItem";
            this.mnItemCloseToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.mnItemCloseToolStripMenuItem.Text = "Thoát";
            this.mnItemCloseToolStripMenuItem.Click += new System.EventHandler(this.mnItemCloseToolStripMenuItem_Click);
            // 
            // txtUrl
            // 
            this.txtUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUrl.Location = new System.Drawing.Point(169, 12);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(208, 26);
            this.txtUrl.TabIndex = 2;
            // 
            // txtCheckInStart
            // 
            this.txtCheckInStart.CustomFormat = "HH:mm";
            this.txtCheckInStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCheckInStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtCheckInStart.Location = new System.Drawing.Point(169, 44);
            this.txtCheckInStart.Name = "txtCheckInStart";
            this.txtCheckInStart.ShowUpDown = true;
            this.txtCheckInStart.Size = new System.Drawing.Size(99, 26);
            this.txtCheckInStart.TabIndex = 3;
            // 
            // txtCheckInEnd
            // 
            this.txtCheckInEnd.CustomFormat = "HH:mm";
            this.txtCheckInEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCheckInEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtCheckInEnd.Location = new System.Drawing.Point(277, 44);
            this.txtCheckInEnd.Name = "txtCheckInEnd";
            this.txtCheckInEnd.ShowUpDown = true;
            this.txtCheckInEnd.Size = new System.Drawing.Size(100, 26);
            this.txtCheckInEnd.TabIndex = 4;
            // 
            // txtRecheck
            // 
            this.txtRecheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRecheck.Location = new System.Drawing.Point(169, 108);
            this.txtRecheck.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.txtRecheck.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtRecheck.Name = "txtRecheck";
            this.txtRecheck.Size = new System.Drawing.Size(208, 26);
            this.txtRecheck.TabIndex = 5;
            this.txtRecheck.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Báo lại";
            // 
            // txtCheckOutEnd
            // 
            this.txtCheckOutEnd.CustomFormat = "HH:mm";
            this.txtCheckOutEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCheckOutEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtCheckOutEnd.Location = new System.Drawing.Point(277, 76);
            this.txtCheckOutEnd.Name = "txtCheckOutEnd";
            this.txtCheckOutEnd.ShowUpDown = true;
            this.txtCheckOutEnd.Size = new System.Drawing.Size(100, 26);
            this.txtCheckOutEnd.TabIndex = 9;
            // 
            // txtCheckOutStart
            // 
            this.txtCheckOutStart.CustomFormat = "HH:mm";
            this.txtCheckOutStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCheckOutStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtCheckOutStart.Location = new System.Drawing.Point(169, 76);
            this.txtCheckOutStart.Name = "txtCheckOutStart";
            this.txtCheckOutStart.ShowUpDown = true;
            this.txtCheckOutStart.Size = new System.Drawing.Size(99, 26);
            this.txtCheckOutStart.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(14, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(149, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Thời gian Check out";
            // 
            // chkAutoSystem
            // 
            this.chkAutoSystem.AutoSize = true;
            this.chkAutoSystem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAutoSystem.Location = new System.Drawing.Point(169, 140);
            this.chkAutoSystem.Name = "chkAutoSystem";
            this.chkAutoSystem.Size = new System.Drawing.Size(168, 24);
            this.chkAutoSystem.TabIndex = 10;
            this.chkAutoSystem.Text = "Chạy cùng windown";
            this.chkAutoSystem.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(18, 174);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(165, 32);
            this.btnStart.TabIndex = 11;
            this.btnStart.Text = "Chạy";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.Location = new System.Drawing.Point(18, 174);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(165, 32);
            this.btnStop.TabIndex = 12;
            this.btnStop.Text = "Dừng";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.Location = new System.Drawing.Point(14, 140);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(43, 20);
            this.lblTime.TabIndex = 13;
            this.lblTime.Text = "Time";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(189, 174);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(165, 32);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 218);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.chkAutoSystem);
            this.Controls.Add(this.txtCheckOutEnd);
            this.Controls.Add(this.txtCheckOutStart);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtRecheck);
            this.Controls.Add(this.txtCheckInEnd);
            this.Controls.Add(this.txtCheckInStart);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblUrl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Check ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtRecheck)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblUrl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.DateTimePicker txtCheckInStart;
        private System.Windows.Forms.DateTimePicker txtCheckInEnd;
        private System.Windows.Forms.NumericUpDown txtRecheck;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker txtCheckOutEnd;
        private System.Windows.Forms.DateTimePicker txtCheckOutStart;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkAutoSystem;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnItemShowToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnItemCloseToolStripMenuItem;
    }
}

