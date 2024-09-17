using api.Data;
using api.Dtos.Comment;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories;

public class CommentRepository(AppDbContext db) : ICommentRepository
{
    AppDbContext _db = db;

    public async Task<Comment> CreateCommentAsync(Comment comment)
    {
        await _db.Comments.AddAsync(comment);
        await _db.SaveChangesAsync();
        return comment;
    }

    public async Task<List<Comment>> GetAllCommentsAsync()
    {
        return await _db.Comments.Include(c => c.AppUser).ToListAsync();
    }

    public Task<List<Comment>> GetStockCommentsAsync(int stockId)
    {
        throw new NotImplementedException();
    }

    public async Task<Comment?> GetCommentByIdAsync(int id)
    {
        return await _db.Comments.Include(c => c.AppUser).FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Comment?> UpdateCommentAsync(int id, UpdateCommentDTO comment)
    {
        var commentExists = await _db.Comments.FindAsync(id);
        if (commentExists is null)
        {
            return null;
        }
        commentExists.Title = comment.Title;
        commentExists.Content = comment.Content;
        await _db.SaveChangesAsync();
        return commentExists;
    }

    public async Task<Comment?> DeleteCommentAsync(int id)
    {
        var commentExists = await _db.Comments.FindAsync(id);
        if (commentExists is null)
        {
            return null;
        }

        _db.Comments.Remove(commentExists);
        await _db.SaveChangesAsync();
        return commentExists;
    }
}
