using System.Threading.Tasks;
using SampleMessagingApp.Core.Model.Identity;
using SampleMessagingApp.Templates;
using SampleMessagingApp.Templates.Model;

namespace SampleMessagingApp.Core.Services.Email
{
    public class EmailConfirmationService : IEmailConfirmationService
    {
        private readonly IEmailService service;
        private readonly ITemplateEngine engine;

        public EmailConfirmationService(IEmailService service, ITemplateEngine engine)
        {
            this.service = service;
            this.engine = engine;
        }

        public async Task SendEmailConfirmationAsync(ApplicationUser user, string confirmationUrl)
        {
            var recipient = user.Email;
            var subject = "E-Mail Address Confirmation";
            var body = CreateBody(confirmationUrl);

            await service.SendEmailAsync(recipient, subject, body);
        }

        public string CreateBody(string confirmationUrl)
        {
            TemplateData templateData = new TemplateData
            {
                Template = "Please confirm your registration: {{ConfirmationUrl}}.",
                Parameters = new
                {
                    ConfirmationUrl = confirmationUrl
                }
            };

            return engine.Render(templateData);
        }

    }
}
