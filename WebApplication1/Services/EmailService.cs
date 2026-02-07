using ChineseAuctionProject.Interfaces;
using System.Net;
using System.Net.Mail;

namespace ChineseAuctionProject.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendWinnerNotificationAsync(string email, string userName, string giftName)
        {
            var smtpSection = _configuration.GetSection("Smtp");
            var smtpServer = smtpSection["Server"];
            var smtpPort = int.Parse(smtpSection["Port"] ?? "587");
            var smtpUser = smtpSection["User"];
            var smtpPass = smtpSection["Password"];
            var fromEmail = smtpSection["FromEmail"];

            using var client = new SmtpClient(smtpServer, smtpPort)
            {
                Credentials = new NetworkCredential(smtpUser, smtpPass),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(fromEmail),
                Subject = "Congratulations! You won the raffle!",
                Body = $"Dear {userName},\n\nCongratulations! You have won the raffle for the gift: {giftName}.\n\nPlease contact us to claim your prize.\n\nBest regards,\nChinese Auction Team",
                IsBodyHtml = false
            };
            mailMessage.To.Add(email);

            await client.SendMailAsync(mailMessage);
        }
    }
}