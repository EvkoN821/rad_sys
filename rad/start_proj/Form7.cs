using Microsoft.Office.Interop.Excel;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
namespace start_proj
{
    public partial class Form7 : Form
    {
        public NpgsqlConnection oCon;
        public string SQL;
        public string connection;
        public int id;
        public int supplier_id;
        public Form7(NpgsqlConnection oCon, string connection)
        {
            InitializeComponent();
            this.oCon = oCon;
            this.connection = connection;
            UpdateGrid();

            List<Suppliers> l6 = new List<Suppliers>();
            string s = "select supplier_id, supplier_name from Suppliers";
            oCon.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(s, oCon);
            using (NpgsqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    string s1 = dr["supplier_id"].ToString();
                    supplier_id = Convert.ToInt32(s1);
                    string name1 = dr["supplier_name"].ToString();
                    Suppliers supplier = new Suppliers(supplier_id, name1);
                    l6.Add(supplier);
                }

            }
            comboBox1.DataSource = l6;
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "id";
            oCon.Close();

            /*
            List<Suppliers> l5 = new List<Suppliers>();
            string s2 = "select supplier_id, supplier_name from Suppliers";
            oCon.Open();
            NpgsqlCommand cmd1 = new NpgsqlCommand(s2, oCon);
            using (NpgsqlDataReader dr = cmd1.ExecuteReader())
            {
                while (dr.Read())
                {
                    string s12 = dr["supplier_id"].ToString();
                    supplier_id = Convert.ToInt32(s12);
                    string name12 = dr["supplier_name"].ToString();
                    Suppliers supplier = new Suppliers(supplier_id, name12);
                    l5.Add(supplier);
                }
            }
            checkedListBox1.DataSource = l5;
            checkedListBox1.DisplayMember = "name";
            checkedListBox1.ValueMember = "id";
            oCon.Close();
            */
        }
        void UpdateGrid()
        {
            SQL = string.Format("SELECT sum(payment_needful) as ob, sum(payment_needful - paid) as unpaid FROM Supplies where supplier_id = '{0}' and ( date_compilling BETWEEN '{1}' AND '{2}')", supplier_id, dateTimePicker1.Value, dateTimePicker2.Value);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(SQL, connection);
            DataSet ds = new DataSet();
            System.Data.Common.DataTableMapping Map = da.TableMappings.Add("Supplies", "Отчет");
            Map.ColumnMappings.Add("ob", "Оборот");
            Map.ColumnMappings.Add("unpaid", "Неоплаченная сумма");
            da.Fill(ds, "Supplies");
            dataGridView1.DataSource = ds.Tables["Отчет"].DefaultView;

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Suppliers supplier = (Suppliers)comboBox1.SelectedItem;
            supplier_id = supplier.id;
            UpdateGrid();
        }
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Suppliers supplier = (Suppliers)checkedListBox1.SelectedItem;
            supplier_id = supplier.id;
            UpdateGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateGrid();
        }
        private void dtp1_Changed(object sender, EventArgs e)
        {
            UpdateGrid();
        }
    }
}
