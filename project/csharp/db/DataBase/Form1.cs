using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBase
{
    public partial class Form1 : Form
    {
        int count = 3;//登陆次数
        const string DEFAULT_user = "请输入账号";
        const string DEFAULT_password = "请输入密码";
        public Form1()
        {
            InitializeComponent();
            SetDefaultText();
        }

        private   void SetDefaultText()
        {
            textBox1.Text = DEFAULT_user;
            textBox1.ForeColor = Color.Gray;
            textBox2.Text = DEFAULT_password;
            textBox2.ForeColor = Color.Gray;
            textBox3.Focus();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            System.Random random = new Random();
            int result = random.Next(1000,9999);
            string str = result.ToString();
            label4.Text = str;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if(textBox1.Text==DEFAULT_user)
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text))
                SetDefaultText();
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == DEFAULT_password)
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox2.Text))
                SetDefaultText();
        }

        private int  judgeicode()
        {
            if (textBox3.Text != label4.Text)
            {
                return 1;
            }
            else
                return 2;

        }
        


        //先判断验证码，后判断的数据
        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();   //取出账号
            string pw = textBox2.Text.Trim();         //取出密码

            //连接字符串型数据库
            string constr = "Server=.;DataBase=da; Integrated Security=True";
            SqlConnection mycon = new SqlConnection(constr);  //实例化连接对象
            mycon.Open();
            SqlCommand mycom = mycon.CreateCommand();         //创建SQL命令执行对象
            string s1 = "select account,password from register where account='" + username + "' and password='" + pw + "'";                             //编写SQL命令
            mycom.CommandText = s1;                          //执行SQL命令
            SqlDataAdapter myDA = new SqlDataAdapter();        //实例化数据适配器
            myDA.SelectCommand = mycom;                        //让适配器执行SELECT命令
            DataSet myDS = new DataSet();                    //实例化结果数据集
            int n = myDA.Fill(myDS, "register"); //将结果放入数据适配器，返回元祖个数
            if (n != 0)
            {
                if (judgeicode() == 2)
                {
                    MessageBox.Show("欢迎使用！");   //登录成功
                    this.Close();
                }
                else
                {
                    MessageBox.Show("验证码输入错误！");
                    textBox3.Focus();   //光标设置在验证码上
                }
            }
            else
                if (count >=0)
            {
                MessageBox.Show("用户名或密码有错。请重新输入！还有" + count .ToString() + "次机会");
             //   textBox1.Text = "";   //清空账号
                textBox2.Text = "";   //清空密码
                textBox2.Focus();   //光标设置在账号上
                count--;
            }
            else
            {
                MessageBox.Show("你输入的用户名或密码已达三次?将退出程序");
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Login  f3 = new Login();
            this.Hide(); 
            f3.ShowDialog();
            if(f3==null)
            {
                this.Show();
            }
        }
    }
}
