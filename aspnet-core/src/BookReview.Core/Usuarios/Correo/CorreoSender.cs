using System;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Net.Mail;

namespace BookReview.Usuarios.Correo
{
    public class CorreoSender : BookReviewServiceBase, ICorreoSender, ITransientDependency
    {
        private readonly IEmailSender _emailSender;
        
        public CorreoSender(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task SendEmailNotificationAsync(Usuario user)
        {
            StringBuilder emailTemplate = new StringBuilder();

            emailTemplate.AppendLine(L("EmailGreetings", user.Nombre));
            emailTemplate.AppendLine(L("EmailBody"));

            await ReplaceBodyAndSend(user.Correo, L("EmailSubject"), emailTemplate);
        }

        private async Task ReplaceBodyAndSend(string emailAddress, string subject, StringBuilder emailTemplate)
        {
            try
            {
                throw new NotImplementedException();
                await _emailSender.SendAsync(new MailMessage
                {
                    To = { emailAddress },
                    Subject = subject,
                    Body = emailTemplate.ToString(),
                    IsBodyHtml = true
                });
            }
            catch (Exception)
            {
                // Do nothing
                //throw;
            }
            
        }
    }
}