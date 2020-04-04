using FoodStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodStore.Abstract
{
    public interface IAffiliateProductRepository
    {
        IEnumerable<AffiliateProduct> GetAffiliateProducts();
        IEnumerable<AffiliateProduct> GetAffiliateProductsByAffiliateId(string affiliateId);
        AffiliateProduct ApproveAffiliateProduct(int AffiliateProductID);
        AffiliateProduct DenyAffiliateProduct(int AffiliateProductID);
        Task<AffiliateProduct> DeleteAffiliateProduct(int AffiliateProductID);
        Task<AffiliateProduct> SaveAffiliateProduct(AffiliateProduct affiliateProduct);

    }
}
