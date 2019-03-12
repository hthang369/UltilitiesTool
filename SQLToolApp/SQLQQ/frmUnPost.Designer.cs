namespace SQLQQ
{
    partial class frmUnPost
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
            this.cboDatabase = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnUnPost = new System.Windows.Forms.Button();
            this.txtQuery = new SQLQQ.Util.TextBoxUtil();
            this.btnRun = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cboTable = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewUtil1 = new SQLQQ.FormUtil.DataGridViewUtil();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboDatabase
            // 
            this.cboDatabase.Enabled = false;
            this.cboDatabase.FormattingEnabled = true;
            this.cboDatabase.Location = new System.Drawing.Point(110, 11);
            this.cboDatabase.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboDatabase.Name = "cboDatabase";
            this.cboDatabase.Size = new System.Drawing.Size(260, 28);
            this.cboDatabase.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnUnPost);
            this.panel1.Controls.Add(this.txtQuery);
            this.panel1.Controls.Add(this.btnRun);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cboTable);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cboDatabase);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1148, 114);
            this.panel1.TabIndex = 1;
            // 
            // btnUnPost
            // 
            this.btnUnPost.Location = new System.Drawing.Point(912, 16);
            this.btnUnPost.Name = "btnUnPost";
            this.btnUnPost.Size = new System.Drawing.Size(93, 33);
            this.btnUnPost.TabIndex = 8;
            this.btnUnPost.Text = "Un Post";
            this.btnUnPost.UseVisualStyleBackColor = true;
            this.btnUnPost.Click += new System.EventHandler(this.btnUnPost_Click);
            // 
            // txtQuery
            // 
            this.txtQuery.Location = new System.Drawing.Point(25, 47);
            this.txtQuery.Multiline = true;
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.Size = new System.Drawing.Size(705, 59);
            this.txtQuery.TabIndex = 7;
            // 
            // btnRun
            // 
            this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRun.Location = new System.Drawing.Point(1023, 14);
            this.btnRun.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(112, 35);
            this.btnRun.TabIndex = 5;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(405, 15);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Table";
            // 
            // cboTable
            // 
            this.cboTable.FormattingEnabled = true;
            this.cboTable.Location = new System.Drawing.Point(465, 11);
            this.cboTable.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboTable.Name = "cboTable";
            this.cboTable.Size = new System.Drawing.Size(265, 28);
            this.cboTable.TabIndex = 2;
            this.cboTable.SelectedIndexChanged += new System.EventHandler(this.cboTable_SelectedIndexChanged);
            this.cboTable.Enter += new System.EventHandler(this.cboTable_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Database";
            // 
            // dataGridViewUtil1
            // 
            this.dataGridViewUtil1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewUtil1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewUtil1.Location = new System.Drawing.Point(0, 114);
            this.dataGridViewUtil1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridViewUtil1.Name = "dataGridViewUtil1";
            this.dataGridViewUtil1.Size = new System.Drawing.Size(1148, 388);
            this.dataGridViewUtil1.TabIndex = 2;
            // 
            // frmUnPost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1148, 502);
            this.Controls.Add(this.dataGridViewUtil1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmUnPost";
            this.Text = "Hủy ghi sổ";
            this.Load += new System.EventHandler(this.frmUnPost_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cboDatabase;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboTable;
        private System.Windows.Forms.Label label1;
        private FormUtil.DataGridViewUtil dataGridViewUtil1;
        private Util.TextBoxUtil txtQuery;
        private System.Windows.Forms.Button btnUnPost;
    }
}