using navbar.Static_Classes;
using System;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace navbar
{
    public partial class FormLogin : Form
    {
        
        public FormLogin()
        {
            int etudiantId=7;
            InitializeComponent();
            etudiantId = etudiantId;
        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (ShowPsw.Checked == true)
            {
                rjTextBox2.PasswordChar = (char)0;
            }
            else
            {
                rjTextBox2.PasswordChar = '*';
            }
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {

        }
        private void FormLogin_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
                FormBorderStyle = FormBorderStyle.None;
            else
                FormBorderStyle = FormBorderStyle.Sizable;
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {

        }

        private void iconPictureBox5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void iconPictureBox4_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;
        }

        private void iconPictureBox3_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void FormLogin_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void rjButton2_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.rjTextBox1.Texts.Trim()) && !string.IsNullOrEmpty(this.rjTextBox2.Text.Trim()))
            {
                AC.sql = "select * from utilisateur, administrateur where email='" + rjTextBox1.Texts + "'and password='" + rjTextBox2.Text + "' and utilisateur.id=administrateur.idutilisateur";
                ACet.sql = "select * from utilisateur, etudiant where email='" + rjTextBox1.Texts + "'and password='" + rjTextBox2.Text + "' and utilisateur.id=etudiant.idutilisateur";
                int etudiantId = 0;
                AC.cmd.Parameters.Clear();
                AC.cmd.CommandType = CommandType.Text;
                AC.cmd.CommandText = AC.sql;

                if (AC.sql != null)
                {
                    AC.OpenConnection();
                    AC.rd = AC.cmd.ExecuteReader();

                    if (AC.rd.HasRows)
                    {
                        while (AC.rd.Read())
                        {
                            AC.currentFullName = AC.rd[0].ToString() + " " + AC.rd[1].ToString();
                            MessageBox.Show("Welcome" + AC.currentFullName, "Login successed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        this.rjTextBox1.Texts = string.Empty;
                        this.rjTextBox2.Text = string.Empty;

                        this.Hide();

                        FormMainMenu F = new FormMainMenu();
                        F.ShowDialog();
                        F = null;
                        //this.Show();
                    }
                    else
                    {
                        AC.rd.Close();
                        AC.CloseConnection();
                        ACet.cmd.Parameters.Clear();
                        ACet.cmd.CommandType = CommandType.Text;
                        ACet.cmd.CommandText = ACet.sql;

                        if (ACet.sql != null)
                        {
                            ACet.OpenConnection();
                            ACet.rd = ACet.cmd.ExecuteReader();

                            if (ACet.rd.HasRows)
                            {
                                while (ACet.rd.Read())
                                {
                                    etudiantId = ACet.rd.GetInt32(0);
                                    ACet.currentFullName = ACet.rd[0].ToString() + " " + ACet.rd[1].ToString();
                                    MessageBox.Show("Welcome" + ACet.currentFullName, "Login successed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                this.rjTextBox1.Texts = string.Empty;
                                this.rjTextBox2.Text = string.Empty;

                                this.Hide();

                                FormEtudiant a = new FormEtudiant(etudiantId);
                                a.ShowDialog();
                                a = null;
                                //a.Show();
                                //this.Show();
                            }
                            else
                            {
                                MessageBox.Show("Sorry Invalid Email or Password", "Try Again", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                if (this.rjTextBox1.CanSelect)
                                {
                                    this.rjTextBox1.Select();
                                }
                            }
                            ACet.rd.Close();
                            ACet.CloseConnection();
                        }
                    }
                    AC.rd.Close();
                    AC.CloseConnection();
                }
            }
            else
            {
                MessageBox.Show("Please input your Email and Password", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (this.rjTextBox1.CanSelect)
                {
                    this.rjTextBox1.Select();
                }
            }
        }

            

            



        private void ShowPsw_CheckedChanged(object sender, EventArgs e)
        {
            if (ShowPsw.Checked == true)
            {
                rjTextBox2.PasswordChar = (char)0;
            }
            else
            {
                rjTextBox2.PasswordChar = '*';
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
             
        }

        private void panelTitleBar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void rjButton1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void rjTextBox1__TextChanged(object sender, EventArgs e)
        {

        }

        private void iconPictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
