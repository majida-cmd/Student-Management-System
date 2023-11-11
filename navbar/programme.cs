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
    public partial class programme : Form
    {
        string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + Application.StartupPath + "\\BD-gestion de note- majda alaabouch.accdb;";
        public programme()
        {
            InitializeComponent();
        }

        private void note_Load(object sender, EventArgs e)
        {
            /*LoadDataGridView.LoadFromTableDB(guna2DataGridView1, "SELECT utilisateur.id, utilisateur.nom, utilisateur.prenom, niveau_sc.nom_ns, filiere.nom_f, annee_scolaire.annee, Matiere.matiere, marks.note_exam, marks.note_controle FROM (((((utilisateur INNER JOIN marks ON utilisateur.id = marks.id_etudiant) INNER JOIN programme ON marks.id_programme = programme.id) INNER JOIN niveau_sc ON programme.id_niveauScolaire = niveau_sc.id)   INNER JOIN filiere ON programme.id_filiere = filiere.id)  INNER JOIN annee_scolaire ON programme.id_anneeScolaire = annee_scolaire.id) INNER JOIN Matiere ON programme.id_matiere = Matiere.id");*/
            loadComboniveau();
            loadComboannee();
            loadCombomatiere();
        }
        private void loadComboniveau()
        {
            string query = "SELECT id, nom FROM niveau";
            OleDbDataReader rs = database.ExecuteQuery(query);
            while (rs.Read())
            {
                Niveau niveau = new Niveau();
                niveau.index_niveau = rs[0].ToString();
                niveau.nom_niveau = rs[1].ToString();
                guna2ComboBox8.Items.Add(niveau);
            }
            database.close();
        }
        
        private void loadComboniveauscolaire()
        {
            Niveau n = (Niveau)guna2ComboBox8.SelectedItem;
            String query = "select id,nom_ns, id_niveau from niveau_sc where id_niveau=" + n.index_niveau;
            OleDbDataReader rs = database.ExecuteQuery(query);
            guna2ComboBox9.Items.Clear();
            while (rs.Read())
            {
                Niveau_scolaire nv = new Niveau_scolaire();
                nv.index_ns = rs[0].ToString();
                nv.nom_ns = rs[1].ToString();
                nv.idniveau = rs[2].ToString();
                guna2ComboBox9.Items.Add(nv);
            }
            database.close();
        }
        private void loadCombofiliere()
        {
            Niveau n = (Niveau)guna2ComboBox8.SelectedItem;
            String query = "select id,nom_f, id_niveau from filiere where id_niveau=" + n.index_niveau;
            OleDbDataReader rs = database.ExecuteQuery(query);
            guna2ComboBox10.Items.Clear();
            while (rs.Read())
            {
                Filiere f = new Filiere();
                f.index = rs[0].ToString();
                f.nom = rs[1].ToString();
                f.idniveau = rs[2].ToString();
                guna2ComboBox10.Items.Add(f);
            }
            database.close();
        }
        private void loadComboannee()
        {
            String query = "select id,annee from annee_scolaire";
            OleDbDataReader rs = database.ExecuteQuery(query);
            while (rs.Read())
            {
                Annee_scolaire annee = new Annee_scolaire();
                annee.index = rs[0].ToString();
                annee.nom = rs[1].ToString();
                guna2ComboBox12.Items.Add(annee);
            }
            database.close();
        }
        private void loadCombomatiere()
        {
            String query = "SELECT id, matiere FROM Matiere";
            OleDbDataReader rs = database.ExecuteQuery(query);
            while (rs.Read())
            {
                Matiere mt = new Matiere();
                mt.index = rs[0].ToString(); // Assuming id is the first column
                mt.nom = rs[1].ToString();  // Assuming nom is the second column
                guna2ComboBox11.Items.Add(mt);
            }
            database.close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*loadComboniveauscolaire();
            loadCombofiliere();*/
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*guna2DataGridView1.Rows.Clear();
            Niveau n = (Niveau)rjComboBox1.SelectedItem;
            Niveau_scolaire ns = (Niveau_scolaire)rjComboBox2.SelectedItem;
            Filiere f = (Filiere)rjComboBox3.SelectedItem;
            Annee_scolaire a = (Annee_scolaire)rjComboBox4.SelectedItem;
            matiere m = (matiere)rjComboBox5.SelectedItem;
            LoadDataGridView.LoadFromTableDB(guna2DataGridView1, "SELECT utilisateur.id, utilisateur.nom, utilisateur.prenom, niveau_sc.nom_ns, filiere.nom_f, annee_scolaire.annee, Matiere.matiere, marks.note_exam, marks.note_controle FROM (((((utilisateur INNER JOIN marks ON utilisateur.id = marks.id_etudiant) INNER JOIN programme ON marks.id_programme = programme.id) INNER JOIN niveau_sc ON programme.id_niveauScolaire = niveau_sc.id) INNER JOIN filiere ON programme.id_filiere = filiere.id) INNER JOIN annee_scolaire ON programme.id_anneeScolaire = annee_scolaire.id) INNER JOIN Matiere ON programme.id_matiere = Matiere.id where Matiere.id="+m.index+" and niveau_sc.id="+ns.index_ns+" and filiere.id=" +f.index+" and annee_scolaire.id="+a.index+"");*/
        }

        private void rjComboBox1_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void rjComboBox2_OnSelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panelTitleBar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            /*guna2DataGridView1.Rows.Clear();
            Niveau n = (Niveau)rjComboBox1.SelectedItem;
            Niveau_scolaire ns = (Niveau_scolaire)rjComboBox2.SelectedItem;
            Filiere f = (Filiere)rjComboBox3.SelectedItem;
            Annee_scolaire a = (Annee_scolaire)rjComboBox4.SelectedItem;
            matiere m = (matiere)rjComboBox5.SelectedItem;
            LoadDataGridView.LoadFromTableDB(guna2DataGridView1, "SELECT utilisateur.id, utilisateur.nom, utilisateur.prenom, niveau_sc.nom_ns, filiere.nom_f, annee_scolaire.annee, Matiere.matiere, marks.note_exam, marks.note_controle FROM (((((utilisateur INNER JOIN marks ON utilisateur.id = marks.id_etudiant) INNER JOIN programme ON marks.id_programme = programme.id) INNER JOIN niveau_sc ON programme.id_niveauScolaire = niveau_sc.id) INNER JOIN filiere ON programme.id_filiere = filiere.id) INNER JOIN annee_scolaire ON programme.id_anneeScolaire = annee_scolaire.id) INNER JOIN Matiere ON programme.id_matiere = Matiere.id where Matiere.id=" + m.index + " and niveau_sc.id=" + ns.index_ns + " and filiere.id=" + f.index + " and annee_scolaire.id=" + a.index + "");*/

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                try
                {
                    Niveau_scolaire ns = (Niveau_scolaire)guna2ComboBox9.SelectedItem;
                    Filiere f = (Filiere)guna2ComboBox10.SelectedItem;
                    Annee_scolaire ans = (Annee_scolaire)guna2ComboBox12.SelectedItem;
                    Matiere mt = (Matiere)guna2ComboBox11.SelectedItem;

                    string query = "INSERT INTO programme (id_niveauScolaire, id_filiere, id_matiere, id_anneeScolaire, coeff_exam, coeff_controle) " +
                                   "VALUES (@nsIndex, @fIndex, @ansIndex, @mtIndex, @coeffExam, @coeffControle)";

                    using (var cmd = new OleDbCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@nsIndex", ns.index_ns);
                        cmd.Parameters.AddWithValue("@fIndex", f.index);
                        cmd.Parameters.AddWithValue("@ansIndex", ans.index);
                        cmd.Parameters.AddWithValue("@mtIndex", mt.index);
                        cmd.Parameters.AddWithValue("@coeffExam", guna2TextBox4.Text);
                        cmd.Parameters.AddWithValue("@coeffControle", guna2TextBox3.Text);

                        int r = cmd.ExecuteNonQuery();

                        if (r == 1)
                        {
                            MessageBox.Show("Programme est bien ajouté.");
                        }
                        else
                        {
                            MessageBox.Show("Erreur d'ajout du programme.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }

            }
        }

        private void guna2ComboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadComboniveauscolaire();
            loadCombofiliere();
        }
    }
}
