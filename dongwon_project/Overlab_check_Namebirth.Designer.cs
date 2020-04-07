namespace dongwon_project
{
    partial class Overlab_Check_Namebirth
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
            this.Overlab_Check = new System.Windows.Forms.DataGridView();
            this.성명생일 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.주민등록번호 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.학번 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.핸드폰번호 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.Overlab_Check)).BeginInit();
            this.SuspendLayout();
            // 
            // Overlab_Check
            // 
            this.Overlab_Check.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Overlab_Check.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.성명생일,
            this.주민등록번호,
            this.학번,
            this.핸드폰번호});
            this.Overlab_Check.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Overlab_Check.Location = new System.Drawing.Point(0, 0);
            this.Overlab_Check.Name = "Overlab_Check";
            this.Overlab_Check.RowHeadersVisible = false;
            this.Overlab_Check.RowTemplate.Height = 23;
            this.Overlab_Check.Size = new System.Drawing.Size(604, 361);
            this.Overlab_Check.TabIndex = 0;
            this.Overlab_Check.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Overlab_Check_CellDoubleClick_1);
            // 
            // 성명생일
            // 
            this.성명생일.HeaderText = "성명생일";
            this.성명생일.Name = "성명생일";
            this.성명생일.Width = 150;
            // 
            // 주민등록번호
            // 
            this.주민등록번호.HeaderText = "주민등록번호";
            this.주민등록번호.Name = "주민등록번호";
            this.주민등록번호.Width = 150;
            // 
            // 학번
            // 
            this.학번.HeaderText = "학번";
            this.학번.Name = "학번";
            this.학번.Width = 150;
            // 
            // 핸드폰번호
            // 
            this.핸드폰번호.HeaderText = "핸드폰번호";
            this.핸드폰번호.Name = "핸드폰번호";
            this.핸드폰번호.Width = 150;
            // 
            // Overlab_Check_Namebirth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 361);
            this.Controls.Add(this.Overlab_Check);
            this.Name = "Overlab_Check_Namebirth";
            this.Text = "성명생일 중복확인";
            this.Load += new System.EventHandler(this.Overlab_Check_Namebirth_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Overlab_Check)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView Overlab_Check;
        private System.Windows.Forms.DataGridViewTextBoxColumn 성명생일;
        private System.Windows.Forms.DataGridViewTextBoxColumn 주민등록번호;
        private System.Windows.Forms.DataGridViewTextBoxColumn 학번;
        private System.Windows.Forms.DataGridViewTextBoxColumn 핸드폰번호;
    }
}