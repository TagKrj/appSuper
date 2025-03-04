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
    public partial class myPhamUC : UserControl
    {
        public myPhamUC()
        {
            InitializeComponent();
            LoadingData();
            cboNhaCungCapMyPham.Items.Clear();
            LoadingCboNCC();

           
        }
        public void LoadingData()
        {
            List<MyPham> MyPhams = MyPhamController.getAllMyPhams();
            dgvMyPham.Rows.Clear();
            foreach (MyPham MyPham in MyPhams)
            {
                dgvMyPham.Rows.Add(MyPham.maSP, MyPham.tenSP, MyPham.nhaCungCap, MyPham.soLuong, MyPham.giaNhap, MyPham.giaBan);
            }
        }
        private void LoadingCboNCC()
        {
            List<NhaCungCap> nhaCungCaps = NhaCungCapController.getAllNhaCungCaps();
            foreach (var nhaCungCap in nhaCungCaps)
            {
                cboNhaCungCapMyPham.Items.Add(nhaCungCap.maNhaCC);
            }
        }
        private void ClearTxt()
        {
            txtMaSPMyPham.Text = "";
            txtTenSPMyPham.Text = "";
            cboNhaCungCapMyPham.SelectedIndex = -1;
            txtSoLuongMyPham.Text = "";
            txtGiaNhapMyPham.Text = "";
            txtGiaBanMyPham.Text = "";
        }

        private void btnThemMyPham_Click(object sender, EventArgs e)
        {
            var MyPham = new MyPham
            {
                maSP = txtMaSPMyPham.Text,
                tenSP = txtTenSPMyPham.Text,
                nhaCungCap = cboNhaCungCapMyPham.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongMyPham.Text),
                giaNhap = decimal.Parse(txtGiaNhapMyPham.Text),
                giaBan = decimal.Parse(txtGiaBanMyPham.Text),
            };
            MyPhamController.AddMyPhams(MyPham);
            LoadingData();
            ClearTxt();
        }

        private void btnSuaMyPham_Click(object sender, EventArgs e)
        {
            var MyPham = new MyPham
            {
                maSP = txtMaSPMyPham.Text,
                tenSP = txtTenSPMyPham.Text,
                nhaCungCap = cboNhaCungCapMyPham.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongMyPham.Text),
                giaNhap = decimal.Parse(txtGiaNhapMyPham.Text),
                giaBan = decimal.Parse(txtGiaBanMyPham.Text),
            };
            MyPhamController.UpdateMyPhams(MyPham);
            LoadingData();
            ClearTxt();
        }

        private void btnXoaMyPham_Click(object sender, EventArgs e)
        {
            var MyPham = new MyPham
            {
                maSP = txtMaSPMyPham.Text,
            };
            MyPhamController.DeleteMyPhams(MyPham);
            LoadingData();
            ClearTxt();
        }

        private void txtSearchMyPham_TextChanged(object sender, EventArgs e)
        {
            var text = txtSearchMyPham.Text;
            List<MyPham> MyPhams = MyPhamController.SearchMyPhams(text);
            dgvMyPham.Rows.Clear();
            foreach (MyPham MyPham in MyPhams)
            {
                dgvMyPham.Rows.Add(MyPham.maSP, MyPham.tenSP, MyPham.nhaCungCap, MyPham.soLuong, MyPham.giaNhap, MyPham.giaBan);
            }
        }

        private void dgvMyPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvMyPham.Rows[e.RowIndex];
                txtMaSPMyPham.Text = row.Cells[0].Value?.ToString();
                txtTenSPMyPham.Text = row.Cells[1].Value?.ToString();
                cboNhaCungCapMyPham.SelectedItem = row.Cells[2].Value?.ToString();
                txtSoLuongMyPham.Text = row.Cells[3].Value?.ToString();
                txtGiaNhapMyPham.Text = row.Cells[4].Value?.ToString();
                txtGiaBanMyPham.Text = row.Cells[5].Value?.ToString();
            }
        }
    }
}
