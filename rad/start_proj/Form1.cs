//using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using Npgsql;

namespace start_proj

{
    public partial class Form1 : Form
    {
        public NpgsqlConnection oCon;
        public string connection;
        public Form1()
        {

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connection = "Server=localhost; Port=5432; User Id=postgres; Password=821612; Database=dbPR;";
            oCon = new NpgsqlConnection(connection);
            Form2 form2 = new Form2(oCon, connection);
            form2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            connection = "Server=localhost; Port=5432; User Id=postgres; Password=821612; Database=dbPR;";
            oCon = new NpgsqlConnection(connection);
            Form3 form3 = new Form3(oCon, connection);
            form3.Show();
        }


        private void button3_Click_1(object sender, EventArgs e)
        {
            connection = "Server=localhost; Port=5432; User Id=postgres; Password=821612; Database=dbPR;";
            oCon = new NpgsqlConnection(connection);
            Form4 form4 = new Form4(oCon, connection);
            form4.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            connection = "Server=localhost; Port=5432; User Id=postgres; Password=821612; Database=dbPR;";
            oCon = new NpgsqlConnection(connection);
            Form5 form5 = new Form5(oCon, connection);
            form5.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            connection = "Server=localhost; Port=5432; User Id=postgres; Password=821612; Database=dbPR;";
            oCon = new NpgsqlConnection(connection);
            Form6 form6 = new Form6(oCon, connection);
            form6.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string SQL = string.Format("SELECT S.supply_detail_id, P.product_id, P.supplier_id, P.product_name, P.unit, P.price, P.weight, S.quanity, S.sum_total \r\nFROM Supply_details AS S\r\nJOIN Products AS P\r\nON P.product_id = S.product_id\r\nWHERE S.supply_id = '{0}';", 11);
            oCon.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(SQL, oCon);

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            string filename = ofd.FileName;
            Microsoft.Office.Interop.Excel.Application excelObject = new Microsoft.Office.Interop.Excel.Application();


            Workbook wb = excelObject.Workbooks.Open(filename, 0, false, 5, "", "", false, XlPlatform.xlWindows, "", true, false, 0, true, false, false);
            Worksheet wsh = (Worksheet)wb.Worksheets[1];
            wsh.Cells[1, 1] = "индентификатор в чеке";
            wsh.Cells[1, 2] = "индентификатор продукта";
            wsh.Cells[1, 3] = "индентификатор поставщика";
            wsh.Cells[1, 4] = "название товара";
            wsh.Cells[1, 5] = "единица товара";
            wsh.Cells[1, 6] = "цена за 1 единицу";
            wsh.Cells[1, 6] = "вес";
            wsh.Cells[1, 6] = "количество";
            wsh.Cells[1, 6] = "итоговая сумма товара";

            using (NpgsqlDataReader dr = cmd.ExecuteReader())
            {
                int k = 2;
                while (dr.Read())
                {
                    wsh.Cells[k, 1] = dr["S.supply_detail_id"].ToString();
                    wsh.Cells[k, 2] = dr["P.product_id"].ToString();
                    wsh.Cells[k, 3] = dr["P.supplier_id"].ToString();
                    wsh.Cells[k, 4] = dr["P.product_name"].ToString();
                    wsh.Cells[k, 5] = dr["P.unit"].ToString();
                    wsh.Cells[k, 6] = dr["P.price"].ToString();
                    wsh.Cells[k, 6] = dr["P.weight"].ToString();
                    wsh.Cells[k, 6] = dr["S.quanity"].ToString();
                    wsh.Cells[k, 6] = dr["S.sum_total"].ToString();
                    k += 1;
                }
            }

            oCon.Close();
            wb.Save();
            excelObject.Visible = true;

        }

        private void button7_Click(object sender, EventArgs e)
        {
            connection = "Server=localhost; Port=5432; User Id=postgres; Password=821612; Database=dbPR;";
            oCon = new NpgsqlConnection(connection);
            Form7 form7 = new Form7(oCon, connection);
            form7.Show();
        }
    }
}