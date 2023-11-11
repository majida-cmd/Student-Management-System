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
    public partial class updateForm : Form
    {
        public updateForm()
        {
            InitializeComponent();
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadComboville();
        }

        private void updateForm_Load(object sender, EventArgs e)
        {
            LoadDataGridView.LoadFromTableDB(guna2DataGridView1, "SELECT utilisateur.id, utilisateur.nom, prenom, etudiant.cin, pays.nom_p, ville.nom_v, adresse, niveau.nom, niveau_sc.nom_ns, filiere.nom_f, annee_scolaire.annee FROM((((((((etudiant INNER JOIN utilisateur ON etudiant.idutilisateur = utilisateur.id) INNER JOIN ville ON etudiant.idville = ville.id) INNER JOIN pays ON ville.id_pays = pays.id) INNER JOIN inscription ON inscription.id_etudiant = etudiant.idutilisateur) INNER JOIN annee_scolaire ON inscription.id_anneeScolaire = annee_scolaire.id) INNER JOIN niveau_sc ON inscription.id_niveauScolaire = niveau_sc.id) INNER JOIN niveau ON niveau_sc.id_niveau = niveau.id) INNER JOIN filiere ON filiere.id_niveau = niveau.id)");
            loadCombopays();
            loadComboniveau();
            loadComboannee();
        }
        private void loadComboville()
        {
            Pays p = (Pays)guna2ComboBox1.SelectedItem;
            String query = "select id,nom_v, id_pays from ville where id_pays=" + p.index;
            OleDbDataReader rs = database.ExecuteQuery(query);

            guna2ComboBox2.Items.Clear();
            while (rs.Read())
            {
                Ville pays = new Ville();
                pays.index = rs[0].ToString();
                pays.nom = rs[1].ToString();
                pays.idpays = rs[2].ToString();
                guna2ComboBox2.Items.Add(pays);
            }
            database.close();
        }
        private void loadComboniveauscolaire()
        {
            Niveau n = (Niveau)guna2ComboBox3.SelectedItem;
            String query = "select id,nom_ns, id_niveau from niveau_sc where id_niveau=" + n.index_niveau;
            OleDbDataReader rs = database.ExecuteQuery(query);
            guna2ComboBox4.Items.Clear();
            while (rs.Read())
            {
                Niveau_scolaire nv = new Niveau_scolaire();
                nv.index_ns = rs[0].ToString();
                nv.nom_ns = rs[1].ToString();
                nv.idniveau = rs[2].ToString();
                guna2ComboBox4.Items.Add(nv);
            }
            database.close();
        }
        private void loadCombofiliere()
        {
            Niveau n = (Niveau)guna2ComboBox3.SelectedItem;
            String query = "select id,nom_f, id_niveau from filiere where id_niveau=" + n.index_niveau;
            OleDbDataReader rs = database.ExecuteQuery(query);
            guna2ComboBox5.Items.Clear();
            while (rs.Read())
            {
                Filiere f = new Filiere();
                f.index = rs[0].ToString();
                f.nom = rs[1].ToString();
                f.idniveau = rs[2].ToString();
                guna2ComboBox5.Items.Add(f);
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
                guna2ComboBox6.Items.Add(annee);
            }
            database.close();
        }
        private void loadComboniveau()
        {
            String query = "select id,nom from niveau";
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
        private void loadCombopays()
        {
            String query = "select id,nom_p from pays";
            OleDbDataReader rs = database.ExecuteQuery(query);
            while (rs.Read())
            {
                Pays pays = new Pays();
                pays.index = rs[0].ToString();
                pays.nom = rs[1].ToString();
                guna2ComboBox1.Items.Add(pays);
            }
            database.close();
        }

        private void guna2ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadComboniveauscolaire();
            loadCombofiliere();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Ville v = (Ville)guna2ComboBox2.SelectedItem;
            Filiere f = (Filiere)guna2ComboBox5.SelectedItem;
            Niveau_scolaire ns = (Niveau_scolaire)guna2ComboBox4.SelectedItem;
            Annee_scolaire a = (Annee_scolaire)guna2ComboBox6.SelectedItem;
            int indice = guna2DataGridView1.CurrentRow.Index;
            string query = "UPDATE etudiant, utilisateur, inscription " +
            "SET nom = '" + guna2TextBox1.Text + "', " +
            "prenom = '" + guna2TextBox2.Text + "', " +
            "cin = '" + guna2TextBox3.Text + "', " +
            "adresse = '" + guna2TextBox4.Text + "', " +
            "idville = " + v.index + ", " +
            "id_niveauScolaire = " + ns.index_ns + ", " +
            "id_filiere = " + f.index + ", " +
            "id_anneeScolaire = " + a.index + " " +
            "WHERE utilisateur.id = " + guna2DataGridView1.Rows[indice].Cells[0].Value + " " +
            "AND etudiant.idutilisateur = inscription.id_etudiant " +
            "AND utilisateur.id = etudiant.idutilisateur";
            int r = database.insert(query);
            guna2DataGridView1.Rows[indice].Cells[1].Value = guna2TextBox1.Text;
            guna2DataGridView1.Rows[indice].Cells[2].Value = guna2TextBox2.Text;
            guna2DataGridView1.Rows[indice].Cells[3].Value = guna2TextBox3.Text;
            guna2DataGridView1.Rows[indice].Cells[4].Value = guna2ComboBox1.Text;
            guna2DataGridView1.Rows[indice].Cells[5].Value = guna2ComboBox2.Text;
            guna2DataGridView1.Rows[indice].Cells[6].Value = guna2TextBox4.Text;
            guna2DataGridView1.Rows[indice].Cells[7].Value = guna2ComboBox3.Text;
            guna2DataGridView1.Rows[indice].Cells[8].Value = guna2ComboBox4.Text;
            guna2DataGridView1.Rows[indice].Cells[9].Value = guna2ComboBox5.Text;
            guna2DataGridView1.Rows[indice].Cells[10].Value = guna2ComboBox6.Text;

            if (r == 1)
            {
                MessageBox.Show("Modification success");
            }
            else
            {
                MessageBox.Show("erreur de modification");
            }
        }

        private void click_list(object sender, DataGridViewCellEventArgs e)
        {
            int indice = e.RowIndex;
            String nom = guna2DataGridView1.Rows[indice].Cells[1].Value.ToString();
            String prenom = guna2DataGridView1.Rows[indice].Cells[2].Value.ToString();
            String cin = guna2DataGridView1.Rows[indice].Cells[3].Value.ToString();
            String pays = guna2DataGridView1.Rows[indice].Cells[4].Value.ToString();
            String ville = guna2DataGridView1.Rows[indice].Cells[5].Value.ToString();
            String adresse = guna2DataGridView1.Rows[indice].Cells[6].Value.ToString();
            String niveau = guna2DataGridView1.Rows[indice].Cells[7].Value.ToString();
            String niveau_sc = guna2DataGridView1.Rows[indice].Cells[8].Value.ToString();
            String filiere = guna2DataGridView1.Rows[indice].Cells[9].Value.ToString();
            String annee = guna2DataGridView1.Rows[indice].Cells[10].Value.ToString();
            guna2TextBox1.Text = nom;
            guna2TextBox2.Text = prenom;
            guna2TextBox3.Text = cin;
            guna2ComboBox1.Text = pays;
            guna2ComboBox2.Text = ville;
            guna2TextBox4.Text = adresse;
            guna2ComboBox3.Text = niveau;
            guna2ComboBox4.Text = niveau_sc;
            guna2ComboBox5.Text = filiere;
            guna2ComboBox6.Text = annee;
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
