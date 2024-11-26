using System.Net.Mail;
using System.Net;

namespace WEB.Models.Mail
{
    public static class MailSetup
    {
        public static string BodyResetPassword = "Mật khẩu mới là : 12345\n Vui lòng đăng nhập và đổi mật khẩu mới!";
        public static string ConfiOrder = "ĐƠN HÀNG CỦA BẠN ĐÃ ĐƯỢC ĐẶT THÀNH CÔNG VUI LÒNG ĐỢI XÁC NHẬN TỪ CỬA HÀNG!";
        public static void SendMail(string subject,string body, string toemail)
        {
            string fromEM = "hoangnhpd08282@fpt.edu.vn";
            string pass = "byeqvjeufrpbixyh";

            MailMessage mail = new MailMessage(fromEM, toemail);
            mail.Subject = subject;
            mail.Body = body;
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromEM, pass),
                EnableSsl = true,
            };
            smtpClient.Send(mail);
            mail.Dispose();

        }
    }
   
}
