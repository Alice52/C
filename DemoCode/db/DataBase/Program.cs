using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DataBase
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //insertdb();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }




    }
}
//        static void  insertdb()
//        {
//            string constr = "Server=.;DataBase=da; Integrated Security=True";
            
//            SqlConnection con = new SqlConnection(constr);
//            con.Open();
//            string sqlstr = "insert into register (account,password,nickname) values('A5','156','zhang')";
//            SqlCommand cmd = new SqlCommand(sqlstr, con);
                
         
//               cmd.ExecuteNonQuery();
                
//            }











//        /*


//            using (SqlConnection con = new SqlConnection(constr))
//            {
//                string sqlstr= "insert into register (account,password,nickname) values('A4','156','zhang')";
//                using (SqlCommand cmd = new SqlCommand(sqlstr, con))
//                {
//                    con.Open();
//                    result = cmd.ExecuteNonQuery();
//                }
//            }
//            if (result > 0)
//                Console.WriteLine("OK");
//            else
//                Console.WriteLine("error");
//        }*/
//    }
//}
