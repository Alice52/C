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
    public partial class Login : Form
    {
        const string DEFAULT_user = "请输入账号";
        const string DEFAULT_password = "请输入密码(大于六位数)";
        const string DEFAULT_nickname = "请输入昵称";
        const string DEFAULT_password2 = "请再次输入密码";


        public Login ()
        {
            InitializeComponent();
            SetDefaultText();
            button1.Enabled = false;
            checkBox1.Focus();
        }

        //public void jud()
        //{
        //    if (this == null)
        //    {
        //        Form1 f3 = new Form1();
        //        f3.ShowDialog();
        //        //this.Close ();
        //    }
        //}

        private  void SetDefaultText()
        {
            textBox1.Text = DEFAULT_user;
            textBox1.ForeColor = Color.Gray;
            textBox2.Text = DEFAULT_password;
            textBox2.ForeColor = Color.Gray;
            textBox3.Text = DEFAULT_password2;
            textBox3.ForeColor = Color.Gray;
            textBox4.Text = DEFAULT_nickname;
            textBox4.ForeColor = Color.Gray;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == DEFAULT_user)
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

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == DEFAULT_password2)
            {
                textBox3.Text = "";
                textBox3.ForeColor = Color.Black;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox3.Text))
                SetDefaultText();
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Text == DEFAULT_nickname)
            {
                textBox4.Text = "";
                textBox4.ForeColor = Color.Black;
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox4.Text))
                SetDefaultText();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            System.Random random = new Random();
            int result = random.Next(1000, 9999);
            string str = result.ToString();
            label6.Text = str;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
                button1.Enabled = true;
            else
                button1.Enabled = false;
        }
        
        private void  password()
        {   
                if (textBox2.Text.Length < 6)
                {
                    MessageBox.Show("输入的密码小于六位！");
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox2.Focus();
                }
                if (textBox2.Text != textBox3.Text)
                {
                    MessageBox.Show("两次输入密码不同！");
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox2.Focus();
                }
        }

        private void button1_Click(object sender, EventArgs e)
        { 
            string username = textBox1.Text.Trim();   //取出账号
            string pw = textBox2.Text.Trim();         //取出密码
            string nn = textBox4.Text.Trim();
            string constr = "Server=.;DataBase=da; Integrated Security=True";
            SqlConnection mycon = new SqlConnection(constr);  //实例化连接对象
            mycon.Open();


            //string s2= "insert into register (account,password,nickname) values('A6','156','zhang')";
            //SqlCommand mycom = new SqlCommand(s2,mycon);         //创建SQL命令执行对象
            //mycom.ExecuteNonQuery();

            int result;
            SqlCommand mycom = mycon.CreateCommand();
            string s1 = "insert into register(account,password,nickname) values('"+username+"','"+pw+"','"+nn+"')";                             //编写SQL命令
            //mycom.CommandText = s1;                          //执行SQL命令
            //result = mycom.ExecuteNonQuery();
            //  MessageBox.Show(s1);

            if (String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrEmpty(textBox2.Text) || String.IsNullOrEmpty(textBox3.Text) || String.IsNullOrEmpty(textBox4.Text) || String.IsNullOrEmpty(textBox5.Text))
            {
                MessageBox.Show("输入信息不全！");
                textBox1.Focus();
            }
            else if (textBox2.Text.Length < 6)
            {
                MessageBox.Show("输入的密码小于六位！");
                textBox2.Text = "";
                textBox3.Text = "";
                textBox2.Focus();
            }
            else if (textBox2.Text != textBox3.Text)
            {
                MessageBox.Show("两次输入密码不同！");
                textBox2.Text = "";
                textBox3.Text = "";
                textBox2.Focus();

            }
            else
            {
                mycom.CommandText = s1;                          //执行SQL命令
                result = mycom.ExecuteNonQuery();
                mycon.Close();

                if (result > 0)
                    MessageBox.Show("注册成功！");
                else
                    MessageBox.Show("注册失败！");

                Form1 f3 = new Form1();
                f3.ShowDialog();
                this.Close ();
            }
        }
    }

}
