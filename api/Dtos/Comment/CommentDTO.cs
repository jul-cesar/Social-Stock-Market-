using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Comment
{
    public record CommentDTO(int Id, string Title, string Content, DateTime CreatedAt, string CreatedBy,int StockId);
}
