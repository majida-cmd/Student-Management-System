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
    public partial class assiduite : Form
    {
        private int etudiantId;
        public assiduite(int etudiantId)
        {
            InitializeComponent();
            this.etudiantId = etudiantId;
        }

        private void assiduite_Load(object sender, EventArgs e)
        {
            string connexionString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + Application.StartupPath + "\\BD-gestion de note- majda alaabouch.accdb;";
            OleDbConnection db = new OleDbConnection(connexionString);
            db.Open();
            String query = "SELECT * FROM utilisateur where id=" + etudiantId;
            OleDbCommand cmd = new OleDbCommand(query, db);
            double note;
            int absent = 20;
            try
            {
                OleDbDataReader rs = cmd.ExecuteReader();
                while (rs.Read())
                {
                    String nom = rs["nom"].ToString();
                    guna2HtmlLabel1.Text = nom;
                    String query2 = "SELECT sum(duree) as drj FROM assiduite WHERE justification = 'oui' and id_etudiant=" + etudiantId; /*drj=3*/
                    OleDbCommand cmd2 = new OleDbCommand(query2, db);
                    int drj = Convert.ToInt32(cmd2.ExecuteScalar());
                    guna2HtmlLabel2.Text= drj.ToString();
                    String query3 = "SELECT  sum(duree) as drnj FROM assiduite WHERE justification = 'non' and id_etudiant=" + etudiantId; /*drnj=0*/
                    OleDbCommand cmd3 = new OleDbCommand(query3, db);
                    int drnj = Convert.ToInt32(cmd3.ExecuteScalar());
                    guna2HtmlLabel3.Text = drnj.ToString();
                    note = absent - ((drnj * 0.125) / 60);
                    String query4 = "SELECT count(*) as nbaj FROM assiduite WHERE ponctualite = 'absent' and justification = 'oui' and id_etudiant=" + etudiantId; /*nbaj=1*/
                    OleDbCommand cmd4 = new OleDbCommand(query4, db);
                    int nbaj = Convert.ToInt32(cmd4.ExecuteScalar());
                    guna2HtmlLabel4.Text=nbaj.ToString();
                    String query5 = "SELECT count(*) as nbanj FROM assiduite WHERE ponctualite = 'absent' and justification = 'non' and id_etudiant=" + etudiantId; /*nbanj=2*/
                    OleDbCommand cmd5 = new OleDbCommand(query5, db);
                    int nbanj = Convert.ToInt32(cmd5.ExecuteScalar());
                    note = note - (nbanj*0.25);
                    guna2HtmlLabel5.Text = nbanj.ToString();
                    guna2HtmlLabel6.Text=note.ToString();
                }
                cmd.Dispose();
                db.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel6_Click(object sender, EventArgs e)
        {

        }
    }
}
