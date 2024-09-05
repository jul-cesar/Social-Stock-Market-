using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Comment
{
    public record CreateCommentDTO(
        [Required]
        [MinLength(3, ErrorMessage = "Title must be atleast 3 characters")]
        [MaxLength(100, ErrorMessage = "Title cannot be over 100 characters.")]
            string Title,
        [Required]
        [MinLength(2, ErrorMessage = "Comment must be atleast 2 characters.")]
        [MaxLength(250, ErrorMessage = "Comment cannot be over 250 characters.")]
            string Content,
        int StockId
    );
}
