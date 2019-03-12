namespace SQLQQ
{
    partial class Formtext
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
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.CrystalReportView = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // CrystalReportView
            // 
            this.CrystalReportView.ActiveViewIndex = -1;
            this.CrystalReportView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CrystalReportView.Cursor = System.Windows.Forms.Cursors.Default;
            this.CrystalReportView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CrystalReportView.Location = new System.Drawing.Point(0, 0);
            this.CrystalReportView.Name = "CrystalReportView";
            this.CrystalReportView.Size = new System.Drawing.Size(1037, 511);
            this.CrystalReportView.TabIndex = 0;
            this.CrystalReportView.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "ReportViewer";
            this.reportViewer1.Size = new System.Drawing.Size(396, 246);
            this.reportViewer1.TabIndex = 0;
            // 
            // Formtext
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 293);
            this.Name = "Formtext";
            this.Text = "Formtext";
            this.Load += new System.EventHandler(this.Formtext_Load);
            this.ResumeLayout(false);

        }

        #endregion
        public CrystalDecisions.Windows.Forms.CrystalReportViewer CrystalReportView;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
    }
}