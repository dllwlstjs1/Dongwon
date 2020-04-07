namespace dongwon_project
{
    partial class CheckBeforeUpdateScholarly
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Update_button = new System.Windows.Forms.Button();
            this.Cancel_button = new System.Windows.Forms.Button();
            this.Check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Scholarly_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Birth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Scholarly_Tel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Scholarly_Phone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Scholarly_Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Check,
            this.Scholarly_Name,
            this.Birth,
            this.Scholarly_Tel,
            this.Scholarly_Phone,
            this.Scholarly_Email});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(814, 370);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            // 
            // Update_button
            // 
            this.Update_button.Location = new System.Drawing.Point(317, 376);
            this.Update_button.Name = "Update_button";
            this.Update_button.Size = new System.Drawing.Size(75, 23);
            this.Update_button.TabIndex = 1;
            this.Update_button.Text = "업데이트";
            this.Update_button.UseVisualStyleBackColor = true;
            this.Update_button.Click += new System.EventHandler(this.Update_button_Click);
            // 
            // Cancel_button
            // 
            this.Cancel_button.Location = new System.Drawing.Point(424, 376);
            this.Cancel_button.Name = "Cancel_button";
            this.Cancel_button.Size = new System.Drawing.Size(75, 23);
            this.Cancel_button.TabIndex = 2;
            this.Cancel_button.Text = "취소";
            this.Cancel_button.UseVisualStyleBackColor = true;
            this.Cancel_button.Click += new System.EventHandler(this.Cancel_button_Click);
            // 
            // Check
            // 
            this.Check.HeaderText = "";
            this.Check.Name = "Check";
            this.Check.Width = 30;
            // 
            // Scholarly_Name
            // 
            this.Scholarly_Name.HeaderText = "성명";
            this.Scholarly_Name.Name = "Scholarly_Name";
            this.Scholarly_Name.Width = 60;
            // 
            // Birth
            // 
            this.Birth.HeaderText = "생년월일";
            this.Birth.Name = "Birth";
            // 
            // Scholarly_Tel
            // 
            this.Scholarly_Tel.HeaderText = "집전화번호";
            this.Scholarly_Tel.Name = "Scholarly_Tel";
            // 
            // Scholarly_Phone
            // 
            this.Scholarly_Phone.HeaderText = "핸드폰번호";
            this.Scholarly_Phone.Name = "Scholarly_Phone";
            // 
            // Scholarly_Email
            // 
            this.Scholarly_Email.HeaderText = "E-Mail";
            this.Scholarly_Email.Name = "Scholarly_Email";
            this.Scholarly_Email.Width = 200;
            // 
            // CheckBeforeUpdateScholarly
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(814, 411);
            this.Controls.Add(this.Cancel_button);
            this.Controls.Add(this.Update_button);
            this.Controls.Add(this.dataGridView1);
            this.Name = "CheckBeforeUpdateScholarly";
            this.Text = "변경사항";
            this.Load += new System.EventHandler(this.CheckBeforeUpdateScholarly_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button Update_button;
        private System.Windows.Forms.Button Cancel_button;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Check;
        private System.Windows.Forms.DataGridViewTextBoxColumn Scholarly_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Birth;
        private System.Windows.Forms.DataGridViewTextBoxColumn Scholarly_Tel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Scholarly_Phone;
        private System.Windows.Forms.DataGridViewTextBoxColumn Scholarly_Email;
    }
}