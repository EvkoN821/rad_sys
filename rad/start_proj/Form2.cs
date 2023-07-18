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
    public partial class Form2 : Form
    {
        public NpgsqlConnection oCon;
        public string SQL;
        public string connection;
        public int supplier_id;
        public Form2(NpgsqlConnection oCon, string connection)
        {
            InitializeComponent();
            this.oCon = oCon;
            this.connection = connection;
            UpdateGrid();
        }

        void UpdateGrid()
        {
            SQL = "SELECT * FROM Suppliers";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(SQL, connection);
            DataSet ds = new DataSet();
            System.Data.Common.DataTableMapping Map = da.TableMappings.Add("Suppliers", "Поставщики");
            Map.ColumnMappings.Add("supplier_id", "Идентификатор");
            Map.ColumnMappings.Add("supplier_name", "Поставщик");
            Map.ColumnMappings.Add("contact_name", "Контактное имя");
            Map.ColumnMappings.Add("address", "Адрес");
            Map.ColumnMappings.Add("city", "Город");
            Map.ColumnMappings.Add("postal_code", "Индекс");
            Map.ColumnMappings.Add("phone", "Телефон");
            da.Fill(ds, "Suppliers");
            dataGridView1.DataSource = ds.Tables["Поставщики"].DefaultView;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = string.Format("INSERT INTO Suppliers(supplier_name, contact_name, address, city, postal_code, phone) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')", textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text);
            oCon.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(sql, oCon);
            cmd.ExecuteNonQuery();
            oCon.Close();
            UpdateGrid();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection t = dataGridView1.SelectedRows;
            if (t.Count > 0)
            {
                DataGridViewRow row = t[0];
                supplier_id = Convert.ToInt32(row.Cells[0].Value);
                textBox1.Text = Convert.ToString(row.Cells[1].Value);
                textBox2.Text = Convert.ToString(row.Cells[2].Value);
                textBox3.Text = Convert.ToString(row.Cells[3].Value);
                textBox4.Text = Convert.ToString(row.Cells[4].Value);
                textBox5.Text = Convert.ToString(row.Cells[5].Value);
                textBox6.Text = Convert.ToString(row.Cells[6].Value);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sql = string.Format("UPDATE Suppliers set supplier_name = '{0}', contact_name = '{1}', address = '{2}', city = '{3}', postal_code = '{4}', phone = '{5}' where supplier_id = {6}", textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, supplier_id);
            oCon.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(sql, oCon);
            cmd.ExecuteNonQuery();
            oCon.Close();
            UpdateGrid();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sql = string.Format("delete from Suppliers where supplier_id = {0}", supplier_id);
            oCon.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(sql, oCon);
            cmd.ExecuteNonQuery();
            oCon.Close();
            UpdateGrid();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
