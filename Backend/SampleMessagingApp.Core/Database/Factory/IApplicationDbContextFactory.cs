using SampleMessagingApp.Core.Database.Context;

namespace SampleMessagingApp.Core.Database.Factory
{
    public interface IApplicationDbContextFactory
    {
        ApplicationDbContext Create();
    }
}
