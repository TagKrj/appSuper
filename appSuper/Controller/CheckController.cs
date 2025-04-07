using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace appSuper.Controller
{
    class CheckController
    {
        public bool CheckMaNotNull(string maSP)
        {
            if (string.IsNullOrWhiteSpace(maSP))
            {
                MessageBox.Show("Mã sản phẩm không được để trống!");
                return false;
            }

            return true; // Mã sản phẩm hợp lệ (không trống)
        }

        public bool CheckSoLuong(int soLuong)
        {
            // Kiểm tra số lượng là số dương
            return soLuong >= 0;
        }

        public bool CheckMail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Email không được để trống!");
                return false;
            }

            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Email không hợp lệ! Vui lòng nhập đúng định dạng.");
                return false;
            }

            return true; // Email hợp lệ
        }

        public bool CheckSdt(string sdt)
        {
            if (string.IsNullOrWhiteSpace(sdt))
            {
                MessageBox.Show("Số điện thoại không được để trống!");
                return false;
            }

            if (!Regex.IsMatch(sdt, @"^0[0-9]{9,10}$"))
            {
                MessageBox.Show("Số điện thoại không hợp lệ! Vui lòng nhập số bắt đầu bằng '0' và có 9-10 chữ số.");
                return false;
            }

            return true; // Số điện thoại hợp lệ
        }

        public bool CheckGia(string gia)
        {
            if (!decimal.TryParse(gia, out decimal val))
            {
                MessageBox.Show("Nhập lại Giá! Vui lòng nhập số.");
                return false;
            }

            if (val < 0)
            {
                MessageBox.Show("Giá trị của Giá không được là số âm!");
                return false;
            }

            return true; // Giá trị hợp lệ
        }
    }
}