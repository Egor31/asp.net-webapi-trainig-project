namespace BusinessLogic.MessageServices
{
    public interface ISmsService
    {
        void SendMessage(string phoneNumber, string message);
    }
}