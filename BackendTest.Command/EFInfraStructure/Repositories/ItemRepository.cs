using BackendTest.Domain;

namespace BackendTest.Command.EFInfraStructure.Repositories
{
    public sealed class ItemRepository : RepositoryBase<Item>
    {
        //Internal due to "UnitOfWork" is the only one that instantiate this object
        internal ItemRepository(AppDbContext context)
            : base(context)
        {
        }
    }
}