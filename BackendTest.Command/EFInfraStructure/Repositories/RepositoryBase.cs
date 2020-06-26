using BackendTest.Domain.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTest.Command.EFInfraStructure.Repositories
{
    public class RepositoryBase<TEntity> where TEntity : AggregateRoot
    {
        private readonly AppDbContext _context;
        private DbSet<TEntity> _set;

        internal RepositoryBase(AppDbContext context)
        {
            _context = context;
        }

        protected DbSet<TEntity> Set =>
            _set ?? (_set = _context.Set<TEntity>());

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        {
            return await Set.ToListAsync();
        }

        public async Task<long> CountAsync()
        {
            return await Set.CountAsync();
        }

        public async Task<TEntity> FindAsync(Guid? id)
        {
            if (id == null)
                return null;
            return await Set.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetFromIdListAsync(IEnumerable<Guid> ids)
        {
            return await Set.Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            await Set.AddAsync(entity);
        }
    }
}