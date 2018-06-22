using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace updateData
{
    public partial class dangnhap : UserControl
    {
        public dangnhap()
        {
            InitializeComponent();
        }
        void ham()
        {
            if (!string.IsNullOrEmpty(txttaikhoan.Text) && !string.IsNullOrEmpty(txtpass.Text))
            {
                string[] kiem = new string[2];
                var con = ketnoi.Khoitao();

                kiem = con.kiemtradangnhap(txttaikhoan.Text, txtpass.Text);
                if (kiem[0] == txttaikhoan.Text && kiem[1] == txtpass.Text)
                {
                    MessageBox.Show("Đăng nhập thành công");
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác \nXem lại");
                    txttaikhoan.Clear();
                    txtpass.Clear();
                    txttaikhoan.Focus();
                }
            }
            else
            {
                MessageBox.Show("Không được để trống dòng nào\nThu lai");
                txttaikhoan.Clear();
                txtpass.Clear();
                txttaikhoan.Focus();
            }
        }
        private void btndangnhap_Click(object sender, EventArgs e)
        {
            ham();
        }

        private void txttaikhoan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtpass.Focus();
            }
        }

        private void txtpass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ham();
            }
        }

        private void dangnhap_Load(object sender, EventArgs e)
        {

            txttaikhoan.Focus();
        }
    }
}
