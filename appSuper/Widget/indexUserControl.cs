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
    public partial class trangChuUC : UserControl
    {
        public trangChuUC()
        {
            InitializeComponent();
        }

        private void indexUserControl_Load(object sender, EventArgs e)
        {
            tableThongke.Rows.Add(5);
            tableThongke.Rows[0].Cells[0].Value = "01";
            tableThongke.Rows[0].Cells[1].Value = "Bùi Hiền Trang";
            tableThongke.Rows[0].Cells[2].Value = "Hà Nội";
            tableThongke.Rows[0].Cells[3].Value = "20";
            tableThongke.Rows[0].Cells[4].Value = "Áo phông thủy thủ";

            tableThongke.Rows.Add(5);
            tableThongke.Rows[1].Cells[0].Value = "01";
            tableThongke.Rows[1].Cells[1].Value = "Bùi Hiền Trang";
            tableThongke.Rows[1].Cells[2].Value = "Hà Nội";
            tableThongke.Rows[1].Cells[3].Value = "20";
            tableThongke.Rows[1].Cells[4].Value = "Áo phông thủy thủ";

            tableThongke.Rows.Add(5);
            tableThongke.Rows[2].Cells[0].Value = "01";
            tableThongke.Rows[2].Cells[1].Value = "Bùi Hiền Trang";
            tableThongke.Rows[2].Cells[2].Value = "Hà Nội";
            tableThongke.Rows[2].Cells[3].Value = "20";
            tableThongke.Rows[2].Cells[4].Value = "Áo phông thủy thủ";

            tableThongke.Rows.Add(5);
            tableThongke.Rows[3].Cells[0].Value = "01";
            tableThongke.Rows[3].Cells[1].Value = "Bùi Hiền Trang";
            tableThongke.Rows[3].Cells[2].Value = "Hà Nội";
            tableThongke.Rows[3].Cells[3].Value = "20";
            tableThongke.Rows[3].Cells[4].Value = "Áo phông thủy thủ";

            tableThongke.Rows.Add(5);
            tableThongke.Rows[3].Cells[0].Value = "01";
            tableThongke.Rows[3].Cells[1].Value = "Bùi Hiền Trang";
            tableThongke.Rows[3].Cells[2].Value = "Hà Nội";
            tableThongke.Rows[3].Cells[3].Value = "20";
            tableThongke.Rows[3].Cells[4].Value = "Áo phông thủy thủ";
        }
    }
}
