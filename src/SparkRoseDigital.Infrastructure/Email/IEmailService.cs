using System.Threading.Tasks;

namespace SparkRoseDigital.Infrastructure.Email
{
    public interface IEmailService
    {
        Task SendAsync(string fromEmail, string toEmail, string subject, string body);
        Task SendFromDoNotReplyAsync(string toEmail, string subject, string body);
    }
}
