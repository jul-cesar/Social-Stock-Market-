using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController(
        ICommentRepository commentRepository,
        IStockRepository stockRepository
    ) : ControllerBase
    {
        private readonly ICommentRepository CommentRepo = commentRepository;
        private readonly IStockRepository StockRepo = stockRepository;

        // [HttpGet]
        // public async Task<ActionResult> GetStockComments(int stockId)
        // {
        //     await CommentRepo.GetStockCommentsAsync(stockId);
        // }

        [HttpGet]
        public async Task<ActionResult> GetAllComments()
        {
            var comments = await CommentRepo.GetAllCommentsAsync();
            var commentsDTO = comments.Select(c => c.ToDTO());

            return Ok(commentsDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await CommentRepo.GetCommentByIdAsync(id);
            if (comment is null)
            {
                return NotFound();
            }
            return Ok(comment.ToDTO());
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> CreateComment(
            [FromRoute] int stockId,
            CreateCommentDTO newComment
        )
        {
            var stockExists = await StockRepo.StockExists(stockId);
            if (!stockExists)
            {
                return BadRequest("Stock does not exists");
            }

            var commentModel = newComment.ToCommentFromCreate(stockId);
            await CommentRepo.CreateCommentAsync(commentModel);
            return CreatedAtAction(
                nameof(GetById),
                new { id = commentModel },
                commentModel.ToDTO()
            );
        }

        [HttpPut("{stockId}")]
        public async Task<IActionResult> UpdateComment(
            [FromRoute] int stockId,
            UpdateCommentDTO comment
        )
        {
            var updatedCommet = await CommentRepo.UpdateCommentAsync(stockId, comment);

            if (updatedCommet is null)
            {
                return NotFound("Comment does not exists");
            }
            return Ok(updatedCommet.ToDTO());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            var comment = await CommentRepo.DeleteCommentAsync(id);
            if (comment is null)
            {
                return NotFound("Comment does not exists");
            }
            return NoContent();
        }
    }
}
