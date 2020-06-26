using BackendTest.Domain;

namespace BackendTest.Command.EFInfraStructure.Repositories
{
    public sealed class StatusRepository : RepositoryBase<Status>
    {
        internal StatusRepository(AppDbContext context)
            : base(context)
        {
        }
    }
}