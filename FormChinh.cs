﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BTL_LTW_QLCHS_G6
{
    public partial class QLCHS : Form
    {
        public QLCHS()
        {
            InitializeComponent();
        }

        string connectionstr = @"Data Source=LAPTOP-D4HEMV8S\MSSQLSERVER02;Initial Catalog=BTL_QLCHS;Integrated Security=True";
        SqlConnection conn = null;
        SqlDataAdapter daTL, daTG, daNXB, daSach, daTCSach;
        DataTable dtTL, dtTG, dtNXB, dtSach, dtTCSach;
        int dong;

        //Quản lý sách:
        private void FormChinh_Load(object sender, EventArgs e)
        {
            //Quản lý sách:
            conn = new SqlConnection(connectionstr);
            conn.Open();
            string sqlSach = "Select MaSach, TenSach, TenTL as TheLoai, TenTG as TacGia, TenNXB as NhaXuatBan, DonGia, SoLuong, NgayNhap  from Sach, TheLoai, TacGia, NXB where Sach.MaTL = TheLoai.MaTL and Sach.MaTG = TacGia.MaTG and Sach.MaNXB = NXB.MaNXB";
            daSach = new SqlDataAdapter(sqlSach, conn);
            dtSach = new DataTable();
            daSach.Fill(dtSach);
            dgv_QLSach.DataSource = dtSach;
                //combobox thể loại:
            string sqlTL = "Select * from TheLoai";
            daTL = new SqlDataAdapter(sqlTL, conn);
            dtTL = new DataTable();
            daTL.Fill(dtTL);
            cb_Theloai.DataSource = dtTL;
            cb_Tctheloai.DataSource = dtTL;
            cb_Theloai.DisplayMember = "TenTL";
            cb_Tctheloai.DisplayMember = "TenTL";
            cb_Theloai.ValueMember = "MaTL";
            cb_Tctheloai.ValueMember = "MaTL";
                //combobox tác giả:
            string sqlTG = "Select * from TacGia";
            daTG = new SqlDataAdapter(sqlTG, conn);
            dtTG = new DataTable();
            daTG.Fill(dtTG);
            cb_Tacgia.DataSource = dtTG;
            cb_Tctacgia.DataSource = dtTG;
            cb_Tacgia.DisplayMember = "TenTG";
            cb_Tctacgia.DisplayMember = "TenTG";
            cb_Tacgia.ValueMember = "MaTG";
            cb_Tctacgia.ValueMember = "MaTG";
                //combobox NXB:
            string sqlNXB = "Select * from NXB";
            daNXB = new SqlDataAdapter(sqlNXB, conn);
            dtNXB = new DataTable();
            daNXB.Fill(dtNXB);
            cb_Nxb.DataSource = dtNXB;
            cb_Tcnxb.DataSource = dtNXB;
            cb_Nxb.DisplayMember = "TenNXB";
            cb_Tcnxb.DisplayMember = "TenNXB";
            cb_Nxb.ValueMember = "MaNXB";
            cb_Tcnxb.ValueMember = "MaNXB";
        }

        private void bt_Themsach_Click(object sender, EventArgs e)
        {
            if(tb_Masach.Text ==""|| tb_Tensach.Text == "" || tb_Dongia.Text == "" || cb_Theloai.Text == "" || cb_Tacgia.Text == "" || cb_Nxb.Text == "" || nud_Soluongnhap.Value == 0)
            {
                MessageBox.Show("Chưa nhập đủ thông tin sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    string sach_Insert = "Insert into Sach values('" + tb_Masach.Text + "', N'" + tb_Tensach.Text + "', '" + tb_Dongia.Text + "', '" + nud_Soluongnhap.Value + "', '" + datetime_Nhapsach.Text + "', '" + cb_Theloai.SelectedValue + "', '" + cb_Tacgia.SelectedValue+ "', '" + cb_Nxb.SelectedValue + "')";
                    SqlCommand cmdSach = new SqlCommand(sach_Insert, conn);
                    cmdSach.ExecuteNonQuery();
                    dtSach.Rows.Clear();
                    daSach.Fill(dtSach);
                    tb_Masach.Text = "";
                    tb_Tensach.Text = "";
                    tb_Dongia.Text = "";
                    nud_Soluongnhap.Value = 0;
                    tb_Masach.Focus();
                    MessageBox.Show("Thêm sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (SqlException)
                {
                    MessageBox.Show("Mã sách đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void bt_Sua_Click(object sender, EventArgs e)
        {
            if (tb_Masach.Text == "" || tb_Tensach.Text == "" || tb_Dongia.Text == "" || cb_Theloai.Text == "" || cb_Tacgia.Text == "" || cb_Nxb.Text == "" || nud_Soluongnhap.Value == 0)
            {
                MessageBox.Show("Chưa nhập đủ thông tin sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    dong = dgv_QLSach.CurrentRow.Index;
                    string maSach = dgv_QLSach.Rows[dong].Cells[0].Value.ToString();
                    string sach_Update = "Update Sach set MaSach = '" + tb_Masach.Text + "', TenSach = N'" + tb_Tensach.Text + "', DonGia = '" + tb_Dongia.Text + "', SoLuong = '" + nud_Soluongnhap.Value + "', NgayNhap = '" + datetime_Nhapsach.Text + "', MaTL = '" + cb_Theloai.SelectedValue + "', MaTG = '" + cb_Tacgia.SelectedValue + "', MaNXB = '" + cb_Nxb.SelectedValue + "' where MaSach = '" + maSach + "'";
                    SqlCommand cmdSach = new SqlCommand(sach_Update, conn);
                    cmdSach.ExecuteNonQuery();
                    dtSach.Rows.Clear();
                    daSach.Fill(dtSach);
                    tb_Masach.Text = "";
                    tb_Tensach.Text = "";
                    tb_Dongia.Text = "";
                    nud_Soluongnhap.Value = 0;
                    tb_Masach.Focus();
                    MessageBox.Show("Sửa sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (SqlException)
                {
                    MessageBox.Show("Mã sách đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void bt_Xoa_Click(object sender, EventArgs e)
        {
            dong = dgv_QLSach.CurrentRow.Index;
            string maSach = dgv_QLSach.Rows[dong].Cells[0].Value.ToString();
            string sach_Delete = "Delete from Sach where MaSach = '" + maSach + "'";
            SqlCommand cmdSach = new SqlCommand(sach_Delete, conn);
            cmdSach.ExecuteNonQuery();
            dtSach.Rows.Clear();
            daSach.Fill(dtSach);
            tb_Masach.Text = "";
            tb_Tensach.Text = "";
            tb_Dongia.Text = "";
            nud_Soluongnhap.Value = 0;
            tb_Masach.Focus();
            MessageBox.Show("Xoá sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgv_QLSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dong = dgv_QLSach.CurrentRow.Index;
            tb_Masach.Text = dgv_QLSach.Rows[dong].Cells[0].Value.ToString();
            tb_Tensach.Text = dgv_QLSach.Rows[dong].Cells[1].Value.ToString();
            cb_Theloai.Text = dgv_QLSach.Rows[dong].Cells[2].Value.ToString();
            cb_Tacgia.Text = dgv_QLSach.Rows[dong].Cells[3].Value.ToString();
            cb_Nxb.Text = dgv_QLSach.Rows[dong].Cells[4].Value.ToString();
            tb_Dongia.Text = dgv_QLSach.Rows[dong].Cells[5].Value.ToString();
            nud_Soluongnhap.Value = Convert.ToInt32(dgv_QLSach.Rows[dong].Cells[6].Value);
            datetime_Nhapsach.Text = dgv_QLSach.Rows[dong].Cells[7].Value.ToString();
        }

        //Tra cứu:
        private void bt_Timsach_Click(object sender, EventArgs e)
        {
            if(checkb_Theloai.Checked == false && checkb_Tacgia.Checked == false && checkb_Nxb.Checked == false)
            {
                MessageBox.Show("Chưa chọn cách thức tra cứu sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                if (checkb_Theloai.Checked == true)
                {
                    string sqlTCSach = "Select MaSach, TenSach, TenTL as TheLoai, TenTG as TacGia, TenNXB as NhaXuatBan, DonGia, SoLuong, NgayNhap  from Sach, TheLoai, TacGia, NXB where Sach.MaTL = TheLoai.MaTL and Sach.MaTG = TacGia.MaTG and Sach.MaNXB = NXB.MaNXB and TenTL = N'" + cb_Tctheloai.Text + "'";
                    daTCSach = new SqlDataAdapter(sqlTCSach, conn);
                    dtTCSach = new DataTable();
                    daTCSach.Fill(dtTCSach);
                    dgv_Tracuu.DataSource = dtTCSach;
                    if (dgv_Tracuu.RowCount == 0)
                    {
                        MessageBox.Show("Không có sách thuộc thể loại " + cb_Tctheloai.Text + "!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    if(checkb_Tacgia.Checked == true)
                    {
                        string sqlTCSach = "Select MaSach, TenSach, TenTL as TheLoai, TenTG as TacGia, TenNXB as NhaXuatBan, DonGia, SoLuong, NgayNhap  from Sach, TheLoai, TacGia, NXB where Sach.MaTL = TheLoai.MaTL and Sach.MaTG = TacGia.MaTG and Sach.MaNXB = NXB.MaNXB and TenTG = N'" + cb_Tctacgia.Text + "'";
                        daTCSach = new SqlDataAdapter(sqlTCSach, conn);
                        dtTCSach = new DataTable();
                        daTCSach.Fill(dtTCSach);
                        dgv_Tracuu.DataSource = dtTCSach;
                        if (dgv_Tracuu.RowCount == 0)
                        {
                            MessageBox.Show("Không có sách của tác giả " + cb_Tctacgia.Text + "!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                        if(checkb_Nxb.Checked == true)
                        {
                            string sqlTCSach = "Select MaSach, TenSach, TenTL as TheLoai, TenTG as TacGia, TenNXB as NhaXuatBan, DonGia, SoLuong, NgayNhap  from Sach, TheLoai, TacGia, NXB where Sach.MaTL = TheLoai.MaTL and Sach.MaTG = TacGia.MaTG and Sach.MaNXB = NXB.MaNXB and TenNXB = N'" + cb_Tcnxb.Text + "'";
                            daTCSach = new SqlDataAdapter(sqlTCSach, conn);
                            dtTCSach = new DataTable();
                            daTCSach.Fill(dtTCSach);
                            dgv_Tracuu.DataSource = dtTCSach;
                            if (dgv_Tracuu.RowCount == 0)
                            {
                                MessageBox.Show("Không có sách của NXB " + cb_Tcnxb.Text + "!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
        }


        private void bt_Khachhang_Click(object sender, EventArgs e)
        {
            FormQLKH formQLKH = new FormQLKH();
            formQLKH.ShowDialog();
        }

        private void bt_Theloai_Click(object sender, EventArgs e)
        {
            FormQLTL formQLTL = new FormQLTL();
            formQLTL.ShowDialog();
        }

        private void bt_Tacgia_Click(object sender, EventArgs e)
        {
            FormQLTG formQLTG = new FormQLTG();
            formQLTG.ShowDialog();
        }

        private void bt_Nxb_Click(object sender, EventArgs e)
        {
            FormQLNXB formQLNXB = new FormQLNXB();
            formQLNXB.ShowDialog();
        }

        private void dgv_Tracuu_MouseHover_1(object sender, EventArgs e)
        {

        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            dtSach.Rows.Clear();
            daSach.Fill(dtSach);
        }

        private void rb_Tracuusach_CheckedChanged(object sender, EventArgs e)
        {
            //Kiểm tra có chọn Tra cứu sách không thì hiển thị nội dung tương ứng

                string sqlTCSach = "Select MaSach, TenSach, TenTL as TheLoai, TenTG as TacGia, TenNXB as NhaXuatBan, DonGia, SoLuong, NgayNhap  from Sach, TheLoai, TacGia, NXB" +
                    " where Sach.MaTL = TheLoai.MaTL and Sach.MaTG = TacGia.MaTG and Sach.MaNXB = NXB.MaNXB and TenTL = N'null'";
                daTCSach = new SqlDataAdapter(sqlTCSach, conn);
                dtTCSach = new DataTable();
                daTCSach.Fill(dtTCSach);
                dgv_Tracuu.DataSource = dtTCSach;


        }


        private void checkb_Theloai_Click(object sender, EventArgs e)
        {
            //Kiểm tra nếu chọn Tra cứu theo thể loại thì cho phép chọn nội dung tương ứng
            if (checkb_Theloai.Checked)
            {
                cb_Tctheloai.Enabled = true;
                checkb_Tacgia.Checked = false;
                checkb_Nxb.Checked = false;
                cb_Tctacgia.Enabled = false;
                cb_Tcnxb.Enabled = false;
            }
            else
            {
                cb_Tctacgia.Enabled = true;
                cb_Tcnxb.Enabled = true;
            }
        }

        private void checkb_Tacgia_Click(object sender, EventArgs e)
        {
            //Kiểm tra nếu chọn Tra cứu theo tác giả thì cho phép chọn nội dung tương ứng
            if (checkb_Tacgia.Checked)
            {
                cb_Tctacgia.Enabled = true;
                checkb_Theloai.Checked = false;
                checkb_Nxb.Checked = false;
                cb_Tctheloai.Enabled = false;
                cb_Tcnxb.Enabled = false;
            }
            else
            {
                cb_Tctheloai.Enabled = true;
                cb_Tcnxb.Enabled = true;
            }
        }

        private void checkb_Nxb_Click(object sender, EventArgs e)
        {
            //Kiểm tra nếu chọn Tra cứu theo NXB thì cho phép chọn nội dung tương ứng
            if (checkb_Nxb.Checked)
            {
                cb_Tcnxb.Enabled = true;
                checkb_Theloai.Checked = false;
                checkb_Tacgia.Checked = false;
                cb_Tctheloai.Enabled = false;
                cb_Tctacgia.Enabled = false;
            }
            else
            {
                cb_Tctheloai.Enabled = true;
                cb_Tctacgia.Enabled = true;
            }
        }
        
       
        private void dgv_Tracuusach_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gb_Tracuu_Enter(object sender, EventArgs e)
        {

        }


        
    }
}
