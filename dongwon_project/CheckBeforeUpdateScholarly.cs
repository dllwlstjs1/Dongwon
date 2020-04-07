using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dongwon_project
{
    public partial class CheckBeforeUpdateScholarly : Form
    {
        private String columnName;
        private String data_admin;
        private String data_table_col;
        private String data_col;
        private String data;
        private String number;
        public CheckBeforeUpdateScholarly()
        {
            InitializeComponent();
        }

        public DataTable Value { get; set; }
        public int Case { get; set; }
        private void CheckBeforeUpdateScholarly_Load(object sender, EventArgs e)
        {

            if (Case == 1)
            {
                columnName = "변경된_성명";
                this.dataGridView1.Columns.Add("New_Scholarly_Name", columnName);
                this.dataGridView1.Columns[5].Width = 200;
                InputData(columnName);
            }
            else if(Case == 2)
            {
                columnName = "변경된_연락처";
                this.dataGridView1.Columns.Add("New_Scholarly_Tel", columnName);
                this.dataGridView1.Columns[5].Width = 200;
                InputData(columnName);
            }
            else if(Case == 3)
            {
                columnName = "변경된_핸드폰";
                this.dataGridView1.Columns.Add("New_Scholarly_Phone", columnName);
                this.dataGridView1.Columns[5].Width = 200;
                InputData(columnName);
            }
            else if(Case == 4)
            {
                columnName = "변경된_메일";
                this.dataGridView1.Columns.Add("New_Scholarly_Email", columnName);
                this.dataGridView1.Columns[5].Width = 200;
                InputData(columnName);
            }
        }
        private void InputData(String columnName)
        {
            for (int i = 0; i < Value.Rows.Count; i++)
            {
                this.dataGridView1.Rows.Add(false, Value.Rows[i]["성명"], Value.Rows[i]["생년월일"], Value.Rows[i]["연락처"], Value.Rows[i]["핸드폰"], Value.Rows[i]["메일"], Value.Rows[i][columnName]);
            }
        }
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(this.dataGridView1.Rows[e.RowIndex].Cells["Check"].Value.Equals(true))
                this.dataGridView1.Rows[e.RowIndex].Cells["Check"].Value = false;
            else
                this.dataGridView1.Rows[e.RowIndex].Cells["Check"].Value = true;
        }
        private void Update_button_Click(object sender, EventArgs e)
        {
            //------------------------------------Set Table Name-----------------------------------------------------------
            if (Case == 1)
            {
                data_col = "New_Scholarly_Name";
                data_admin = "관리자확인_성명";
                data_table_col = "성명";
            }
            else if (Case == 2)
            {
                data_col = "New_Scholarly_Tel";
                data_admin = "관리자확인_연락처";
                data_table_col = "연락처";
            }
            else if (Case == 3)
            {
                data_col = "New_Scholarly_Phone";
                data_admin = "관리자확인_핸드폰";
                data_table_col = "핸드폰";
            }
            else if (Case == 4)
            {
                data_col = "New_Scholarly_Email";
                data_admin = "관리자확인_메일";
                data_table_col = "메일";
            }                
            //-------------------------------------------------------------------------------------------------------------

            lock (DBhelper.DBConn)
            {
                try
                {
                    for (int i = 0; i < Value.Rows.Count; i++)
                    {
                        if (this.dataGridView1.Rows[i].Cells["Check"].Value.Equals(true))
                        {
                            data = dataGridView1.Rows[i].Cells[data_col].Value.ToString();
                            number = Value.Rows[i]["주민등록번호"].ToString();

                            using (SqlCommand update = DBhelper.conn.CreateCommand())
                            {
                                update.CommandText = $"UPDATE 학적기초 SET {data_table_col} = '{data}', {data_admin} = 'true' where 주민등록번호 = {number}";
                                update.Connection = DBhelper.conn;
                                update.ExecuteNonQuery();
                            }
                        }

                        else if (this.dataGridView1.Rows[i].Cells["Check"].Value.Equals(false))
                        {
                            number = Value.Rows[i]["주민등록번호"].ToString();

                            using (SqlCommand update = DBhelper.conn.CreateCommand())
                            {
                                update.CommandText = $"UPDATE 학적기초 SET {data_admin} = 'true' where 주민등록번호 = {number}";
                                update.Connection = DBhelper.conn;
                                update.ExecuteNonQuery();
                            }
                        }
                    }
                    MessageBox.Show("업데이트가 완료되었습니다.");
                    this.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }              
            }
        }
        private void Cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
