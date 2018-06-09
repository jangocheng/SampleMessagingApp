using System.Threading.Tasks;
using SampleMessagingApp.Core.Model.Identity;

namespace SampleMessagingApp.Core.Services.Email
{
    public class EmailConfirmationService : IEmailConfirmationService
    {
        private readonly IEmailService service;

        public EmailConfirmationService(IEmailService service)
        {
            this.service = service;
        }

        public async Task SendEmailConfirmationAsync(ApplicationUser user, string confirmationUrl)
        {
            string recipient = user.Email;
            string subject = "E-Mail Address Confirmation";
            string body = $"Please confirm your registration to the Sample App by clicking on: {confirmationUrl}";

            await service.SendEmailAsync(recipient, subject, body);
        }
    }
}
