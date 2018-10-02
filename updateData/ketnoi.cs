using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace updateData
{
    class ketnoi
    {
        #region Khoi tao ketnoi
        private MySqlConnection conn = null;
        private MySqlConnection conupdate = null;

        private ketnoi()
        {
            string connstring = string.Format("Server=localhost;port=3306; database=cnf; User Id=hts; password=hoanglaota");
            conn = new MySqlConnection(connstring);
        }
        private ketnoi(string host)
        {
            //string connstring = string.Format("Server=27.72.29.28;port=3306; database=cnf; User Id=kho; password=1234");
            //string connstring = string.Format("Server=localhost;port=3306; database=cnf; User Id=hts; password=1211");
            string connstring = string.Format("Server="+host+";port=3306; database=cnf; User Id=hts; password=hoanglaota");
            conupdate = new MySqlConnection(connstring);
        }
        private static ketnoi _khoitao = null;
        private static ketnoi _khoitaou = null;
        public static ketnoi Khoitao()
        {
            if (_khoitao == null)
            {
                _khoitao = new ketnoi();
            }
            return _khoitao;
        }
        public static ketnoi Khoitao(string host)
        {
            if (_khoitaou == null)
            {
                _khoitaou = new ketnoi(host);
            }
            return _khoitaou;
        }
        // dong mo ket noi conn
        public void Open()
        {
            if (conn.State != System.Data.ConnectionState.Open)
            {
                try
                {
                    conn.Open();
                }
                catch (Exception)
                {

                    MessageBox.Show("Không kết nối được đến máy chủ ","Lỗi");
                }
            }
        }
        public void Close()
        {
            if (conn.State != System.Data.ConnectionState.Closed)
            {
                conn.Close();
            }
        }
        // dong mo ket noi conupdate
        public void Openu()
        {
            if (conupdate.State != System.Data.ConnectionState.Open)
            {
                try
                {
                    conupdate.Open();
                }
                catch (Exception)
                {

                    MessageBox.Show("Không kết nối được đến máy chủ ", "Lỗi");
                }
            }
        }
        public void Closeu()
        {
            if (conupdate.State != System.Data.ConnectionState.Closed)
            {
                conupdate.Close();
            }
        }
        #endregion

        #region Xuly
        public string layngay(string cotcanlay)
        {
            string h = null;
            string sql = string.Format("select {0} from ngaycapnhat", cotcanlay);
            Open();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader dtr = cmd.ExecuteReader();
            while (dtr.Read())
            {
                h = dtr[0].ToString();
            }
            Close();
            return h;
        }
        public DataTable layBang(string tenbangcanlay)
        {
            DataTable dt = new DataTable();
            string sql = string.Format("select * from {0}", tenbangcanlay);
            Open();
            MySqlDataAdapter dta = new MySqlDataAdapter(sql, conn);
            dta.Fill(dt);
            Close();
            return dt;
        }
        #endregion

        #region update 
        static string duongdanUpload = null;
        public static void Setduongdan(string duongdan)
        {
            duongdanUpload = duongdan;
        }
        //static string duongdanUpload = @"C:\\ProgramData\\MySQL\\MySQL Server 8.0\\Uploads\\";

        public void chayUpdatedata(string tenfile)
        {
            string ngay = DateTime.Now.ToString("dd-MM-yyyy");
            //string sqlbackup = @"truncate  data_backup ; insert data_backup select * from data ; truncate data";
            string sqlbackup = @"truncate data";
            string sql = string.Format("load data infile '{0}' into table data fields terminated by \",\" enclosed by '\"' lines terminated by \"\\r\\n\" ignore 1 lines", duongdanUpload + tenfile);
            string sqlngaycapnhat = @"update ngaycapnhat set ngaydata = '"+ngay+"'";
            Open();
            MySqlCommand cmd = new MySqlCommand(sqlbackup, conn);
            cmd.ExecuteNonQuery();
            
            cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();

            cmd = new MySqlCommand(sqlngaycapnhat, conn);
            cmd.ExecuteNonQuery();
            Close();
        }

        public void chayUpdatekhuyenmai(string tenfile)
        {
            string ngay = DateTime.Now.ToString("dd-MM-yyyy");
            //string sqlbackup = @"truncate  khuyenmai_backup ; insert khuyenmai_backup select * from khuyenmai ; truncate khuyenmai";
            string sqlbackup = @"truncate khuyenmai";
            string sql = string.Format("load data infile '{0}' into table khuyenmai fields terminated by \",\" enclosed by '\"' lines terminated by \"\\r\\n\" ignore 1 lines", duongdanUpload + tenfile);
            string sqlngaycapnhat = @"update ngaycapnhat set ngaykhuyenmai = '" + ngay + "' , tongmakhuyenmai = (select count(*) from khuyenmai) ";
            Open();
            MySqlCommand cmd = new MySqlCommand(sqlbackup, conn);
            cmd.ExecuteNonQuery();

            cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();

            cmd = new MySqlCommand(sqlngaycapnhat, conn);
            cmd.ExecuteNonQuery();
            Close();
        }
        #endregion

        #region dang nhap
        public string[] kiemtradangnhap(string user,string pass)
        {
            string[] h = new string[2];
            string sql = string.Format("select taikhoan,pass from dangnhap where taikhoan = '{0}' and pass = '{1}'", user, pass);
            Open();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader dtr = cmd.ExecuteReader();
            while (dtr.Read())
            {
                h[0] = dtr[0].ToString();
                h[1] = dtr[1].ToString();
            }
            Close();
            return h;
        }
        #endregion

        #region cap nhat phan mem
        public string LaydulieuCapnhat(string tencotcanlay,string tenungdung)
        {
            
            string kq = null;
            string sql = "select "+tencotcanlay+" from bangcapnhatphanmem where tenungdung = '" + tenungdung + "'";
            Openu();
            MySqlCommand cmd = new MySqlCommand(sql, conupdate);
            MySqlDataReader dtr = cmd.ExecuteReader();
            dtr.Read();
            kq = dtr[0].ToString();
            Closeu();
            return kq;
        }
        public void UpdatePhienbancapnhat(string tenungdung)
        {
            string ngay = DateTime.Now.ToString("dd-MM-yyyy");
            int sophienban =int.Parse(LaydulieuCapnhat("phienban", tenungdung));
            sophienban = sophienban + 1;
            string sql = "update bangcapnhatphanmem set phienban = '" + sophienban.ToString() + "' , ngay = '" + ngay + "' where tenungdung = '"+tenungdung+"'";
            Openu();
            MySqlCommand cmd = new MySqlCommand(sql, conupdate);
            cmd.ExecuteNonQuery();
            Closeu();
        }
        #endregion
    }
}
