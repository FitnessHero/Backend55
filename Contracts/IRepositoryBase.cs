using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Contracts
{
	public interface IRepositoryBase<T> where T : class
	{
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
		EntityEntry<T> Create(T entity);
        EntityEntry<T> Update(T entity);
        void Delete(T entity);
    }
}
