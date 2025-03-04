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
    public partial class thucPhamUC : UserControl
    {
        public thucPhamUC()
        {
            InitializeComponent();
            LoadingData();
            cboNhaCungCapThucPham.Items.Clear();
            LoadingCboNCC();


        }
        public void LoadingData()
        {
            List<ThucPham> ThucPhams = ThucPhamController.getAllThucPhams();
            dgvThucPham.Rows.Clear();
            foreach (ThucPham ThucPham in ThucPhams)
            {
                dgvThucPham.Rows.Add(ThucPham.maSP, ThucPham.tenSP, ThucPham.nhaCungCap, ThucPham.soLuong, ThucPham.giaNhap, ThucPham.giaBan);
            }
        }
        private void LoadingCboNCC()
        {
            List<NhaCungCap> nhaCungCaps = NhaCungCapController.getAllNhaCungCaps();
            foreach (var nhaCungCap in nhaCungCaps)
            {
                cboNhaCungCapThucPham.Items.Add(nhaCungCap.maNhaCC);
            }
        }
        private void ClearTxt()
        {
            txtMaSPThucPham.Text = "";
            txtTenSPThucPham.Text = "";
            cboNhaCungCapThucPham.SelectedIndex = -1;
            txtSoLuongThucPham.Text = "";
            txtGiaNhapThucPham.Text = "";
            txtGiaBanThucPham.Text = "";
        }

        private void btnThemThucPham_Click(object sender, EventArgs e)
        {
            var ThucPham = new ThucPham
            {
                maSP = txtMaSPThucPham.Text,
                tenSP = txtTenSPThucPham.Text,
                nhaCungCap = cboNhaCungCapThucPham.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongThucPham.Text),
                giaNhap = decimal.Parse(txtGiaNhapThucPham.Text),
                giaBan = decimal.Parse(txtGiaBanThucPham.Text),
            };
            ThucPhamController.AddThucPhams(ThucPham);
            LoadingData();
            ClearTxt();
        }

        private void btnSuaThucPham_Click(object sender, EventArgs e)
        {
            var ThucPham = new ThucPham
            {
                maSP = txtMaSPThucPham.Text,
                tenSP = txtTenSPThucPham.Text,
                nhaCungCap = cboNhaCungCapThucPham.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongThucPham.Text),
                giaNhap = decimal.Parse(txtGiaNhapThucPham.Text),
                giaBan = decimal.Parse(txtGiaBanThucPham.Text),
            };
            ThucPhamController.UpdateThucPhams(ThucPham);
            LoadingData();
            ClearTxt();
        }

        private void btnXoaThucPham_Click(object sender, EventArgs e)
        {
            var ThucPham = new ThucPham
            {
                maSP = txtMaSPThucPham.Text,
            };
            ThucPhamController.DeleteThucPhams(ThucPham);
            LoadingData();
            ClearTxt();
        }

        private void txtSearchThucPham_TextChanged(object sender, EventArgs e)
        {
            var text = txtSearchThucPham.Text;
            List<ThucPham> ThucPhams = ThucPhamController.SearchThucPhams(text);
            dgvThucPham.Rows.Clear();
            foreach (ThucPham ThucPham in ThucPhams)
            {
                dgvThucPham.Rows.Add(ThucPham.maSP, ThucPham.tenSP, ThucPham.nhaCungCap, ThucPham.soLuong, ThucPham.giaNhap, ThucPham.giaBan);
            }
        }

        private void dgvThucPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvThucPham.Rows[e.RowIndex];
                txtMaSPThucPham.Text = row.Cells[0].Value?.ToString();
                txtTenSPThucPham.Text = row.Cells[1].Value?.ToString();
                cboNhaCungCapThucPham.SelectedItem = row.Cells[2].Value?.ToString();
                txtSoLuongThucPham.Text = row.Cells[3].Value?.ToString();
                txtGiaNhapThucPham.Text = row.Cells[4].Value?.ToString();
                txtGiaBanThucPham.Text = row.Cells[5].Value?.ToString();
            }
        }
    }
}
