using BackendTest.Command.EFInfraStructure.Repositories;
using System.Threading.Tasks;

namespace BackendTest.Command.EFInfraStructure
{
    public class UnitOfWork
    {
        private readonly AppDbContext _context;

        internal UnitOfWork(string connectionString)
        {
            _context = new AppDbContext(connectionString);
        }

        private ItemRepository _item;
        public ItemRepository Item =>
            _item ?? (_item = new ItemRepository(_context));

        private StatusRepository _status;
        public StatusRepository Status =>
            _status ?? (_status = new StatusRepository(_context));

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}