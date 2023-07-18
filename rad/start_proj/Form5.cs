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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace start_proj
{
    public partial class Form5 : Form
    {
        public NpgsqlConnection oCon;
        public string SQL;
        public string connection;
        public int supply_id;
        public Form5(NpgsqlConnection oCon, string connection)
        {
            InitializeComponent();
            this.oCon = oCon;
            this.connection = connection;
            UpdateGrid();
            List<Supplies> l3 = new List<Supplies>();
            string s = string.Format("select supply_id, date_compilling from Supplies where date_compilling + interval '3 month'>='{0}'", DateTime.Today);
            oCon.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(s, oCon);
            using (NpgsqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    string s1 = dr["supply_id"].ToString();
                    supply_id = Convert.ToInt32(s1);
                    string name1 = dr["date_compilling"].ToString();
                    Supplies supply1 = new Supplies(supply_id, name1);
                    l3.Add(supply1);
                }

            }
            comboBox1.DataSource = l3;
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "id";
            oCon.Close();
        }
        void UpdateGrid()
        {
            SQL = "SELECT * FROM payment_detail";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(SQL, connection);
            DataSet ds = new DataSet();
            System.Data.Common.DataTableMapping Map = da.TableMappings.Add("payment_detail", "Оплата");
            Map.ColumnMappings.Add("payment_id", "Идентификатор");
            Map.ColumnMappings.Add("supply_id", "Индентификатор поставки");
            Map.ColumnMappings.Add("date_pay", "Дата оплаты");
            Map.ColumnMappings.Add("payment", "Сумма оплаты");
            da.Fill(ds, "payment_detail");
            dataGridView1.DataSource = ds.Tables["Оплата"].DefaultView;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //DateTime date1;
            string sql = string.Format("INSERT INTO payment_detail(supply_id, date_pay, payment) VALUES ('{0}', '{1}', '{2}')", supply_id, DateTime.Today, textBox1.Text); //dateTimePicker1.Value
            oCon.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(sql, oCon);
            cmd.ExecuteNonQuery();
            UpdateGrid();
            oCon.Close();
            textBox1.Text = "";
            comboBox1.Text = "";
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Supplies supply = (Supplies)comboBox1.SelectedItem;
            supply_id = supply.id;
        }
    }
    public class Supplies
    {
        public int id { get; set; }
        public string name { get; set; }
        public Supplies(int id, string name)
        {
            this.id = id;
            this.name = "№ " + Convert.ToString(id) + "   дата: " + name;
        }
    }
}
