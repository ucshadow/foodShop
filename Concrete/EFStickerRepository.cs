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
    public class EFStickerRepository : IStickerRepository
    {
        private readonly EFDbContext _context = new EFDbContext();

        public void AddProduct(int stickerId, int productId)
        {
            var sticker = _context.Stickers.FirstOrDefault(e => e.StickerId == stickerId);
            if (sticker == null) return;
            if (sticker.ProductsAppliedTo == null) sticker.ProductsAppliedTo = "";
            sticker.ProductsAppliedTo += $" [{productId}]";
            Save();
        }

        public void ClearProducts(int stickerId)
        {
            var sticker = _context.Stickers.FirstOrDefault(e => e.StickerId == stickerId);
            if (sticker == null || sticker.ProductsAppliedTo == null) return;
            sticker.ProductsAppliedTo = "";
            Save();
        }

        public async Task<Sticker> DeleteSticker(int stickerId)
        {
            var dbEntry = await _context.Stickers.FindAsync(stickerId);
            if (dbEntry == null) return null;
            _context.Stickers.Remove(dbEntry);
            Save();
            return dbEntry;
        }

        public bool IsOnProduct(int productId)
        {
            var str = $"[{productId}]";
            return _context.Stickers.FirstOrDefault(e => e.ProductsAppliedTo.Contains(str)) != null;
        }

        public List<Sticker> GetStickersOnProduct(int productId)
        {
            var str = $"[{productId}]";
            return _context.Stickers.Where(e => e.ProductsAppliedTo.Contains(str)).ToList();
        }

        public List<Sticker> GetAllStickers()
        {
            return _context.Stickers.ToList();
        }

        public Sticker GetStickerById(int stickerId)
        {
            return _context.Stickers.FirstOrDefault(e => e.StickerId == stickerId);
        }

        public Sticker GetStickerByName(string name)
        {
            return _context.Stickers.FirstOrDefault(e => e.Name == name);
        }

        public void RemoveProduct(int stickerId, int productId)
        {
            var sticker = _context.Stickers.FirstOrDefault(e => e.StickerId == stickerId);
            if (sticker == null || sticker.ProductsAppliedTo == null) return;
            sticker.ProductsAppliedTo.Replace($"[{productId}]", "");
            Save();
        }

        public async Task<Sticker> SaveSticker(Sticker sticker)
        {
            if(sticker.StickerId == 0)
            {
                _context.Stickers.Add(sticker);
                Save();
                return sticker;
            }

            var dbEntry = await _context.Stickers.FindAsync(sticker.StickerId);
            if(dbEntry != null)
            {
                dbEntry.Name = sticker.Name;
                dbEntry.ExpirationDate = sticker.ExpirationDate;
                dbEntry.Description = sticker.Description;
                dbEntry.ProductsAppliedTo = sticker.ProductsAppliedTo;
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
                throw;
            }
        }
    }
}