using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace appSuper
{
    public partial class indexForm : Form
    {
        public indexForm()
        {
            InitializeComponent();
            trangChuUC uC = new trangChuUC();
            addUserControl(uC);
        }
        private void moveImageBox(object sender)
        {
            Guna2Button b = (Guna2Button)sender;
            imgSlide.Location = new Point(b.Location.X + 92, b.Location.Y - 17);
            imgSlide.SendToBack();
        }
        private void btnIndex_CheckedChanged(object sender, EventArgs e)
        {
            moveImageBox(sender);
        }

        private void btnThoat_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addUserControl(UserControl uc)
        {
            panelContainer.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            uc.BringToFront();
            panelContainer.Controls.Add(uc);
        }

        private void btnIndex_Click(object sender, EventArgs e)
        {
            trangChuUC uC = new trangChuUC();
            addUserControl(uC);
        }

        private void btnThoiTrang_Click(object sender, EventArgs e)
        {
            thoiTrangUC uC = new thoiTrangUC();
            addUserControl(uC);
        }

        private void btnDienTu_Click(object sender, EventArgs e)
        {
            dienTuUC uC = new dienTuUC();
            addUserControl(uC);
        }

        private void btnGiaDung_Click(object sender, EventArgs e)
        {
            giaDungUC uC = new giaDungUC();
            addUserControl(uC);
        }

        private void btnMyPham_Click(object sender, EventArgs e)
        {
            myPhamUC uC = new myPhamUC();
            addUserControl(uC);
        }

        private void btnThucPham_Click(object sender, EventArgs e)
        {
            thucPhamUC uC = new thucPhamUC();
            addUserControl(uC);
        }

        private void btnDoChoi_Click(object sender, EventArgs e)
        {
            doChoiUC uC = new doChoiUC();
            addUserControl(uC);
        }

        private void btnTheThao_Click(object sender, EventArgs e)
        { 
            theThaoUC uC = new theThaoUC();
            addUserControl(uC);
        }

        private void btnSach_Click(object sender, EventArgs e)
        { 
            sachUC uC = new sachUC();
            addUserControl(uC);
        }

        private void btnThuCung_Click(object sender, EventArgs e)
        {
            thuCungUC uC = new thuCungUC();
            addUserControl(uC);
        }

        private void btnThuoc_Click(object sender, EventArgs e)
        {
            thuocUC uC = new thuocUC();
            addUserControl(uC);
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            nhanVienUC uC = new nhanVienUC();
            addUserControl(uC);
        }

        private void btnNhaCC_Click(object sender, EventArgs e)
        {
            nhaCCUC uC = new nhaCCUC();
            addUserControl(uC);
        }
    }
}
