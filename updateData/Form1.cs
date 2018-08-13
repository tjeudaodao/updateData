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
using System.Diagnostics;

namespace updateData
{
    public partial class Form1 : Form
    {
        string duongdanUploadgoc = @"C:\ProgramData\MySQL\MySQL Server 8.0\Uploads\";
        string duongdanApp = @"E:/kho/app/luutru";
        string duongdan1 = null;
        string duongdan2 = null;

        public Form1()
        {
            InitializeComponent();
            dangnhap fdangnhap = new dangnhap();

            fdangnhap.Location = new Point(11, 11);
            fdangnhap.Name = "usdangnhap";
            this.Controls.Add(fdangnhap);

            fdangnhap.BringToFront();

        }

        void updatefileData()
        {
            string s1 = duongdan1;
            if (!string.IsNullOrEmpty(s1))
            {
                s1 = s1.Replace(@"\","/");
                var proc1 = new ProcessStartInfo();
                string Command;
                Command = string.Format("sqlite3 -cmd \".open {0}/databarcode.db\" -cmd \"DROP TABLE IF EXISTS data; \" -cmd \"CREATE TABLE data(masp TEXT,barcode TEXT);\" -cmd \".mode csv\" -cmd \".import {1} data\"", duongdanApp, s1);
                proc1.WorkingDirectory = @"C:\Windows\System32";
                proc1.FileName = @"C:\Windows\System32\cmd.exe";
                proc1.Verb = "runas";
                proc1.Arguments = "/C " + Command;
                Process.Start(proc1);
            }
        }
        void updatefileKhuyenmai()
        {
            string s1 = duongdan2;
            if (!string.IsNullOrEmpty(s1))
            {
                s1 = s1.Replace(@"\", "/");
                //string strCmdText = string.Format("/C sqlite3 -cmd \".open {0}/datakhuyenmai.db\" -cmd \"DROP TABLE IF EXISTS khuyenmai; \" -cmd \"CREATE TABLE khuyenmai(matong TEXT,giagoc TEXT,giagiam TEXT);\" -cmd \".mode csv\" -cmd \".import {1} khuyenmai\"", duongdanApp, s1);
                var proc1 = new ProcessStartInfo();
                string Command;
                Command = string.Format("sqlite3 -cmd \".open {0}/datakhuyenmai.db\" -cmd \"DROP TABLE IF EXISTS khuyenmai; \" -cmd \"CREATE TABLE khuyenmai(matong TEXT,giagoc TEXT,giagiam TEXT);\" -cmd \".mode csv\" -cmd \".import {1} khuyenmai\"", duongdanApp, s1);
                proc1.WorkingDirectory = @"C:\Windows\System32";
                proc1.FileName = @"C:\Windows\System32\cmd.exe";
                proc1.Verb = "runas";
                proc1.Arguments = "/C " + Command;
                Process.Start(proc1);
            }
        }
        private void btnChonfile_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openF = new OpenFileDialog();
                openF.Filter = "chon file csv (*.csv)|*.csv";
                if (openF.ShowDialog() == DialogResult.OK)
                {
                    duongdan1 = openF.FileName;
                    lbduongdan.Text = duongdan1;
                    btnChay.Enabled = true;
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
                //string soluongbandau = lbsoluongma.Text;
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
                //string soluongthaydoi = (Int32.Parse(lbsoluongma.Text) - int.Parse(soluongbandau)).ToString();
                updatefileData();
                btnChay.Enabled = false;
                MessageBox.Show("Đã cập nhật xong");
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
                    duongdan2 = openF.FileName;
                    lbduongdan2.Text = duongdan2;
                    btnchayupdate2.Enabled = true;
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
                //string soluongbandau = lbsoluongma2.Text;
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
                //string soluongthaydoi = (Int32.Parse(lbsoluongma2.Text) - int.Parse(soluongbandau)).ToString();
                updatefileKhuyenmai();
                btnchayupdate2.Enabled = false;
                MessageBox.Show("Đã cập nhật xong");
            }
            catch (Exception)
            {

                MessageBox.Show("Co loi ket noi voi may chu hoac file chon co van de");
            }
            
        }

        
    }
}
