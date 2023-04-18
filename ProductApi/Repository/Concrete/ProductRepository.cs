using ProductApi.Data;
using ProductApi.Models;
using ProductApi.Repository.Abstract;
using System.Linq.Expressions;

namespace ProductApi.Repository.Concrete
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly  ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Product model)
        {
            try
            {
                _context.Products.Add(model);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }

        public void Delete(Product model)
        {
            _context.Products.Remove(model);
            _context.SaveChanges();
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public Product GetByEmail(Expression<Func<Product, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Product GetById(Expression<Func<Product, bool>> predicate) => _context.Products.SingleOrDefault(predicate);

        public void Update(Product model)
        {
            _context.Products.Update(model);
            _context.SaveChanges();
        }
    }
}
