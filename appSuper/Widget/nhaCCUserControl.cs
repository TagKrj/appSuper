using appSuper.Controller;
using appSuper.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace appSuper
{
    public partial class nhaCCUC : UserControl
    {
        public nhaCCUC()
        {
            InitializeComponent();
            LoadingData();
        }
        private void LoadingData()
        {
            dgvNhaCC.Rows.Clear();
            List<NhaCungCap> nhaCungCaps = NhaCungCapController.getAllNhaCungCaps();
            foreach (NhaCungCap nhaCungCap in nhaCungCaps)
            {
                dgvNhaCC.Rows.Add(nhaCungCap.maNhaCC, nhaCungCap.tenNhaCC,nhaCungCap.diaChi);

            }
        }

        private void btnThemNhaCC_Click(object sender, EventArgs e)
        {
            var nhaCungCap = new NhaCungCap
            {
                maNhaCC = txtMaNhaCC.Text,
                tenNhaCC = txtTenNhaCC.Text,
                diaChi = txtDiaChiNhaCC.Text
            };
            NhaCungCapController.AddNhaCungCaps(nhaCungCap);
            LoadingData();
        }

        private void btnSuaNhaCC_Click(object sender, EventArgs e)
        {
            var nhaCungCap = new NhaCungCap
            {
                maNhaCC = txtMaNhaCC.Text,
                tenNhaCC = txtTenNhaCC.Text,
                diaChi = txtDiaChiNhaCC.Text
            };
            NhaCungCapController.UpdateNhaCungCaps(nhaCungCap);
            LoadingData();
        }

        private void dgvNhaCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNhaCC.Rows[e.RowIndex];
                txtMaNhaCC.Text = row.Cells[0].Value?.ToString();
                txtTenNhaCC.Text = row.Cells[1].Value?.ToString();
                txtDiaChiNhaCC.Text = row.Cells[2].Value?.ToString();
            }
        }

        private void btnXoaNhaCC_Click(object sender, EventArgs e)
        {
            var nhaCungCap = new NhaCungCap
            {
                maNhaCC = txtMaNhaCC.Text
            };
            NhaCungCapController.DeleteNhaCungCaps(nhaCungCap);
            LoadingData();
        }

        private void txtSearchNhaCC_TextChanged(object sender, EventArgs e)
        {
            dgvNhaCC.Rows.Clear();
            var searchText = txtSearchNhaCC.Text;
            List<NhaCungCap> nhaCungCaps = NhaCungCapController.SearchNhaCungCaps(searchText);
            foreach (NhaCungCap nhaCungCap in nhaCungCaps)
            {
                dgvNhaCC.Rows.Add(nhaCungCap.maNhaCC, nhaCungCap.tenNhaCC, nhaCungCap.diaChi);

            }
        }
    }
}
