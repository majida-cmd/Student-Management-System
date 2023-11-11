 using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace navbar
{
    internal class LoadDataGridView
    {
        public static void LoadFromTableDB(DataGridView dgv, String query)
        {
            OleDbDataReader rs = database.ExecuteQuery(query);
            while (rs.Read())
            {
                String[] data = new string[rs.FieldCount];
                for (int i = 0; i < rs.FieldCount; i++)
                {
                    data[i] = rs[i].ToString();
                }
                dgv.Rows.Add(data);
            }
            database.close();
        }
    }
}
