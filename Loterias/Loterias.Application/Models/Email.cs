namespace Loterias.Application.Models
{
    public class Email
    {
        public string Name { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string PlainTextContext { get; set; }
        public string HtmlContent { get; set; }
        public static Email NewEmail(string name, string recipient, string subject, string plainTextContext, string htmlContent) => new(name, recipient, subject, plainTextContext, htmlContent);

        private Email(string name, string recipient, string subject, string plainTextContext, string htmlContent)
        {
            Name = name;
            Recipient = recipient;
            Subject = subject;
            PlainTextContext = plainTextContext;
            HtmlContent = htmlContent;
        }
    }
}
