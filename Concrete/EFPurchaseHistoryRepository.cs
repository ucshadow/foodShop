using FoodStore.Abstract;
using FoodStore.Domain.Concrete;
using FoodStore.Entities;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace FoodStore.Concrete
{
    public class EFPurchaseHistoryRepository : IPurchaseHistoryRepository
    {

        private readonly EFDbContext _context = new EFDbContext();

        public IEnumerable<Purchase> PurchaseHistory {
            get { return _context.PurchaseHistory; }
        }

        public Purchase DeletePurchase(int purchaseID)
        {
            var dbEntry = _context.PurchaseHistory.FindAsync(purchaseID);
            if (dbEntry == null || dbEntry.Result == null) return null;
            _context.PurchaseHistory.Remove(dbEntry.Result);
            _context.SaveChanges();
            return dbEntry.Result;
        }

        public void SavePurchase(Purchase purchase)
        {
            if(purchase.PurchaseID == 0)
            {
                _context.PurchaseHistory.Add(purchase);
            }       
            else
            {
                // should use async :D
                Purchase dbEntry = _context.PurchaseHistory.Find(purchase.PurchaseID);
                if(dbEntry != null)
                {
                    dbEntry.Rating = purchase.Rating;
                }
            }
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