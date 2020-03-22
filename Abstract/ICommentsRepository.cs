using FoodStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodStore.Abstract
{
    public interface ICommentsRepository
    {
        IEnumerable<Comment> GetProductComments(int productId);
        IEnumerable<Comment> GetUserComments(int userId);
        Task<Comment> SaveComment(Comment comment);
        Task<Comment> DeleteComment(int commentId);
        Task<Comment> VoteOnComment(int commentId, int vote);
    }
}
