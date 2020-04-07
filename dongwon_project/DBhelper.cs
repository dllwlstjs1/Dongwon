using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dongwon_project
{
    public class DBhelper
    {
        public static SqlConnection conn = null;
        public static string DBConnString { get; private set; }

        public static bool DBConnCheck = false;

        private static int errBoxCount = 0;

        /// <summary>
        /// 생성자
        /// </summary>
        public DBhelper() { }

        public static SqlConnection DBConn
        {
            get
            {
                if (!ConnectToDB())
                {
                    return null;
                }
                return conn;
            }
        }

        ///<summary>
        ///Database 접속 시도
        ///</summary>
        /// <returns></returns>
        public static bool ConnectToDB()
        {
            if (conn == null)
            {
                //서버명, 초기 DB명, 인증 방법
                DBConnString = String.Format("server={0};database={1};user id={2};password={3}", "203.247.57.156", "Dongwon", "dllwlstjs1", "asd753951");

                conn = new SqlConnection(DBConnString);
            }

            try
            {
                if (!IsDBConnected)
                {
                    conn.Open();

                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        DBConnCheck = true;
                    }
                    else
                    {
                        DBConnCheck = false;
                    }
                }
            }

            catch (SqlException e)
            {
                errBoxCount++;
                if (errBoxCount == 1)
                {
                    MessageBox.Show(e.Message, "DBHelper - ConnectToDB()");
                }
                return false;
            }
            return true;
        }

        ///<summary>
        /// Database Open 여부 확인
        ///</summary>
        public static bool IsDBConnected
        {
            get
            {
                if(conn.State != System.Data.ConnectionState.Open)
                {
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// Database 해제
        /// </summary>
        /// 
        public static void Close()
        {
            if (IsDBConnected)
                DBConn.Close();
        }
    }
}
