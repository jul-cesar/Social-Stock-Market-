using api.Dtos.Comment;
using api.Models;

namespace api.Mappers;

public static class CommentMapper
{
    public static CommentDTO ToDTO(this Comment comment)
    {
        return new CommentDTO(
            comment.Id,
            comment.Title,
            comment.Content,
            comment.CreatedAt,
            comment.StockId
        );
    }

    public static Comment ToCommentFromCreate(this CreateCommentDTO comment, int stockId)
    {
        return new Comment
        {
            Title = comment.Title,
            Content = comment.Content,
            StockId = stockId,
        };
    }
}
