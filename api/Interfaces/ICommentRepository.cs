using api.Dtos.Comment;
using api.Models;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetStockCommentsAsync(int stockId);
        Task<List<Comment>> GetAllCommentsAsync();
        Task<Comment> CreateCommentAsync(Comment comment);

        Task<Comment?> GetCommentByIdAsync(int id);

        Task<Comment?> UpdateCommentAsync(int id, UpdateCommentDTO comment);

        Task<Comment?> DeleteCommentAsync(int id);
    }
}
