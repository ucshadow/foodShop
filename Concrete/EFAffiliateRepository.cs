using FoodStore.Abstract;
using FoodStore.Domain.Concrete;
using FoodStore.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FoodStore.Concrete
{
    public class EFAffiliateRepository : IAffiliateRepository
    {

        private readonly EFDbContext _context = new EFDbContext();

        public Affiliate AddAffiliateSell(Product product, ShippingDetails details, string affiliateId)
        {
            throw new NotImplementedException();
        }

        public Affiliate AddAffiliateSell(Product product, ShippingDetails details, int quantity, string affiliateId)
        {
            if (product == null || quantity < 1) return null;
            var affiliate = GetAffiliateById(affiliateId);
            if (affiliate == null) return null;

            if (affiliate.Sells == null) affiliate.Sells = new List<AffiliateSell>();

            affiliate.Sells.Add(new AffiliateSell
            {
                ProductID = product.ProductID,
                ProductName = product.Name,
                ProductImage = product.Picture,
                Quantity = quantity,
                SellDate = new DateTime().ToString(),
                SellPrice = product.Price,
                ShippingDetails = details
            });
            Save();
            return affiliate;
        }

        public Affiliate CreateAffiliate(string userId, string name)
        {
            var dbEntry = _context.Affiliates.Add(new Affiliate
            {
                AffiliateId = Guid.NewGuid().ToString(),
                UserId = userId,
                Sells = new List<AffiliateSell>(),
                AffiliateName = name
            });

            Save();
            return dbEntry;
        }

        public async Task<Affiliate> DeleteAffiliate(string afiliateId)
        {
            var dbEntry = await _context.Affiliates.FindAsync(afiliateId);
            if (dbEntry == null) return null;
            _context.Affiliates.Remove(dbEntry);
            _context.SaveChanges();
            return dbEntry;
        }

        public Affiliate GetAffiliateById(string id)
        {
            return _context.Affiliates.Include(e => e.Sells).FirstOrDefault(e => e.AffiliateId == id);
        }

        public Affiliate GetAffiliateByUserId(string userId)
        {
            return _context.Affiliates.Include(e => e.Sells).FirstOrDefault(e => e.UserId == userId);
        }

        public List<Affiliate> GetAffiliates()
        {
            return _context.Affiliates.ToList();
        }

        private void Save()
        {
            try
            {
                _context.SaveChanges();
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