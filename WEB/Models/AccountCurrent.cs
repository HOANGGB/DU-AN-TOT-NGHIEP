using System;

namespace WEB.Models
{
    public static class AccountCurrent
    {
        private static NguoiDung acc;
/*
  = new NguoiDung();       {
            NguoiDungID = 3,
            HoTen = "Nguyễn Văn A",
            HinhAnh = "",
            Email = "nguyenvana@example.com",
            MatKhauHash = "hashedDefaultPassword", 
            VaiTro = Vaitro.Shiper,
            SoDienThoai = "0123456789",
            NgayTao = DateTime.Now
        };*/

        public static NguoiDung Acc
        {
            get { return acc; }
        }
        public static void SetUser(NguoiDung user)
        {
            acc = user;
        }
        public static void Logout()
        {
            acc = null;
        }
    }
}
