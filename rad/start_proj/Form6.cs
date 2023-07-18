using System.Globalization;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using System.Collections;


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using System.Xml;
using Aspose.Cells;
//using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;

namespace start_proj
{
    public partial class Form6 : Form
    {
        public NpgsqlConnection oCon;
        public string SQL;
        public string connection;
        public int supply_id;
        public int count = 0;
        public Form6(NpgsqlConnection oCon, string connection)
        {
            InitializeComponent();
            this.oCon = oCon;
            this.connection = connection;
            List<Supplies> l4 = new List<Supplies>();
            string s = "select supply_id, date_compilling from Supplies";
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
                    l4.Add(supply1);
                }

            }
            comboBox1.DataSource = l4;
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "id";
            oCon.Close();
            UpdateGrid();

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Supplies supply = (Supplies)comboBox1.SelectedItem;
            supply_id = supply.id;
            UpdateGrid();
        }
        void UpdateGrid()
        {
            SQL = string.Format("SELECT S.supply_detail_id, P.product_id, P.supplier_id, P.product_name, P.unit, P.price, P.weight, S.quanity, S.sum_total \r\nFROM Supply_details AS S\r\nJOIN Products AS P\r\nON P.product_id = S.product_id\r\nWHERE S.supply_id = '{0}';", supply_id);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(SQL, connection);
            DataSet ds = new DataSet();
            System.Data.Common.DataTableMapping Map = da.TableMappings.Add("supply_details", "накладная");
            Map.ColumnMappings.Add("supply_detail_id", "индентификатор_в_чеке");
            Map.ColumnMappings.Add("product_id", "индентификатор_продукта");
            Map.ColumnMappings.Add("supplier_id", "индентификатор_поставщика");
            Map.ColumnMappings.Add("product_name", "название_товара");
            Map.ColumnMappings.Add("unit", "единица_товара");
            Map.ColumnMappings.Add("price", "цена_за_1_единицу");
            Map.ColumnMappings.Add("weight", "вес");
            Map.ColumnMappings.Add("quanity", "количество");
            Map.ColumnMappings.Add("sum_total", "итоговая_сумма_товара");
            da.Fill(ds, "supply_details");
            dataGridView1.DataSource = ds.Tables["накладная"].DefaultView;
        }
        private System.Data.DataTable GetDataTableFromDGV(DataGridView dgv)
        {
            System.Data.DataTable table = new System.Data.DataTable();
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                if (column.Visible)
                {
                    table.Columns.Add();
                }
            }
            object[] cellValues = new object[dgv.Columns.Count];
            foreach (DataGridViewRow row in dgv.Rows)
            {
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    cellValues[i] = row.Cells[i].Value;
                }
                table.Rows.Add(cellValues);
            }
            return table;
        }
        private void button1_Click(object sender, EventArgs e)
        {


            /*
            //string SQL = string.Format("SELECT S.supply_detail_id, P.product_id, P.supplier_id, P.product_name, P.unit, P.price, P.weight, S.quanity, S.sum_total \r\nFROM Supply_details AS S\r\nJOIN Products AS P\r\nON P.product_id = S.product_id\r\nWHERE S.supply_id = '{0}';", 11);
            //oCon.Open();
            //NpgsqlCommand cmd = new NpgsqlCommand(SQL, oCon);

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
            /*
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
            
            //oCon.Close();
            wb.Save();
            excelObject.Visible = true;
            */
            string connString = "Server=localhost; Port=5432; User Id=postgres; Password=821612; Database=dbPR;";
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                // Открытие подключения
                conn.Open();

                // Создание SQL-запроса с параметром client_id
                //string sql = "SELECT t.tovar_name, r.tovar_number_one, r.tovar_summ_one, o.tovar_summ, o.otgruzka_date, c.company_name, c.fio, c.email FROM otgruzki o INNER JOIN dogovors d ON o.dogovor_id = d.dogovor_id INNER JOIN rasshifrovka r ON d.dogovor_id = r.dogovor_id INNER JOIN tovars t ON r.tovar_id = t.tovar_id INNER JOIN clients c ON d.client_id = c.client_id WHERE d.otmetka_ob_oplate = true AND o.otgruzka_date BETWEEN '2023-04-01' AND '2023-06-30' AND c.client_id = 1;";
                string sql = string.Format("SELECT S.supply_detail_id, P.product_id, P.supplier_id, P.product_name, P.unit, P.price, P.weight, S.quanity, S.sum_total \r\nFROM Supply_details AS S\r\nJOIN Products AS P\r\nON P.product_id = S.product_id\r\nWHERE S.supply_id = '{0}';", 11);
                // Создание команды с параметрами
                using (NpgsqlCommand command = new NpgsqlCommand(sql, conn))
                {
                    //command.Parameters.AddWithValue("@client_id", 1);
                    //command.Parameters.AddWithValue("@start_date", new DateTime(year: 2023, month: 3, day: 1));
                    //command.Parameters.AddWithValue("@end_date", new DateTime(year: 2023, month: 7, day: 29));
                    // Создание адаптера данных и заполнение таблицы
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        // Создание экземпляра приложения Excel
                        Excel.Application excel = new Excel.Application();
                        // Создание нового рабочего книги
                        Excel.Workbook workbook = excel.Workbooks.Add(Type.Missing);
                        // Создание нового листа в книге
                        Excel.Worksheet worksheet = (Excel.Worksheet)workbook.ActiveSheet;
                        worksheet.Name = "Накладная";
                        // Настройка шапки таблицы
                        worksheet.Cells[3, 1] = "индентификатор в чеке";
                        worksheet.Cells[3, 2] = "индентификатор продукта";
                        worksheet.Cells[3, 3] = "индентификатор поставщика";
                        worksheet.Cells[3, 4] = "название товара";
                        worksheet.Cells[3, 5] = "единица товара";
                        worksheet.Cells[3, 6] = "цена за 1 единицу";
                        worksheet.Cells[3, 7] = "вес";
                        worksheet.Cells[3, 8] = "количество";
                        worksheet.Cells[3, 9] = "итоговая сумма товара";
                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            worksheet.Cells[i + 4, 1] = dataTable.Rows[i]["supply_detail_id"].ToString();
                            worksheet.Cells[i + 4, 2] = dataTable.Rows[i]["product_id"].ToString();
                            worksheet.Cells[i + 4, 3] = dataTable.Rows[i]["supplier_id"].ToString();
                            worksheet.Cells[i + 4, 4] = dataTable.Rows[i]["product_name"].ToString();
                            worksheet.Cells[i + 4, 5] = dataTable.Rows[i]["unit"].ToString();
                            worksheet.Cells[i + 4, 6] = dataTable.Rows[i]["price"].ToString();
                            worksheet.Cells[i + 4, 7] = dataTable.Rows[i]["weight"].ToString();
                            worksheet.Cells[i + 4, 8] = dataTable.Rows[i]["quanity"].ToString();
                            worksheet.Cells[i + 4, 9] = dataTable.Rows[i]["sum_total"].ToString();
                        }
                        worksheet.Cells[1, 1] = "Номер отгрузки";
                        worksheet.Cells[1, 2] = "1";
                        worksheet.Cells[2, 1] = "Дата отгрузки";
                        worksheet.Cells[2, 2] = "23.05.2023";
                        // Сохранение книги в файл и закрытие приложения Excel
                        workbook.SaveAs("Накладная1.xlsx");
                        workbook.Close();
                        excel.Quit();
                    }
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            XmlDocument document = new XmlDocument();
            XmlNode root = document.CreateElement("export");
            document.AppendChild(root);


            //string[] strr = { "индентификатор_в_чеке", "индентификатор_продукта", "индентификатор_поставщика", "название_товара", "единица_товара", "цена_за_1_единицу", "вес", "количество", "итоговая_сумма_товара" };
            for (int j = 0; j < dataGridView1.RowCount - 1; j++)
            {
                XmlNode record = document.CreateElement("record");
                record.InnerText = j.ToString();
                root.AppendChild(record);

                for (int i = 0; i < dataGridView1.ColumnCount; i++) // dataGridView1.ColumnCount
                {
                    XmlNode column = document.CreateElement(dataGridView1.Columns[i].HeaderText);//strr[i] dataGridView1.Columns[i].HeaderText);
                    column.InnerText = dataGridView1.Rows[j].Cells[i].Value.ToString();
                    record.AppendChild(column);
                }
            }
            count += 1;

            string xml_name = string.Format("exportDocument_№{0}.xml", count);
            document.Save(xml_name);

            Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook();

            // Импорт XML в книгу
            // ImportXml принимает в качестве аргументов путь к файлу XML, имя конечного листа, целевую строку и столбец. 
            workbook.ImportXml(xml_name, "Sheet1", 0, 0);

            // Вызовите метод «Сохранить», чтобы сохранить книгу в формате XLSX.
            string xlsx_name = string.Format("накладная_№{0}.xlsx", count);
            workbook.Save(xlsx_name, Aspose.Cells.SaveFormat.Auto);
            MessageBox.Show("Накладная сформирована", "Выполнено", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
