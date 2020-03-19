using FoodStore.Abstract;
using FoodStore.Domain.Concrete;
using FoodStore.Entities;
using System.Collections.Generic;

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
            _context.PurchaseHistory.Add(purchase);         
            _context.SaveChanges();
        }
    }
}