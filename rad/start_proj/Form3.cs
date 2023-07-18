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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace start_proj
{
    public partial class Form3 : Form
    {
        public NpgsqlConnection oCon;
        public string SQL;
        public string connection;
        public int id;
        public int supplier_id;
        public Form3(NpgsqlConnection oCon, string connection)
        {
            InitializeComponent();
            this.oCon = oCon;
            this.connection = connection;
            UpdateGrid();
            List<Suppliers> l1 = new List<Suppliers>();
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
                    l1.Add(supplier);
                }

            }
            comboBox1.DataSource = l1;
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "id";
            oCon.Close();
        }
        void UpdateGrid()
        {
            SQL = "SELECT * FROM Products";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(SQL, connection);
            DataSet ds = new DataSet();
            System.Data.Common.DataTableMapping Map = da.TableMappings.Add("Products", "Товары");
            Map.ColumnMappings.Add("product_id", "Идентификатор");
            Map.ColumnMappings.Add("supplier_id", "Поставщик");
            Map.ColumnMappings.Add("product_name", "товар");
            Map.ColumnMappings.Add("unit", "единица");
            Map.ColumnMappings.Add("price", "цена");
            Map.ColumnMappings.Add("weight", "вес");
            da.Fill(ds, "Products");
            dataGridView1.DataSource = ds.Tables["Товары"].DefaultView;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = string.Format("INSERT INTO Products(supplier_id, product_name, unit, price, weight) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}')", supplier_id, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text);
            oCon.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(sql, oCon);
            cmd.ExecuteNonQuery();
            oCon.Close();
            UpdateGrid();
            comboBox1.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Suppliers supplier = (Suppliers)comboBox1.SelectedItem;
            supplier_id = supplier.id;
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection t = dataGridView1.SelectedRows;
            if (t.Count > 0)
            {
                DataGridViewRow row = t[0];
                id = Convert.ToInt32(row.Cells[0].Value);
                comboBox1.Text = Convert.ToString(row.Cells[1].Value);
                textBox1.Text = Convert.ToString(row.Cells[2].Value);
                textBox2.Text = Convert.ToString(row.Cells[3].Value);
                textBox3.Text = Convert.ToString(row.Cells[4].Value);
                textBox4.Text = Convert.ToString(row.Cells[5].Value);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sql = string.Format("UPDATE Products set supplier_id = '{0}', product_name = '{1}', unit = '{2}', price = '{3}', weight = '{4}' where product_id = '{5}'", supplier_id, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, id);
            oCon.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(sql, oCon);
            cmd.ExecuteNonQuery();
            oCon.Close();
            UpdateGrid();
            comboBox1.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sql = string.Format("delete from Products where product_id = {0}", id);
            oCon.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(sql, oCon);
            cmd.ExecuteNonQuery();
            oCon.Close();
            UpdateGrid();
            comboBox1.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";

        }
    }
    public class Suppliers
    {
        public int id { get; set; }
        public string name { get; set; }
        public Suppliers(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

    }
}
