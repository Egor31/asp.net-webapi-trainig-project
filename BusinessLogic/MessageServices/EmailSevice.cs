using Microsoft.Extensions.Logging;

namespace BusinessLogic.MessageServices
{
    public class EmailService : IEmailService
    {
        ILogger _logger;

        public EmailService(ILogger<EmailService> logger) =>
            _logger = logger;

        public void SendMessage(string email, string message) =>
            _logger.LogInformation($"Person with email {email} was notified with message: {message}");
    }
}