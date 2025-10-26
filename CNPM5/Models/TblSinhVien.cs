using System.ComponentModel.DataAnnotations;
using System;

namespace CNPM5.Models
{
    public class TblSinhVien
    {
        [Key]
        [StringLength(20)]
        public string MaSV { get; set; }

        [Required]
        [StringLength(100)]
        public string HoTen { get; set; }

        [Required]
        [StringLength(10)]
        public string GioiTinh { get; set; } // Nam / Nữ

        public DateTime? NgaySinh { get; set; }

        [StringLength(100)]
        public string Khoa { get; set; }

        [StringLength(50)]
        public string Lop { get; set; }

        [StringLength(15)]
        public string SDT { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(200)]
        public string DiaChi { get; set; }

        [StringLength(50)]
        public string TrangThai { get; set; } = "Đang ở"; // Giá trị mặc định
    }
}
