using ChatMensagem.Domain;
using System.Data.Common;
using System.Linq.Expressions;

namespace ChatMensagem.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        public Task<IEnumerable<T>> GetAsync
        (
            CancellationToken cancellationToken,
            params Expression<Func<T, object>>[] joins
        );

        public Task<IEnumerable<T>> GetAsync
        (
            Expression<Func<T, bool>> lambda,
            CancellationToken cancellationToken,
            params Expression<Func<T, object>>[] joins
        );

        public Task<IEnumerable<T>> TakeOrderedAsync
        (
            Expression<Func<T, bool>> lambda,
            int count,
            Expression<Func<T, object>> orderByDescending,
            CancellationToken cancellationToken,
            params Expression<Func<T, object>>[] joins
        );

        public Task<T> GetFirstAsync
        (
            Expression<Func<T, bool>> lambda,
            CancellationToken cancellationToken,
            params Expression<Func<T, object>>[] joins
        );

        public Task<T> GetOrderedFirstAsync
        (
            Expression<Func<T, bool>> lambda,
            Expression<Func<T, object>> orderByDescending,
            CancellationToken cancellationToken,
            params Expression<Func<T, object>>[] joins
        );

        public Task<T> GetSingleAsync
        (
            CancellationToken cancellationToken,
            params Expression<Func<T, object>>[] joins
        );

        public Task<bool> ExistsAsync
        (
            Expression<Func<T, bool>> lambda,
            CancellationToken cancellationToken
        );

        public Task AddAsync
        (
            T entity,
            CancellationToken cancellationToken
        );

        public Task AddCollectionAsync
        (
            IEnumerable<T> entities,
            CancellationToken cancellationToken
        );

        public Task UpdateAsync
        (
            T entity,
            CancellationToken cancellationToken
        );

        public Task UpdateCollectionAsync
        (
            IEnumerable<T> entities,
            CancellationToken cancellationToken
        );

        public Task RemoveAsync
        (
            T entity,
            CancellationToken cancellationToken
        );

        public Task<int> SaveChangesAsync
        (
            CancellationToken cancellationToken
        );

        DbConnection DbConnection { get; set; }
    }
}
