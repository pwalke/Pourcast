namespace RightpointLabs.Pourcast.Domain.Services
{
    public interface ITextMessageService
    {
        void SendTextMessage(string phoneNumber, string message);
    }
}


