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
    public partial class Overlab_check_Form : Form
    {
        public delegate void FormSendDataHandler(int serial_number);
        public event FormSendDataHandler FormSendEvent;

        public DataTable Value { get; set; }

        public Overlab_check_Form()
        {
            InitializeComponent();
        }

        private void Overlab_check_Form_Load(object sender, EventArgs e)
        {
            for(int i = 0; i< Value.Rows.Count; i++)
            {
                Overlab_check.Rows.Add(Value.Rows[i]["성명"], Value.Rows[i]["학번"], Value.Rows[i]["군번"], Value.Rows[i]["핸드폰"]);
            }
        }

        private void Overlab_check_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.FormSendEvent(int.Parse(Value.Rows[e.RowIndex]["연번"].ToString()));
            this.Close();
        }
    }
}
