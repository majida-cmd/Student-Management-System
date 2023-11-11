using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.OleDb;

namespace navbar
{
    public partial class Table : Form
    {
        public Table()
        {
            InitializeComponent();
        }
        private void Table_Load(object sender, EventArgs e)
        {
            LoadDataGridView.LoadFromTableDB(guna2DataGridView1, "select utilisateur.id, nom, prenom, cin,date_naissance, genre, email, telephone, whatsapp,  fixe, pays.nom_p, ville.nom_v, adresse from etudiant, ville, pays, utilisateur where etudiant.idutilisateur=utilisateur.id and etudiant.idville=ville.id and ville.id_pays=pays.id");

        }
        private void rjButton1_Click(object sender, EventArgs e)
        {
            int indice = guna2DataGridView1.CurrentRow.Index;
            String query = "DELETE utilisateur.*, etudiant.*, inscription.* FROM(utilisateur INNER JOIN etudiant ON utilisateur.id = etudiant.idutilisateur) INNER JOIN inscription ON inscription.id_etudiant = etudiant.idutilisateur WHERE utilisateur.id =" + guna2DataGridView1.Rows[indice].Cells[0].Value;
            int r = database.insert(query);
            DataGridViewRow selectedrow = guna2DataGridView1.CurrentRow;
            guna2DataGridView1.Rows.Remove(selectedrow);
            if (r == 1)
            {
                MessageBox.Show("ligne bien supprimer");
            }
            else
            {
                MessageBox.Show("erreur!!!");
            }
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panelTitleBar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
