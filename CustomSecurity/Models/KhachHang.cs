using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomSecurity.Models
{
    public partial class KhachHang
    {
        public KhachHang()
        {
            BanBe = new HashSet<BanBe>();
            HoaDon = new HashSet<HoaDon>();
            YeuThich = new HashSet<YeuThich>();
        }

        [Display(Name="Mã khách hàng")]
        [MaxLength(20)]
        [Key]
        public string MaKh { get; set; }
        [Display(Name = "Mật khẩu")]
        public string MatKhau { get; set; }
        [Display(Name = "Họ tên")]
        public string HoTen { get; set; }
        public bool GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public string Hinh { get; set; }
        public bool HieuLuc { get; set; }
        public int VaiTro { get; set; }
        public string RandomKey { get; set; }

        public ICollection<BanBe> BanBe { get; set; }
        public ICollection<HoaDon> HoaDon { get; set; }
        public ICollection<YeuThich> YeuThich { get; set; }
    }
}
