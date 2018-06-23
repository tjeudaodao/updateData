using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace updateData
{
    public partial class Form1 : Form
    {
        string duongdanUploadgoc = @"C:\ProgramData\MySQL\MySQL Server 8.0\Uploads\";
        public Form1()
        {
            InitializeComponent();
            dangnhap fdangnhap = new dangnhap();

            fdangnhap.Location = new Point(11, 11);
            fdangnhap.Name = "usdangnhap";
            this.Controls.Add(fdangnhap);

            fdangnhap.BringToFront();
        }

        private void btnChonfile_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openF = new OpenFileDialog();
                openF.Filter = "chon file csv (*.csv)|*.csv";
                if (openF.ShowDialog() == DialogResult.OK)
                {
                    string duongdanfile = openF.FileName;
                    lbduongdan.Text = duongdanfile;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Co loi ket noi voi may chu hoac file chon co van de");
            }
           
        }

        private void btnChay_Click(object sender, EventArgs e)
        {
            try
            {
                string soluongbandau = lbsoluongma.Text;
                string tenfile = Path.GetFileName(lbduongdan.Text);
                if (File.Exists(duongdanUploadgoc + tenfile))
                {
                    File.Delete(duongdanUploadgoc + tenfile);
                }
                File.Copy(lbduongdan.Text, duongdanUploadgoc + tenfile);
                var con = ketnoi.Khoitao();
                con.chayUpdatedata(tenfile);

                datag1.DataSource = con.layBang("data");
                lbngaycapnhat.Text = con.layngay("ngaydata");

                lbsoluongma.Text = (datag1.RowCount - 1).ToString();
                string soluongthaydoi = (Int32.Parse(lbsoluongma.Text) - int.Parse(soluongbandau)).ToString();
                MessageBox.Show("Đã cập nhật xong:\n Cập nhật được : " + soluongthaydoi + " mã mới");
            }
            catch (Exception)
            {

                MessageBox.Show("Co loi ket noi voi may chu hoac file chon co van de");

            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                var con = ketnoi.Khoitao();
                lbngaycapnhat.Text = con.layngay("ngaydata");
                datag1.DataSource = con.layBang("data");
                lbsoluongma.Text = (datag1.RowCount - 1).ToString();


                lbngaycapnhat2.Text = con.layngay("ngaykhuyenmai");
                datag2.DataSource = con.layBang("khuyenmai");
                lbsoluongma2.Text = (datag2.RowCount - 1).ToString();
            }
            catch (Exception)
            {

                MessageBox.Show("Co loi ket noi voi may chu hoac file chon co van de");
                pbbaoloi.Image = Properties.Resources.dead;
            }
           
        }

        private void btnchonfile2_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openF = new OpenFileDialog();
                openF.Filter = "chon file csv (*.csv)|*.csv";
                if (openF.ShowDialog() == DialogResult.OK)
                {
                    string duongdanfile = openF.FileName;
                    lbduongdan2.Text = duongdanfile;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Co loi ket noi voi may chu hoac file chon co van de");
            }
            
        }

        private void btnchayupdate2_Click(object sender, EventArgs e)
        {
            try
            {
                string soluongbandau = lbsoluongma2.Text;
                string tenfile = Path.GetFileName(lbduongdan2.Text);
                if (File.Exists(duongdanUploadgoc + tenfile))
                {
                    File.Delete(duongdanUploadgoc + tenfile);
                }
                File.Copy(lbduongdan2.Text, duongdanUploadgoc + tenfile);
                var con = ketnoi.Khoitao();
                con.chayUpdatekhuyenmai(tenfile);

                datag2.DataSource = con.layBang("khuyenmai");
                lbngaycapnhat2.Text = con.layngay("ngaykhuyenmai");

                lbsoluongma2.Text = (datag2.RowCount - 1).ToString();
                string soluongthaydoi = (Int32.Parse(lbsoluongma2.Text) - int.Parse(soluongbandau)).ToString();

                MessageBox.Show("Đã cập nhật xong:\n Cập nhật được : " + soluongthaydoi + " mã mới");
            }
            catch (Exception)
            {

                MessageBox.Show("Co loi ket noi voi may chu hoac file chon co van de");
            }
            
        }
    }
}
