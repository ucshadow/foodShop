using FoodStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodStore.Models
{
    public class AdminAffiliatesModel
    {
        public List<Affiliate> Affiliates { get; set; } = new List<Affiliate>();
        public List<AffiliateProduct> Products { get; set; } = new List<AffiliateProduct>();

        public List<AffiliateProduct> GetPendingApproval()
        {
            return Products.Where(e => e.PendingAdminApproval).ToList();
        }

        public List<AffiliateProduct> GetDenied()
        {
            return Products.Where(e => !e.Approved).ToList();
        }

        public List<AffiliateProduct> GetApproved()
        {
            return Products.Where(e => e.Approved).ToList();
        }
    }
}