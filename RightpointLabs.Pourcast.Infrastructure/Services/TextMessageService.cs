namespace RightpointLabs.Pourcast.Infrastructure.Services
{

    using RightpointLabs.Pourcast.Application.Transactions;
    using RightpointLabs.Pourcast.Domain.Services;
    using Twilio;
    public class TextMessageService : ITextMessageService
    {

        public void SendTextMessage(string phoneNumber, string message)
        {
            // Find your Account Sid and Auth Token at twilio.com/user/account
            string AccountSid = "AC14b38b312497096faa1466a635b2878c";
            string AuthToken = "d1ab64fc9cca6fa95b542d9e5c6ac7ac";

            var twilio = new TwilioRestClient(AccountSid, AuthToken);
            var messageResult = twilio.SendMessage("+13312156622", "+1" + phoneNumber, message);

            if (messageResult.RestException != null)
            {
                var error = messageResult.RestException.Message;
                //TODO  log or throw error;
            }
        }

    }
}
