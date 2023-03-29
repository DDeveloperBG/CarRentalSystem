namespace WebAPI.Services.BusinessLogic.EmailSender
{
    public interface IEmailSenderService
    {
        void SendEmail(
           string to,
           string subject,
           string htmlContent);
    }
}
