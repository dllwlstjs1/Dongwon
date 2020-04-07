using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dongwon_project
{
    class Personal_Information
    {
        public  DataTable db_data { get; private set; }

        public Personal_Information() { }

        public void Input_Data(DataTable data)
        {
            db_data = data;
        }
    }
}
