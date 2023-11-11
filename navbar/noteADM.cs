using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace navbar
{

    public partial class noteADM : Form
    {
        public static OleDbConnection db;
        public static String connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + Application.StartupPath + "\\BD-gestion de note- majda alaabouch.accdb;";
        public noteADM()
        {
            InitializeComponent();
        }

        private void guna2ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadprenom();
        }
        private void loadname()
        {
            String query = "select id, nom from utilisateur WHERE id IN (SELECT idutilisateur FROM etudiant)";
            OleDbDataReader rs = database.ExecuteQuery(query);
            while (rs.Read())
            {
                string value = rs["nom"].ToString();
                guna2ComboBox1.Items.Add(value);
            }
            database.close();
        }
        private void loadprenom()
        {
            if (guna2ComboBox1.SelectedItem != null)
            {
                string nom = guna2ComboBox1.SelectedItem.ToString();
                String query = "SELECT id, nom, prenom FROM utilisateur WHERE id IN (SELECT idutilisateur FROM etudiant) and nom = ?";

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    using (OleDbCommand cmd = new OleDbCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@nom", nom);

                        using (OleDbDataReader rs = cmd.ExecuteReader())
                        {
                            guna2ComboBox2.Items.Clear();

                            while (rs.Read())
                            {
                                string value = rs["prenom"].ToString();
                                guna2ComboBox2.Items.Add(value);
                            }
                        }
                    }
                }
            }
        }

        private void loadniveau()
        {
            string query = "SELECT id, nom FROM niveau " +
                           "WHERE id IN (SELECT id_niveau FROM niveau_sc WHERE id IN (SELECT id_niveauScolaire FROM programme))";
            OleDbDataReader rs = database.ExecuteQuery(query);
            while (rs.Read())
            {
                Niveau niveau = new Niveau();
                niveau.index_niveau = rs[0].ToString();
                niveau.nom_niveau = rs[1].ToString();
                guna2ComboBox3.Items.Add(niveau);
            }
            database.close();
        }

        private void loadmatiere()
        {
            String query = "SELECT id, matiere FROM Matiere " +
                           "WHERE id IN(SELECT id_matiere FROM programme)";
            OleDbDataReader rs = database.ExecuteQuery(query);
            while (rs.Read())
            {
                Matiere mt = new Matiere();
                mt.index = rs[0].ToString(); // Assuming id is the first column
                mt.nom = rs[1].ToString();  // Assuming nom is the second column
                guna2ComboBox7.Items.Add(mt);
            }
            database.close();
        }


        private void loadComboniveauscolaire()
        {
            Niveau n = (Niveau)guna2ComboBox3.SelectedItem;
            String query = "select id,nom_ns, id_niveau from niveau_sc where id_niveau=" + n.index_niveau + "AND id IN (SELECT id_niveauScolaire FROM programme)";
            OleDbDataReader rs = database.ExecuteQuery(query);
            guna2ComboBox6.Items.Clear();
            while (rs.Read())
            {
                Niveau_scolaire nv = new Niveau_scolaire();
                nv.index_ns = rs[0].ToString();
                nv.nom_ns = rs[1].ToString();
                nv.idniveau = rs[2].ToString();
                guna2ComboBox6.Items.Add(nv);
            }
            database.close();
        }

        private void loadCombofiliere()
        {
            Niveau n = (Niveau)guna2ComboBox3.SelectedItem;
            String query = "select id,nom_f, id_niveau from filiere where id_niveau=" + n.index_niveau + "AND id IN (SELECT id_filiere FROM programme)";
            OleDbDataReader rs   =database.ExecuteQuery(query);
            guna2ComboBox4.Items.Clear();
            while (rs.Read())
            {
                Filiere f = new Filiere();
                f.index = rs[0].ToString();
                f.nom = rs[1].ToString();
                f.idniveau = rs[2].ToString();
                guna2ComboBox4.Items.Add(f);
            }
            database.close();
        }
        private void loadCombofiliereP()
        {

        }
        private void loadComboannee()
        {
            String query = "select id,annee from annee_scolaire WHERE id IN (SELECT id_anneeScolaire FROM programme)";
            OleDbDataReader rs = database.ExecuteQuery(query);
            while (rs.Read())
            {
                Annee_scolaire annee = new Annee_scolaire();
                annee.index = rs[0].ToString();
                annee.nom = rs[1].ToString();
                guna2ComboBox5.Items.Add(annee);
            }
            database.close();
        }
        private void loadComboanneeP()
        {

        }

        private void noteADM_Load(object sender, EventArgs e)
        {
            LoadDataGridView.LoadFromTableDB(guna2DataGridView1, "SELECT utilisateur.nom, utilisateur.prenom,niveau.nom, niveau_sc.nom_ns, filiere.nom_f, annee_scolaire.annee FROM (((((((etudiant INNER JOIN utilisateur ON etudiant.idutilisateur = utilisateur.id) INNER JOIN marks ON marks.id_etudiant = etudiant.idutilisateur) INNER JOIN programme ON programme.id =marks.id_programme) INNER JOIN annee_scolaire ON programme.id_anneeScolaire = annee_scolaire.id) INNER JOIN niveau_sc ON programme.id_niveauScolaire = niveau_sc.id) INNER JOIN filiere ON programme.id_filiere = filiere.id) INNER JOIN niveau ON niveau_sc.id_niveau = niveau.id);");
            LoadDataGridView.LoadFromTableDB(guna2DataGridView2, "SELECT utilisateur.nom, utilisateur.prenom, Matiere.matiere, note_exam, note_controle,(programme.coeff_exam * marks.note_exam + programme.coeff_controle * marks.note_controle) FROM (((etudiant INNER JOIN utilisateur ON etudiant.idutilisateur = utilisateur.id) INNER JOIN marks ON marks.id_etudiant = etudiant.idutilisateur) INNER JOIN programme ON programme.id =marks.id_programme) INNER JOIN Matiere ON programme.id_matiere = Matiere.id;");
            loadname();
            loadniveau();
            loadComboannee();
            loadComboanneeP();
            loadmatiere();
        }

        private void guna2ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadComboniveauscolaire();
            loadCombofiliere();

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                // Retrieve id_etudiant
                string query1 = "SELECT etudiant.idutilisateur FROM etudiant " +
                                "INNER JOIN utilisateur ON etudiant.idutilisateur = utilisateur.id " +
                                "WHERE utilisateur.nom = @nom AND utilisateur.prenom = @prenom";

                using (OleDbCommand cmd1 = new OleDbCommand(query1, connection))
                {
                    cmd1.Parameters.AddWithValue("@nom", guna2ComboBox1.Text);
                    cmd1.Parameters.AddWithValue("@prenom", guna2ComboBox2.Text);
                    int idetudiant = (int)cmd1.ExecuteScalar();

                    Niveau_scolaire ns = (Niveau_scolaire)guna2ComboBox6.SelectedItem;
                    Filiere f = (Filiere)guna2ComboBox4.SelectedItem;
                    Annee_scolaire ans = (Annee_scolaire)guna2ComboBox5.SelectedItem;
                    Matiere mt = (Matiere)guna2ComboBox7.SelectedItem;

                    string query2 = "SELECT programme.id FROM programme " +
                                    "WHERE programme.id_niveauScolaire = @id_ns " +
                                    "AND programme.id_filiere = @id_filiere " +
                                    "AND programme.id_matiere = @id_matiere " +
                                    "AND programme.id_anneeScolaire = @id_annee";

                    using (OleDbCommand cmd2 = new OleDbCommand(query2, connection))
                    {
                        cmd2.Parameters.AddWithValue("@id_ns", ns.index_ns);
                        cmd2.Parameters.AddWithValue("@id_filiere", f.index);
                        cmd2.Parameters.AddWithValue("@id_matiere", mt.index);
                        cmd2.Parameters.AddWithValue("@id_annee", ans.index);

                        int idprogramme = (int)cmd2.ExecuteScalar();

                        string query3 = "INSERT INTO marks (id_etudiant, id_programme, note_exam, note_controle) " +
                                        "VALUES (@id_etudiant, @id_programme, @note_exam, @note_controle)";

                        using (OleDbCommand cmd3 = new OleDbCommand(query3, connection))
                        {
                            cmd3.Parameters.AddWithValue("@id_etudiant", idetudiant);
                            cmd3.Parameters.AddWithValue("@id_programme", idprogramme);
                            cmd3.Parameters.AddWithValue("@note_exam", guna2TextBox1.Text);
                            cmd3.Parameters.AddWithValue("@note_controle", guna2TextBox2.Text);

                            int result = cmd3.ExecuteNonQuery();

                            if (result == 1)
                            {
                                MessageBox.Show("Note est bien ajoutée.");
                            }
                            else
                            {
                                MessageBox.Show("Erreur d'ajout de Note.");
                            }
                        }
                    }
                }
            }
        }

        private void guna2ComboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            /*using (OleDbConnection connection = new OleDbConnection(connectionString))
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

            }*/
        }

        private void guna2TextBox3_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void keypress_search(object sender, KeyPressEventArgs e)
        {
            /*using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                if (guna2TextBox3.Text != "")
                {
                    guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    OleDbDataAdapter adap = new OleDbDataAdapter();
                    DataSet ds = new DataSet();
                    DataView dv = new DataView();

                    string query = $"SELECT utilisateur.nom, utilisateur.prenom, niveau.nom, niveau_sc.nom_ns, filiere.nom_f, annee_scolaire.annee FROM (((((((etudiant INNER JOIN utilisateur ON etudiant.idutilisateur = utilisateur.id) INNER JOIN marks ON marks.id_etudiant = etudiant.idutilisateur) INNER JOIN programme ON programme.id =marks.id_programme) INNER JOIN annee_scolaire ON programme.id_anneeScolaire = annee_scolaire.id) INNER JOIN niveau_sc ON programme.id_niveauScolaire = niveau_sc.id) INNER JOIN filiere ON programme.id_filiere = filiere.id) INNER JOIN niveau ON niveau_sc.id_niveau = niveau.id) WHERE utilisateur.nom LIKE '%{guna2TextBox3}%' OR utilisateur.prenom LIKE '%{guna2TextBox3}%' OR niveau.nom LIKE '%{guna2TextBox3}%' OR niveau_sc.nom_ns LIKE '%{guna2TextBox3}%' OR filiere.nom_f LIKE '%{guna2TextBox3}%' OR annee_scolaire.annee LIKE '%{guna2TextBox3}%';";
                    connection.Open();
                    adap = new OleDbDataAdapter(query, connection);
                    adap.Fill(ds);
                    dv = new DataView(ds.Tables[0]);
                    guna2DataGridView1.DataSource = dv;
                    connection.Close();
                }
                else if (guna2TextBox3.Text == "")
                {
                    guna2DataGridView1.Refresh();
                }
            }*/
        }
    }
}


