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
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conn1"].ConnectionString);
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Load_grdNhaxuatban();
        }

        private void Load_grdNhaxuatban()
        {

            if (con.State == ConnectionState.Closed)
                con.Open();

            // B2. Tạo đối tượng cmd lấy dữ liệu từ bảng nxb
            //B3. Tạo đối tượng dataadapter để lấy dữ liệu từ command
            SqlDataAdapter da = new SqlDataAdapter("select * from NHAXUATBAN", con);
            //B4. Đổ dữ liệu từ adapter vào datatable
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            con.Close();
            // B5. Hiển thị data base lên datagridview
            grdNhaxuatban.DataSource = dt;
            grdNhaxuatban.Refresh();
    
        }


        private void CheckTrungmanxb(string p_Manxb, ref int p_kq)
        {
            

        if (con.State == ConnectionState.Closed)
            con.Open();

        //B2: Tạo đối tượng command để thực hiện ktra trùng mã nxb

        SqlCommand cmd = new SqlCommand("CheckTrungmanxb", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@manxb", sqlDbType: SqlDbType.NVarChar, 50).Value = p_Manxb;
        SqlParameter kq = new SqlParameter("@ketqua", SqlDbType.Int);
        cmd.Parameters.Add(kq).Direction = ParameterDirection.Output;
        cmd.ExecuteNonQuery();


        //B3: Lấy giá trị từ command đưa ra ngoài 
        p_kq = (int)kq.Value;
        cmd.Dispose();
        con.Clone();
    }

    private void btnLuu_Click(object sender, EventArgs e)
        {
            string p_Manxb = txtManxb.Text.Trim();
            string p_Tennxb = txtTennxb.Text.Trim();
            string p_Dienthoai = txtDienthoai.Text.Trim();
            string p_Email = txtEmail.Text.Trim() ;
            string p_Diachi = txtDiachi.Text.Trim() ;
            string p_Ghichu = txtGhichu.Text.Trim() ;

            if(con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            string sql = "Insert NHAXUATBAN Values ('" + p_Manxb + "',N'" + p_Tennxb + "',N'" + p_Dienthoai + "'," +
                "N'" + p_Email + "',N'" + p_Diachi + "',N'" + p_Ghichu + "')";
            SqlCommand cmd = new SqlCommand(sql,con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            MessageBox.Show("Thêm mới thành công!!!");
            
        }


        private void btnSua_Click(object sender, EventArgs e, SqlConnection con)
        {
            //B1: Lấy dữ liệu từ các control đưa vào biến
            string p_manxb = txtManxb.Text.Trim();
            string p_hoten = txtTennxb.Text.Trim();
            string p_dienthoai = txtDienthoai.Text.Trim();
            string p_email = txtEmail.Text.Trim();
            string p_diachi = txtDiachi.Text.Trim();
            string p_ghichu = txtGhichu.Text.Trim();
            //B2: Kết nối đến bd
            SqlConnection conn = new SqlConnection("con");
            if(conn.State == ConnectionState.Closed)
            conn.Open();
            //B3. Tạo đối tượng command để tiến hành sửa dl
            string sql = "Update Nhaxuatban Set Tennxb = N'" + p_hoten + "'," +
                " Dienthoai ='" + p_dienthoai + "', Email = '" + p_email + "'," +
                "Diachi = N'" + p_diachi + "', Ghichu =N'" + p_ghichu + "'where Manxb ='" + p_manxb + "'";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
            MessageBox.Show("Sửa thành công");
            Load_grdNhaxuatban();
            txtManxb.Enabled = true;
        }

        private void grdNhaxuatban_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
