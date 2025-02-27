using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using appSuper.Controller;
using appSuper.Model;

namespace appSuper
{
    public partial class nhanVienUC : UserControl
    {
        public nhanVienUC()
        {
            InitializeComponent();
            LoadingData();
        }
        public void LoadingData()
        {
            dgvNhanVien.Rows.Clear();
            List<NhanVien> nhanViens = NhanVienController.getAllNhanViens();
            foreach (NhanVien nhanVien in nhanViens)
            {
                dgvNhanVien.Rows.Add(nhanVien.maNV, nhanVien.tenNV, nhanVien.namSinh, nhanVien.soDT, nhanVien.email,nhanVien.diaChi);
            }
        }

        private void btnThemNV_Click(object sender, EventArgs e)
        {
            var nhanViens = new NhanVien
            {
                maNV = txtMaNV.Text,
                tenNV = txtTenNV.Text,
                namSinh = txtNamSinh.Value,
                soDT = txtSoDT.Text,
                email = txtEmail.Text,
                diaChi = txtDiaChi.Text
            };
            NhanVienController.AddNhanViens(nhanViens);
            LoadingData();
        }

        private void btnSuaNV_Click(object sender, EventArgs e)
        {
            var nhanViens = new NhanVien
            {
                maNV = txtMaNV.Text,
                tenNV = txtTenNV.Text,
                namSinh = txtNamSinh.Value,
                soDT = txtSoDT.Text,
                email = txtEmail.Text,
                diaChi = txtDiaChi.Text
            };
            NhanVienController.UpdateNhanViens(nhanViens);
            LoadingData();
        }

        private void btnXoaNV_Click(object sender, EventArgs e)
        {
            var nhanViens = new NhanVien
            {
                maNV = txtMaNV.Text,
            };
            NhanVienController.DeleteNhanViens(nhanViens);
            LoadingData();
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];
                txtMaNV.Text = row.Cells[0].Value?.ToString();
                txtTenNV.Text = row.Cells[1].Value?.ToString();
                txtNamSinh.Value = DateTime.Parse(row.Cells[2].Value?.ToString());
                txtSoDT.Text = row.Cells[3].Value?.ToString();
                txtEmail.Text = row.Cells[4].Value?.ToString();
                txtDiaChi.Text = row.Cells[5].Value?.ToString();
            }
        }

        private void txtSearcbNV_TextChanged(object sender, EventArgs e)
        {
            dgvNhanVien.Rows.Clear();
            var text = txtSearcbNV.Text;
            List<NhanVien> nhanViens = NhanVienController.SearchNhanViens(text);
            foreach (NhanVien nhanVien in nhanViens)
            {
                dgvNhanVien.Rows.Add(nhanVien.maNV, nhanVien.tenNV, nhanVien.namSinh, nhanVien.soDT, nhanVien.email, nhanVien.diaChi);
            }
        }
    }
}
