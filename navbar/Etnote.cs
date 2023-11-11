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
    public partial class Etnote : Form
    {
        public static OleDbConnection db;
        public static String connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + Application.StartupPath + "\\BD-gestion de note- majda alaabouch.accdb;";
        private int etudiantId;
        public Etnote(int etudiantId)
        {
            InitializeComponent();
            this.etudiantId = etudiantId;
        }

        private void Etnote_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDataGridView.LoadFromTableDB(guna2DataGridView1, "SELECT Matiere.matiere, note_exam, coeff_exam, note_controle, coeff_controle,(programme.coeff_exam * marks.note_exam + programme.coeff_controle * marks.note_controle) FROM (Matiere INNER JOIN Programme ON Matiere.id = programme.id_matiere) INNER JOIN marks  ON marks.id_programme = programme.id Where marks.id_etudiant=" + etudiantId);

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT SUM(programme.coeff_exam * marks.note_exam + programme.coeff_controle * marks.note_controle)/COUNT(programme.coeff_exam * marks.note_exam + programme.coeff_controle * marks.note_controle) as rslt FROM marks INNER JOIN programme ON marks.id_programme = programme.id WHERE marks.id_etudiant=" + etudiantId;
                    /*textBox1.Text = query;*/
                    using (OleDbCommand cmd = new OleDbCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@etudiantId", etudiantId);

                        if (connection.State != ConnectionState.Open)
                        {
                            connection.Open();
                        }

                        decimal rslt = Convert.ToDecimal(cmd.ExecuteScalar());
                        label2.Text = rslt.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
