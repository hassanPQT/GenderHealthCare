using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace BusinessAccess.Helpers
{
    public class EmailHelper
    {
        private readonly IConfiguration _configuration;

        public EmailHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendResetPasswordEmail(string toEmail, string resetToken)
        {
            var emailSettings = _configuration.GetSection("EmailSettings").Get<EmailSettings>();

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Gender Healthcare", emailSettings.FromEmail));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = "Reset Your Password";

            string resetLink = $"https://localhost:5173/login?token={Uri.EscapeDataString(resetToken)}&email={Uri.EscapeDataString(toEmail)}";
            var body = $@"
                <h2>Password Reset Request</h2>
                <p>Click the link below to reset your password:</p>
                <a href='{resetLink}'>Reset Password</a>
                <p>This link will expire in {int.Parse(_configuration["JwtSettings:TokenValidityMins"])} minutes.</p>
            ";

            message.Body = new TextPart("html") { Text = body };

            using var client = new SmtpClient();
            await client.ConnectAsync(emailSettings.SmtpServer, emailSettings.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(emailSettings.SmtpUsername, emailSettings.SmtpPassword);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }

        public async Task SendAppointmentBookingEmail(string toEmail, string customerName, string consultantName, DateTime appointmentTime, int slot, string status, string meetingUrl)
        {
            var emailSettings = _configuration.GetSection("EmailSettings").Get<EmailSettings>();

            string time = "";
            switch (slot)
            {
                case 1:
                    time = "7 giờ đến 9 giờ";
                    break;
                case 2:
                    time = "9 giờ đến 11 giờ";
                    break;
                case 3:
                    time = "1 giờ đến 3 giờ";
                    break;
                case 4:
                    time = "3 giờ đến 5 giờ";
                    break;
            }

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Gender Healthcare", emailSettings.FromEmail));
            message.To.Add(new MailboxAddress(customerName, toEmail));
            message.Subject = "Xác nhận cuộc hẹn tư vấn";

            var body = $@"
        <h2>Xin chào {customerName},</h2>
        <p>Bạn đã đặt cuộc hẹn tư vấn thành công với chuyên gia <strong>{consultantName}</strong>.</p>
        <p><strong>Thời gian:</strong> {time}</p>
        <p><strong>Ngày:</strong> {appointmentTime:dddd, dd/MM/yyyy}</p>";

            if (!string.IsNullOrEmpty(meetingUrl))
            {
                body += $@"
        <p><strong>Link cuộc hẹn (Google Meet):</strong> 
            <a href='{meetingUrl}' target='_blank'>{meetingUrl}</a>
        </p>";
            }

            body += $@"<p>Trạng thái: <strong>{status}</strong>.</p>";

            body += "<p>Vui lòng tham gia đúng giờ. Cảm ơn bạn đã tin tưởng Gender Healthcare!</p>";

            message.Body = new TextPart("html") { Text = body };

            using var client = new SmtpClient();
            await client.ConnectAsync(emailSettings.SmtpServer, emailSettings.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(emailSettings.SmtpUsername, emailSettings.SmtpPassword);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }

        public async Task SendAppointmentUpdateEmail(string toEmail, string customerName, string consultantName, DateTime appointmentTime, int slot, string status, string meetingUrl)
        {
            var emailSettings = _configuration.GetSection("EmailSettings").Get<EmailSettings>();

            string time = "";
            switch (slot)
            {
                case 1:
                    time = "7 giờ đến 9 giờ";
                    break;
                case 2:
                    time = "9 giờ đến 11 giờ";
                    break;
                case 3:
                    time = "1 giờ đến 3 giờ";
                    break;
                case 4:
                    time = "3 giờ đến 5 giờ";
                    break;
            }

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Gender Healthcare", emailSettings.FromEmail));
            message.To.Add(new MailboxAddress(customerName, toEmail));
            message.Subject = "Cập nhận cuộc hẹn tư vấn";

            var body = $@"
        <h2>Xin chào {customerName},</h2>
        <p>Cập nhật cuộc hẹn tư vấn với chuyên gia <strong>{consultantName}</strong>.</p>
        <p><strong>Thời gian:</strong> {time}</p>
        <p><strong>Ngày:</strong> {appointmentTime:dddd, dd/MM/yyyy}</p>";

            if (!string.IsNullOrEmpty(meetingUrl))
            {
                body += $@"
        <p><strong>Link cuộc hẹn (Google Meet):</strong> 
            <a href='{meetingUrl}' target='_blank'>{meetingUrl}</a>
        </p>";
            }

            body += $@"<p>Trạng thái: <strong>{status}</strong>.</p>";

            body += "<p>Vui lòng tham gia đúng giờ. Cảm ơn bạn đã tin tưởng Gender Healthcare!</p>";

            message.Body = new TextPart("html") { Text = body };

            using var client = new SmtpClient();
            await client.ConnectAsync(emailSettings.SmtpServer, emailSettings.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(emailSettings.SmtpUsername, emailSettings.SmtpPassword);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }

        public async Task SendAppointmentCancelEmail(string toEmail, string customerName, string consultantName, DateTime appointmentTime, int slot, string status)
        {
            var emailSettings = _configuration.GetSection("EmailSettings").Get<EmailSettings>();

            string time = "";
            switch (slot)
            {
                case 1:
                    time = "7 giờ đến 9 giờ";
                    break;
                case 2:
                    time = "9 giờ đến 11 giờ";
                    break;
                case 3:
                    time = "1 giờ đến 3 giờ";
                    break;
                case 4:
                    time = "3 giờ đến 5 giờ";
                    break;
            }

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Gender Healthcare", emailSettings.FromEmail));
            message.To.Add(new MailboxAddress(customerName, toEmail));
            message.Subject = "Huỷ cuộc hẹn tư vấn";

            var body = $@"
        <h2>Xin chào {customerName},</h2>
        <p>Cuộc hẹn tư vấn với chuyên gia <strong>{consultantName}</strong> đã bị huỷ!</p>
        <p><strong>Thời gian:</strong> {time}</p>
        <p><strong>Ngày:</strong> {appointmentTime:dddd, dd/MM/yyyy}</p>";

            body += $@"<p>Trạng thái: <strong>{status}</strong>.</p>";

            body += "<p>Mọi chi tiết xem tại trang chủ của Gender Health Care!</p>";

            message.Body = new TextPart("html") { Text = body };

            using var client = new SmtpClient();
            await client.ConnectAsync(emailSettings.SmtpServer, emailSettings.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(emailSettings.SmtpUsername, emailSettings.SmtpPassword);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }

        public async Task SendStaffAccountInfoEmail(string toEmail, string loginName, string fullName, string plainPassword)
        {
            var emailSettings = _configuration.GetSection("EmailSettings").Get<EmailSettings>();

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Gender Healthcare", emailSettings.FromEmail));
            message.To.Add(new MailboxAddress(fullName, toEmail));
            message.Subject = "Thông tin tài khoản Gender Healthcare";

            var loginLink = "https://localhost:5173/login";

            var body = $@"
        <h2>Chào {fullName},</h2>
        <p>Tài khoản của bạn tại hệ thống <strong>Gender Healthcare</strong> đã được tạo thành công.</p>
        <p><strong>Email đăng nhập:</strong> {loginName}</p>
        <p><strong>Mật khẩu:</strong> {plainPassword}</p>
        <p>Bạn có thể đăng nhập tại: <a href='{loginLink}' target='_blank'>{loginLink}</a></p>
        <p>Vui lòng thay đổi mật khẩu sau khi đăng nhập để đảm bảo bảo mật.</p>
        <p>Trân trọng,<br/>Gender Healthcare</p>";

            message.Body = new TextPart("html") { Text = body };

            using var client = new SmtpClient();
            await client.ConnectAsync(emailSettings.SmtpServer, emailSettings.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(emailSettings.SmtpUsername, emailSettings.SmtpPassword);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }


    }



    public class EmailSettings
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
        public string FromEmail { get; set; }
    }
}