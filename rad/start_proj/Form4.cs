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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace start_proj
{
    public partial class Form4 : Form
    {
        public NpgsqlConnection oCon;
        public string SQL;
        public string connection;
        public int id;
        public int supplier_id;
        public int product_id;
        public Form4(NpgsqlConnection oCon, string connection)
        {
            InitializeComponent();
            this.oCon = oCon;
            this.connection = connection;
            UpdateGrid();
            UpdateGrid2();
            List<Suppliers1> l1 = new List<Suppliers1>();
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
                    Suppliers1 supplier = new Suppliers1(supplier_id, name1);
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
            SQL = "SELECT * FROM supply_details";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(SQL, connection);
            DataSet ds = new DataSet();
            System.Data.Common.DataTableMapping Map = da.TableMappings.Add("supply_details", "Чек");
            Map.ColumnMappings.Add("supply_detail_id", "Идентификатор");
            Map.ColumnMappings.Add("supply_id", "Индентификатор поставки");
            Map.ColumnMappings.Add("product_id", "индентификатор товара");
            Map.ColumnMappings.Add("quanity", "количество");
            Map.ColumnMappings.Add("sum_total", "итоговая сумма");
            da.Fill(ds, "supply_details");
            dataGridView1.DataSource = ds.Tables["Чек"].DefaultView;
        }
        void UpdateGrid2()
        {
            SQL = "SELECT * FROM supplies";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(SQL, connection);
            DataSet ds = new DataSet();
            System.Data.Common.DataTableMapping Map = da.TableMappings.Add("supplies", "Поставка");
            Map.ColumnMappings.Add("supply_id", "Номер поставки");
            Map.ColumnMappings.Add("supplier_id", "Индентификатор поставщика");
            Map.ColumnMappings.Add("date_compilling", "дата поставки");
            Map.ColumnMappings.Add("payment_needful", "необходимая сумма оплаты");
            Map.ColumnMappings.Add("paid", "оплаченная сумма");
            da.Fill(ds, "supplies");
            dataGridView2.DataSource = ds.Tables["Поставка"].DefaultView;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Suppliers1 supplier = (Suppliers1)comboBox1.SelectedItem;
            supplier_id = supplier.id;

            List<Products> l2 = new List<Products>();
            string s = string.Format("select product_id, product_name from Products where supplier_id = '{0}'", supplier_id);
            oCon.Close();
            oCon.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(s, oCon);
            using (NpgsqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    string s2 = dr["product_id"].ToString();
                    product_id = Convert.ToInt32(s2);
                    string name2 = dr["product_name"].ToString();
                    Products product = new Products(product_id, name2);
                    l2.Add(product);
                }
            }
            comboBox2.DataSource = l2;
            comboBox2.DisplayMember = "name";
            comboBox2.ValueMember = "id";
            oCon.Close();
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Products product = (Products)comboBox2.SelectedItem;
            product_id = product.id;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = string.Format("INSERT INTO Supplies(supply_id, supplier_id, date_compilling) VALUES ('{0}', '{1}', '{2}')", textBox2.Text, supplier_id, dateTimePicker1.Value);
            oCon.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(sql, oCon);
            cmd.ExecuteNonQuery();
            UpdateGrid2();
            //textBox2.Text = "";
            /*
            oCon.Close();
            UpdateGrid();
            //comboBox1.Text = "";
            List<Products> l2 = new List<Products>();
            string s = string.Format("select product_id, product_name from Products where supplier_id = '{0}'", supplier_id);
            oCon.Open();
            cmd = new NpgsqlCommand(s, oCon);
            using (NpgsqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    string s2 = dr["product_id"].ToString();
                    product_id = Convert.ToInt32(s2);
                    string name2 = dr["product_name"].ToString();
                    Products product = new Products(product_id, name2);
                    l2.Add(product);
                }
            }
            comboBox2.DataSource = l2;
            comboBox2.DisplayMember = "name";
            comboBox2.ValueMember = "id";
            */
            oCon.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string s = string.Format("select price from Products where product_id = '{0}'", product_id);
            oCon.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(s, oCon);
            string price = "0";
            using (NpgsqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    price = dr["price"].ToString();
                }

            }
            long pr = Convert.ToInt64(price) * Convert.ToInt64(textBox1.Text);
            oCon.Close();
            string sql = string.Format("INSERT INTO Supply_details(supply_id, product_id, quanity, sum_total) VALUES ('{0}', '{1}', '{2}', '{3}')", textBox2.Text, product_id, textBox1.Text, pr);
            oCon.Open();
            cmd = new NpgsqlCommand(sql, oCon);
            cmd.ExecuteNonQuery();
            oCon.Close();
            UpdateGrid();
            UpdateGrid2();
            textBox1.Text = "";
            comboBox2.Text = "";
        }
    }
    public class Suppliers1
    {
        public int id { get; set; }
        public string name { get; set; }
        public Suppliers1(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
    public class Products
    {
        public int id { get; set; }
        public string name { get; set; }
        public Products(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }

}
