using Microsoft.Extensions.Logging;

namespace BusinessLogic.MessageServices
{
    public class SmsService : ISmsService
    {
        ILogger _logger;

        public SmsService(ILogger<SmsService> logger) =>
            _logger = logger;

        public void SendMessage(string phoneNumber, string message) =>
            _logger.LogInformation($"Person with phone number {phoneNumber} was notified with message: {message}");
    }
}