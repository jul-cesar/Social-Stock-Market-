

namespace api.Dtos.Comment
{
    public record CommentDTO(int Id, string Title, string Content, DateTime CreatedAt, int StockId);
}
