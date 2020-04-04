using FoodStore.Abstract;
using FoodStore.Domain.Concrete;
using FoodStore.Entities;
using FoodStore.Infrastructure.LocalAPI;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FoodStore.Concrete
{
    public class EFAffiliateProductRepository : IAffiliateProductRepository
    {
        private readonly EFDbContext _context = new EFDbContext();

        public AffiliateProduct ApproveAffiliateProduct(int AffiliateProductID)
        {
            var ap = _context.AffiliateProducts.FirstOrDefault(e => e.AffiliateProductID == AffiliateProductID);
            if (ap == null) return null;
            // approve product
            ap.PendingAdminApproval = false;
            ap.Approved = true;

            // add it as a product
            var p = new Product
            {
                AffiliateId = ap.AffiliateId,
                SoldBy = ap.AffiliateName,
                Name = ap.Name,
                Category = ap.Category,
                Description = ap.Description,
                Discount = 0,
                NumberOfVotes = 0,
                Picture = ap.Picture,
                Price = ap.Price,
                Quantity = ap.Quantity,
                Rating = 0,
                Size = ap.Size,
                Unit = ap.Unit
            };
            _context.Products.Add(p);
            Save(p);
            return ap;
        }

        public AffiliateProduct DenyAffiliateProduct(int AffiliateProductID)
        {
            var ap = _context.AffiliateProducts.FirstOrDefault(e => e.AffiliateProductID == AffiliateProductID);
            if (ap == null) return null;
            ap.PendingAdminApproval = false;
            ap.Approved = false;
            Save();
            return ap;
        }

        public async Task<AffiliateProduct> DeleteAffiliateProduct(int AffiliateProductID)
        {
            var dbEntry = await _context.AffiliateProducts.FindAsync(AffiliateProductID);
            if (dbEntry != null)
            {
                _context.AffiliateProducts.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public IEnumerable<AffiliateProduct> GetAffiliateProducts()
        {
            return _context.AffiliateProducts.ToList();
        }

        public IEnumerable<AffiliateProduct> GetAffiliateProductsByAffiliateId(string affiliateId)
        {
            return _context.AffiliateProducts.Where(e => e.AffiliateId == affiliateId).ToList();
        }

        public async Task<AffiliateProduct> SaveAffiliateProduct(AffiliateProduct affiliateProduct)
        {
            if(affiliateProduct.AffiliateProductID == 0)
            {
                _context.AffiliateProducts.Add(affiliateProduct);
                Save();
                return affiliateProduct;
            }
            var dbEntry = await _context.AffiliateProducts.FindAsync(affiliateProduct.AffiliateProductID);
            if(dbEntry != null)
            {
                dbEntry.AffiliateProductID = affiliateProduct.AffiliateProductID;
                dbEntry.AffiliateName = affiliateProduct.AffiliateName;
                dbEntry.Approved = affiliateProduct.Approved;
                dbEntry.Category = affiliateProduct.Category;
                dbEntry.Description = affiliateProduct.Description;
                dbEntry.Name = affiliateProduct.Name;
                dbEntry.PendingAdminApproval = affiliateProduct.PendingAdminApproval;
                dbEntry.Picture = affiliateProduct.Picture;
                dbEntry.Price = affiliateProduct.Price;
                dbEntry.Quantity = affiliateProduct.Quantity;
                dbEntry.Size = affiliateProduct.Size;
                dbEntry.Unit = affiliateProduct.Unit;
            }

            Save();
            return dbEntry;
        }

        private void Save(Product p = null)
        {
            try
            {
                _context.SaveChanges();
                if(p != null)
                {
                    GlobalProductCache.ProductCache.Add(p);
                }
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
    }
}