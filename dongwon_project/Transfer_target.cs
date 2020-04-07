using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace dongwon_project
{
    public partial class Entry_target : Form
    {
        private DataTable dt;
        public delegate void FormSendDataHandler(DataTable serial_data, int type);
        public event FormSendDataHandler FormSendEvent;

        public DataTable Value{ get; set; }
        public int Type { get; set; }

        public Entry_target()
        {
            InitializeComponent();
        }

        private void Entry_target_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Value;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Thread[] th = { new Thread(MakeExcelData)};

            foreach(Thread t in th)
            {
                t.Start();
            }

            foreach (Thread t in th)
            {
                t.Join();
            }

            ExportExcel();
            this.FormSendEvent(Value, Type);
            this.Close();
        }
        private void MakeExcelData()
        {
            if (Type == 0)
            {
                dt = new DataTable("전입요청");
                DataRow row = null;
                dt.Columns.AddRange(new DataColumn[] { new DataColumn("성명", typeof(String)), new DataColumn("주민등록번호", typeof(String)), new DataColumn("군번", typeof(String)), new DataColumn("전출입여부", typeof(String)), new DataColumn("변동일", typeof(String)) });

                for (int i = 0; i < Value.Rows.Count; i++)
                {
                    row = dt.NewRow();
                    row["성명"] = Value.Rows[i]["성명"].ToString();
                    row["주민등록번호"] = Value.Rows[i]["주민등록번호"].ToString().Insert(6, "-");
                    row["군번"] = "";
                    row["전출입여부"] = "전입";
                    row["변동일"] = DateTime.Now.ToString("yyyyMMdd");
                    dt.Rows.Add(row);
                }
            }
        }
        private void ExportExcel()
        {
            try
            {
                DataSet dataset = new DataSet();
                dataset.Tables.Add(dt);

                Excel.Application ap = new Excel.Application();
                Excel.Workbook excelWorkBook = ap.Workbooks.Add();
                foreach (DataTable data in dataset.Tables)
                {
                    Excel.Worksheet ws = excelWorkBook.Sheets.Add();
                    ws.Name = dt.TableName;
                    ws.Columns[2].NumberFormat = "@";

                    for (int columnHeaderIndex = 1; columnHeaderIndex <= dt.Columns.Count; columnHeaderIndex++)
                    {
                        ws.Cells[1, columnHeaderIndex] = dt.Columns[columnHeaderIndex - 1].ColumnName;
                        ws.Cells[1, columnHeaderIndex].Interior.Color = ColorTranslator.ToOle(Color.Yellow);
                    }

                    for (int rowIndex = 0; rowIndex < dt.Rows.Count; rowIndex++)
                    {
                        for (int columnIndex = 0; columnIndex < dt.Columns.Count; columnIndex++)
                        {
                            ws.Cells[rowIndex + 2, columnIndex + 1] = dt.Rows[rowIndex].ItemArray[columnIndex].ToString();
                        }
                    }
                    ws.Columns.AutoFit();
                }

                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                saveFile.Title = "Excel 저장위치 지정";
                saveFile.FileName = "전입요청 올리기";
                saveFile.DefaultExt = "xlsx";
                saveFile.Filter = "Xlsx file(*.xlsx)|*xlsxl|Xls files(*.xls)|*xls";
                saveFile.ShowDialog();

                if (saveFile.FileName.Length > 0)
                {
                    foreach (String filename in saveFile.FileNames)
                    {
                        String savePath = filename;
                        if (Path.GetExtension(savePath) == ".xls")
                        {
                            excelWorkBook.SaveAs(savePath, Excel.XlFileFormat.xlWorkbookNormal);
                        }
                        else if (Path.GetExtension(savePath) == ".xlsx")
                        {
                            excelWorkBook.SaveAs(savePath, Excel.XlFileFormat.xlOpenXMLWorkbook);
                        }
                    }
                    excelWorkBook.Close(true);
                    ap.Quit();
                }
                MessageBox.Show(dt.TableName + " 엑셀파일 생성 완료", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
