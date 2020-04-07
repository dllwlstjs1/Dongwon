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
    public partial class Boryu : Form
    {
        private String tmp = "(((학적기초.구분) != '비대상')) AND (((전투편성명부.보류사유) != '각급학교 학생')) AND (((전투편성명부.보류사유) Is Not Null))";
        public Form mainform { get; set; }
        private void button1_Click(object sender, EventArgs e) // 메인폼으로 버튼 클릭시
        {
            this.Close();
            mainform.Focus();
        }
        public Boryu()
        {
            InitializeComponent();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void dGViewBolyu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void form_bolyu_Load(object sender, EventArgs e)
        {
            // 보류자 보기 폼을 열면 학생보규자를 제외한 전체 보류인원을 표시함
            rdb_교직원만.Checked = false;
            rdb_전체보기.Checked = true;
            rdb_직권보류제외.Checked = false;
            rdb_학생만.Checked = false;

            refresh_boluy(); // 선택된 인원에 대해서 데이터를 가지고 옴
        }
        // 선택된 값에 따라 동시 선택이 안되는 버튼 값을 수정해줌
        private void refresh_boluy()
        {

            if (rdb_교직원만.Checked || rdb_전체보기.Checked || rdb_학생만.Checked)
            {
                if (rdb_교직원만.Checked) tmp = "(((학적기초.구분) != '비대상') AND ((학적기초.구분) != '학기초과')) AND (((전투편성명부.보류사유) != '각급학교 학생')) AND (((전투편성명부.보류사유) Is Not Null)) AND (((학적기초.구분) = '교직원'))";
                if (rdb_전체보기.Checked) tmp = "(((학적기초.구분) != '비대상') AND ((학적기초.구분) != '학기초과')) AND (((전투편성명부.보류사유) != '각급학교 학생')) AND (((전투편성명부.보류사유) Is Not Null))";

                if (rdb_학생만.Checked) tmp = "(((학적기초.구분) != '비대상') AND ((학적기초.구분) != '학기초과')) AND (((전투편성명부.보류사유) != '각급학교 학생')) AND (((전투편성명부.보류사유) Is Not Null)) AND (((학적기초.구분) = '학부') OR ((학적기초.구분) = '대학원'))";
                if (rdb_직권보류제외.Checked) tmp += " AND (((국동체.보류적용사항) = '' or (국동체.보류적용사항) is null))";
            }
            else
            {
                rdb_전체보기.Checked = true;
                tmp = "(((학적기초.구분) != '비대상') AND ((학적기초.구분) != '학기초과')) AND (((전투편성명부.보류사유) Is Not Null))";
            }

            lock (DBhelper.DBConn)
            {
                if (!DBhelper.IsDBConnected)
                {
                    MessageBox.Show("Database 연결을 확인하세요.");
                    return;
                }
                else
                {
                    DataTable data = SelectQuery("SELECT 학적기초.구분, 국동체.계급, 국동체.군번, 국동체.성명, 국동체.연차, 국동체.부서학과, 국동체.핸드폰번호, 국동체.보류, " +
                                                 "전투편성명부.보류사유,국동체.보류적용사항, 국동체.비고 " +
                                                 "FROM 학적기초 INNER JOIN(국동체 INNER JOIN 전투편성명부 ON 국동체.군번 = 전투편성명부.군번) ON 학적기초.성명군번 = 국동체.성명군번 " +
                                                 $"WHERE {tmp}");

                    dGViewBolyu.DataSource = data;
                }
            }
        }
        private DataTable SelectQuery(String query)
        {
            lock (DBhelper.DBConn)
            {
                DataTable reader_Data = new DataTable();
                using (SqlCommand select = DBhelper.conn.CreateCommand())
                {
                    select.CommandText = query;
                    try
                    {
                        using (SqlDataReader reader = select.ExecuteReader())
                        {
                            reader_Data.Load(reader);
                            reader.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                DBhelper.Close();
                return reader_Data;
            }
        }
        private void rdb_학생만_Click(object sender, EventArgs e)
        {
            if (rdb_학생만.Checked)
            {
                rdb_학생만.Checked = false;
            }
            else
            {
                rdb_학생만.Checked = true;
                rdb_교직원만.Checked = false;
                rdb_전체보기.Checked = false;
            }
            refresh_boluy();
        }
        private void rdb_교직원만_Click(object sender, EventArgs e)
        {

            if (rdb_교직원만.Checked)
            {
                rdb_교직원만.Checked = false;
            }
            else
            {
                rdb_학생만.Checked = false;
                rdb_교직원만.Checked = true;
                rdb_전체보기.Checked = false;
            }
            refresh_boluy();
        }
        private void rdb_직권보류제외_Click(object sender, EventArgs e)
        {
            if (rdb_직권보류제외.Checked)
            {
                rdb_직권보류제외.Checked = false;
            }
            else
            {
                
                rdb_직권보류제외.Checked = true;
                
                
            }
            refresh_boluy();
        }
        private void rdb_전체보기_Click(object sender, EventArgs e)
        {
            if (!rdb_전체보기.Checked)
            {
                rdb_학생만.Checked = false;
                rdb_교직원만.Checked = false;
                rdb_전체보기.Checked = true;
                rdb_직권보류제외.Checked = false;
            }
            else
            {
                rdb_학생만.Checked = false;
                rdb_교직원만.Checked = false;
                rdb_전체보기.Checked = true;
                rdb_직권보류제외.Checked = false;
            }
            refresh_boluy();
        }
    }
}

