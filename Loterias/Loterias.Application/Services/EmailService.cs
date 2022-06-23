using Loterias.Application.Models;
using Loterias.Application.Services.Interfaces;
using Loterias.Application.Utils.Email.Templates;
using Loterias.Application.Utils.Settings;
using Microsoft.Extensions.Options;
using System;

namespace Loterias.Application.Services
{
    public class EmailService : IMessageService<Email>
    {
        private readonly EmailSendingSettings _emailSendingInfo;

        public EmailService(IOptions<EmailSendingSettings> optionsEmailSending)
        {
            _emailSendingInfo = optionsEmailSending?.Value;
        }

        public Email GetMessage(RecommendedGame recommendedGame)
        {
            try
            {
                RecommendedGameHtml html = recommendedGame;
                return Email.NewEmail(_emailSendingInfo.Name, _emailSendingInfo.Recipient, _emailSendingInfo.Subject, string.Empty, html.Html);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
