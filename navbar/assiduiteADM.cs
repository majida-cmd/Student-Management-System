using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace navbar
{
    public partial class assiduiteADM : Form
    {
        string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + Application.StartupPath + "\\BD-gestion de note- majda alaabouch.accdb;";
        public assiduiteADM()
        {
            InitializeComponent();
        }

        private void assiduiteADM_Load(object sender, EventArgs e)
        {
            loadname();
            loaddata();

            OleDbConnection db = new OleDbConnection(connectionString);
            db.Open();
            String query = "SELECT utilisateur.nom, utilisateur.prenom, assiduite.ponctualite, assiduite.date_assiduite, assiduite.duree, assiduite.justification FROM (utilisateur INNER JOIN etudiant ON utilisateur.id = etudiant.idutilisateur) LEFT JOIN assiduite ON etudiant.idutilisateur = assiduite.id_etudiant";

            OleDbCommand cmd = new OleDbCommand(query, db);

            try
            {
                OleDbDataReader rs = cmd.ExecuteReader();

                while (rs.Read())
                {
                    string nom = rs["nom"].ToString();
                    string prenom = rs["prenom"].ToString();
                    string ponctualite = rs["ponctualite"].ToString();
                    string date_assiduite = rs["date_assiduite"].ToString();
                    int duree = Convert.ToInt32(rs["duree"]);
                    string justification = rs["justification"].ToString();

                    // Add the data to your DataGridView
                    guna2DataGridView1.Rows.Add(nom, prenom, ponctualite, date_assiduite, duree, justification);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                db.Close();
            }
            
        }
        

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
        private void loaddata()
        {
            OleDbConnection db = new OleDbConnection(connectionString);
            db.Open();
            String query = "SELECT * FROM utilisateur where id IN (select idutilisateur from etudiant)";
            OleDbCommand cmd = new OleDbCommand(query, db);
            int absent = 20;
            try
            {
                OleDbDataReader rs = cmd.ExecuteReader();
                while (rs.Read())
                {
                    int id = Convert.ToInt32(rs[0]);
                    String nom = rs[1].ToString();
                    String prenom = rs[2].ToString();
                    String query3 = "SELECT  sum(duree) as drnj FROM assiduite WHERE justification = 'non' AND id_etudiant=" + id;
                    OleDbCommand cmd3 = new OleDbCommand(query3, db);
                    object value1 = cmd3.ExecuteScalar();
                    int drnj = value1 == DBNull.Value ? 0 : Convert.ToInt32(value1);
                    double note = absent - ((drnj * 0.125) / 60);
                    String query5 = "SELECT count(*) as nbanj FROM assiduite WHERE justification = 'non' and ponctualite = 'absent' AND id_etudiant=" + id;
                    OleDbCommand cmd5 = new OleDbCommand(query5, db);
                    int nbanj = Convert.ToInt32(cmd5.ExecuteScalar());
                    note += -(nbanj * 0.25);
                    drnj = drnj;
                    nbanj = nbanj;
                    note = note;
                    guna2DataGridView2.Rows.Add(nom, prenom, note);
                }
                cmd.Dispose();
                db.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            String date = guna2DateTimePicker1.Value.ToShortDateString();
            String query = "INSERT INTO assiduite(id_etudiant, ponctualite, date_assiduite, duree, justification)" +
                "SELECT etudiant.idutilisateur,'"+ guna2ComboBox3.Text+"','"+ date+"',"+ guna2TextBox1.Text+",'"+ guna2ComboBox4.Text + "' FROM etudiant INNER JOIN utilisateur ON etudiant.idutilisateur = utilisateur.id WHERE utilisateur.nom =" +
                " '" + guna2ComboBox1.Text + "' AND utilisateur.prenom = '" + guna2ComboBox2.Text + "'";

            int r = database.insert(query); 

            if (r == 1)
            {
                MessageBox.Show("Assiduité est bien ajoutée.");
            }
            else
            {
                MessageBox.Show("Erreur d'ajout d'assiduité.");
            }
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void search_bar(object sender, EventArgs e)
        {
            
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
        }
    }
    }

