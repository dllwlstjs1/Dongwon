using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dongwon_project
{
    public partial class Overlab_Check_Namebirth : Form
    {
        public Overlab_Check_Namebirth()
        {
            InitializeComponent();
        }

        public delegate void FormSendDataHandler(int serial_number);
        public event FormSendDataHandler FormSendEvent;

        public DataTable Value { get; set; }

        private void Overlab_Check_Namebirth_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < Value.Rows.Count; i++)
            {
                Overlab_Check.Rows.Add(Value.Rows[i]["성명생일"], Value.Rows[i]["주민등록번호"], Value.Rows[i]["학번"], Value.Rows[i]["핸드폰"]);
            }
        }

        private void Overlab_Check_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            this.FormSendEvent(int.Parse(Value.Rows[e.RowIndex]["연번"].ToString()));
            this.Close();
        }
    }
}
