using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelDataReader;
using Z.Dapper.Plus;

namespace dongwon_project
{
    public partial class Main_Form : Form
    {
        private DataTable db_data = null;
        private DataTable table = new DataTable();

        private int check_row;
        public string filename { get; set; }

        private string Excel03ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
        private string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
        private String select_list = "주민등록번호, 구분, 학기초과여부, 변동일, 병종, '단과대대학원', 전공, 학습과정, 학기제, 이수학기, 학적상태, 학번, 군번, 성명, 검색비대상, 메일수신동의, 메일, 핸드폰, 전출사유, 연번";
        public Main_Form()
        {
            InitializeComponent();
            Datatable_set();
        }
        private void Datatable_set()
        {
            // column을 추가합니다.
            table.Columns.Add("개인차수", typeof(string)); 
            table.Columns.Add("사번/학번", typeof(string));
            table.Columns.Add("훈련선택일자", typeof(string));

            //// 각각의 행에 내용을 입력합니다.
            //table.Rows.Add("ID 1", "제목 1번", "사용중", "2019/03/11", "2019/03/18");
            //table.Rows.Add("ID 2", "제목 2번", "미사용", "2019/03/12", "2019/03/18");
            //table.Rows.Add("ID 3", "제목 3번", "미사용", "2019/03/13", "2019/03/18");
            //table.Rows.Add("ID 4", "제목 4번", "사용중", "2019/03/14", "2019/03/18");

            // 값들이 입력된 테이블을 DataGridView에 입력합니다.
            dataGridView1.DataSource = table;
        }

        //---------------------------클릭시 초기화----------------------------
        private void M_input_Click(object sender, EventArgs e)
        {
            M_input.Clear();
            S_input.Text = "학번";
            N_input.Text = "이름";
            P_input.Text = "핸드폰번호";
        }
        private void S_input_Click(object sender, EventArgs e)
        {
            S_input.Clear();
            M_input.Text = "군번";
            N_input.Text = "이름";
            P_input.Text = "핸드폰번호";
        }
        private void N_input_Click(object sender, EventArgs e)
        {
            N_input.Clear();
            M_input.Text = "군번";
            S_input.Text = "학번";
            P_input.Text = "핸드폰번호";
        }
        private void P_input_Click(object sender, EventArgs e)
        {
            P_input.Clear();
            M_input.Text = "군번";
            S_input.Text = "학번";
            N_input.Text = "이름";
        }
        //-------------------------------------------------------------------

        //--------------------------텍스트 박스 키 입력 이벤트-----------------
        private void M_input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lock (DBhelper.DBConn)
                {
                    if (!DBhelper.IsDBConnected)
                    {
                        MessageBox.Show("Database 연결을 확인하세요.");
                        return;
                    }
                    else
                    {
                        DB_getdata(1);
                    }
                }
            }
        }
        private void S_input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lock (DBhelper.DBConn)
                {
                    if (!DBhelper.IsDBConnected)
                    {
                        MessageBox.Show("Database 연결을 확인하세요.");
                        return;
                    }
                    else
                    {
                        DB_getdata(2);
                    }
                }
            }
        }
        private void N_input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lock (DBhelper.DBConn)
                {
                    if (!DBhelper.IsDBConnected)
                    {
                        MessageBox.Show("Database 연결을 확인하세요.");
                        return;
                    }
                    else
                    {
                        DB_getdata(3);
                    }
                }
            }
        }
        private void P_input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lock (DBhelper.DBConn)
                {
                    if (!DBhelper.IsDBConnected)
                    {
                        MessageBox.Show("Database 연결을 확인하세요.");
                        return;
                    }
                    else
                    {
                        DB_getdata(4);
                    }
                }
            }
        }
        private void DB_getdata(int type)
        {
            String search_text = "";
            String search_column = "";

            //-------------------------DB검색 데이터 설정------------------------------------
            if (type == 1)
            {
                search_text = M_input.Text.Trim();
                search_column = "군번";
            }
            else if (type == 2)
            {
                search_text = S_input.Text.Trim();
                search_column = "학번";
            }
            else if (type == 3)
            {
                search_text = N_input.Text.Trim();
                search_column = "성명";
            }
            else if (type == 4)
            {
                search_text = P_input.Text.Trim();
                search_column = "핸드폰";
            }
            DataTable data = SelectQuery($"Select {select_list} FROM 학적기초 where {search_column}= '{search_text}'");
            if (Overlab_check(data) == false)
            {
                Personal_data_set(data);
            }
        }
        //-------------------------------------------------------------------

        //-------------------------텍스트 박스 값 설정 기본 셋팅---------------
        private void Personal_data_set(DataTable data)
        {
            RRN.Text = data.Rows[0]["주민등록번호"].ToString();
            Belong.Text = data.Rows[0]["소속"].ToString();
            if (Truefalse_check(data.Rows[0]["학기초과여부"].ToString()) == true)
            {
                Excess_Semester.Text = "Yes";
            }
            else
            {
                Excess_Semester.Text = "No";
            }
            Change_date.Text = data.Rows[0]["변동일"].ToString();
            Army_type.Text = data.Rows[0]["병종"].ToString();
            Department.Text = data.Rows[0]["단과대대학원"].ToString();
            Major.Text = data.Rows[0]["학과"].ToString();
            Process.Text = data.Rows[0]["학습과정"].ToString();
            Semester.Text = data.Rows[0]["학기제"].ToString();
            Complition_Semester.Text = data.Rows[0]["이수학기"].ToString();
            State.Text = data.Rows[0]["학적상태"].ToString();
            Student_ID.Text = data.Rows[0]["학번"].ToString();
            Military_ID.Text = data.Rows[0]["군번"].ToString();
            User_Name.Text = data.Rows[0]["성명"].ToString();
            if (Truefalse_check(data.Rows[0]["검색비대상"].ToString()) == true)
            {
                Search_target.Checked = true;
            }
            else
            {
                Search_target.Checked = false;
            }
            if (Truefalse_check(data.Rows[0]["메일수신동의"].ToString()) == true)
            {
                Receive_mail.Checked = true;
            }
            else
            {
                Receive_mail.Checked = false;
            }
            Mail.Text = data.Rows[0]["메일"].ToString();
            Phone_number.Text = data.Rows[0]["핸드폰"].ToString();
            Reason.Items.Add(data.Rows[0]["전출사유"].ToString());
        }
        private bool Truefalse_check(String data)
        {
            if (data == "True")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool Overlab_check(DataTable data)
        {
            if (db_data.Rows.Count > 1)
            {
                Overlab_check_Form form = new Overlab_check_Form();
                form.Value = db_data;
                form.FormSendEvent += new Overlab_check_Form.FormSendDataHandler(DieaseUpdateEventMethod);
                form.ShowDialog();
                return true;
            }
            return false;
        }
        private void DieaseUpdateEventMethod(int serial_number)
        {
            Personal_data_set(SelectQuery($"Select {select_list} FROM 학적기초 where 연번 = {serial_number}"));
        }
        private void Clear_Click(object sender, EventArgs e)
        {
            RRN.Text = "";
            Excess_Semester.Text = "";
            Belong.Text = "";
            Change_date.Text = "";
            Army_type.Text = "";
            Department.Text = "";
            Major.Text = "";
            Process.Text = "";
            Semester.Text = "";
            Complition_Semester.Text = "";
            State.Text = "";
            Student_ID.Text = "";
            Military_ID.Text = "";
            User_Name.Text = "";
            Mail.Text = "";
            Phone_number.Text = "";
            Reason.Items.Clear();
            Search_target.Checked = false;
            Receive_mail.Checked = false;
        }
        //-------------------------------------------------------------------
        private void 신학적자료업데이트ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel Files|*.xls;*.xlsx" })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    String filePath = openFileDialog.FileName;

                    using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                    {
                        //1. Reading Excel file(Extension type define)
                        IExcelDataReader excelReader = Excel_ConnectionString(stream, filePath);

                        //2. DataSet - The result of each spreadsheet will be created in the result.Tables
                        DataSet result = excelReader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            //3. DataSet - Create column names from first row
                            ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true
                            }
                        });
                        DataTableCollection tableCollection = result.Tables;
                        excelReader.Close();

                        DataTable dt = result.Tables[0];

                        if (dt != null)
                        {
                            List<신학적> list = new List<신학적>();
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                신학적 dataset = new 신학적();
                                dataset.구분 = dt.Rows[i]["구분"].ToString();
                                dataset.단과대대학원 = dt.Rows[i]["단과대/대학원"].ToString();
                                dataset.전공 = dt.Rows[i]["전공"].ToString();
                                dataset.학과 = dt.Rows[i]["학과"].ToString();
                                dataset.학습과정 = dt.Rows[i]["학습과정"].ToString();
                                dataset.학번 = dt.Rows[i]["학번"].ToString();
                                dataset.주민등록번호 = dt.Rows[i]["주민등록번호"].ToString();
                                dataset.군번 = DataProcessing(dt.Rows[i]["군번"].ToString());
                                dataset.학년제 = Null_check(dt.Rows[i]["학년제"].ToString());
                                dataset.학기제 = Null_check(dt.Rows[i]["학기제"].ToString());
                                dataset.학년 = Null_check(dt.Rows[i]["학년"].ToString());
                                dataset.이수학기 = Null_check(dt.Rows[i]["이수학기"].ToString());
                                dataset.성명 = dt.Rows[i]["성명"].ToString();
                                dataset.학적상태 = dt.Rows[i]["학적상태"].ToString();
                                dataset.학적사유 = dt.Rows[i]["학적사유"].ToString();
                                dataset.주소 = dt.Rows[i]["주소"].ToString();
                                dataset.연락처 = DataProcessing(dt.Rows[i]["연락처"].ToString());
                                dataset.핸드폰 = DataProcessing(dt.Rows[i]["핸드폰"].ToString());
                                dataset.Email = dt.Rows[i]["E-MAIL"].ToString();
                                dataset.변동일 = DateTime.Now.ToString("yyyyMMdd");
                                dataset.순번 = dt.Rows[i]["순번"].ToString();
                                list.Add(dataset);
                            }

                            try
                            {
                                String tablename = "신학적";

                                //-----------DB테이블 초기화------------------------------------
                                Table_Reset(tablename);
                                //-------------------------------------------------------------

                                DapperPlusManager.Entity<신학적>().Table(tablename);
                                String connectionString = String.Format("server={0};database={1};user id={2};password={3}", "203.247.57.82", "Dongwon", "dllwlstjs1", "asd753951");
                                if (list != null)
                                {
                                    using (IDbConnection db = new SqlConnection(connectionString))
                                    {
                                        db.BulkInsert(list);
                                    }
                                }
                                MessageBox.Show($"{tablename} 자료가 업데이트 되었습니다.");

                                //-------------신학적 데이터 학적기초로 업데이트--------------------------
                                Update_Table_Sin();
                                //-------------임시 테이블 초기화---------------------------------------
                                tableCollection.Clear();

                                MessageBox.Show("모든 자료가 업데이트 되었습니다.");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            };
        }
        private void 전투편성ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel Files|*.xls;*.xlsx" })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    String filePath = openFileDialog.FileName;

                    using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                    {
                        //1. Reading Excel file(Extension type define)
                        IExcelDataReader excelReader = Excel_ConnectionString(stream, filePath);

                        //2. DataSet - The result of each spreadsheet will be created in the result.Tables
                        DataSet result = excelReader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            //3. DataSet - Create column names from first row
                            ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true
                            }
                        });
                        DataTableCollection tableCollection = result.Tables;
                        excelReader.Close();

                        DataTable dt = result.Tables[0];

                        if (dt != null)
                        {
                            List<전투편성> list = new List<전투편성>();
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                전투편성 dataset = new 전투편성();
                                dataset.성명 = dt.Rows[i]["성명"].ToString();
                                dataset.군번 = dt.Rows[i]["군번"].ToString();
                                dataset.소속 = dt.Rows[i]["소속"].ToString();
                                dataset.직책 = dt.Rows[i]["직책"].ToString();
                                dataset.실시_총 = dt.Rows[i]["실시/총(H)"].ToString();
                                dataset.주소 = dt.Rows[i]["주소"].ToString();
                                dataset.집전화번호 = dt.Rows[i]["집전화번호"].ToString();
                                dataset.핸드폰번호 = dt.Rows[i]["핸드폰번호"].ToString();
                                dataset.응소시간 = dt.Rows[i]["응소시간"].ToString();
                                dataset.작전요소 = dt.Rows[i]["작전요소"].ToString();
                                dataset.운용장소 = dt.Rows[i]["운용장소"].ToString();
                                dataset.전역부대 = dt.Rows[i]["전역부대"].ToString();
                                dataset.주특기 = dt.Rows[i]["주특기"].ToString();
                                dataset.연차 = dt.Rows[i]["연차"].ToString();
                                dataset.소집부대 = dt.Rows[i]["소집부대"].ToString();
                                dataset.동원일시 = dt.Rows[i]["동원일시"].ToString();
                                dataset.집결일시 = dt.Rows[i]["집결일시"].ToString();
                                dataset.연번 = dt.Rows[i]["연번"].ToString();
                                dataset.보류구분 = dt.Rows[i]["보류구분"].ToString();
                                dataset.보류사유 = dt.Rows[i]["보류사유"].ToString();
                                dataset.성명군번 = dt.Rows[i]["성명"].ToString() + dt.Rows[i]["군번"].ToString();
                                list.Add(dataset);
                            }

                            try
                            {
                                String tablename = "전투편성명부";

                                //-----------DB테이블 초기화------------------------------------
                                Table_Reset(tablename);
                                //-------------------------------------------------------------

                                DapperPlusManager.Entity<전투편성>().Table(tablename);
                                String connectionString = String.Format("server={0};database={1};user id={2};password={3}", "203.247.57.82", "Dongwon", "dllwlstjs1", "asd753951");
                                if (list != null)
                                {
                                    using (IDbConnection db = new SqlConnection(connectionString))
                                    {
                                        db.BulkInsert(list);
                                    }
                                }
                                MessageBox.Show($"{tablename} 자료가 업데이트 되었습니다.");

                                //-------------임시 테이블 초기화---------------------------------------
                                tableCollection.Clear();
                                //---------------------------------------------------------------------
                                Update_Table_Jun();

                                MessageBox.Show("모든 자료가 업데이트 되었습니다.");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            };
        }
        private void 편성카드ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel Files|*.xls;*.xlsx" })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    String filePath = openFileDialog.FileName;

                    using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                    {
                        //1. Reading Excel file(Extension type define)
                        IExcelDataReader excelReader = Excel_ConnectionString(stream, filePath);

                        //2. DataSet - The result of each spreadsheet will be created in the result.Tables
                        DataSet result = excelReader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            //3. DataSet - Create column names from first row
                            ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true
                            }
                        });
                        DataTableCollection tableCollection = result.Tables;
                        excelReader.Close();

                        DataTable dt = result.Tables[0];

                        if (dt != null)
                        {
                            List<국동체> list = new List<국동체>();
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                국동체 dataset = new 국동체();
                                dataset.성명 = dt.Rows[i]["성명"].ToString();
                                dataset.생년월일 = dt.Rows[i]["생년월일"].ToString();
                                dataset.연차 = int.Parse(dt.Rows[i]["연차"].ToString());
                                dataset.소속 = dt.Rows[i]["소속"].ToString();
                                dataset.직책 = dt.Rows[i]["직책"].ToString();
                                dataset.군별 = dt.Rows[i]["군별"].ToString();
                                dataset.계급 = dt.Rows[i]["계급"].ToString();
                                dataset.동원 = dt.Rows[i]["동원"].ToString();
                                dataset.보류 = dt.Rows[i]["보류"].ToString();
                                dataset.주특기 = dt.Rows[i]["주특기"].ToString();
                                dataset.군번 = dt.Rows[i]["군번"].ToString().Replace("-", "");
                                dataset.주소 = dt.Rows[i]["주소"].ToString();
                                dataset.집전화번호 = DataProcessing(dt.Rows[i]["집전화번호"].ToString());
                                dataset.핸드폰번호 = DataProcessing(dt.Rows[i]["핸드폰번호"].ToString());
                                dataset.직장번호 = DataProcessing(dt.Rows[i]["직장번호"].ToString());
                                dataset.응소시간 = dt.Rows[i]["응소시간"].ToString();
                                dataset.부서학과 = dt.Rows[i]["부서학과"].ToString();
                                dataset.사번학번 = dt.Rows[i]["사번/학번"].ToString();
                                dataset.장기출타지역 = dt.Rows[i]["장기출타지역"].ToString();
                                dataset.장기출타시작일 = dt.Rows[i]["장기출타시작일"].ToString();
                                dataset.장기출타종료일 = dt.Rows[i]["장기출타종료일"].ToString();
                                dataset.동원지정부대 = dt.Rows[i]["동원지정부대"].ToString();
                                dataset.동원훈련하령여부 = dt.Rows[i]["동원훈련하령여부"].ToString();
                                dataset.입영일 = dt.Rows[i]["입영일"].ToString();
                                dataset.전역일 = dt.Rows[i]["전역일"].ToString();
                                dataset.전역부대 = dt.Rows[i]["전역부대"].ToString();
                                dataset.Email = dt.Rows[i]["E-MAIL"].ToString();
                                dataset.전시근로 = dt.Rows[i]["전시근로"].ToString();
                                dataset.보류적용사항 = dt.Rows[i]["보류적용사항"].ToString();
                                dataset.복학대학명 = dt.Rows[i]["복학 대학명"].ToString();
                                dataset.복학년도 = dt.Rows[i]["복학년도"].ToString();
                                dataset.복학예정사항 = dt.Rows[i]["복학 예정사항"].ToString();
                                dataset.계좌번호 = dt.Rows[i]["계좌번호"].ToString();
                                dataset.은행명 = dt.Rows[i]["은행명"].ToString();
                                dataset.비고 = dt.Rows[i]["비고"].ToString();
                                list.Add(dataset);
                            }

                            try
                            {
                                String tablename = "국동체";

                                //-----------DB테이블 초기화------------------------------------
                                Table_Reset(tablename);
                                //-------------------------------------------------------------

                                DapperPlusManager.Entity<국동체>().Table(tablename);
                                String connectionString = String.Format("server={0};database={1};user id={2};password={3}", "203.247.57.82", "Dongwon", "dllwlstjs1", "asd753951");
                                if (list != null)
                                {
                                    using (IDbConnection db = new SqlConnection(connectionString))
                                    {
                                        db.BulkInsert(list);
                                    }
                                }
                                MessageBox.Show($"{tablename} 자료가 업데이트 되었습니다.");

                                Update_Table_Pyun();
                                MessageBox.Show("모든 자료가 업데이트 되었습니다.");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            };
        }
        private void 병력동원소집자명부업데이트ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel Files|*.xls;*.xlsx" })
            {
                DataSet dt = new DataSet();

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    String filePath = openFileDialog.FileName;
                    String fileExtension = Path.GetExtension(filePath);
                    String connectionString = string.Empty;
                    String sheetName = string.Empty;

                    connectionString = Excel_ConnectionString(fileExtension, filePath);

                    using (OleDbConnection con = new OleDbConnection(connectionString))
                    {
                        try
                        {
                            string query = "select [연번], [성명], [생년월일], [군번], [연차], [지정계급], [지정병과/특기], [소집부대], [부대대표번호], [소속], [직책], [증창설,손보], [동원일시], [집결일시], [집결지], [인도/인접지], [전역부대], [이메일], [핸드폰], [전화번호], [주소] from [병력동원 소집자명부$]";

                            OleDbCommand ocmd = new OleDbCommand(query, con);
                            con.Open();
                            OleDbDataAdapter oda = new OleDbDataAdapter(query, con);
                            con.Close();
                            oda.Fill(dt);
                            DataTable Exceldt = dt.Tables[0];
                            Exceldt.AcceptChanges();

                            lock (DBhelper.DBConn)
                            {
                                SqlBulkCopy objbulk = new SqlBulkCopy(DBhelper.conn);
                                //assigning Destination table name      
                                objbulk.DestinationTableName = "동원소집자명부";
                                objbulk.ColumnMappings.Add("연번", "연번");
                                objbulk.ColumnMappings.Add("성명", "성명");
                                objbulk.ColumnMappings.Add("생년월일", "생년월일");
                                objbulk.ColumnMappings.Add("군번", "군번");
                                objbulk.ColumnMappings.Add("연차", "연차");
                                objbulk.ColumnMappings.Add("지정계급", "지정계급");
                                objbulk.ColumnMappings.Add("[지정병과/특기]", "지정병과/특기");
                                objbulk.ColumnMappings.Add("소집부대", "소집부대");
                                objbulk.ColumnMappings.Add("부대대표번호", "부대대표번호");
                                objbulk.ColumnMappings.Add("소속", "소속");
                                objbulk.ColumnMappings.Add("직책", "직책");
                                objbulk.ColumnMappings.Add("[증창설,손보]", "증창설,손보");
                                objbulk.ColumnMappings.Add("동원일시", "동원일시");
                                objbulk.ColumnMappings.Add("집결일시", "집결일시");
                                objbulk.ColumnMappings.Add("집결지", "집결지");
                                objbulk.ColumnMappings.Add("[인도/인접지]", "인도/인접지");
                                objbulk.ColumnMappings.Add("전역부대", "전역부대");
                                objbulk.ColumnMappings.Add("이메일", "이메일");
                                objbulk.ColumnMappings.Add("핸드폰", "핸드폰");
                                objbulk.ColumnMappings.Add("전화번호", "전화번호");
                                objbulk.ColumnMappings.Add("주소", "주소");

                                if (!DBhelper.IsDBConnected)
                                {
                                    MessageBox.Show("Database 연결을 확인하세요.");
                                    return;
                                }
                                else
                                {
                                    SqlCommand delete = new SqlCommand("DELETE FROM 동원소집자명부");
                                    delete.Connection = DBhelper.conn;
                                    delete.ExecuteNonQuery();
                                    MessageBox.Show("Resume Deleted");

                                    objbulk.WriteToServer(Exceldt);
                                    MessageBox.Show("동원소집자명부 자료가 성공적으로 업데이트 되었습니다.");
                                    objbulk.Close();
                                    DBhelper.Close();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }
        private void 전출자명단ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel Files|*.xls;*.xlsx" })
            {
                DataSet dt = new DataSet();

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    String filePath = openFileDialog.FileName;
                    String fileExtension = Path.GetExtension(filePath);
                    String connectionString = string.Empty;
                    String sheetName = string.Empty;

                    connectionString = Excel_ConnectionString(fileExtension, filePath);

                    using (OleDbConnection con = new OleDbConnection(connectionString))
                    {
                        try
                        {
                            string query = "select [과정], [대학], [학과(부)/전공], [학번], [이수학기], [성명], [주민등록번호], [학적사유], [학적], [군번], [연락처], [변동일] from [학생전출자명단$]";

                            OleDbCommand ocmd = new OleDbCommand(query, con);
                            con.Open();
                            OleDbDataAdapter oda = new OleDbDataAdapter(query, con);
                            con.Close();
                            oda.Fill(dt);
                            DataTable Exceldt = dt.Tables[0];
                            Exceldt.AcceptChanges();

                            lock (DBhelper.DBConn)
                            {
                                SqlBulkCopy objbulk = new SqlBulkCopy(DBhelper.conn);
                                //assigning Destination table name      
                                objbulk.DestinationTableName = "전출자_대상명단";
                                objbulk.ColumnMappings.Add("과정", "과정");
                                objbulk.ColumnMappings.Add("대학", "대학");
                                objbulk.ColumnMappings.Add("[학과(부)/전공]", "학과(부)/전공");
                                objbulk.ColumnMappings.Add("학번", "학번");
                                objbulk.ColumnMappings.Add("이수학기", "이수학기");
                                objbulk.ColumnMappings.Add("성명", "성명");
                                objbulk.ColumnMappings.Add("주민등록번호", "주민등록번호");
                                objbulk.ColumnMappings.Add("학적사유", "학적사유");
                                objbulk.ColumnMappings.Add("학적", "학적");
                                objbulk.ColumnMappings.Add("군번", "군번");
                                objbulk.ColumnMappings.Add("연락처", "연락처");
                                objbulk.ColumnMappings.Add("변동일", "변동일");

                                if (!DBhelper.IsDBConnected)
                                {
                                    MessageBox.Show("Database 연결을 확인하세요.");
                                    return;
                                }
                                else
                                {
                                    SqlCommand delete = new SqlCommand("DELETE FROM 전출자_대상명단");
                                    delete.Connection = DBhelper.conn;
                                    delete.ExecuteNonQuery();
                                    MessageBox.Show("Resume Deleted");

                                    objbulk.WriteToServer(Exceldt);
                                    MessageBox.Show("전출자 대상명단 자료가 성공적으로 업데이트 되었습니다.");
                                    objbulk.Close();
                                    DBhelper.Close();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }
        private void 재직자명부ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel Files|*.xls;*.xlsx" })
            {
                DataSet dt = new DataSet();

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    String filePath = openFileDialog.FileName;
                    String fileExtension = Path.GetExtension(filePath);
                    String connectionString = string.Empty;
                    String sheetName = string.Empty;

                    connectionString = Excel_ConnectionString(fileExtension, filePath);

                    using (OleDbConnection con = new OleDbConnection(connectionString))
                    {
                        try
                        {
                            string query = "select [성명], [사번], [주민등록번호], [주소], [대학처], [학과부서], [신분], [직종], [전화번호], [핸드폰], [임용일자] from [교직원재직자명부$]";

                            OleDbCommand ocmd = new OleDbCommand(query, con);
                            con.Open();
                            OleDbDataAdapter oda = new OleDbDataAdapter(query, con);
                            con.Close();
                            oda.Fill(dt);
                            DataTable Exceldt = dt.Tables[0];
                            Exceldt.AcceptChanges();

                            lock (DBhelper.DBConn)
                            {
                                SqlBulkCopy objbulk = new SqlBulkCopy(DBhelper.conn);
                                //assigning Destination table name      
                                objbulk.DestinationTableName = "교직원_재직자명부";
                                objbulk.ColumnMappings.Add("성명", "성명");
                                objbulk.ColumnMappings.Add("사번", "사번");
                                objbulk.ColumnMappings.Add("주민등록번호", "주민등록번호");
                                objbulk.ColumnMappings.Add("주소", "주소");
                                objbulk.ColumnMappings.Add("대학처", "대학처");
                                objbulk.ColumnMappings.Add("학과부서", "학과부서");
                                objbulk.ColumnMappings.Add("신분", "신분");
                                objbulk.ColumnMappings.Add("직종", "직종");
                                objbulk.ColumnMappings.Add("전화번호", "전화번호");
                                objbulk.ColumnMappings.Add("핸드폰", "핸드폰");
                                objbulk.ColumnMappings.Add("임용일자", "임용일자");

                                if (!DBhelper.IsDBConnected)
                                {
                                    MessageBox.Show("Database 연결을 확인하세요.");
                                    return;
                                }
                                else
                                {
                                    SqlCommand delete = new SqlCommand("DELETE FROM 교직원_재직자명부");
                                    delete.Connection = DBhelper.conn;
                                    delete.ExecuteNonQuery();
                                    MessageBox.Show("Resume Deleted");

                                    objbulk.WriteToServer(Exceldt);
                                    MessageBox.Show("교직원 재직자명부 자료가 성공적으로 업데이트 되었습니다.");
                                    objbulk.Close();
                                    DBhelper.Close();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }
        private void 퇴직및임용자명부ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel Files|*.xls;*.xlsx" })
            {
                DataSet dt = new DataSet();

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    String filePath = openFileDialog.FileName;
                    String fileExtension = Path.GetExtension(filePath);
                    String connectionString = string.Empty;
                    String sheetName = string.Empty;

                    connectionString = Excel_ConnectionString(fileExtension, filePath);

                    using (OleDbConnection con = new OleDbConnection(connectionString))
                    {
                        try
                        {
                            string query = "select [재직상태], [사번], [성명], [핸드폰], [변동일자] from [교직원퇴직자명부$]";

                            OleDbCommand ocmd = new OleDbCommand(query, con);
                            con.Open();
                            OleDbDataAdapter oda = new OleDbDataAdapter(query, con);
                            con.Close();
                            oda.Fill(dt);
                            DataTable Exceldt = dt.Tables[0];
                            Exceldt.AcceptChanges();

                            lock (DBhelper.DBConn)
                            {
                                SqlBulkCopy objbulk = new SqlBulkCopy(DBhelper.conn);
                                //assigning Destination table name      
                                objbulk.DestinationTableName = "교직원_퇴직자명부";
                                objbulk.ColumnMappings.Add("재직상태", "재직상태");
                                objbulk.ColumnMappings.Add("사번", "사번");
                                objbulk.ColumnMappings.Add("성명", "성명");
                                objbulk.ColumnMappings.Add("핸드폰", "핸드폰");
                                objbulk.ColumnMappings.Add("변동일자", "변동일자");

                                if (!DBhelper.IsDBConnected)
                                {
                                    MessageBox.Show("Database 연결을 확인하세요.");
                                    return;
                                }
                                else
                                {
                                    SqlCommand delete = new SqlCommand("DELETE FROM 교직원_퇴직자명부");
                                    delete.Connection = DBhelper.conn;
                                    delete.ExecuteNonQuery();
                                    MessageBox.Show("Resume Deleted");

                                    objbulk.WriteToServer(Exceldt);
                                    MessageBox.Show("교직원 퇴직자명부 자료가 성공적으로 업데이트 되었습니다.");
                                    objbulk.Close();
                                    DBhelper.Close();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }
        private void 전입자파일올리기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel Files|*.xls;*.xlsx" })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    String filePath = openFileDialog.FileName;

                    using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                    {
                        //1. Reading Excel file(Extension type define)
                        IExcelDataReader excelReader = Excel_ConnectionString(stream, filePath);

                        //2. DataSet - The result of each spreadsheet will be created in the result.Tables
                        DataSet result = excelReader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            //3. DataSet - Create column names from first row
                            ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true
                            }
                        });
                        DataTableCollection tableCollection = result.Tables;
                        excelReader.Close();

                        DataTable dt = result.Tables[0];

                        if (dt != null)
                        {
                            List<전입자> list = new List<전입자>();
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                전입자 dataset = new 전입자();
                                dataset.학과_전공 = dt.Rows[i]["학과(부)/전공"].ToString();
                                dataset.학번 = dt.Rows[i]["학번"].ToString();
                                dataset.학년_이수학기 = dt.Rows[i]["학년/이수학기"].ToString();
                                dataset.성명 = dt.Rows[i]["성명"].ToString();
                                dataset.주민등록번호 = dt.Rows[i]["주민등록번호"].ToString();
                                dataset.학적사유 = dt.Rows[i]["학적사유"].ToString();
                                dataset.학적상태 = dt.Rows[i]["학적상태"].ToString();
                                dataset.군번 = dt.Rows[i]["군번"].ToString().Replace("-", "");
                                dataset.연락처 = dt.Rows[i]["연락처"].ToString();
                                dataset.변동일 = dt.Rows[i]["변동일"].ToString();
                                list.Add(dataset);
                            }

                            try
                            {
                                String tablename = "전입대상";

                                //-----------DB테이블 초기화------------------------------------
                                Table_Reset(tablename);
                                //-------------------------------------------------------------

                                DapperPlusManager.Entity<전입자>().Table(tablename);
                                String connectionString = String.Format("server={0};database={1};user id={2};password={3}", "203.247.57.82", "Dongwon", "dllwlstjs1", "asd753951");
                                if (list != null)
                                {
                                    using (IDbConnection db = new SqlConnection(connectionString))
                                    {
                                        db.BulkInsert(list);
                                    }
                                }
                                MessageBox.Show($"{tablename} 자료가 업데이트 되었습니다.");
                                Update_Table_Transference();
                                //MessageBox.Show("모든 자료가 업데이트 되었습니다.");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            };
        }
        private void 전출자파일올리기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel Files|*.xls;*.xlsx" })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    String filePath = openFileDialog.FileName;

                    using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                    {
                        //1. Reading Excel file(Extension type define)
                        IExcelDataReader excelReader = Excel_ConnectionString(stream, filePath);

                        //2. DataSet - The result of each spreadsheet will be created in the result.Tables
                        DataSet result = excelReader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            //3. DataSet - Create column names from first row
                            ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true
                            }
                        });
                        DataTableCollection tableCollection = result.Tables;
                        excelReader.Close();

                        DataTable dt = result.Tables[0];

                        if (dt != null)
                        {
                            List<전출자> list = new List<전출자>();
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                전출자 dataset = new 전출자();
                                dataset.과정 = dt.Rows[i]["과정"].ToString();
                                dataset.대학 = dt.Rows[i]["대학"].ToString();
                                dataset.학과_전공 = dt.Rows[i]["학과(부)/전공"].ToString();
                                dataset.학번 = dt.Rows[i]["학번"].ToString();
                                dataset.이수학기 = dt.Rows[i]["이수학기"].ToString();
                                dataset.성명 = dt.Rows[i]["성명"].ToString();
                                dataset.주민등록번호 = dt.Rows[i]["주민등록번호"].ToString();
                                dataset.학적사유 = dt.Rows[i]["학적사유"].ToString();
                                dataset.학적 = dt.Rows[i]["학적"].ToString();
                                dataset.군번 = dt.Rows[i]["군번"].ToString().Replace("-", "");
                                dataset.연락처 = dt.Rows[i]["연락처"].ToString();
                                dataset.변동일 = dt.Rows[i]["변동일"].ToString();
                                list.Add(dataset);
                            }

                            try
                            {
                                String tablename = "전출대상";

                                //-----------DB테이블 초기화------------------------------------
                                Table_Reset(tablename);
                                //-------------------------------------------------------------

                                DapperPlusManager.Entity<전출자>().Table(tablename);
                                String connectionString = String.Format("server={0};database={1};user id={2};password={3}", "203.247.57.82", "Dongwon", "dllwlstjs1", "asd753951");
                                if (list != null)
                                {
                                    using (IDbConnection db = new SqlConnection(connectionString))
                                    {
                                        db.BulkInsert(list);
                                    }
                                }
                                MessageBox.Show($"{tablename} 자료가 업데이트 되었습니다.");
                                Update_Table_Moveout();
                                //MessageBox.Show("모든 자료가 업데이트 되었습니다.");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            };
        }
        private void 보류자보기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Boryu form = new Boryu();
            form.mainform = this;
            form.ShowDialog();
        }
        private void 금전출납부ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Money form = new Money();
            form.ShowDialog();
        }
        private int Null_check(String data)
        {
            if (data == null || data == "")
                return 0;
            else
                return int.Parse(data);
        }
        private IExcelDataReader Excel_ConnectionString(FileStream stream, String filePath)
        {
            IExcelDataReader excelReader;
            //1. Reading Excel file
            if (Path.GetExtension(filePath).ToUpper() == ".XLS")
            {
                //1.1 Reading from a binary Excel file ('97-2003 format; *.xls)
                excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
            }
            else
            {
                //1.2 Reading from a OpenXml Excel file (2007 format; *.xlsx)
                excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            }
            return excelReader;
        }
        private String Excel_ConnectionString(String fileExtension, String filePath)
        {
            String connectionString;
            // 확장자로 구분하여 커넥션 스트링을 가져옮
            switch (fileExtension)
            {
                case ".xls":    //Excel 97-03
                    connectionString = string.Format(Excel03ConString, filePath);
                    return connectionString;
                case ".xlsx":  //Excel 07
                    connectionString = string.Format(Excel07ConString, filePath);
                    return connectionString; ;
            }
            return null;
        }
        private void Table_Reset(String tablename)
        {
            InsertUpdateDeleteQuery($"DELETE FROM {tablename}");
            MessageBox.Show("테이블 초기화 완료");
        }
        private void Update_Table_Sin()
        {
            InsertUpdateDeleteQuery("UPDATE 학적기초 SET 학적기초.구분 = 신학적.구분, 학적기초.단과대대학원 = 신학적.단과대대학원, 학적기초.전공 = 신학적.전공, 학적기초.학과 = 신학적.학과, 학적기초.학습과정 = 신학적.학습과정, 학적기초.학번 = 신학적.학번, 학적기초.주민등록번호 = 신학적.주민등록번호, 학적기초.학년제 = 신학적.학년제, 학적기초.학기제 = 신학적.학기제, 학적기초.학년 = 신학적.학년, 학적기초.이수학기 = 신학적.이수학기, 학적기초.학적상태 = 신학적.학적상태, 학적기초.학적사유 = 신학적.학적사유, 학적기초.변동일 = 신학적.변동일, 학적기초.주소 = 신학적.주소 " +
                                    "FROM 학적기초 INNER JOIN 신학적 ON 학적기초.주민등록번호 = 신학적.주민등록번호 " +
                                    "WHERE 학적기초.주민등록번호 is not null");

            InsertUpdateDeleteQuery("INSERT INTO 학적기초 ( 구분, 단과대대학원, 전공, 학습과정, 학번, 주민등록번호, 학년제, 학기제, 학년, 이수학기, 성명, 학적상태, 학적사유, 주소, 연락처, 핸드폰, 변동일, 학과 ) " +
                                    "SELECT 신학적.구분, 신학적.단과대대학원, 신학적.전공, 신학적.학습과정, 신학적.학번, 신학적.주민등록번호, 신학적.학년제, 신학적.학기제, 신학적.학년, 신학적.이수학기, 신학적.성명, 신학적.학적상태, 신학적.학적사유, 신학적.주소, 신학적.연락처, 신학적.핸드폰, 신학적.변동일, 신학적.학과 " +
                                    "FROM 신학적 LEFT JOIN 학적기초 ON 신학적.주민등록번호 = 학적기초.주민등록번호 WHERE 학적기초.주민등록번호 Is Null");

            MessageBox.Show("테이블 업데이트 완료");

            //------------------------성명 일치여부 확인-------------------------------------------------------------------------------------------------------------------
            OpenDialog("SELECT 학적기초.성명, 학적기초.생년월일, 학적기초.연락처 , 학적기초.핸드폰, 학적기초.주민등록번호, 학적기초.메일, 신학적.성명 as '변경된_성명'" +
                        "FROM 학적기초 LEFT JOIN 신학적 ON 신학적.주민등록번호 = 학적기초.주민등록번호 " +
                        "where 학적기초.성명 != 신학적.성명 and 학적기초.관리자확인_성명 = 'false'", 1);
            //------------------------연락처 일치여부 확인-----------------------------------------------------------------------------------------------------------------
            OpenDialog("SELECT 학적기초.성명, 학적기초.생년월일, 학적기초.연락처 , 학적기초.핸드폰, 학적기초.주민등록번호, 학적기초.메일, 신학적.연락처 as '변경된_연락처'" +
                        "FROM 학적기초 LEFT JOIN 신학적 ON 신학적.주민등록번호 = 학적기초.주민등록번호 " +
                        "where 학적기초.연락처 != 신학적.연락처 and 학적기초.관리자확인_연락처 = 'false'", 2);
            //------------------------핸드폰 번호 일치여부 확인------------------------------------------------------------------------------------------------------------
            OpenDialog("SELECT 학적기초.성명, 학적기초.생년월일, 학적기초.연락처 , 학적기초.핸드폰, 학적기초.주민등록번호, 학적기초.메일, 신학적.핸드폰 as '변경된_핸드폰'" +
                        "FROM 학적기초 LEFT JOIN 신학적 ON 신학적.주민등록번호 = 학적기초.주민등록번호 " +
                        "where 학적기초.핸드폰 != 신학적.핸드폰 and 학적기초.관리자확인_핸드폰 = 'false'", 3);
            //------------------------메일 일치여부 확인-------------------------------------------------------------------------------------------------------------------
            OpenDialog("SELECT 학적기초.성명, 학적기초.생년월일, 학적기초.연락처 , 학적기초.핸드폰, 학적기초.주민등록번호, 학적기초.메일, 신학적.Email as '변경된_메일'" +
                        "FROM 학적기초 LEFT JOIN 신학적 ON 신학적.주민등록번호 = 학적기초.주민등록번호 " +
                        "where 학적기초.메일 != 신학적.Email and 학적기초.관리자확인_메일 = 'false'", 4);
            //------------------------학기제가 0인 사람 학기제 부여---------------------------------------------------------------------------------------------------------
            InsertUpdateDeleteQuery("UPDATE 학적기초 SET 학기제 = 학년제*2 " +
                        "FROM 학적기초 " +
                        "WHERE 학기제 = 0");
            //------------------------학기 초과 여부-----------------------------------------------------------------------------------------------------------------------
            InsertUpdateDeleteQuery("UPDATE 학적기초 SET 학기초과여부 = 'true' " +
                                    "FROM 학적기초 " +
                                    "WHERE 이수학기 >= 학기제");
            //------------------------성명생일 생성------------------------------------------------------------------------------------------------------------------------
            MakeNameBirth(SelectQuery("SELECT 성명, 주민등록번호 " +
                                        "FROM 학적기초 " +
                                        "WHERE 성명생일 is null"));
            //------------------------구분 변경---------------------------------------------------------------------------------------------------------------------------
            String date = DateTime.Now.ToString("yyyyMMdd");
            //학기초과여부
            InsertUpdateDeleteQuery($"UPDATE 학적기초 SET 구분 = '학기초과' " +
                                     "FROM 학적기초 " +
                                     "WHERE 학기초과여부 = 'true'");
            ////비대상여부
            InsertUpdateDeleteQuery($"UPDATE 학적기초 SET 학적기초.구분 = '비대상', 학적기초.변동일 = '{date}' " +
                                     "from 학적기초 LEFT JOIN 신학적 ON 학적기초.주민등록번호 = 신학적.주민등록번호 LEFT JOIN 교직원 ON 학적기초.주민등록번호 = 교직원.주민등록번호 " +
                                     "WHERE ((Not(학적기초.구분) = '비대상') AND ((신학적.주민등록번호) Is Null) AND ((교직원.주민등록번호) Is Null))");
        }
        private void Update_Table_Pyun()
        {
            //---------------------성명군번, 성명생일 생성-------------------------------------------------------------------------------------------
            MakeNameMnumNameBirth(SelectQuery("SELECT 성명, 군번, 생년월일 FROM 국동체"));
            MessageBox.Show("성명군번 및 성명생일이 생성되었습니다.");
            //---------------------지역, 지역구분 생성-------------------------------------------------------------------------------------
            Regional_division(SelectQuery("SELECT 성명군번, 주소 FROM 국동체"));
            MessageBox.Show("지역 및 지역구분이 생성되었습니다.");
            //---------------------계급, 계급세 구분---------------------------------------------------------------------------------------
            Militaryrank_division(SelectQuery("SELECT 성명군번, 계급 FROM 국동체"));
            MessageBox.Show("계급 및 계급세 구분이 생성되었습니다.");

            //성명생일 중복 여부 판단
            Check_Omited_Namebirth(SelectQuery("SELECT 학적기초.성명생일 " +
                                               "FROM 학적기초 INNER JOIN 국동체 on 학적기초.성명생일 = 국동체.성명생일 " +
                                               "WHERE 학적기초.군번 is null or 학적기초.군번 = '' " +
                                               "GROUP BY 학적기초.성명생일 HAVING COUNT (학적기초.성명생일) > 1;"));
            
            InsertUpdateDeleteQuery("UPDATE 학적기초 SET 학적기초.군번 = 국동체.군번, 학적기초.성명 = 국동체.성명, 학적기초.성명생일 = 국동체.성명생일, 학적기초.성명군번 = 국동체.성명군번, 관리자확인_성명 = 'True'" +
                                    "FROM 학적기초 INNER JOIN 국동체 ON 학적기초.핸드폰 = 국동체.핸드폰번호 " +
                                    "WHERE 학적기초.군번 is null or 학적기초.군번 = '';");
            MessageBox.Show("업데이트 완료");

            InsertUpdateDeleteQuery("UPDATE 학적기초 SET 학적기초.성명군번 = 국동체.성명군번 " +
                                    "FROM 학적기초 INNER JOIN 국동체 ON 학적기초.군번 = 국동체.군번 " +
                                    "WHERE 학적기초.군번 is not null or 학적기초.군번 != '';");
            MessageBox.Show("업데이트 완료");

            Check_Omited(SelectQuery("SELECT 학적기초.성명군번 " +
                                     "FROM 학적기초 INNER JOIN 국동체 ON 학적기초.성명군번 = 국동체.성명군번 " +
                                     "WHERE 학적기초.성명군번 is not null or 학적기초.군번 != '' " +
                                     "GROUP BY 학적기초.성명군번 HAVING COUNT (학적기초.성명군번) > 1"));
            MessageBox.Show("테이블 업데이트 완료");
        }
        private void Update_Table_Jun()
        {
            Split_time(SelectQuery("SELECT 실시_총, 성명군번 FROM 전투편성명부 WHERE 성명군번 IS NOT NULL"));
        }
        private void Update_Table_Transference()
        {
            if(SelectQuery("SELECT 전입대상.성명 FROM 전입대상 WHERE NOT EXISTS " +
                        "(SELECT 주민등록번호 FROM 학적기초 WHERE 학적기초.주민등록번호 = 전입대상.주민등록번호)").Rows.Count > 0)
            {
                MessageBox.Show("신학적 자료를 먼저 업데이트 해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            UpdateList(SelectQuery("SELECT 학년_이수학기, 주민등록번호, 학적사유, 학적상태, 군번, 변동일 FROM 전입대상"));

            //------------------------학기 초과 여부-----------------------------------------------------------------------------------------------------------------------
            InsertUpdateDeleteQuery("UPDATE 학적기초 SET 학기초과여부 = 'true' " +
                                    "FROM 학적기초 " +
                                    "WHERE 이수학기 >= 학기제");

            Transfer_target_Form(SelectQuery("SELECT 전입대상.학년_이수학기 as [학년/이수학기], 전입대상.성명, 전입대상.학적사유, 전입대상.학적상태, 전입대상.군번 AS 자기기록군번, 전입대상.연락처, 전입대상.변동일, 학적기초.군번 AS 병무청군번, 학적기초.성명 AS 학적기초성명, 학적기초.학기초과여부, 학적기초.이수학기, 전입대상.학번, 전입대상.주민등록번호, 학적기초.연번 " +
                                                   "FROM(전입대상 INNER JOIN 학적기초 ON 전입대상.주민등록번호 = 학적기초.주민등록번호) LEFT JOIN 국동체 ON 학적기초.주민등록번호 = 국동체.주민등록번호 " +
                                                   "WHERE(((학적기초.학기초과여부) = 'False') AND((학적기초.전입요청) = 'False') AND((국동체.성명)Is Null));"), 0);  //0 = 전입대상, 1 = 전출대상
        }
        private void Update_Table_Moveout()
        {
            InsertUpdateDeleteQuery("UPDATE 학적기초 SET 학적기초.이수학기 = 전출대상.이수학기, 학적기초.학적사유 = 전출대상.학적사유, 학적기초.학적상태 = 전출대상.학적상태, 학적기초.변동일 = 전출대상.변동일 " +
                                    "FROM 학적기초 LEFT JOIN 전출대상 ON 전출대상.주민등록번호 = 학적기초.주민등록번호 " +
                                    "WHERE (학적기초.구분 = '비대상') or (학적기초.구분 != '비대상' and 학적기초.변동일 < 전출대상.변동일");

            //Transfer_target_Form(SelectQuery("SELECT 전입대상.학년_이수학기 as [학년/이수학기], 전입대상.성명, 전입대상.학적사유, 전입대상.학적상태, 전입대상.군번 AS 자기기록군번, 전입대상.연락처, 전입대상.변동일, 학적기초.군번 AS 병무청군번, 학적기초.성명 AS 학적기초성명, 학적기초.학기초과여부, 학적기초.이수학기, 전입대상.학번, 전입대상.주민등록번호, 학적기초.연번 " +
            //                                       "FROM(전입대상 INNER JOIN 학적기초 ON 전입대상.주민등록번호 = 학적기초.주민등록번호) LEFT JOIN 국동체 ON 학적기초.주민등록번호 = 국동체.주민등록번호 " +
            //                                       "WHERE(((학적기초.학기초과여부) = 'False') AND((학적기초.전입요청) = 'False') AND((국동체.성명)Is Null));"), 1);  //0 = 전입대상, 1 = 전출대상
        }
        private void OpenDialog(String query, int type)
        {
            CheckBeforeUpdateScholarly check = new CheckBeforeUpdateScholarly();
            check.Value = SelectQuery(query);
            check.Case = type;
            check.ShowDialog();
        }
        private void MakeNameBirth(DataTable data)
        {
            try
            {
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    String maked = String.Format(data.Rows[i]["성명"].ToString() + data.Rows[i]["주민등록번호"].ToString().Substring(0, 6));
                    String check = data.Rows[i]["주민등록번호"].ToString();

                    InsertUpdateDeleteQuery($"UPDATE 학적기초 SET 성명생일 = '{maked}'" +
                                                "FROM 학적기초 " +
                                            $"WHERE 주민등록번호 = '{check}'");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }                
        }
        private void MakeNameMnumNameBirth(DataTable data)
        {
            try
            {
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    String name = data.Rows[i]["성명"].ToString();
                    String m_num = data.Rows[i]["군번"].ToString();
                    String birth = data.Rows[i]["생년월일"].ToString();
                    String namemnum = String.Format(name + m_num);
                    String namebirth = String.Format(name + birth);

                    InsertUpdateDeleteQuery($"UPDATE 국동체 SET 성명군번 = '{namemnum}', 성명생일 = '{namebirth}' FROM 국동체 WHERE 성명 = '{name}' and 군번 = '{m_num}'");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Regional_division(DataTable data)
        {
            try
            {
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    String namemnum = data.Rows[i]["성명군번"].ToString();
                    String address = data.Rows[i]["주소"].ToString();
                    String[] regional = address.Split(' ');

                    if (regional[0] == "대전광역시")
                    {
                        InsertUpdateDeleteQuery($"UPDATE 국동체 SET 지역 = '{regional[0]}', 지역구분 = '자도' FROM 국동체 WHERE 성명군번 = '{namemnum}'");
                    }
                    else if (regional[0] == "세종특별시" || regional[0] == "청원군" || regional[0] == "청주시" || regional[0] == "보은군" || regional[0] == "옥천군" || regional[0] == "금산군" || regional[0] == "논산시" || regional[0] == "공주시")
                    {
                        InsertUpdateDeleteQuery($"UPDATE 국동체 SET 지역 = '{regional[0]}', 지역구분 = '인접시군' FROM 국동체 WHERE 성명군번 = '{namemnum}'");
                    }
                    else
                    {
                        InsertUpdateDeleteQuery($"UPDATE 국동체 SET 지역 = '{regional[0]}', 지역구분 = '타도' FROM 국동체 WHERE 성명군번 = '{namemnum}'");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Militaryrank_division(DataTable data)
        {
            try
            {
                String namemnum = "";
                String rank = "";
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    namemnum = data.Rows[i]["성명군번"].ToString();
                    rank = data.Rows[i]["계급"].ToString();

                    if (rank == "이병" || rank == "상병" || rank == "병장")
                    {
                        InsertUpdateDeleteQuery($"UPDATE 국동체 SET 계급세구분 = '병', 계급구분 = '병' FROM 국동체 WHERE 성명군번 = '{namemnum}'");
                    }
                    else if (rank == "하사" || rank == "중사" || rank == "상사" || rank == "원사")
                    {
                        InsertUpdateDeleteQuery($"UPDATE 국동체 SET 계급세구분 = '부사관', 계급구분 = '간부' FROM 국동체 WHERE 성명군번 = '{namemnum}'");
                    }
                    else if (rank == "준위")
                    {
                        InsertUpdateDeleteQuery($"UPDATE 국동체 SET 계급세구분 = '준사관', 계급구분 = '간부' FROM 국동체 WHERE 성명군번 = '{namemnum}'");
                    }
                    else
                    {
                        InsertUpdateDeleteQuery($"UPDATE 국동체 SET 계급세구분 = '장교', 계급구분 = '간부' FROM 국동체 WHERE 성명군번 = '{namemnum}'");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Split_time(DataTable data)
        {
            for(int i = 0; i<data.Rows.Count; i++)
            {
                String[] temp = data.Rows[i]["실시_총"].ToString().Split('/');
                int doing = int.Parse(temp[0]);
                int all = int.Parse(temp[1]);
                int dont = doing - all;
                String namemnum = data.Rows[i]["성명군번"].ToString();

                InsertUpdateDeleteQuery($"UPDATE 국동체 SET 실시 = '{doing.ToString()}', 총 = '{all.ToString()}', 미실시 = '{dont.ToString()}' " +
                                        $"WHERE 성명군번 = '{namemnum}'");
            }
            InsertUpdateDeleteQuery("UPDATE 국동체 SET 훈련대상 = CASE WHEN 미실시 < 0 THEN 'true' ELSE 'false' END");
        }
        private void InsertUpdateDeleteQuery(String query)
        {
            lock (DBhelper.DBConn)
            {
                using (SqlCommand command = DBhelper.conn.CreateCommand())
                {
                    command.CommandText = query;
                    command.Connection = DBhelper.conn;
                    check_row = command.ExecuteNonQuery();
                }
                DBhelper.Close();
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

                    using (SqlDataReader reader = select.ExecuteReader())
                    {
                        reader_Data.Load(reader);
                        reader.Close();
                    }
                }
                DBhelper.Close();
                return reader_Data;
            }
        }
        private String DataProcessing(String data)
        {
            String[] temp = data.Split(new String[] { "-", "(", ")", "[", "]", "부", "모", " ", "누나", "부우", "형", "母", "父", "제", "동생" }, StringSplitOptions.RemoveEmptyEntries);

            String text = "";
            for(int i =0; i < temp.Length; i++)
            {
                text += temp[i];
            }
            return text;
        }
        private void Check_Omited(DataTable data)
        {
            if (data.Rows.Count == 0)
            {
                MessageBox.Show("True", "데이터 검사", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Update_Information();
            }
            else
            {
                MessageBox.Show("False", "데이터 검사", MessageBoxButtons.OK, MessageBoxIcon.Error);
                for(int i = 0; i<data.Rows.Count; i++)
                {
                    MessageBox.Show(data.Rows[i]["성명군번"].ToString());
                }
            }
        }
        private void Check_Omited_Namebirth(DataTable data)
        {
            if (data.Rows.Count > 0)
            {
                if(data.Rows.Count > 1)
                {
                    for(int i=0; i<data.Rows.Count; i++)
                    {
                        Overlab_Check_Namebirth form = new Overlab_Check_Namebirth();
                        form.Value = SelectQuery("SELECT 성명생일, 주민등록번호, 학번, 핸드폰, 연번 " +
                                                       "FROM 학적기초 " +
                                                       $"WHERE 성명생일 = '{data.Rows[i]["성명생일"].ToString()}';");
                        form.FormSendEvent += new Overlab_Check_Namebirth.FormSendDataHandler(DieaseUpdateEventMethod_Namebirth);
                        form.ShowDialog();
                    }
                }
                else
                {
                    Overlab_Check_Namebirth form = new Overlab_Check_Namebirth();
                    form.Value = data;
                    form.FormSendEvent += new Overlab_Check_Namebirth.FormSendDataHandler(DieaseUpdateEventMethod_Namebirth);
                    form.ShowDialog();
                }
            }
            else
            {
                InsertUpdateDeleteQuery("UPDATE 학적기초 SET 학적기초.군번 = 국동체.군번 " +
                                        "FROM 학적기초 INNER JOIN 국동체 ON 학적기초.성명생일 = 국동체.성명생일 " +
                                        "WHERE 학적기초.군번 is null or 학적기초.군번 = '';");
            }
        }
        private void DieaseUpdateEventMethod_Namebirth(int serial_number)
        {
            InsertUpdateDeleteQuery("UPDATE 학적기초 SET 학적기초.군번 = 국동체.군번 " +
                                    "FROM 학적기초 INNER JOIN 국동체 ON 학적기초.성명생일 = 국동체.성명생일 " +
                                    $"WHERE (학적기초.연번 = {serial_number}) and (학적기초.군번 is null or 학적기초.군번 = '');");
        }
        private void DieaseUpdateEventMethod_Enroll(DataTable serial_data, int type)
        {
            if(type == 0)
            {
                String date = DateTime.Now.ToString("yyyyMMdd");

                for (int i = 0; i < serial_data.Rows.Count; i++)
                {
                    int key = int.Parse(serial_data.Rows[i]["연번"].ToString());
                    InsertUpdateDeleteQuery($"UPDATE 학적기초 SET 전입요청일자 = {date} where 연번 = {key}");
                }
            }
        }
        private void NotInGuk(DataTable data)
        {
            for (int i = 0; i < data.Rows.Count; i++)
            {
                String temp = data.Rows[i]["전역일"].ToString().Substring(0, 4);
                String year = DateTime.Now.ToString("yyyy");
                int years = int.Parse(year) - int.Parse(temp);
                InsertUpdateDeleteQuery($"UPDATE 학적기초 SET 학적기초.년차 = {years} " +
                                         "FROM 학적기초, 국동체 " +
                                         "WHERE (학적기초.전역일 is not null or 학적기초.전역일 != '') and 학적기초.성명군번 NOT IN (SELECT 성명군번 from 국동체);");
            }
        }
        private void UpdateList(DataTable data)
        {
            전입자 dataset = new 전입자();

            for (int i = 0; i<data.Rows.Count; i++)
            {
                String[] temp = data.Rows[i]["학년_이수학기"].ToString().Split('/');
                String Grade = temp[0];
                String Semester = temp[1];
                dataset.학적사유 = data.Rows[i]["학적사유"].ToString();
                dataset.학적상태 = data.Rows[i]["학적상태"].ToString();
                dataset.군번 = data.Rows[i]["군번"].ToString();
                dataset.변동일 = data.Rows[i]["학적상태"].ToString();
                InsertUpdateDeleteQuery($"UPDATE 학적기초 SET 학년 = '{Grade}', 이수학기 = '{Semester}', 학적사유 = '{dataset.학적사유}', 학적상태 = '{dataset.학적상태}', 군번 = '{dataset.군번}', 변동일 = '{dataset.변동일}'");
            }

        }
        private void Update_Information()
        {
            //--------------------------------------------------------------국동체 DB에 있는 인원들 자료 처리-------------------------------------------------------------------------------------------------------
            InsertUpdateDeleteQuery("UPDATE 학적기초 SET 학적기초.주소 = 국동체.주소, 학적기초.병종 = '예비군', 학적기초.전역일 = 국동체.전역일, 학적기초.년차 = 국동체.연차, 학적기초.은행 = 국동체.은행명, 학적기초.계좌 = 국동체.계좌번호 " +
                                    "FROM 학적기초 INNER JOIN 국동체 ON 학적기초.성명군번 = 국동체.성명군번 " +
                                    "WHERE 학적기초.성명군번 is not null or 학적기초.성명군번 != '';");
            //--------------------------------------------------------------국동체 DB에 없는 인원들 자료 처리-   ------------------------------------------------------------------------------------------------------
            NotInGuk(SelectQuery("SELECT 성명군번, 전역일 FROM 학적기초 WHERE (성명군번 is not null or 성명군번 != '') and (전역일 is not null or 전역일 != '');"));

            //--------------------------------------------------------------학적기초로부터 국동체 업데이트----------------------------------------------------------------------------------------------------------
            InsertUpdateDeleteQuery("UPDATE 국동체 SET 국동체.교직원학생구분 = 학적기초.구분 " +
                                    "FROM 국동체 INNER JOIN 학적기초 ON 학적기초.성명군번 = 국동체.성명군번 " +
                                    "WHERE 국동체.성명군번 is not null or 국동체.성명군번 != '';");
        }
        private void Transfer_target_Form(DataTable data, int type)
        {
            Entry_target entry_target = new Entry_target();
            entry_target.Value = data;
            entry_target.Type = type;
            entry_target.FormSendEvent += new Entry_target.FormSendDataHandler(DieaseUpdateEventMethod_Enroll);
            entry_target.ShowDialog();
        }
    } 
}
