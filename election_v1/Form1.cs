using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using election_v1.Properties;
using System.Data.SqlClient;

namespace election_v1
{
    public partial class Form1 : Form
    {
        SqlConnection cn;
        SqlCommand cmd;
        //SqlDataReader sdr;
        //SqlDataAdapter sda;

        public Form1()        {
            
            InitializeComponent();
        }

        void UpdateDB(int candID, int candVotes)
        {            
            cn.Open();
            cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update election2021 set candVotes = " +  ++candVotes + " where id = " + candID;
            cmd.ExecuteNonQuery();
            cn.Close();        

        }

        int getCurrentVotes(int candId)
        {
            cn.Open();
            cmd = cn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select candVotes from election2021 where id = " + candId;
            cmd.ExecuteNonQuery();
            //DataTable dt = new DataTable();
            SqlDataReader sdr = cmd.ExecuteReader();
            sdr.Read();
            
            int currNoVotes =  int.Parse(sdr[0].ToString()); //current number of votes
            cn.Close();
            return currNoVotes;
        }
        void disableButtons()
        {
            /*button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;*/
            groupBox2.Enabled = false;
            groupBox2.Text = "Voting Disabled, To Enable Press Ctrl + W";
        }
        void enableButtons()
        {
            /*button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;*/
            groupBox2.Enabled = true;
            groupBox2.Text = "Voting Enabled";

        }
        private void button1_Click(object sender, EventArgs e)
        {
            int candVotes = getCurrentVotes(101);
            UpdateDB(101, candVotes);
            disableButtons();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int candVotes = getCurrentVotes(102);
            UpdateDB(102, candVotes);
            disableButtons();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int candVotes = getCurrentVotes(103);
            UpdateDB(103, candVotes);
            disableButtons();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int candVotes = getCurrentVotes(104);
            UpdateDB(104, candVotes);
            disableButtons();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "sttk1122")
            {
                cn.Open();
                cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from election2021 ORDER BY candVotes desc";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
                
                //cmd.CommandText = "SELECT MAX(candVotes)  FROM election2021";
                /*cmd.CommandText = "SELECT * FROM election2021 ORDER BY candVotes desc";
                cmd.ExecuteNonQuery();
                //DataTable dt2 = new DataTable();
                SqlDataReader sdr = cmd.ExecuteReader();
                sdr.Read();
                String cName = sdr[1].ToString(); //current number of votes
                textBox2.Text = cName;*/
                cn.Close();
                textBox1.Text = "";
            }
            else
            {
                //MessageBox.Show();
                MessageBox.Show("Please enter correct password!!!", "Error",  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\Database.mdf; Integrated Security = True");

            //cn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; Integrated Security = True; database=Database.mdf");
            DateTime current = DateTime.Now;
            this.Text = "St. Thomas Election " + current.Year + "                                                  " +
                "                                                                                              ©Rishikesh Sahani";
            this.MinimumSize = new Size(900, 500);
            this.MaximumSize = new Size(900, 500);
            this.MaximizeBox = false;
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 15F, GraphicsUnit.Pixel);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 15F, GraphicsUnit.Pixel);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.W)
            {
                enableButtons();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

            
            if (textBox1.Text == "sttk1122")
            {
                textBox1.Text = "";
                var confirmResult = MessageBox.Show("Are you sure to reset the database?", "Confirm Reset!!", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    cn.Open();
                    cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "update election2021 set candVotes = 0";
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
            else
            {
                //MessageBox.Show();
                MessageBox.Show("Please enter correct password!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
        }
    }

    public class Election
    {
        public string candName { get; set; }       
        public int candVotes { get; set; }
        public int candID { get; set; }

        public Election(String _candName, int _votes, int _candID )
        {
            candName = _candName;
            candVotes = _votes;
            candID = _candID;
        }
    }
}
