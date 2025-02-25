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
    public partial class thuocUC : UserControl
    {
        public thuocUC()
        {
            InitializeComponent();
            dgvThuoc.Rows.Clear();
            LoadingData();
        }

        private void LoadingData()
        {
            List<Thuoc> thuoc = ThuocController.GetAllThuocs();
            foreach (Thuoc t in thuoc)
            {
                dgvThuoc.Rows.Add(t.maSP, t.tenSP, t.nhaCungCap, t.soLuong, t.giaNhap, t.giaBan);

            }
        }
    }
}
