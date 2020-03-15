using FoodStore.Domain.Concrete;
using FoodStore.Entities;
using System.Collections.Generic;

namespace FoodStore.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private readonly EFDbContext _context = new EFDbContext();
        public IEnumerable<Product> Products {
            get { return _context.Products; }
        }

        private static List<Product> _clone { get; set; }

        public List<Product> GetClone()
        {
            if(_clone == null)
            {
                var z = new List<Product>(Products);
                _clone = new List<Product>();
                foreach(var i in z)
                {
                    var p = new Product
                    {
                        Category = i.Category,
                        Description = i.Description,
                        Name = i.Name,
                        Picture = i.Picture,
                        Price = i.Price,
                        ProductID = i.ProductID,
                        Quantity = i.Quantity,
                        Size = i.Size,
                        Unit = i.Unit
                    };
                    _clone.Add(p);
                }
            }
            return _clone;
        }

        public void SaveProduct(Product product)
        {
            if (product.ProductID == 0)
            {
                _context.Products.Add(product);
            }
            else
            {
                Product dbEntry = _context.Products.Find(product.ProductID);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                    dbEntry.Quantity = product.Quantity;
                    dbEntry.Size = product.Size;
                    dbEntry.Unit = product.Unit;
                    dbEntry.Picture = product.Picture;
                }
            }
            _context.SaveChanges();
        }

        public Product DeleteProduct(int productID)
        {
            Product dbEntry = _context.Products.Find(productID);
            if (dbEntry != null)
            {
                _context.Products.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
