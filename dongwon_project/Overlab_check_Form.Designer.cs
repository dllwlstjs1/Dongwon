namespace dongwon_project
{
    partial class Overlab_check_Form
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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.Overlab_check = new System.Windows.Forms.DataGridView();
            this.이름 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.학번 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.군번 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.핸드폰 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.Overlab_check)).BeginInit();
            this.SuspendLayout();
            // 
            // Overlab_check
            // 
            this.Overlab_check.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Overlab_check.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.이름,
            this.학번,
            this.군번,
            this.핸드폰});
            this.Overlab_check.Dock = System.Windows.Forms.DockStyle.Top;
            this.Overlab_check.Location = new System.Drawing.Point(0, 0);
            this.Overlab_check.Name = "Overlab_check";
            this.Overlab_check.RowHeadersVisible = false;
            this.Overlab_check.RowTemplate.Height = 23;
            this.Overlab_check.Size = new System.Drawing.Size(604, 358);
            this.Overlab_check.TabIndex = 0;
            this.Overlab_check.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Overlab_check_CellDoubleClick);
            // 
            // 이름
            // 
            this.이름.HeaderText = "성명";
            this.이름.Name = "이름";
            this.이름.ReadOnly = true;
            this.이름.Width = 150;
            // 
            // 학번
            // 
            this.학번.HeaderText = "학번";
            this.학번.Name = "학번";
            this.학번.ReadOnly = true;
            this.학번.Width = 150;
            // 
            // 군번
            // 
            this.군번.HeaderText = "군번";
            this.군번.Name = "군번";
            this.군번.ReadOnly = true;
            this.군번.Width = 150;
            // 
            // 핸드폰
            // 
            this.핸드폰.HeaderText = "핸드폰";
            this.핸드폰.Name = "핸드폰";
            this.핸드폰.ReadOnly = true;
            this.핸드폰.Width = 150;
            // 
            // Overlab_check_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(604, 361);
            this.Controls.Add(this.Overlab_check);
            this.Name = "Overlab_check_Form";
            this.Text = "중복인원 확인";
            this.Load += new System.EventHandler(this.Overlab_check_Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Overlab_check)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DataGridView Overlab_check;
        private System.Windows.Forms.DataGridViewTextBoxColumn 이름;
        private System.Windows.Forms.DataGridViewTextBoxColumn 학번;
        private System.Windows.Forms.DataGridViewTextBoxColumn 군번;
        private System.Windows.Forms.DataGridViewTextBoxColumn 핸드폰;
    }
}