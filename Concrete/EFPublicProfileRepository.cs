using FoodStore.Abstract;
using FoodStore.Domain.Concrete;
using FoodStore.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FoodStore.Concrete
{
    public class EFPublicProfileRepository : IPublicProfilesRepository
    {

        private readonly EFDbContext _context = new EFDbContext();

        public PublicProfile GetPublicProfile(string userId)
        {
            return _context.PublicProfiles.FirstOrDefault(e => e.UserId == userId);
        }

        public PublicProfile GetCommentPublicProfile(string AspUserdId)
        {
            return _context.PublicProfiles.FirstOrDefault(e => e.UserId == AspUserdId);
        }

        public PublicProfile GetPublicProfileByName(string name)
        {
            return _context.PublicProfiles.FirstOrDefault(e => e.Name == name);
        }

        public PublicProfile SaveProfile(PublicProfile profile, string userId)
        {

            profile.UserId = userId;
            _context.PublicProfiles.Add(profile);
            Save();
            return profile;
            
        }

        public PublicProfile EditProfile(PublicProfile profile, string userId)
        {
            var dbEntry = _context.PublicProfiles.FirstOrDefault(e => e.UserId == userId);
            if (dbEntry != null)
            {                
                dbEntry.ShowPurchaseHistory = profile.ShowPurchaseHistory;
                dbEntry.Avatar = profile.Avatar;
                dbEntry.City = profile.City;
            }

            if(dbEntry.Name != profile.Name)
            {
                dbEntry.Name = profile.Name;
            }

            Save();
            return dbEntry;
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
            }
        }
    }
}