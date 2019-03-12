namespace SetTabIndex
{
    partial class FormSetTabIndex
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSetTabIndex));
            this.grcControl = new DevExpress.XtraGrid.GridControl();
            this.gvControl = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDefault = new System.Windows.Forms.Button();
            this.cbbAlignType = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbbGroup = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSetTabindex = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.cbbScreen = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.cbbUserGroup = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.cbbModule = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbbDatabase = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.grcControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvControl)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grcControl
            // 
            this.grcControl.AllowDrop = true;
            this.grcControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcControl.Location = new System.Drawing.Point(0, 119);
            this.grcControl.MainView = this.gvControl;
            this.grcControl.Name = "grcControl";
            this.grcControl.Size = new System.Drawing.Size(715, 203);
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
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDefault);
            this.panel1.Controls.Add(this.cbbAlignType);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.cbbGroup);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.btnSetTabindex);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtServerName);
            this.panel1.Controls.Add(this.cbbScreen);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtUserName);
            this.panel1.Controls.Add(this.cbbUserGroup);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtPassword);
            this.panel1.Controls.Add(this.cbbModule);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.cbbDatabase);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(715, 119);
            this.panel1.TabIndex = 15;
            // 
            // btnDefault
            // 
            this.btnDefault.Location = new System.Drawing.Point(604, 77);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(99, 21);
            this.btnDefault.TabIndex = 39;
            this.btnDefault.Text = "Set Default";
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // cbbAlignType
            // 
            this.cbbAlignType.FormattingEnabled = true;
            this.cbbAlignType.Items.AddRange(new object[] {
            "TopLeft",
            "LeftTop"});
            this.cbbAlignType.Location = new System.Drawing.Point(474, 75);
            this.cbbAlignType.Name = "cbbAlignType";
            this.cbbAlignType.Size = new System.Drawing.Size(124, 21);
            this.cbbAlignType.TabIndex = 38;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(415, 79);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 13);
            this.label9.TabIndex = 37;
            this.label9.Text = "Align Type";
            // 
            // cbbGroup
            // 
            this.cbbGroup.FormattingEnabled = true;
            this.cbbGroup.Location = new System.Drawing.Point(474, 52);
            this.cbbGroup.Name = "cbbGroup";
            this.cbbGroup.Size = new System.Drawing.Size(124, 21);
            this.cbbGroup.TabIndex = 36;
            this.cbbGroup.Enter += new System.EventHandler(this.cbbGroup_Enter);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(415, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 35;
            this.label8.Text = "Group";
            // 
            // btnSetTabindex
            // 
            this.btnSetTabindex.Location = new System.Drawing.Point(604, 50);
            this.btnSetTabindex.Name = "btnSetTabindex";
            this.btnSetTabindex.Size = new System.Drawing.Size(99, 21);
            this.btnSetTabindex.TabIndex = 34;
            this.btnSetTabindex.Text = "Set TabIndex";
            this.btnSetTabindex.UseVisualStyleBackColor = true;
            this.btnSetTabindex.Click += new System.EventHandler(this.btnSetTabindex_Click);
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
            // cbbScreen
            // 
            this.cbbScreen.FormattingEnabled = true;
            this.cbbScreen.Location = new System.Drawing.Point(284, 75);
            this.cbbScreen.Name = "cbbScreen";
            this.cbbScreen.Size = new System.Drawing.Size(128, 21);
            this.cbbScreen.TabIndex = 33;
            this.cbbScreen.SelectedIndexChanged += new System.EventHandler(this.cbbScreen_SelectedIndexChanged);
            this.cbbScreen.Enter += new System.EventHandler(this.cbbScreen_Enter);
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
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(218, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 32;
            this.label7.Text = "Screen";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(87, 28);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(128, 20);
            this.txtUserName.TabIndex = 23;
            this.txtUserName.Text = "sa";
            // 
            // cbbUserGroup
            // 
            this.cbbUserGroup.FormattingEnabled = true;
            this.cbbUserGroup.Location = new System.Drawing.Point(284, 52);
            this.cbbUserGroup.Name = "cbbUserGroup";
            this.cbbUserGroup.Size = new System.Drawing.Size(128, 21);
            this.cbbUserGroup.TabIndex = 31;
            this.cbbUserGroup.Enter += new System.EventHandler(this.cbbUserGroup_Enter);
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
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(218, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 30;
            this.label6.Text = "User Group";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(87, 51);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(128, 20);
            this.txtPassword.TabIndex = 25;
            this.txtPassword.Text = "abc123";
            // 
            // cbbModule
            // 
            this.cbbModule.FormattingEnabled = true;
            this.cbbModule.Location = new System.Drawing.Point(284, 29);
            this.cbbModule.Name = "cbbModule";
            this.cbbModule.Size = new System.Drawing.Size(128, 21);
            this.cbbModule.TabIndex = 29;
            this.cbbModule.Enter += new System.EventHandler(this.cbbModule_Enter);
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
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(218, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 28;
            this.label5.Text = "Module";
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
            // FormSetTabIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 322);
            this.Controls.Add(this.grcControl);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormSetTabIndex";
            this.Text = "Set Tab Index";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grcControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvControl)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grcControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gvControl;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDefault;
        private System.Windows.Forms.ComboBox cbbAlignType;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbbGroup;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnSetTabindex;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtServerName;
        private System.Windows.Forms.ComboBox cbbScreen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.ComboBox cbbUserGroup;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.ComboBox cbbModule;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbbDatabase;
    }
}

