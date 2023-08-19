using ChatMensagem.Domain;
using ChatMensagem.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Linq.Expressions;

namespace ChatMensagem.Dados.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly IChatMensagemDbContext _context;

        public Repository(ChatMensagemDbContext context)
        {
            _context = context;
        }

        protected IQueryable<T> Query
        (
            params Expression<Func<T, object>>[] joins
        )
        {
            var query = _context
                .Set<T>()
                .AsQueryable();
            return joins == null ? query : joins.Aggregate(query, (current, include) => current.Include(include));
        }

        protected IQueryable<T> Query
        (
            Expression<Func<T, object>> orderByDescending,
            params Expression<Func<T, object>>[] joins
        )
        {
            var query = _context
                .Set<T>()
                .OrderByDescending(orderByDescending)
                .AsQueryable();
            return joins == null ? query : joins.Aggregate(query, (current, include) => current.Include(include));
        }

        public virtual async Task<IEnumerable<T>> GetAsync
        (
            CancellationToken cancellationToken,
            params Expression<Func<T, object>>[] joins
        )
        {
            return await Query(joins).ToListAsync(cancellationToken);
        }

        public virtual async Task<IEnumerable<T>> GetAsync
        (
            Expression<Func<T, bool>> lambda,
            CancellationToken cancellationToken,
            params Expression<Func<T, object>>[] joins
        )
        {
            return await Query(joins)
                .Where(lambda)
                .ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> TakeOrderedAsync
        (
            Expression<Func<T, bool>> lambda,
            int count,
            Expression<Func<T, object>> orderByDescending,
            CancellationToken cancellationToken,
            params Expression<Func<T, object>>[] joins
        )
        {
            return await Query(orderByDescending, joins)
                .Where(lambda)
                .Take(count)
                .ToListAsync();
        }

        public virtual async Task<T> GetFirstAsync
        (
            Expression<Func<T, bool>> lambda,
            CancellationToken cancellationToken,
            params Expression<Func<T, object>>[] joins
        )
        {
            var entity = await Query(joins).FirstOrDefaultAsync(lambda, cancellationToken);
            return entity;
        }

        public virtual async Task<T> GetOrderedFirstAsync
        (
            Expression<Func<T, bool>> lambda,
            Expression<Func<T, object>> orderByDescending,
            CancellationToken cancellationToken,
            params Expression<Func<T, object>>[] joins
        )
        {
            var entity = await Query(orderByDescending, joins).FirstOrDefaultAsync();
            return entity;
        }

        public virtual async Task<T> GetSingleAsync
        (
            CancellationToken cancellationToken,
            params Expression<Func<T, object>>[] joins
        )
        {
            var entity = await Query(joins).SingleOrDefaultAsync(cancellationToken);
            return entity;
        }

        public virtual async Task<bool> ExistsAsync
        (
            Expression<Func<T, bool>> lambda,
            CancellationToken cancellationToken
        )
        {
            return await Query().AnyAsync(lambda, cancellationToken);
        }

        public virtual async Task AddAsync
        (
            T entity,
            CancellationToken cancellationToken
        )
        {
            await _context
                .Set<T>()
                .AddAsync(entity, cancellationToken);
        }

        public virtual async Task AddCollectionAsync
        (
            IEnumerable<T> entities,
            CancellationToken cancellationToken
        )
        {
            await _context
                .Set<T>()
                .AddRangeAsync(entities, cancellationToken);
        }

        public virtual async Task UpdateAsync
        (
            T entity,
            CancellationToken cancellationToken
        )
        {
            await Task.Run(() =>
            {
                _context
                    .Set<T>()
                    .Update(entity);
            }, cancellationToken);
        }

        public virtual async Task UpdateCollectionAsync
        (
            IEnumerable<T> entities,
            CancellationToken cancellationToken
        )
        {
            await Task.Run(() =>
            {
                _context
                    .Set<T>()
                    .UpdateRange(entities);
            }, cancellationToken);
        }

        public virtual async Task RemoveAsync
        (
            T entity,
            CancellationToken cancellationToken
        )
        {
            await Task.Run(() =>
            {
                _context
                    .Set<T>()
                    .Remove(entity);
            }, cancellationToken);
        }

        public virtual async Task<int> SaveChangesAsync
        (
            CancellationToken cancellationToken
        )
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public DbConnection DbConnection { get; set; }
    }
}
