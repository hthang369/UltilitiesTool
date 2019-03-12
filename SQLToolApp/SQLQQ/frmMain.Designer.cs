﻿namespace SQLQQ
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.sQLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.functionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.informationManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.themesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.syncDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setTabIndexMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unPostToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbbDatabase = new System.Windows.Forms.ComboBox();
            this.cbbServer = new System.Windows.Forms.ComboBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.lstFunction = new System.Windows.Forms.ListView();
            this.FunctionList = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.statusStrip1.Location = new System.Drawing.Point(0, 359);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(323, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sQLToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 26);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(323, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // sQLToolStripMenuItem
            // 
            this.sQLToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.editToolStripMenuItem,
            this.delToolStripMenuItem});
            this.sQLToolStripMenuItem.Name = "sQLToolStripMenuItem";
            this.sQLToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.sQLToolStripMenuItem.Text = "SQL";
            this.sQLToolStripMenuItem.Click += new System.EventHandler(this.sQLToolStripMenuItem_Click);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.addToolStripMenuItem.Text = "Add (+)";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // delToolStripMenuItem
            // 
            this.delToolStripMenuItem.Name = "delToolStripMenuItem";
            this.delToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.delToolStripMenuItem.Text = "Del";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.functionsToolStripMenuItem,
            this.informationManagerToolStripMenuItem,
            this.themesToolStripMenuItem,
            this.syncDBToolStripMenuItem,
            this.setTabIndexMenuItem,
            this.unPostToolStripMenuItem,
            this.importConfigToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // functionsToolStripMenuItem
            // 
            this.functionsToolStripMenuItem.Name = "functionsToolStripMenuItem";
            this.functionsToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.functionsToolStripMenuItem.Text = "Functions";
            // 
            // informationManagerToolStripMenuItem
            // 
            this.informationManagerToolStripMenuItem.Name = "informationManagerToolStripMenuItem";
            this.informationManagerToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.informationManagerToolStripMenuItem.Text = "Information Manager";
            // 
            // themesToolStripMenuItem
            // 
            this.themesToolStripMenuItem.Name = "themesToolStripMenuItem";
            this.themesToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.themesToolStripMenuItem.Text = "Themes";
            // 
            // syncDBToolStripMenuItem
            // 
            this.syncDBToolStripMenuItem.Name = "syncDBToolStripMenuItem";
            this.syncDBToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.syncDBToolStripMenuItem.Text = "SyncDB";
            this.syncDBToolStripMenuItem.Click += new System.EventHandler(this.syncDBToolStripMenuItem_Click);
            // 
            // setTabIndexMenuItem
            // 
            this.setTabIndexMenuItem.Name = "setTabIndexMenuItem";
            this.setTabIndexMenuItem.Size = new System.Drawing.Size(187, 22);
            this.setTabIndexMenuItem.Text = "Set TabIndex";
            this.setTabIndexMenuItem.Click += new System.EventHandler(this.setTabIndexMenuItem_Click);
            // 
            // unPostToolStripMenuItem
            // 
            this.unPostToolStripMenuItem.Name = "unPostToolStripMenuItem";
            this.unPostToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.unPostToolStripMenuItem.Text = "UnPost";
            this.unPostToolStripMenuItem.Click += new System.EventHandler(this.unPostToolStripMenuItem_Click);
            // 
            // importConfigToolStripMenuItem
            // 
            this.importConfigToolStripMenuItem.Name = "importConfigToolStripMenuItem";
            this.importConfigToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.importConfigToolStripMenuItem.Text = "Import Config";
            this.importConfigToolStripMenuItem.Click += new System.EventHandler(this.importConfigToolStripMenuItem_Click);
            // 
            // helpsToolStripMenuItem
            // 
            this.helpsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpsToolStripMenuItem.Name = "helpsToolStripMenuItem";
            this.helpsToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.helpsToolStripMenuItem.Text = "Helps";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbbDatabase);
            this.groupBox1.Controls.Add(this.cbbServer);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(323, 72);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Database";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Server";
            // 
            // cbbDatabase
            // 
            this.cbbDatabase.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbbDatabase.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbbDatabase.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbDatabase.FormattingEnabled = true;
            this.cbbDatabase.Location = new System.Drawing.Point(84, 42);
            this.cbbDatabase.Name = "cbbDatabase";
            this.cbbDatabase.Size = new System.Drawing.Size(231, 23);
            this.cbbDatabase.TabIndex = 1;
            // 
            // cbbServer
            // 
            this.cbbServer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbbServer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbbServer.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbServer.FormattingEnabled = true;
            this.cbbServer.Location = new System.Drawing.Point(84, 13);
            this.cbbServer.Name = "cbbServer";
            this.cbbServer.Size = new System.Drawing.Size(231, 23);
            this.cbbServer.TabIndex = 0;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // lstFunction
            // 
            this.lstFunction.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lstFunction.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.FunctionList});
            this.lstFunction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstFunction.FullRowSelect = true;
            this.lstFunction.GridLines = true;
            this.lstFunction.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstFunction.Location = new System.Drawing.Point(0, 122);
            this.lstFunction.Name = "lstFunction";
            this.lstFunction.Size = new System.Drawing.Size(323, 237);
            this.lstFunction.TabIndex = 4;
            this.lstFunction.UseCompatibleStateImageBehavior = false;
            this.lstFunction.View = System.Windows.Forms.View.Details;
            this.lstFunction.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstFunction_KeyDown);
            // 
            // FunctionList
            // 
            this.FunctionList.Width = 250;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 381);
            this.Controls.Add(this.lstFunction);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Padding = new System.Windows.Forms.Padding(0, 26, 0, 0);
            this.Text = "SQLQQ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this._frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem sQLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbbDatabase;
        private System.Windows.Forms.ComboBox cbbServer;
        private System.Windows.Forms.ToolStripMenuItem functionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem informationManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem themesToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolStripMenuItem syncDBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem delToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setTabIndexMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unPostToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importConfigToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader FunctionList;
        public System.Windows.Forms.ListView lstFunction;
    }
}