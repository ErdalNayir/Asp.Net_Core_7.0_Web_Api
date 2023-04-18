using System.Linq.Expressions;

namespace ProductApi.Repository.Abstract
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(Expression<Func<T, bool>> predicate);
        T GetByEmail(Expression<Func<T, bool>> predicate);
        void Add(T model);
        void Update(T model);
        void Delete(T model);
    }
}
