using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Office.Interop.Excel;


namespace CapnhatNXB
{
    public partial class Form2 : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conn1"].ConnectionString);

        public Form2()
        {
            InitializeComponent();
        }

        public void Load_dgvTacgia()
        {
            if(con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlDataAdapter da = new SqlDataAdapter("Select * from Tacgia",con);
            System.Data.DataTable tb = new System.Data.DataTable();
            da.Fill(tb);
            dgvTacgia.DataSource = tb;
            dgvTacgia.Refresh();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Load_dgvTacgia();
        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        private void ReadExcel()
        {
            //kiểm tra xem filename đã có dữ liệu chưa
            if (filename == null)
            {
                MessageBox.Show("Chưa chọn file");
            }
            else
            {
                xls.Application Excel = new xls.Application();// tạp một app làm việc mới
                // mở dữ liệu từ file
                Excel.Workbooks.Open(filename);
                //đọc dữ liệu từng sheet của excel
                foreach (xls.Worksheet wsheet in Excel.Worksheets)
                {
                    int i = 2;  //để đọc từng dòng của sheet bắt đầu từ dòng số 2
                    do
                    {
                        if (wsheet.Cells[i, 1].Value == null && wsheet.Cells[i, 2].Value == null && wsheet.Cells[i, 3].Value == null)
                        {
                            break;
                        }
                        else
                        {
                            //Đổ dòng dữ liệu vào DB
                            ThemSV(wsheet.Cells[i, 1].Value, wsheet.Cells[i, 2].Value, wsheet.Cells[i, 3].Value, wsheet.Cells[i, 4].Value,
                                wsheet.Cells[i, 5].Value, wsheet.Cells[i, 6].Value, wsheet.Cells[i, 7].Value, wsheet.Cells[i, 8].Value);
                            i++;
                        }
                    }
                    while (true);
                }
            }

        }
    }
}
