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
    public class EFCommentRepository : ICommentsRepository
    {

        private readonly EFDbContext _context = new EFDbContext();

        public async Task<Comment> DeleteComment(int commentId)
        {
            var dbEntry = await _context.Comments.FindAsync(commentId);
            if (dbEntry == null) return null;
            _context.Comments.Remove(dbEntry);
            _context.SaveChanges();
            return dbEntry;
        }

        public IEnumerable<Comment> GetProductComments(int productId)
        {
            return _context.Comments.Where(e => e.ProductID == productId);
        }

        public IEnumerable<Comment> GetUserComments(int userId)
        {
            return _context.Comments.Where(e => e.AspNetUserId == userId);
        }

        public async Task<Comment> SaveComment(Comment comment)
        {
            if(comment.CommentId == 0)
            {
                _context.Comments.Add(comment);
                return comment;
            }

            var dbEntry = await _context.Comments.FindAsync(comment.CommentId);
            if(dbEntry != null)
            {
                dbEntry.AddedOn = comment.AddedOn;
                dbEntry.CommentType = comment.CommentType;
                dbEntry.Content = comment.Content;
                dbEntry.Likes = comment.Likes;
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
            return dbEntry;
        }

        public async Task<Comment> VoteOnComment(int commentId, int vote)
        {

            var dbEntry = await _context.Comments.FindAsync(commentId);
            if (dbEntry != null)
            {
                dbEntry.Likes += vote;
            }
            else
            {
                return null; // ??
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
            return dbEntry;
        }
    }
}