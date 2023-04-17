using ProductApi.Data;
using ProductApi.Models;
using ProductApi.Repository.Abstract;
using System.Linq;
using System.Linq.Expressions;

namespace ProductApi.Repository.Concrete
{
    public class CategoryRepository : IRepository<Category>

    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Category model)
        {
            _context.Categories.Add(model);
            _context.SaveChanges();
        }

        public void Delete(Category model)
        {
            _context.Categories.Remove(model);
            _context.SaveChanges();
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        public Category GetById(Expression<Func<Category, bool>> predicate)
        {
           return _context.Categories.SingleOrDefault(predicate);
        }

        public void Update(Category model)
        {
            _context.Categories.Update(model);
            _context.SaveChanges();
        }
    }
}
