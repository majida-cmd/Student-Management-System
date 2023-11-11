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
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void rjButton1_Click(object sender, EventArgs e)
        {

        }

        private void Home_Load(object sender, EventArgs e)
        {
            loadCombopays();
            loadComboniveau();
            loadComboannee();
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
                rjComboBox1.Items.Add(pays);
            }
            database.close();
        }
        private void loadComboville()
        {
            Pays p = (Pays)rjComboBox1.SelectedItem;
            String query = "select id,nom_v, id_pays from ville where id_pays=" + p.index;
            OleDbDataReader rs = database.ExecuteQuery(query);
            rjComboBox2.Items.Clear();
            while (rs.Read())
            {
                Ville pays = new Ville();
                pays.index = rs[0].ToString();
                pays.nom = rs[1].ToString();
                pays.idpays = rs[2].ToString();
                rjComboBox2.Items.Add(pays);
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
                rjComboBox3.Items.Add(niveau);
            }
            database.close();
        }
        private void loadComboniveauscolaire()
        {
            Niveau n = (Niveau)rjComboBox3.SelectedItem;
            String query = "select id,nom_ns, id_niveau from niveau_sc where id_niveau=" + n.index_niveau;
            OleDbDataReader rs = database.ExecuteQuery(query);
            rjComboBox4.Items.Clear();
            while (rs.Read())
            {
                Niveau_scolaire nv = new Niveau_scolaire();
                nv.index_ns = rs[0].ToString();
                nv.nom_ns = rs[1].ToString();
                nv.idniveau = rs[2].ToString();
                rjComboBox4.Items.Add(nv);
            }
            database.close();
        }
        private void loadCombofiliere()
        {
            Niveau n = (Niveau)rjComboBox3.SelectedItem;
            String query = "select id,nom_f, id_niveau from filiere where id_niveau=" + n.index_niveau;
            OleDbDataReader rs = database.ExecuteQuery(query);
            rjComboBox5.Items.Clear();
            while (rs.Read())
            {
                Filiere f = new Filiere();
                f.index = rs[0].ToString();
                f.nom = rs[1].ToString();
                f.idniveau = rs[2].ToString();
                rjComboBox5.Items.Add(f);
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
                rjComboBox6.Items.Add(annee);
            }
            database.close();
        }

        private void rjButton3_Click(object sender, EventArgs e)
        {

        }

        private void rjTextBox1__TextChanged(object sender, EventArgs e)
        {

        }

        private void rjTextBox2__TextChanged(object sender, EventArgs e)
        {

        }

        private void rjDatePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void rjComboBox1_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            loadComboville();
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void rjDatePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void rjComboBox3_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            loadComboniveauscolaire();
            loadCombofiliere();
        }

        private void rjButton1_Click_1(object sender, EventArgs e)
        {

            String query = "insert into utilisateur " +
                "(`nom`, `prenom`, `email`, `password`) values('" + rjTextBox1.Texts + "','" + rjTextBox2.Texts + "','" + rjTextBox3.Texts + "','" + rjTextBox4.Texts + "')";
            int r = database.insert(query);
            if (r == 1)
            {
                Ville v = (Ville)rjComboBox2.SelectedItem;

                String connectionString = database.connectionString;
                OleDbConnection db = new OleDbConnection(connectionString);
                db.Open();

                String query2 = "select MAX(id) from utilisateur";
                OleDbCommand cmd1 = new OleDbCommand(query2, db);
                int maxId = (int)cmd1.ExecuteScalar();

                String date = rjDatePicker1.Value.ToShortDateString();

                String query3 = "insert into etudiant" +
                    " (idutilisateur,`cin`,`telephone`,`date_naissance`,`genre`,`fixe`,`adresse`,`idville`,`whatsapp`) " +
                    "values(" + maxId + ",'" + rjTextBox5.Texts + "','" + rjTextBox6.Texts + "','" + date + "','" + rjComboBox7.Texts + "','"
                    + rjTextBox7.Texts + "','" + rjTextBox9.Texts + "','" + v.index + "','" + rjTextBox8.Texts + "')";
                int r1 = database.insert(query3);
                MessageBox.Show("success");

                if (r1 == 1)
                {
                    cmd1.ExecuteReader();
                    cmd1.Dispose();

                    String query5 = "select MAX(id) from utilisateur";
                    OleDbCommand cmd2 = new OleDbCommand(query5, db);
                    int maxId2 = (int)cmd2.ExecuteScalar();

                    Filiere f = (Filiere)rjComboBox5.SelectedItem;

                    Niveau_scolaire ns = (Niveau_scolaire)rjComboBox4.SelectedItem;

                    Annee_scolaire an = (Annee_scolaire)rjComboBox6.SelectedItem;

                    String date2 = rjDatePicker2.Value.ToShortDateString();

                    String query4 = "insert into inscription (`date_inscription`,`id_filiere`,`id_etudiant`,`id_niveauScolaire`,`id_anneeScolaire`)";
                    query4 += "values ('" + date2 + "','" + f.index + "','" + maxId2 + "','" + ns.index_ns + "','" + an.index + "')";
                    int r3 = database.insert(query4);
                     if (r3 == 1)
                     {
                         cmd2.ExecuteReader();
                         cmd2.Dispose();
                         db.Close();
                         MessageBox.Show("utilisateur et etudiant et inscription  sont bien ajouter");
                     }
                     else
                     {
                         MessageBox.Show("erreur d'ajout d'inscription");
                     }

                }
                else
                {
                    MessageBox.Show("erreur d'ajout d'étudiant");
                }
                
            }
            else
            {
                MessageBox.Show("erreur d'ajout d'utilisateur ");
            }
        }
    }
}


