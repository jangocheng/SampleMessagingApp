
namespace SampleMessagingApp.Core.Model.Email
{
    public class EmailOptions
    {
        public string SenderAddress { get; set; }

        public string SmtpServer { get; set; }

        public int SmtpPort { get; set; }

        public string SmtpUsername { get; set; }

        public string SmtpPassword { get; set; }
    }
}
