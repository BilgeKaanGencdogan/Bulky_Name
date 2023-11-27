using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;

namespace Bulky.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            //_db.Products.Update(obj);

            var objFromDb = _db.Products.FirstOrDefault(u => u.Id == obj.Id);

            if (objFromDb != null)
            {
                obj.Title = objFromDb.Title;
                obj.Description = objFromDb.Description;
                obj.ISBN = objFromDb.ISBN;
                obj.Price = objFromDb.Price;
                obj.ListPrice = objFromDb.ListPrice;
                obj.Price100 = objFromDb.Price100;
                obj.CategoryId = objFromDb.CategoryId;
                obj.Author = objFromDb.Author;
                obj.Price50 = objFromDb.Price50;
                if (obj.ImageUrl != null)
                {
                    obj.ImageUrl = objFromDb.ImageUrl;
                }



            }
        }
    }
}
