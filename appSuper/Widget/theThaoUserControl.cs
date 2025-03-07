using System.Collections.Generic;
using System.Windows.Forms;
using appSuper.Controller;
using appSuper.Model;

namespace appSuper
{
    public partial class theThaoUC : UserControl
    {
        public theThaoUC()
        {
            InitializeComponent();
            LoadingData();
            cboNhaCungCapTheThao.Items.Clear();
            LoadingCboNCC();
        }
        private void LoadingData()
        {
            dgvTheThao.Rows.Clear();
            List<TheThao> TheThaos = TheThaoController.getAllTheThaos();
            foreach (TheThao TheThao in TheThaos)
            {
                dgvTheThao.Rows.Add(TheThao.maSP, TheThao.tenSP, TheThao.nhaCungCap, TheThao.soLuong, TheThao.giaNhap, TheThao.giaBan);

            }
        }

        private void btnThemTheThao_Click(object sender, System.EventArgs e)
        {
            var TheThao = new TheThao
            {
                maSP = txtMaSPTheThao.Text,
                tenSP = txtTenSPTheThao.Text,
                nhaCungCap = cboNhaCungCapTheThao.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongTheThao.Text),
                giaNhap = decimal.Parse(txtGiaNhapTheThao.Text),
                giaBan = decimal.Parse(txtGiaBanTheThao.Text)
            };
            TheThaoController.AddTheThaos(TheThao);
            LoadingData();
        }

        private void btnSuaTheThao_Click(object sender, System.EventArgs e)
        {
            var TheThao = new TheThao
            {
                maSP = txtMaSPTheThao.Text,
                tenSP = txtTenSPTheThao.Text,
                nhaCungCap = cboNhaCungCapTheThao.SelectedItem.ToString(),
                soLuong = int.Parse(txtSoLuongTheThao.Text),
                giaNhap = decimal.Parse(txtGiaNhapTheThao.Text),
                giaBan = decimal.Parse(txtGiaBanTheThao.Text)
            };
            TheThaoController.UpdateTheThaos(TheThao);
            LoadingData();
        }

        private void btnXoaTheThao_Click(object sender, System.EventArgs e)
        {
            var TheThao = new TheThao
            {
                maSP = txtMaSPTheThao.Text,
            };
            TheThaoController.DeleteTheThaos(TheThao);
            LoadingData();
        }

        private void txtSearchTheThao_TextChanged(object sender, System.EventArgs e)
        {
            dgvTheThao.Rows.Clear();
            var text = txtSearchTheThao.Text;
            List<TheThao> TheThaos = TheThaoController.SearchTheThaos(text);
            foreach (TheThao TheThao in TheThaos)
            {
                dgvTheThao.Rows.Add(TheThao.maSP, TheThao.tenSP, TheThao.nhaCungCap, TheThao.soLuong, TheThao.giaNhap, TheThao.giaBan);
            }
        }

        private void dgvTheThao_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvTheThao.Rows[e.RowIndex];
                txtMaSPTheThao.Text = row.Cells[0].Value.ToString();
                txtTenSPTheThao.Text = row.Cells[1].Value.ToString();
                cboNhaCungCapTheThao.SelectedItem = row.Cells[2].Value.ToString();
                txtSoLuongTheThao.Text = row.Cells[3].Value.ToString();
                txtGiaNhapTheThao.Text = row.Cells[4].Value.ToString();
                txtGiaBanTheThao.Text = row.Cells[5].Value.ToString();
            }
        }
        private void LoadingCboNCC()
        {
            List<NhaCungCap> nhaCungCaps = NhaCungCapController.getAllNhaCungCaps();
            foreach (var nhaCungCap in nhaCungCaps)
            {
                cboNhaCungCapTheThao.Items.Add(nhaCungCap.maNhaCC);
            }
        }
    }
}
