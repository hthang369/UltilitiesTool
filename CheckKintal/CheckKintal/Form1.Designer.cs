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
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnItemShowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnItemCloseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabAuth = new System.Windows.Forms.TabPage();
            this.gdcListUser = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCheckout = new System.Windows.Forms.Button();
            this.btnCheckin = new System.Windows.Forms.Button();
            this.tabSetting = new System.Windows.Forms.TabPage();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblTime = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.chkAutoSystem = new System.Windows.Forms.CheckBox();
            this.txtCheckOutEnd = new System.Windows.Forms.DateTimePicker();
            this.txtCheckOutStart = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRecheck = new System.Windows.Forms.NumericUpDown();
            this.txtCheckInEnd = new System.Windows.Forms.DateTimePicker();
            this.txtCheckInStart = new System.Windows.Forms.DateTimePicker();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblUrl = new System.Windows.Forms.Label();
            this.tabAddUser = new System.Windows.Forms.TabPage();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.userAuth1 = new CheckKintal.UserAuth();
            this.contextMenuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabAuth.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gdcListUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRecheck)).BeginInit();
            this.tabAddUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Text = "Nhắc nhở check kintai";
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
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 60000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabAuth);
            this.tabControl1.Controls.Add(this.tabSetting);
            this.tabControl1.Controls.Add(this.tabAddUser);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(396, 241);
            this.tabControl1.TabIndex = 15;
            // 
            // tabAuth
            // 
            this.tabAuth.Controls.Add(this.gdcListUser);
            this.tabAuth.Controls.Add(this.panel1);
            this.tabAuth.Location = new System.Drawing.Point(4, 25);
            this.tabAuth.Name = "tabAuth";
            this.tabAuth.Padding = new System.Windows.Forms.Padding(3);
            this.tabAuth.Size = new System.Drawing.Size(388, 212);
            this.tabAuth.TabIndex = 0;
            this.tabAuth.Text = "Login";
            this.tabAuth.UseVisualStyleBackColor = true;
            // 
            // gdcListUser
            // 
            this.gdcListUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gdcListUser.Location = new System.Drawing.Point(3, 3);
            this.gdcListUser.MainView = this.gridView1;
            this.gdcListUser.Name = "gdcListUser";
            this.gdcListUser.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.repositoryItemCheckEdit2});
            this.gdcListUser.Size = new System.Drawing.Size(382, 171);
            this.gdcListUser.TabIndex = 4;
            this.gdcListUser.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
            this.gridView1.GridControl = this.gdcListUser;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Chọn";
            this.gridColumn1.ColumnEdit = this.repositoryItemCheckEdit1;
            this.gridColumn1.FieldName = "chkSelected";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "UserName";
            this.gridColumn2.FieldName = "Username";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Tự động check in";
            this.gridColumn3.ColumnEdit = this.repositoryItemCheckEdit2;
            this.gridColumn3.FieldName = "AutoCheckIn";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Thông báo";
            this.gridColumn4.FieldName = "lblShow";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCheckout);
            this.panel1.Controls.Add(this.btnCheckin);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 174);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(382, 35);
            this.panel1.TabIndex = 3;
            // 
            // btnCheckout
            // 
            this.btnCheckout.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnCheckout.Location = new System.Drawing.Point(75, 0);
            this.btnCheckout.Name = "btnCheckout";
            this.btnCheckout.Size = new System.Drawing.Size(75, 35);
            this.btnCheckout.TabIndex = 2;
            this.btnCheckout.Text = "Check out";
            this.btnCheckout.UseVisualStyleBackColor = true;
            // 
            // btnCheckin
            // 
            this.btnCheckin.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnCheckin.Location = new System.Drawing.Point(0, 0);
            this.btnCheckin.Name = "btnCheckin";
            this.btnCheckin.Size = new System.Drawing.Size(75, 35);
            this.btnCheckin.TabIndex = 1;
            this.btnCheckin.Text = "Check in";
            this.btnCheckin.UseVisualStyleBackColor = true;
            // 
            // tabSetting
            // 
            this.tabSetting.Controls.Add(this.btnSave);
            this.tabSetting.Controls.Add(this.lblTime);
            this.tabSetting.Controls.Add(this.btnStop);
            this.tabSetting.Controls.Add(this.btnStart);
            this.tabSetting.Controls.Add(this.chkAutoSystem);
            this.tabSetting.Controls.Add(this.txtCheckOutEnd);
            this.tabSetting.Controls.Add(this.txtCheckOutStart);
            this.tabSetting.Controls.Add(this.label4);
            this.tabSetting.Controls.Add(this.label3);
            this.tabSetting.Controls.Add(this.txtRecheck);
            this.tabSetting.Controls.Add(this.txtCheckInEnd);
            this.tabSetting.Controls.Add(this.txtCheckInStart);
            this.tabSetting.Controls.Add(this.txtUrl);
            this.tabSetting.Controls.Add(this.label2);
            this.tabSetting.Controls.Add(this.lblUrl);
            this.tabSetting.Location = new System.Drawing.Point(4, 25);
            this.tabSetting.Name = "tabSetting";
            this.tabSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tabSetting.Size = new System.Drawing.Size(388, 212);
            this.tabSetting.TabIndex = 1;
            this.tabSetting.Text = "Setting";
            this.tabSetting.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(186, 172);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(165, 32);
            this.btnSave.TabIndex = 29;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.Location = new System.Drawing.Point(11, 138);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(43, 20);
            this.lblTime.TabIndex = 28;
            this.lblTime.Text = "Time";
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.Location = new System.Drawing.Point(15, 172);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(165, 32);
            this.btnStop.TabIndex = 27;
            this.btnStop.Text = "Dừng";
            this.btnStop.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(15, 172);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(165, 32);
            this.btnStart.TabIndex = 26;
            this.btnStart.Text = "Chạy";
            this.btnStart.UseVisualStyleBackColor = true;
            // 
            // chkAutoSystem
            // 
            this.chkAutoSystem.AutoSize = true;
            this.chkAutoSystem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAutoSystem.Location = new System.Drawing.Point(166, 138);
            this.chkAutoSystem.Name = "chkAutoSystem";
            this.chkAutoSystem.Size = new System.Drawing.Size(168, 24);
            this.chkAutoSystem.TabIndex = 25;
            this.chkAutoSystem.Text = "Chạy cùng windown";
            this.chkAutoSystem.UseVisualStyleBackColor = true;
            // 
            // txtCheckOutEnd
            // 
            this.txtCheckOutEnd.CustomFormat = "HH:mm";
            this.txtCheckOutEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCheckOutEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtCheckOutEnd.Location = new System.Drawing.Point(274, 74);
            this.txtCheckOutEnd.Name = "txtCheckOutEnd";
            this.txtCheckOutEnd.ShowUpDown = true;
            this.txtCheckOutEnd.Size = new System.Drawing.Size(100, 26);
            this.txtCheckOutEnd.TabIndex = 24;
            // 
            // txtCheckOutStart
            // 
            this.txtCheckOutStart.CustomFormat = "HH:mm";
            this.txtCheckOutStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCheckOutStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtCheckOutStart.Location = new System.Drawing.Point(166, 74);
            this.txtCheckOutStart.Name = "txtCheckOutStart";
            this.txtCheckOutStart.ShowUpDown = true;
            this.txtCheckOutStart.Size = new System.Drawing.Size(99, 26);
            this.txtCheckOutStart.TabIndex = 23;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(11, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(149, 20);
            this.label4.TabIndex = 22;
            this.label4.Text = "Thời gian Check out";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(11, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 20);
            this.label3.TabIndex = 21;
            this.label3.Text = "Báo lại";
            // 
            // txtRecheck
            // 
            this.txtRecheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRecheck.Location = new System.Drawing.Point(166, 106);
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
            this.txtRecheck.TabIndex = 20;
            this.txtRecheck.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtCheckInEnd
            // 
            this.txtCheckInEnd.CustomFormat = "HH:mm";
            this.txtCheckInEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCheckInEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtCheckInEnd.Location = new System.Drawing.Point(274, 42);
            this.txtCheckInEnd.Name = "txtCheckInEnd";
            this.txtCheckInEnd.ShowUpDown = true;
            this.txtCheckInEnd.Size = new System.Drawing.Size(100, 26);
            this.txtCheckInEnd.TabIndex = 19;
            // 
            // txtCheckInStart
            // 
            this.txtCheckInStart.CustomFormat = "HH:mm";
            this.txtCheckInStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCheckInStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtCheckInStart.Location = new System.Drawing.Point(166, 42);
            this.txtCheckInStart.Name = "txtCheckInStart";
            this.txtCheckInStart.ShowUpDown = true;
            this.txtCheckInStart.Size = new System.Drawing.Size(99, 26);
            this.txtCheckInStart.TabIndex = 18;
            // 
            // txtUrl
            // 
            this.txtUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUrl.Location = new System.Drawing.Point(166, 10);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(208, 26);
            this.txtUrl.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 20);
            this.label2.TabIndex = 16;
            this.label2.Text = "Thời gian Check In";
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUrl.Location = new System.Drawing.Point(11, 13);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(42, 20);
            this.lblUrl.TabIndex = 15;
            this.lblUrl.Text = "URL";
            // 
            // tabAddUser
            // 
            this.tabAddUser.Controls.Add(this.btnEdit);
            this.tabAddUser.Controls.Add(this.btnDelete);
            this.tabAddUser.Controls.Add(this.btnAdd);
            this.tabAddUser.Controls.Add(this.userAuth1);
            this.tabAddUser.Location = new System.Drawing.Point(4, 25);
            this.tabAddUser.Name = "tabAddUser";
            this.tabAddUser.Padding = new System.Windows.Forms.Padding(3);
            this.tabAddUser.Size = new System.Drawing.Size(388, 212);
            this.tabAddUser.TabIndex = 2;
            this.tabAddUser.Text = "Add User";
            this.tabAddUser.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(167, 156);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 4;
            this.btnEdit.Text = "Update";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(86, 156);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(5, 156);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // userAuth1
            // 
            this.userAuth1.Dock = System.Windows.Forms.DockStyle.Top;
            this.userAuth1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userAuth1.Location = new System.Drawing.Point(3, 3);
            this.userAuth1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.userAuth1.Name = "userAuth1";
            this.userAuth1.Size = new System.Drawing.Size(382, 145);
            this.userAuth1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 241);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Check ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabAuth.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gdcListUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tabSetting.ResumeLayout(false);
            this.tabSetting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRecheck)).EndInit();
            this.tabAddUser.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnItemShowToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnItemCloseToolStripMenuItem;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabAuth;
        private System.Windows.Forms.TabPage tabSetting;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.CheckBox chkAutoSystem;
        private System.Windows.Forms.DateTimePicker txtCheckOutEnd;
        private System.Windows.Forms.DateTimePicker txtCheckOutStart;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown txtRecheck;
        private System.Windows.Forms.DateTimePicker txtCheckInEnd;
        private System.Windows.Forms.DateTimePicker txtCheckInStart;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblUrl;
        private System.Windows.Forms.TabPage tabAddUser;
        private UserAuth userAuth1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCheckout;
        private System.Windows.Forms.Button btnCheckin;
        private DevExpress.XtraGrid.GridControl gdcListUser;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private System.Windows.Forms.Button btnEdit;
    }
}

