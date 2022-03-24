namespace BusinessLogic.MessageServices
{
    public interface IEmailService
    {
        void SendMessage(string email, string message);
    }
}