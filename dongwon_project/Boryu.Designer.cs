namespace dongwon_project
{
    partial class Boryu
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.rdb_학생만 = new System.Windows.Forms.RadioButton();
            this.rdb_직권보류제외 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.rdb_전체보기 = new System.Windows.Forms.RadioButton();
            this.rdb_교직원만 = new System.Windows.Forms.RadioButton();
            this.dGViewBolyu = new System.Windows.Forms.DataGridView();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGViewBolyu)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.rdb_학생만);
            this.groupBox2.Controls.Add(this.rdb_직권보류제외);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.rdb_전체보기);
            this.groupBox2.Controls.Add(this.rdb_교직원만);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1264, 56);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1140, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 44);
            this.button1.TabIndex = 13;
            this.button1.Text = "메인 폼으로";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rdb_학생만
            // 
            this.rdb_학생만.AutoCheck = false;
            this.rdb_학생만.AutoSize = true;
            this.rdb_학생만.Checked = true;
            this.rdb_학생만.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.rdb_학생만.ForeColor = System.Drawing.Color.White;
            this.rdb_학생만.Location = new System.Drawing.Point(647, 20);
            this.rdb_학생만.Name = "rdb_학생만";
            this.rdb_학생만.Size = new System.Drawing.Size(77, 20);
            this.rdb_학생만.TabIndex = 12;
            this.rdb_학생만.Text = "학생만";
            this.rdb_학생만.UseVisualStyleBackColor = true;
            this.rdb_학생만.Click += new System.EventHandler(this.rdb_학생만_Click);
            // 
            // rdb_직권보류제외
            // 
            this.rdb_직권보류제외.AutoCheck = false;
            this.rdb_직권보류제외.AutoSize = true;
            this.rdb_직권보류제외.Checked = true;
            this.rdb_직권보류제외.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.rdb_직권보류제외.ForeColor = System.Drawing.Color.White;
            this.rdb_직권보류제외.Location = new System.Drawing.Point(830, 20);
            this.rdb_직권보류제외.Name = "rdb_직권보류제외";
            this.rdb_직권보류제외.Size = new System.Drawing.Size(151, 20);
            this.rdb_직권보류제외.TabIndex = 10;
            this.rdb_직권보류제외.Text = "직권보류자 제외";
            this.rdb_직권보류제외.UseVisualStyleBackColor = true;
            this.rdb_직권보류제외.Click += new System.EventHandler(this.rdb_직권보류제외_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 20F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(40, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "보류자 목록";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // rdb_전체보기
            // 
            this.rdb_전체보기.AutoSize = true;
            this.rdb_전체보기.Checked = true;
            this.rdb_전체보기.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.rdb_전체보기.ForeColor = System.Drawing.Color.White;
            this.rdb_전체보기.Location = new System.Drawing.Point(989, 20);
            this.rdb_전체보기.Name = "rdb_전체보기";
            this.rdb_전체보기.Size = new System.Drawing.Size(100, 20);
            this.rdb_전체보기.TabIndex = 11;
            this.rdb_전체보기.TabStop = true;
            this.rdb_전체보기.Text = "전원 검색";
            this.rdb_전체보기.UseVisualStyleBackColor = true;
            this.rdb_전체보기.Click += new System.EventHandler(this.rdb_전체보기_Click);
            // 
            // rdb_교직원만
            // 
            this.rdb_교직원만.AutoCheck = false;
            this.rdb_교직원만.AutoSize = true;
            this.rdb_교직원만.Checked = true;
            this.rdb_교직원만.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.rdb_교직원만.ForeColor = System.Drawing.Color.White;
            this.rdb_교직원만.Location = new System.Drawing.Point(728, 20);
            this.rdb_교직원만.Name = "rdb_교직원만";
            this.rdb_교직원만.Size = new System.Drawing.Size(94, 20);
            this.rdb_교직원만.TabIndex = 8;
            this.rdb_교직원만.Text = "교직원만";
            this.rdb_교직원만.UseVisualStyleBackColor = true;
            this.rdb_교직원만.Click += new System.EventHandler(this.rdb_교직원만_Click);
            // 
            // dGViewBolyu
            // 
            this.dGViewBolyu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGViewBolyu.Location = new System.Drawing.Point(12, 88);
            this.dGViewBolyu.Name = "dGViewBolyu";
            this.dGViewBolyu.RowTemplate.Height = 23;
            this.dGViewBolyu.Size = new System.Drawing.Size(1264, 941);
            this.dGViewBolyu.TabIndex = 9;
            this.dGViewBolyu.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGViewBolyu_CellContentClick);
            // 
            // form_bolyu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.dGViewBolyu);
            this.Controls.Add(this.groupBox2);
            this.Name = "form_bolyu";
            this.Text = "보류자 관리";
            this.Load += new System.EventHandler(this.form_bolyu_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGViewBolyu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdb_직권보류제외;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rdb_전체보기;
        private System.Windows.Forms.RadioButton rdb_교직원만;
        private System.Windows.Forms.RadioButton rdb_학생만;
        private System.Windows.Forms.DataGridView dGViewBolyu;
        private System.Windows.Forms.Button button1;
    }
}