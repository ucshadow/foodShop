using FoodStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodStore.Abstract
{
    public interface IAffiliateRepository
    {
        Affiliate GetAffiliateByUserId(string userId);
        Affiliate GetAffiliateById(string id);
        Affiliate CreateAffiliate(string userId, string name);
        Task<Affiliate> DeleteAffiliate(string afiliateId);
        Affiliate AddAffiliateSell(Product product, ShippingDetails shipping, string affiliateId);
        Affiliate AddAffiliateSell(Product product, ShippingDetails shipping, int quantity, string affiliateId);

        List<Affiliate> GetAffiliates();
    }
}
