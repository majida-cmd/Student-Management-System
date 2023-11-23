using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace navbar
{
    public partial class etProfile : Form
    {
        private int etudiantId;
        public etProfile(int etudiantId)
        {
            InitializeComponent();
            this.etudiantId = etudiantId;
        }

        private void etProfile_Load(object sender, EventArgs e)
        {
            label4.Visible = false;
            label5.Visible = false;
            guna2HtmlLabel6.Visible = false;
            guna2HtmlLabel9.Visible = false;
            rjButton1.Visible = false;
            rjTextBox1.Visible = false;
            rjTextBox2.Visible = false;
            String query = "SELECT * FROM utilisateur WHERE utilisateur.id="+ etudiantId;
            OleDbDataReader rs = database.ExecuteQuery(query);
            while (rs.Read())
            {
                label2.Text =rs["nom"].ToString();
                label3.Text =rs["prenom"].ToString();
                rjTextBox3.Texts = rs["email"].ToString();
                //rjTextBox3.Text = rs["email"].ToString();
                guna2TextBox4.Text = rs["password"].ToString();
            }
        }

        private void ShowPsw_CheckedChanged(object sender, EventArgs e)
        {
            if (ShowPsw.Checked == true)
            {
                guna2TextBox4.PasswordChar = (char)0;
            }
            else
            {
                guna2TextBox4.PasswordChar = '*';
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void rjTextBox4__TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void rjButton2_Click(object sender, EventArgs e)
        {
            label4.Visible = true;
            label5.Visible = true;
            rjTextBox1.Visible = true;
            rjTextBox2.Visible = true;
            rjButton1.Visible=true;
            if (rjTextBox1.Texts == "" && rjTextBox2.Texts == "")
            {
                guna2HtmlLabel9.Visible = true;
                rjTextBox1.Focus();
                return;
            }
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            String query = "UPDATE utilisateur SET password='"+ rjTextBox2.Text +"' WHERE utilisateur.id="+ etudiantId;
            int r = database.insert(query);
            //textBox1.Text=query;
            guna2TextBox4.Text = rjTextBox2.Texts;
            label4.Visible = false;
            guna2HtmlLabel6.Visible = false;
            guna2HtmlLabel9.Visible = false;
            label5.Visible = false;
            rjTextBox1.Visible = false;
            rjTextBox2.Visible = false;
            rjButton1.Visible=false;
            if (r == 1)
            {
                MessageBox.Show("Modification success");
            }
            else
            {
                MessageBox.Show("erreur de modification");
            }
        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {

        }

        private void rjTextBox2__TextChanged(object sender, EventArgs e)
        {
            guna2TextBox4.Clear();
            guna2TextBox4.Text = rjTextBox2.Texts;
            if (rjTextBox1.Texts != rjTextBox2.Texts)
            {
                guna2HtmlLabel6.Visible = true;
                rjTextBox2.Focus();
                return;
            }
            else
            {
                guna2HtmlLabel6.Visible = false;
            }
        }

        private void rjTextBox1__TextChanged(object sender, EventArgs e)
        {
            guna2HtmlLabel9.Visible = false;
        }

        private void guna2HtmlLabel6_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel9_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel5_Click(object sender, EventArgs e)
        {

        }
    }
}
