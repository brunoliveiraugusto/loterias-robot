using Loterias.Application.Models;
using Loterias.Application.Services.Interfaces;
using Loterias.Application.Utils.Email.Templates;
using Loterias.Application.Utils.Settings;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace Loterias.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSendingInfo _emailSendingInfo;
        private readonly SendGridInfo _sendGridInfo;

        public EmailService(IOptions<EmailSendingInfo> optionsEmailSending, IOptions<SendGridInfo> optionsSendGrid)
        {
            _emailSendingInfo = optionsEmailSending?.Value;
            _sendGridInfo = optionsSendGrid?.Value;
        }

        public async Task<bool> ProcessEmailSubmission(RecommendedGame recommendedGame)
        {
            try
            {
                RecommendedGameHtml html = recommendedGame;
                Email email = Email.NewEmail(_emailSendingInfo.Name, _emailSendingInfo.Recipient, _emailSendingInfo.Subject, string.Empty, html.Html);
                return await Send(email);
            }
            catch (Exception)
            {
                //TODO: Lançar exceção de envio de e-mail
                throw;
            }
        }

        private async Task<bool> Send(Email email)
        {
            SendGridClient client = new(_sendGridInfo.Key);
            EmailAddress from = new(_sendGridInfo.From, _sendGridInfo.Name);
            EmailAddress to = new(email.Recipient, email.Name);
            SendGridMessage message = MailHelper.CreateSingleEmail(from, to, email.Subject, email.PlainTextContext, email.HtmlContent);
            var response = await client.SendEmailAsync(message);

            return response.IsSuccessStatusCode;
        }
    }
}
