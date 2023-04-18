using ProductApi.Data;
using ProductApi.Models;
using ProductApi.Repository.Abstract;
using System.Linq.Expressions;

namespace ProductApi.Repository.Concrete
{
    public class SellerRepository : IRepository<Seller>
    {
        private readonly ApplicationDbContext _context;

        public SellerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Seller model)
        {
            _context.Sellers.Add(model);
            _context.SaveChanges();
        }

        public void Delete(Seller model)
        {
            _context.Sellers.Remove(model);
            _context.SaveChanges();
        }

        public IEnumerable<Seller> GetAll()
        {
            return _context.Sellers.ToList();
        }

        public Seller GetById(Expression<Func<Seller, bool>> predicate)
        {
            return _context.Sellers.SingleOrDefault(predicate);
        }

        public Seller GetByEmail(Expression<Func<Seller, bool>> predicate)
        {
            return _context.Sellers.SingleOrDefault(predicate);
        }


        public void Update(Seller model)
        {
            _context.Sellers.Update(model);
            _context.SaveChanges();
        }
    }
}
