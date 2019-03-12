namespace SQLQQ.FormUtil
{
    partial class DataGridViewUtil
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.lblRow = new System.Windows.Forms.ToolStripLabel();
            this.lblFillter = new System.Windows.Forms.ToolStripLabel();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.lblCount = new System.Windows.Forms.ToolStripLabel();
            this.timerCount = new SQLQQ.FormUtil.TimerCount();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvResult
            // 
            this.dgvResult.AllowUserToAddRows = false;
            this.dgvResult.AllowUserToDeleteRows = false;
            this.dgvResult.AllowUserToOrderColumns = true;
            this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResult.Location = new System.Drawing.Point(0, 0);
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.ReadOnly = true;
            this.dgvResult.Size = new System.Drawing.Size(755, 341);
            this.dgvResult.TabIndex = 0;
            this.dgvResult.AllowUserToOrderColumnsChanged += new System.EventHandler(this.dgvResult_AllowUserToOrderColumnsChanged);
            this.dgvResult.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvResult_CellFormatting);
            this.dgvResult.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvResult_DataError);
            this.dgvResult.SelectionChanged += new System.EventHandler(this.dgvResult_SelectionChanged);
            this.dgvResult.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvResult_KeyDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Controls.Add(this.timerCount);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 341);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(755, 30);
            this.panel1.TabIndex = 3;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.lblRow,
            this.lblFillter,
            this.progressBar,
            this.lblCount});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(700, 25);
            this.toolStrip1.TabIndex = 9;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(36, 23);
            this.toolStripLabel1.Text = "Row:";
            // 
            // lblRow
            // 
            this.lblRow.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.lblRow.Name = "lblRow";
            this.lblRow.Size = new System.Drawing.Size(51, 23);
            this.lblRow.Text = "{0} / {1}";
            // 
            // lblFillter
            // 
            this.lblFillter.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.lblFillter.Name = "lblFillter";
            this.lblFillter.Size = new System.Drawing.Size(39, 23);
            this.lblFillter.Text = "Fillter";
            // 
            // progressBar
            // 
            this.progressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.progressBar.MarqueeAnimationSpeed = 50;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 22);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            // 
            // lblCount
            // 
            this.lblCount.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.lblCount.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(123, 23);
            this.lblCount.Text = "| {0} | {1} | Count: {2}";
            // 
            // timerCount
            // 
            this.timerCount.AutoSize = true;
            this.timerCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.timerCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timerCount.Location = new System.Drawing.Point(700, 0);
            this.timerCount.Margin = new System.Windows.Forms.Padding(4);
            this.timerCount.Name = "timerCount";
            this.timerCount.Size = new System.Drawing.Size(55, 30);
            this.timerCount.TabIndex = 6;
            // 
            // DataGridViewUtil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvResult);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "DataGridViewUtil";
            this.Size = new System.Drawing.Size(755, 371);
            this.Load += new System.EventHandler(this.DataGridViewUtil_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private TimerCount timerCount;
        public System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel lblRow;
        private System.Windows.Forms.ToolStripLabel lblFillter;
        private System.Windows.Forms.ToolStripLabel lblCount;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
    }
}
