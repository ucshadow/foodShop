using FoodStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodStore.Abstract
{
    public interface IPurchaseHistoryRepository
    {
        IEnumerable<Purchase> PurchaseHistory { get; }

        void SavePurchase(Purchase purchase);

        Purchase DeletePurchase(int purchaseID);
    }
}
