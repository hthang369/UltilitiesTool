namespace CreateCompany
{
    partial class CompareHiepData
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
            this.panel = new System.Windows.Forms.Panel();
            this.txtField1 = new DevExpress.XtraEditors.MemoEdit();
            this.btnaddfield = new System.Windows.Forms.Button();
            this.cbokeys = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btCompare = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbotable2 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label11 = new System.Windows.Forms.Label();
            this.cbxDatabaseTo = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtServerTo = new System.Windows.Forms.TextBox();
            this.txtUsernameTo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPasswordTo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbotable1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label10 = new System.Windows.Forms.Label();
            this.txtServerFrom = new System.Windows.Forms.TextBox();
            this.cbxDatabaseFrom = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtUsernameFrom = new System.Windows.Forms.TextBox();
            this.txtPasswordFrom = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.gridControlTables = new DevExpress.XtraGrid.GridControl();
            this.gridViewTables = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.label9 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtField1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbokeys.Properties)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbotable2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxDatabaseTo.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbotable1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxDatabaseFrom.Properties)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlTables)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTables)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.txtField1);
            this.panel.Controls.Add(this.btnaddfield);
            this.panel.Controls.Add(this.cbokeys);
            this.panel.Controls.Add(this.label13);
            this.panel.Controls.Add(this.label12);
            this.panel.Controls.Add(this.btCompare);
            this.panel.Controls.Add(this.groupBox2);
            this.panel.Controls.Add(this.groupBox1);
            this.panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(1157, 227);
            this.panel.TabIndex = 0;
            // 
            // txtField1
            // 
            this.txtField1.Location = new System.Drawing.Point(392, 158);
            this.txtField1.Name = "txtField1";
            this.txtField1.Properties.ReadOnly = true;
            this.txtField1.Size = new System.Drawing.Size(278, 49);
            this.txtField1.TabIndex = 27;
            // 
            // btnaddfield
            // 
            this.btnaddfield.Location = new System.Drawing.Point(703, 159);
            this.btnaddfield.Name = "btnaddfield";
            this.btnaddfield.Size = new System.Drawing.Size(29, 23);
            this.btnaddfield.TabIndex = 26;
            this.btnaddfield.Text = "...";
            this.btnaddfield.UseVisualStyleBackColor = true;
            this.btnaddfield.Click += new System.EventHandler(this.btnaddfield_Click);
            // 
            // cbokeys
            // 
            this.cbokeys.Location = new System.Drawing.Point(100, 160);
            this.cbokeys.Name = "cbokeys";
            this.cbokeys.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbokeys.Size = new System.Drawing.Size(186, 20);
            this.cbokeys.TabIndex = 24;
            this.cbokeys.QueryPopUp += new System.ComponentModel.CancelEventHandler(this.cbokeys_QueryPopUp);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(65, 163);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(25, 13);
            this.label13.TabIndex = 25;
            this.label13.Text = "Key";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(357, 166);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 13);
            this.label12.TabIndex = 23;
            this.label12.Text = "Field";
            // 
            // btCompare
            // 
            this.btCompare.Location = new System.Drawing.Point(703, 94);
            this.btCompare.Name = "btCompare";
            this.btCompare.Size = new System.Drawing.Size(97, 23);
            this.btCompare.TabIndex = 21;
            this.btCompare.Text = "Compare";
            this.btCompare.UseVisualStyleBackColor = true;
            this.btCompare.Click += new System.EventHandler(this.btCompare_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbotable2);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.cbxDatabaseTo);
            this.groupBox2.Controls.Add(this.txtServerTo);
            this.groupBox2.Controls.Add(this.txtUsernameTo);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtPasswordTo);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(389, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(284, 147);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Server To";
            // 
            // cbotable2
            // 
            this.cbotable2.Location = new System.Drawing.Point(92, 116);
            this.cbotable2.Name = "cbotable2";
            this.cbotable2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbotable2.Size = new System.Drawing.Size(186, 20);
            this.cbotable2.TabIndex = 23;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(18, 119);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(34, 13);
            this.label11.TabIndex = 22;
            this.label11.Text = "Table";
            // 
            // cbxDatabaseTo
            // 
            this.cbxDatabaseTo.Location = new System.Drawing.Point(92, 90);
            this.cbxDatabaseTo.Name = "cbxDatabaseTo";
            this.cbxDatabaseTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxDatabaseTo.Size = new System.Drawing.Size(186, 20);
            this.cbxDatabaseTo.TabIndex = 15;
            this.cbxDatabaseTo.SelectedIndexChanged += new System.EventHandler(this.cbxDatabaseTo_SelectedIndexChanged);
            this.cbxDatabaseTo.QueryPopUp += new System.ComponentModel.CancelEventHandler(this.cbxDatabaseTo_QueryPopUp);
            // 
            // txtServerTo
            // 
            this.txtServerTo.Location = new System.Drawing.Point(92, 20);
            this.txtServerTo.Name = "txtServerTo";
            this.txtServerTo.Size = new System.Drawing.Size(186, 20);
            this.txtServerTo.TabIndex = 4;
            // 
            // txtUsernameTo
            // 
            this.txtUsernameTo.Location = new System.Drawing.Point(92, 43);
            this.txtUsernameTo.Name = "txtUsernameTo";
            this.txtUsernameTo.Size = new System.Drawing.Size(186, 20);
            this.txtUsernameTo.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 93);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Database";
            // 
            // txtPasswordTo
            // 
            this.txtPasswordTo.Location = new System.Drawing.Point(92, 66);
            this.txtPasswordTo.Name = "txtPasswordTo";
            this.txtPasswordTo.Size = new System.Drawing.Size(186, 20);
            this.txtPasswordTo.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 71);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Password";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "User Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Server Name";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbotable1);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtServerFrom);
            this.groupBox1.Controls.Add(this.cbxDatabaseFrom);
            this.groupBox1.Controls.Add(this.txtUsernameFrom);
            this.groupBox1.Controls.Add(this.txtPasswordFrom);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(8, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(284, 147);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Server From";
            // 
            // cbotable1
            // 
            this.cbotable1.Location = new System.Drawing.Point(92, 116);
            this.cbotable1.Name = "cbotable1";
            this.cbotable1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbotable1.Size = new System.Drawing.Size(186, 20);
            this.cbotable1.TabIndex = 21;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(18, 119);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(34, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "Table";
            // 
            // txtServerFrom
            // 
            this.txtServerFrom.Location = new System.Drawing.Point(92, 20);
            this.txtServerFrom.Name = "txtServerFrom";
            this.txtServerFrom.Size = new System.Drawing.Size(186, 20);
            this.txtServerFrom.TabIndex = 1;
            // 
            // cbxDatabaseFrom
            // 
            this.cbxDatabaseFrom.Location = new System.Drawing.Point(92, 90);
            this.cbxDatabaseFrom.Name = "cbxDatabaseFrom";
            this.cbxDatabaseFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxDatabaseFrom.Size = new System.Drawing.Size(186, 20);
            this.cbxDatabaseFrom.TabIndex = 19;
            this.cbxDatabaseFrom.SelectedIndexChanged += new System.EventHandler(this.cbxDatabaseFrom_SelectedIndexChanged);
            this.cbxDatabaseFrom.QueryPopUp += new System.ComponentModel.CancelEventHandler(this.cbxDatabaseFrom_QueryPopUp);
            // 
            // txtUsernameFrom
            // 
            this.txtUsernameFrom.Location = new System.Drawing.Point(92, 43);
            this.txtUsernameFrom.Name = "txtUsernameFrom";
            this.txtUsernameFrom.Size = new System.Drawing.Size(186, 20);
            this.txtUsernameFrom.TabIndex = 2;
            // 
            // txtPasswordFrom
            // 
            this.txtPasswordFrom.Location = new System.Drawing.Point(92, 66);
            this.txtPasswordFrom.Name = "txtPasswordFrom";
            this.txtPasswordFrom.Size = new System.Drawing.Size(186, 20);
            this.txtPasswordFrom.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Server Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "User Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Database";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Password";
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 538);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1157, 3);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1157, 311);
            this.panel3.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.gridControlTables);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 13);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1157, 298);
            this.panel5.TabIndex = 1;
            // 
            // gridControlTables
            // 
            this.gridControlTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlTables.Location = new System.Drawing.Point(0, 0);
            this.gridControlTables.MainView = this.gridViewTables;
            this.gridControlTables.Name = "gridControlTables";
            this.gridControlTables.Size = new System.Drawing.Size(1157, 298);
            this.gridControlTables.TabIndex = 0;
            this.gridControlTables.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewTables,
            this.gridView1});
            // 
            // gridViewTables
            // 
            this.gridViewTables.GridControl = this.gridControlTables;
            this.gridViewTables.Name = "gridViewTables";
            this.gridViewTables.OptionsView.ShowGroupPanel = false;
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControlTables;
            this.gridView1.Name = "gridView1";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(39, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Tables";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 227);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1157, 311);
            this.panel1.TabIndex = 1;
            // 
            // CompareHiepData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1157, 499);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel);
            this.Name = "CompareHiepData";
            this.Text = "Compare";
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtField1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbokeys.Properties)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbotable2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxDatabaseTo.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbotable1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxDatabaseFrom.Properties)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlTables)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTables)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtServerTo;
        private System.Windows.Forms.TextBox txtUsernameTo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPasswordTo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtServerFrom;
        private System.Windows.Forms.TextBox txtUsernameFrom;
        private System.Windows.Forms.TextBox txtPasswordFrom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Button btCompare;
        private DevExpress.XtraEditors.ComboBoxEdit cbxDatabaseTo;
        private DevExpress.XtraEditors.ComboBoxEdit cbxDatabaseFrom;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private DevExpress.XtraGrid.GridControl gridControlTables;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewTables;
        private System.Windows.Forms.Label label9;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.ComboBoxEdit cbotable2;
        private System.Windows.Forms.Label label11;
        private DevExpress.XtraEditors.ComboBoxEdit cbotable1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private DevExpress.XtraEditors.ComboBoxEdit cbokeys;
        private System.Windows.Forms.Button btnaddfield;
        private DevExpress.XtraEditors.MemoEdit txtField1;
    }
}