using LuanVan.Areas.Store.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Timers;

namespace LuanVan.Models
{
    public class Services
    {
        private LuanVanContext _context = new LuanVanContext();

        // Loại sản phẩm
        // Danh sách loại sản phẩm
        public IQueryable<LoaiSanPham> danhSachLoaiSP()
        {
            return _context.LoaiSanPhams.Where(x => x.MaLoaiSp != null);
        }

        // Lấy loại sản phẩm
        public LoaiSanPham? getLoai(string maLoai)
        {
            return danhSachLoaiSP().Where(x => x.MaLoaiSp == maLoai).FirstOrDefault();
        }

        // Chỉnh sửa loại sản phẩm
        public void suaLoaiSanPham(LoaiSanPham sP)
        {
            var loaiSP = getLoai(sP.MaLoaiSp);
            _context.Update(loaiSP);
            loaiSP.TenLoaiSp = sP.TenLoaiSp;
            _context.SaveChanges();
        }

        // Thêm loại sản phẩm
        public void themLoaiSP(LoaiSanPham loaiSP)
        {
            var new_maloai = "" + (danhSachLoaiSP().Count() + 1);
            LoaiSanPham newLoaiSP = new LoaiSanPham();
            newLoaiSP.MaLoaiSp = loaiSP.MaLoaiSp;
            newLoaiSP.TenLoaiSp = loaiSP.TenLoaiSp;
            _context.LoaiSanPhams.Add(newLoaiSP);
            _context.SaveChanges();
        }

        // Xóa loại sản phẩm
        public int xoaLoaiSP(string maLoaiSP)
        {
            var khoaChinh = _context.SanPhams.Where(x => x.MaLoaiSp == maLoaiSP).ToList();
            if (khoaChinh.Any())
            {
                return 0;
            }
            _context.LoaiSanPhams.Remove(_context.LoaiSanPhams.Find(maLoaiSP));
            _context.SaveChanges();
            return 1;
        }

        // Giỏ hàng
        // Danh sách giỏ hàng
        public IQueryable<GioHang> danhSachGioHang(int tt = 2, string maKhachHang = null)
        {
            var rs = _context.GioHangs.Where(x => x.MaKhachHang == maKhachHang);
            if (tt == 2)
            {
                return _context.GioHangs.Where(x => x.MaGioHang != null).Include(x => x.MaSanPhamNavigation);
            }

            return rs.Where(x => x.TrangThai == tt).Include(x => x.MaSanPhamNavigation);
        }

        // Lấy giỏ hàng
        public GioHang getGioHang(string maGioHang)
        {
            if (!string.IsNullOrEmpty(maGioHang))
            {
                return _context.GioHangs.Where(x => x.MaGioHang == maGioHang).Include(x => x.MaSanPhamNavigation).FirstOrDefault();
            }
            return null;
        }

        // Thêm giỏ hàng
        public string themGioHang(string maSP, string maKH = null)
        {
            string maGH = "GH" + (danhSachGioHang(2).Count() + 1);
            var rs = danhSachGioHang(0, maKH).Where(x => x.MaSanPham == maSP);
            if (rs.Any())
            {
                var model = rs.FirstOrDefault();
                _context.Update(model);
                model.SoLuongDat = model.SoLuongDat + 1;
                _context.SaveChanges();
                return model.MaGioHang;
            }

            GioHang gH = new GioHang();
            gH.MaGioHang = maGH;
            gH.MaKhachHang = maKH;
            if (maKH == null)
            {
                gH.TrangThai = 2;
            }
            else
            {
                gH.TrangThai = 0;
            }
            gH.SoLuongDat = 1;
            gH.MaSanPham = maSP;
            _context.GioHangs.Add(gH);
            _context.SaveChanges();
            return gH.MaGioHang;
        }

        // Xóa đơn đặt
        public void xoaGioHang(string maGH)
        {
            if (!string.IsNullOrEmpty(maGH))
            {
                var model = _context.GioHangs.Find(maGH);
                _context.Update(model);
                if (model.TrangThai == 0 || model.TrangThai == 2)
                {
                    model.TrangThai = -1;
                }
                _context.SaveChanges();
            }
        }

        // Sản phẩm
        // Danh sách sản phẩm
        public IQueryable<SanPham> danhSachSanPham()
        {
            return _context.SanPhams.Where(x => x.MaSanPham != null);
        }

        // Danh sách sản phẩm theo loại
        public IQueryable<SanPham> danhSachSanPham(string maLoai = null)
        {
            return _context.SanPhams.Where(x => x.MaSanPham != null).Where(x => x.MaLoaiSp == maLoai);
        }

        // Lấy sản phẩm
        public SanPham? getSanPham(string maSanPham)
        {
            return danhSachSanPham().Where(x => x.MaSanPham == maSanPham).FirstOrDefault();
        }

        // Lấy sản phẩm
        public SanPham? GetSanPham(string maSanPham)
        {
            return _context.SanPhams.Find(maSanPham);
        }

        // Xóa sản phẩm
        public void xoaSanPham(string maSP)
        {
            if (!string.IsNullOrEmpty(maSP))
            {
                var model = _context.SanPhams.Find(maSP);
                _context.SanPhams.Remove(model);
                _context.SaveChanges();
            }
        }

        // Sửa sản phẩm
        public void suaSanPham(SanPham sanPham)
        {
            var sP = getSanPham(sanPham.MaSanPham);
            _context.Update(sP);
            _context.SaveChanges();
        }

        // Thêm sản phẩm
        public void themSanPham(SanPham sanPham)
        {
            _context.SanPhams.Add(sanPham);
            _context.SaveChanges();
        }

        public void themSanPham(SanPham sanPham, IFormFile file)
        {
            string maSP = "" + (danhSachSanPham().Count() + 1);
            sanPham.MaSanPham = maSP;
            if (file != null)
            {
                var path = getDataPath(file.FileName);
                using var stream = new FileStream(path, FileMode.Create);
                file.CopyTo(stream);
                sanPham.HinhAnh = file.FileName;
            }
            _context.SanPhams.Add(sanPham);
            _context.SaveChanges();
        }

        public void suaSanPham(SanPham sanPham, IFormFile file)
        {
            var sp = getSanPham(sanPham.MaSanPham);
            _context.Update(sp);
            if (file != null)
            {
                var path = getDataPath(file.FileName);
                using var stream = new FileStream(path, FileMode.Create);
                file.CopyTo(stream);
                sp.HinhAnh = file.FileName;
            }

            _context.SaveChanges();
        }
        // Tìm kiếm sản phẩm
        public IQueryable<SanPham> timKiem(string key)
        {
            return danhSachSanPham().Where(x => x.TenSanPham.Contains(key));
        }

        // Nhà sản xuất
        // Danh sách nhà sản xuất
        public IQueryable<NhaSanXuat> danhSachNSX()
        {
            return _context.NhaSanXuats.Where(x => x.MaNsx != null);
        }

        // Lấy nhà sản xuất
        public NhaSanXuat getNSX(string maNSX)
        {
            return danhSachNSX().Where(x => x.MaNsx == maNSX).FirstOrDefault();
        }

        // Khách hàng
        // Danh sách khách hàng
        public IQueryable<KhachHang> danhSachKH()
        {
            return _context.KhachHangs.Where(x => x.MaKhachHang != null);
        }

        // Lấy khách hàng
        public KhachHang getKH(string maKH)
        {
            return danhSachKH().Where(x => x.MaKhachHang == maKH).FirstOrDefault();
        }

        // Lấy khách hàng
        public KhachHang GetKH(string maKH)
        {
            return _context.KhachHangs.Find(maKH);
        }

        // Lấy khách hàng
        public KhachHang GetKHByEmail(string email)
        {
            return _context.KhachHangs.Find(email);
        }

        // Thêm khách hàng ( Register)
        public string themKH(RegisterModel model)
        {
            string maKH = "" + (danhSachKH().Count() + 1);
            KhachHang kh = new KhachHang();
            kh.MaKhachHang = maKH;
            kh.HoKhachHang = model.HoKhachHang;
            kh.TenKhachHang = model.TenKhachHang;
            kh.NgaySinh = model.NgaySinh;
            kh.GioiTinh = model.GioiTinh;
            kh.SoDienThoai = model.SoDienThoai;
            kh.Email = model.Email;
            kh.DiaChi = model.DiaChi;
            kh.MatKhau = getMD5(model.MatKhau);
            kh.TrangThai = "1";
            _context.KhachHangs.Add(kh);
            _context.SaveChanges();
            return maKH;
        }

        // Sửa khách hàng
        public void suaKhachHang(KhachHang khachHang)
        {
            var kH = getKH(khachHang.MaKhachHang);
            _context.Update(kH);
            kH.HoKhachHang = khachHang.HoKhachHang;
            kH.TenKhachHang = khachHang.TenKhachHang;
            kH.NgaySinh = khachHang.NgaySinh;
            kH.GioiTinh = khachHang.GioiTinh;
            kH.SoDienThoai = khachHang.SoDienThoai;
            kH.DiaChi = khachHang.DiaChi;
            kH.Email = khachHang.Email;
            kH.MatKhau = getMD5(khachHang.MatKhau);
            kH.TrangThai = khachHang.TrangThai;

            _context.SaveChanges();

        }

        // Sửa trạng thái khách hàng
        public void suaTTKhachHang(string maKH, string trangThai)
        {
            var kH = GetKH(maKH);
            _context.Update(kH);
            kH.TrangThai = trangThai;
            _context.SaveChanges();

        }
        // Khách hàng login
        public KhachHang? loginKH(string input, string pwd)
        {
            if (IsValidEmail(input))
            {
                return danhSachKH().Where(x => x.Email == input).Where(x => x.MatKhau == getMD5(pwd)).FirstOrDefault();
            } 
            else 
                return danhSachKH().Where(x => x.SoDienThoai == input).Where(x => x.MatKhau == getMD5(pwd)).FirstOrDefault();
        }

        static bool IsValidPhoneNumber(string input)
        {
            return Regex.IsMatch(input, @"^\d{10,11}$");
        }

        static bool IsValidEmail(string input)
        {
            return Regex.IsMatch(input, @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");
        }

        // Hóa dơn
        // Danh sách hóa đơn
        public IQueryable<HoaDon> danhSachHoaDon(string maKH = null)
        {
            if (maKH == null)
            {
                return _context.HoaDons.Where(x => x.MaHoaDon != null).Include(x => x.ChiTietHds);
            }
            return _context.HoaDons.Where(x => x.MaKhachHang == maKH).Include(x => x.ChiTietHds);
        }

        // Lấy hóa đơn
        public HoaDon? getHoaDon(string maHD)
        {
            return danhSachHoaDon().Where(x => x.MaHoaDon == maHD).FirstOrDefault();
        }

        // Thêm hóa đơn
        public string themHoaDon(string maKH = "")
        {
            HoaDon hoaDon = new HoaDon();
            hoaDon.MaHoaDon = "" + (danhSachHoaDon().Count() + 1);
            if (maKH == "")
            {
                hoaDon.MaKhachHang = null;
            }
            else
            {
                hoaDon.MaKhachHang = maKH;
            }
            hoaDon.NgayXuatHd = DateTime.Now;
            _context.HoaDons.Add(hoaDon);
            _context.SaveChanges();
            return hoaDon.MaHoaDon;
        }

        // Chi tiết hóa đơn
        // Danh sách chi tiết hóa đơn
        public IQueryable<ChiTietHd> danhSachChiTietHD(string maHD = null)
        {
            if (maHD == null)
            {
                return _context.ChiTietHds.Where(x => x.MaChiTietHd != null);
            }
            return _context.ChiTietHds.Where(x => x.MaHoaDon == maHD);
        }

        // Thêm chi tiết hóa đơn
        public string themChiTietHD(string maHD, string maGH)
        {
            ChiTietHd chiTietHD = new ChiTietHd();
            chiTietHD.MaChiTietHd = "" + (danhSachChiTietHD().Count() + 1);
            chiTietHD.MaGioHang = maGH;
            getGioHang(maGH).TrangThai = 1;
            chiTietHD.MaHoaDon = maHD;
            _context.ChiTietHds.Add(chiTietHD);
            _context.SaveChanges();
            return chiTietHD.MaChiTietHd;
        }

        // Tăng số lượng đơn đặt
        public void increase(string maGH, int soLuong)
        {
            var gioHang = getGioHang(maGH);
            _context.Update(gioHang);
            gioHang.SoLuongDat = soLuong;

            _context.SaveChanges();
        }

        // Nhân viên
        // Danh sách nhân viên
        public IQueryable<NhanVien> danhSachNhanVien()
        {
            return _context.NhanViens.Where(x => x.MaNhanVien != null);
        }

        // Lấy nhân viên
        public NhanVien? getNV(string maNV)
        {
            return danhSachNhanVien().Where(x => x.MaNhanVien == maNV).FirstOrDefault();
        }

        // Lấy nhân viên
        public NhanVien getNhanVien(string maNV)
        {
            return _context.NhanViens.Find(maNV);
        }

        // Thêm nhân viên
        public void themNV(string hoNV, string tenNV, DateTime ngaySinh, string gioiTinh, string soDT, string diaChi, string email,  string matKhau)
        {

            NhanVien nV = new NhanVien();
            string maNV = "" + (danhSachNhanVien().Count() + 1);
            nV.MaNhanVien = maNV;
            nV.HoNhanVien = hoNV;
            nV.TenNhanVien = tenNV;
            nV.NgaySinh = ngaySinh;
            nV.GioiTinh = gioiTinh;
            nV.SoDienThoai = soDT;
            nV.DiaChi = diaChi;
            nV.Email = email;
            nV.MatKhau = getMD5(matKhau);
            nV.TrangThai = "1";
            nV.MaRole = "1";
            _context.NhanViens.Add(nV);
            _context.SaveChanges(true);
        }

        // Xóa nhân viên
        public void xoaNhanVien(string maNV)
        {
            if (!string.IsNullOrEmpty(maNV))
            {
                var model = _context.NhanViens.Find(maNV);
                _context.NhanViens.Remove(model);
                _context.SaveChanges();
            }
        }

        // Sửa nhân viên
        public void suaNhanVien(NhanVien nhanVien)
        {
            var nV = getNV(nhanVien.MaNhanVien);
            _context.Update(nV);
            nV.HoNhanVien = nhanVien.HoNhanVien;
            nV.TenNhanVien = nhanVien.TenNhanVien;
            nV.NgaySinh = nhanVien.NgaySinh;
            nV.GioiTinh = nhanVien.GioiTinh;
            nV.SoDienThoai = nhanVien.SoDienThoai;
            nV.DiaChi = nhanVien.DiaChi;
            nV.Email = nhanVien.Email;
            nV.MatKhau = getMD5(nhanVien.MatKhau);
            nV.TrangThai = nhanVien.TrangThai;
            nV.MaRole= nhanVien.MaRole;

            _context.SaveChanges();

        }

        // Nhân viên (Đăng nhập)
        public NhanVien? loginNV(string input, string matKhau)
        {
            if (IsValidEmail(input))
            {
                return danhSachNhanVien().Where(x => x.Email == input).Where(x => x.MatKhau == getMD5(matKhau)).FirstOrDefault();
            }
            else
                return danhSachNhanVien().Where(x => x.SoDienThoai == input).Where(x => x.MatKhau == getMD5(matKhau)).FirstOrDefault();
        }

        // Lấy đường dẫn
        public string getDataPath(string file) => $"wwwroot\\images\\product\\{file}";


        // Tổng giá trị hóa đơn
        public long tongGiaTri(string maHD)
        {
            long sum = 0;
            var dds = danhSachChiTietHD(maHD).ToList();
            foreach (var dd in dds)
            {
                sum += (long)dd.MaGioHangNavigation.SoLuongDat * dd.MaGioHangNavigation.MaSanPhamNavigation.GiaBan;
            }
            return sum;
        }

        // Mã hóa mật khẩu MD5
        public static string getMD5(string password)
        {
            MD5 mD5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(password);
            byte[] targetData = mD5.ComputeHash(fromData);

            string byte2String = null;
            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }
            return byte2String;
        }

        private static System.Timers.Timer _timer;
        private static string _otp;
        public string OTP()
        {
            // Generate OTP
            var random = new Random();
            _otp = random.Next(100000, 1000000).ToString();

            // Start timer for OTP expiration
            //_timer = new System.Timers.Timer(60000);  // expire after 1 minute
            //_timer.Elapsed += Timer_Elapsed;
            //_timer.Start();

            return _otp;
        }
        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _otp = null;
            _timer.Stop();
        }

        public string getTrangThai(string maKH)
        {
            //var result = (from c in KhachHang
            //              where c.Email == email
            //              select c.TrangThai).FirstOrDefault();
            //return result;

            var trangThai = (from kh in _context.KhachHangs
                             where kh.MaKhachHang == maKH
                             select kh.TrangThai).FirstOrDefault();

            return trangThai;

        }

        public List<string> GetEmailListFromDB()
        {
            using (var db = new LuanVanContext())
            {
                return db.KhachHangs
                         .Select(c => c.Email)
                         .ToList();
            }
        }

    }
}
