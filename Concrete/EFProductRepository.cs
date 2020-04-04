using FoodStore.Abstract;
using FoodStore.Domain.Concrete;
using FoodStore.Entities;
using FoodStore.Infrastructure.LocalAPI;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace FoodStore.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private readonly EFDbContext _context = new EFDbContext();
        public IEnumerable<Product> Products {
            get { return _context.Products; }
        }

        private static List<Product> Clone { get; set; }

        public List<Product> GetClone()
        {
            if (Clone == null)
            {
                var z = new List<Product>(Products);
                Clone = new List<Product>();
                foreach (var i in z)
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
                        Unit = i.Unit,
                        Rating = i.Rating,
                        NumberOfVotes = i.NumberOfVotes,
                        Discount = i.Discount,
                        SoldBy = i.SoldBy,
                        AffiliateId = i.AffiliateId
                    };
                    Clone.Add(p);
                }
            }
            return Clone;
        }

        public void SaveProduct(Product product)
        {
            Product p;
            if (product.ProductID == 0)
            {
                p = _context.Products.Add(product);
            }
            else
            {
                Product dbEntry = _context.Products.Find(product.ProductID);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description == null || product.Description.Trim().Length == 0 ? "No description provided" : product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                    dbEntry.Quantity = product.Quantity;
                    dbEntry.Size = product.Size;
                    dbEntry.Unit = product.Unit;
                    dbEntry.Picture = product.Picture;
                    dbEntry.Rating = product.Rating;
                    dbEntry.NumberOfVotes = product.NumberOfVotes;
                    dbEntry.Discount = product.Discount;
                    dbEntry.SoldBy = product.SoldBy;
                    dbEntry.AffiliateId = product.AffiliateId;
                }
                p = dbEntry;
            }
            // this may throw validation errors and fail silently so Ill leave it like this :D
            try
            {
                _context.SaveChanges();
                Clone.Add(p);
                GlobalProductCache.ProductCache.Add(p);
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public Product DeleteProduct(int productID)
        {
            var dbEntry = _context.Products.Find(productID);
            if (dbEntry != null)
            {
                _context.Products.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
