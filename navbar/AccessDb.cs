using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Windows.Markup;


namespace navbar.Static_Classes
{
    class AC
    {
        public static OleDbConnection con = new OleDbConnection();
        public static OleDbCommand cmd = new OleDbCommand("",con);
        public static OleDbDataReader rd;
        public static string currentFullName;
        public static string sql;

        public static string getConnectionString()
        {
            /* Provider = Microsoft.ACE.OLEDB.12.0; Data Source = "C:\Users\HUAWAI\OneDrive\Bureau\C# majda\navbar\navbar\navbar\bin\Debug\BD-gestion de note- majda alaabouch.accdb"*/
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + Application.StartupPath + "\\BD-gestion de note- majda alaabouch.accdb;";
            return connectionString;
        }
        public static void OpenConnection()
        {
            try
            {
                if(con.State == ConnectionState.Closed) 
                {
                    con.ConnectionString = getConnectionString();
                    con.Open();
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Erruer!!" + Environment.NewLine + "Description: " +ex.Message.ToString(), "C# Access DataBse",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        public static void CloseConnection()
        {
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erruer!!Close");
            }
        }
    }
}
