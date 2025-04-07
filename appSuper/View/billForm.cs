using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace appSuper.View
{
    public partial class billForm: Form
    {
        public billForm()
        {
            InitializeComponent();
        }

        private void billForm_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(2);
            dataGridView1.Rows[0].Cells[0].Value = "Bùi Hiền Trang";
            dataGridView1.Rows[0].Cells[1].Value = "01"; 
            dataGridView1.Rows[0].Cells[2].Value = "234.000";
            dataGridView1.Rows[0].Cells[3].Value = "457.777";

            dataGridView1.Rows.Add(2);
            dataGridView1.Rows[1].Cells[0].Value = "Bùi Hiền Trang";
            dataGridView1.Rows[1].Cells[1].Value = "01";
            dataGridView1.Rows[1].Cells[2].Value = "234.000";
            dataGridView1.Rows[1].Cells[3].Value = "457.777";

            dataGridView1.Rows.Add(2);
            dataGridView1.Rows[2].Cells[0].Value = "Bùi Hiền Trang";
            dataGridView1.Rows[2].Cells[1].Value = "01";
            dataGridView1.Rows[2].Cells[2].Value = "234.000";
            dataGridView1.Rows[2].Cells[3].Value = "457.777";

            dataGridView1.Rows.Add(2);
            dataGridView1.Rows[3].Cells[0].Value = "Bùi Hiền Trang";
            dataGridView1.Rows[3].Cells[1].Value = "01";
            dataGridView1.Rows[3].Cells[2].Value = "234.000";
            dataGridView1.Rows[3].Cells[3].Value = "457.777";

        }
    }
}
