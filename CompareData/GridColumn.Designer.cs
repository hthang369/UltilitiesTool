namespace CreateCompany
{
    partial class GridColumn
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnPutSelectedItems = new System.Windows.Forms.Button();
            this.gridControlColumns = new DevExpress.XtraGrid.GridControl();
            this.gridViewColumns = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.label9 = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlColumns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewColumns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(659, 280);
            this.panel3.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnPutSelectedItems);
            this.panel5.Controls.Add(this.gridControlColumns);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 13);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(659, 267);
            this.panel5.TabIndex = 1;
            // 
            // btnPutSelectedItems
            // 
            this.btnPutSelectedItems.Location = new System.Drawing.Point(545, 223);
            this.btnPutSelectedItems.Name = "btnPutSelectedItems";
            this.btnPutSelectedItems.Size = new System.Drawing.Size(70, 22);
            this.btnPutSelectedItems.TabIndex = 1;
            this.btnPutSelectedItems.Text = "ADD";
            this.btnPutSelectedItems.UseVisualStyleBackColor = true;
            this.btnPutSelectedItems.Click += new System.EventHandler(this.btnPutSelectedItems_Click);
            // 
            // gridControlColumns
            // 
            this.gridControlColumns.Dock = System.Windows.Forms.DockStyle.Top;
            this.gridControlColumns.Location = new System.Drawing.Point(0, 0);
            this.gridControlColumns.MainView = this.gridViewColumns;
            this.gridControlColumns.Name = "gridControlColumns";
            this.gridControlColumns.Size = new System.Drawing.Size(659, 204);
            this.gridControlColumns.TabIndex = 0;
            this.gridControlColumns.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewColumns,
            this.gridView1});
            // 
            // gridViewColumns
            // 
            this.gridViewColumns.GridControl = this.gridControlColumns;
            this.gridViewColumns.Name = "gridViewColumns";
            this.gridViewColumns.OptionsView.ShowGroupPanel = false;
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControlColumns;
            this.gridView1.Name = "gridView1";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Column";
            // 
            // GridColumn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 280);
            this.Controls.Add(this.panel3);
            this.Name = "GridColumn";
            this.Text = "GridColumn";
            this.Load += new System.EventHandler(this.GridColumn_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlColumns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewColumns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private DevExpress.XtraGrid.GridControl gridControlColumns;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewColumns;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnPutSelectedItems;
    }
}