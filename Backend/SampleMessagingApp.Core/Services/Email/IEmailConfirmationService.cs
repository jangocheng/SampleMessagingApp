using System.Threading.Tasks;
using SampleMessagingApp.Core.Model.Identity;

namespace SampleMessagingApp.Core.Services.Email
{
    public interface IEmailConfirmationService
    {
        Task SendConfirmationMailAsync(ApplicationUser user, string confirmationUrl);
    }
}