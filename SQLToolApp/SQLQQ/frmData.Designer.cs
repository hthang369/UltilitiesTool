using System;
using System.Windows.Forms;

namespace SQLQQ
{
    partial class frmData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmData));
            this.dgrSearch = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.sttRowCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.sttID = new System.Windows.Forms.ToolStripStatusLabel();
            this.sttNo = new System.Windows.Forms.ToolStripStatusLabel();
            this.sttDate = new System.Windows.Forms.ToolStripStatusLabel();
            this.sttName = new System.Windows.Forms.ToolStripStatusLabel();
            this.sttAction = new System.Windows.Forms.ToolStripStatusLabel();
            this.sttColorForeignColumn = new System.Windows.Forms.ToolStripStatusLabel();
            this.sttToolTipText = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cbbOperation = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.sttFrNo = new System.Windows.Forms.ToolStripLabel();
            this.sttFrName = new System.Windows.Forms.ToolStripLabel();
            this.sttFrDate = new System.Windows.Forms.ToolStripLabel();
            this.txtOperation = new System.Windows.Forms.ToolStripTextBox();
            this.lblOperation = new System.Windows.Forms.ToolStripLabel();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgrSearch)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgrSearch
            // 
            this.dgrSearch.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgrSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrSearch.Location = new System.Drawing.Point(0, 0);
            this.dgrSearch.Margin = new System.Windows.Forms.Padding(4);
            this.dgrSearch.Name = "dgrSearch";
            this.dgrSearch.Size = new System.Drawing.Size(985, 330);
            this.dgrSearch.TabIndex = 0;
            this.dgrSearch.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrSearch_CellContentClick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sttRowCount,
            this.sttID,
            this.sttNo,
            this.sttDate,
            this.sttName,
            this.sttAction,
            this.sttColorForeignColumn,
            this.sttToolTipText});
            this.statusStrip1.Location = new System.Drawing.Point(0, 355);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(985, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusStrip1_ItemClicked);
            // 
            // sttRowCount
            // 
            this.sttRowCount.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sttRowCount.Name = "sttRowCount";
            this.sttRowCount.Size = new System.Drawing.Size(45, 17);
            this.sttRowCount.Text = "Count :";
            // 
            // sttID
            // 
            this.sttID.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sttID.Name = "sttID";
            this.sttID.Size = new System.Drawing.Size(25, 17);
            this.sttID.Text = "ID :";
            // 
            // sttNo
            // 
            this.sttNo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sttNo.Name = "sttNo";
            this.sttNo.Size = new System.Drawing.Size(29, 17);
            this.sttNo.Text = "No :";
            // 
            // sttDate
            // 
            this.sttDate.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sttDate.Name = "sttDate";
            this.sttDate.Size = new System.Drawing.Size(38, 17);
            this.sttDate.Text = "Date :";
            // 
            // sttName
            // 
            this.sttName.Name = "sttName";
            this.sttName.Size = new System.Drawing.Size(45, 17);
            this.sttName.Text = "Name :";
            // 
            // sttAction
            // 
            this.sttAction.Name = "sttAction";
            this.sttAction.Size = new System.Drawing.Size(71, 17);
            this.sttAction.Text = "l Not Action";
            this.sttAction.Click += new System.EventHandler(this.sttAction_Click);
            // 
            // sttColorForeignColumn
            // 
            this.sttColorForeignColumn.Name = "sttColorForeignColumn";
            this.sttColorForeignColumn.Size = new System.Drawing.Size(103, 17);
            this.sttColorForeignColumn.Text = "Color : Not Action";
            // 
            // sttToolTipText
            // 
            this.sttToolTipText.Name = "sttToolTipText";
            this.sttToolTipText.Size = new System.Drawing.Size(115, 17);
            this.sttToolTipText.Text = "ToolTip : Not Action";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgrSearch);
            this.panel2.Controls.Add(this.toolStrip1);
            this.panel2.Controls.Add(this.statusStrip1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(985, 377);
            this.panel2.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.cbbOperation,
            this.toolStripSeparator1,
            this.toolStripLabel2,
            this.sttFrNo,
            this.sttFrName,
            this.sttFrDate,
            this.txtOperation,
            this.lblOperation});
            this.toolStrip1.Location = new System.Drawing.Point(0, 330);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(985, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(59, 22);
            this.toolStripLabel1.Text = "Operation";
            // 
            // cbbOperation
            // 
            this.cbbOperation.BackColor = System.Drawing.SystemColors.Info;
            this.cbbOperation.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbOperation.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cbbOperation.Name = "cbbOperation";
            this.cbbOperation.Size = new System.Drawing.Size(111, 25);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(74, 22);
            this.toolStripLabel2.Text = "Foreign Data";
            // 
            // sttFrNo
            // 
            this.sttFrNo.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sttFrNo.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.sttFrNo.Name = "sttFrNo";
            this.sttFrNo.Size = new System.Drawing.Size(35, 22);
            this.sttFrNo.Text = "Fr No";
            // 
            // sttFrName
            // 
            this.sttFrName.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sttFrName.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.sttFrName.Name = "sttFrName";
            this.sttFrName.Size = new System.Drawing.Size(50, 22);
            this.sttFrName.Text = "Fr Name";
            // 
            // sttFrDate
            // 
            this.sttFrDate.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sttFrDate.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.sttFrDate.Name = "sttFrDate";
            this.sttFrDate.Size = new System.Drawing.Size(44, 22);
            this.sttFrDate.Text = "Fr Date";
            // 
            // txtOperation
            // 
            this.txtOperation.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.txtOperation.BackColor = System.Drawing.SystemColors.Info;
            this.txtOperation.Enabled = false;
            this.txtOperation.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOperation.ForeColor = System.Drawing.Color.Maroon;
            this.txtOperation.Name = "txtOperation";
            this.txtOperation.Size = new System.Drawing.Size(150, 25);
            // 
            // lblOperation
            // 
            this.lblOperation.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.lblOperation.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOperation.ForeColor = System.Drawing.Color.DarkRed;
            this.lblOperation.Name = "lblOperation";
            this.lblOperation.Size = new System.Drawing.Size(80, 22);
            this.lblOperation.Text = "Not Operation";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // frmData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(985, 377);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmData";
            this.Text = "Information Data";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmData_FormClosing);
            this.Load += new System.EventHandler(this.frmSearch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgrSearch)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgrSearch;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripStatusLabel sttAction;
        private System.Windows.Forms.ToolStripStatusLabel sttRowCount;
        private System.Windows.Forms.ToolStripStatusLabel sttColorForeignColumn;
        private System.Windows.Forms.ToolStripStatusLabel sttToolTipText;
        private System.Windows.Forms.ToolStripStatusLabel sttID;
        private System.Windows.Forms.ToolStripStatusLabel sttName;
        private System.Windows.Forms.ToolStripStatusLabel sttNo;
        private System.Windows.Forms.ToolStripStatusLabel sttDate;
        private System.Windows.Forms.ProgressBar probWaiting;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel sttFrNo;
        private System.Windows.Forms.ToolStripLabel sttFrName;
        private System.Windows.Forms.ToolStripLabel sttFrDate;
        private ToolStripComboBox cbbOperation;
        private ToolStripTextBox txtOperation;
        private ToolStripLabel lblOperation;
        private NotifyIcon notifyIcon1;
    }
}