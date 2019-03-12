namespace SetTabIndex
{
    partial class FormGenScriptDB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGenScriptDB));
            this.grcControl = new DevExpress.XtraGrid.GridControl();
            this.gvControl = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkData = new DevExpress.XtraEditors.CheckEdit();
            this.chkStruct = new DevExpress.XtraEditors.CheckEdit();
            this.btnGenScript = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbbDatabase = new System.Windows.Forms.ComboBox();
            this.radAll = new DevExpress.XtraEditors.CheckEdit();
            this.radTable = new DevExpress.XtraEditors.CheckEdit();
            this.radStore = new DevExpress.XtraEditors.CheckEdit();
            this.chkAll = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.grcControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvControl)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkData.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkStruct.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radAll.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTable.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radStore.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAll.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // grcControl
            // 
            this.grcControl.AllowDrop = true;
            this.grcControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcControl.Location = new System.Drawing.Point(0, 113);
            this.grcControl.MainView = this.gvControl;
            this.grcControl.Name = "grcControl";
            this.grcControl.Size = new System.Drawing.Size(715, 209);
            this.grcControl.TabIndex = 14;
            this.grcControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvControl});
            // 
            // gvControl
            // 
            this.gvControl.BestFitMaxRowCount = 0;
            this.gvControl.GridControl = this.grcControl;
            this.gvControl.Name = "gvControl";
            this.gvControl.OptionsView.ColumnAutoWidth = false;
            this.gvControl.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gvControl.OptionsView.ShowAutoFilterRow = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkAll);
            this.panel1.Controls.Add(this.radStore);
            this.panel1.Controls.Add(this.radTable);
            this.panel1.Controls.Add(this.radAll);
            this.panel1.Controls.Add(this.chkData);
            this.panel1.Controls.Add(this.chkStruct);
            this.panel1.Controls.Add(this.btnGenScript);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtServerName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtUserName);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtPassword);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cbbDatabase);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(715, 113);
            this.panel1.TabIndex = 15;
            // 
            // chkData
            // 
            this.chkData.EditValue = true;
            this.chkData.Location = new System.Drawing.Point(385, 32);
            this.chkData.Name = "chkData";
            this.chkData.Properties.Caption = "Data";
            this.chkData.Size = new System.Drawing.Size(75, 19);
            this.chkData.TabIndex = 36;
            // 
            // chkStruct
            // 
            this.chkStruct.EditValue = true;
            this.chkStruct.Location = new System.Drawing.Point(317, 32);
            this.chkStruct.Name = "chkStruct";
            this.chkStruct.Properties.Caption = "Cấu trúc";
            this.chkStruct.Size = new System.Drawing.Size(75, 19);
            this.chkStruct.TabIndex = 36;
            // 
            // btnGenScript
            // 
            this.btnGenScript.Location = new System.Drawing.Point(256, 74);
            this.btnGenScript.Name = "btnGenScript";
            this.btnGenScript.Size = new System.Drawing.Size(99, 21);
            this.btnGenScript.TabIndex = 34;
            this.btnGenScript.Text = "Gen Script";
            this.btnGenScript.UseVisualStyleBackColor = true;
            this.btnGenScript.Click += new System.EventHandler(this.btnGenScript_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Server Name";
            // 
            // txtServerName
            // 
            this.txtServerName.Location = new System.Drawing.Point(87, 5);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(128, 20);
            this.txtServerName.TabIndex = 21;
            this.txtServerName.Text = "10.6.8.252,2000";
            this.txtServerName.Validated += new System.EventHandler(this.txtServerName_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "UserName";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(87, 28);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(128, 20);
            this.txtUserName.TabIndex = 23;
            this.txtUserName.Text = "sa";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(87, 51);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(128, 20);
            this.txtPassword.TabIndex = 25;
            this.txtPassword.Text = "abc123";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "Database";
            // 
            // cbbDatabase
            // 
            this.cbbDatabase.FormattingEnabled = true;
            this.cbbDatabase.Location = new System.Drawing.Point(87, 74);
            this.cbbDatabase.Name = "cbbDatabase";
            this.cbbDatabase.Size = new System.Drawing.Size(128, 21);
            this.cbbDatabase.TabIndex = 27;
            this.cbbDatabase.SelectedIndexChanged += new System.EventHandler(this.cbbDatabase_SelectedIndexChanged);
            this.cbbDatabase.Enter += new System.EventHandler(this.cbbDatabase_Enter);
            // 
            // radAll
            // 
            this.radAll.EditValue = true;
            this.radAll.Location = new System.Drawing.Point(254, 7);
            this.radAll.Name = "radAll";
            this.radAll.Properties.Caption = "Tất cả";
            this.radAll.Size = new System.Drawing.Size(62, 19);
            this.radAll.TabIndex = 37;
            this.radAll.CheckedChanged += new System.EventHandler(this.radAll_CheckedChanged);
            // 
            // radTable
            // 
            this.radTable.Location = new System.Drawing.Point(317, 7);
            this.radTable.Name = "radTable";
            this.radTable.Properties.Caption = "Table";
            this.radTable.Size = new System.Drawing.Size(57, 19);
            this.radTable.TabIndex = 38;
            this.radTable.Tag = "U";
            this.radTable.CheckedChanged += new System.EventHandler(this.radObject_CheckedChanged);
            // 
            // radStore
            // 
            this.radStore.Location = new System.Drawing.Point(385, 7);
            this.radStore.Name = "radStore";
            this.radStore.Properties.Caption = "Store Procedure";
            this.radStore.Size = new System.Drawing.Size(116, 19);
            this.radStore.TabIndex = 39;
            this.radStore.Tag = "P";
            this.radStore.CheckedChanged += new System.EventHandler(this.radObject_CheckedChanged);
            // 
            // chkAll
            // 
            this.chkAll.EditValue = true;
            this.chkAll.Location = new System.Drawing.Point(254, 32);
            this.chkAll.Name = "chkAll";
            this.chkAll.Properties.Caption = "Tất cả";
            this.chkAll.Size = new System.Drawing.Size(57, 19);
            this.chkAll.TabIndex = 40;
            // 
            // FormGenScriptDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 322);
            this.Controls.Add(this.grcControl);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormGenScriptDB";
            this.Text = "Gen Script DB";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grcControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvControl)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkData.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkStruct.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radAll.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTable.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radStore.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAll.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grcControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gvControl;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnGenScript;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtServerName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbbDatabase;
        private DevExpress.XtraEditors.CheckEdit chkData;
        private DevExpress.XtraEditors.CheckEdit chkStruct;
        private DevExpress.XtraEditors.CheckEdit radStore;
        private DevExpress.XtraEditors.CheckEdit radTable;
        private DevExpress.XtraEditors.CheckEdit radAll;
        private DevExpress.XtraEditors.CheckEdit chkAll;
    }
}

