using System.Security.Claims;
using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/comments")]
[ApiController]
public class CommentController(
    ICommentRepository commentRepository,
    IStockRepository stockRepository,
    UserManager<AppUser> userManager
    
) : ControllerBase
{
    private readonly ICommentRepository CommentRepo = commentRepository;
    private readonly IStockRepository StockRepo = stockRepository;
    private readonly UserManager<AppUser>_userManager = userManager;

    // [HttpGet]
    // public async Task<ActionResult> GetStockComments(int stockId)
    // {
    //     await CommentRepo.GetStockCommentsAsync(stockId);
    // }

    [HttpGet]
    public async Task<ActionResult> GetAllComments()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var comments = await CommentRepo.GetAllCommentsAsync();
        var commentsDTO = comments.Select(c => c.ToDTO());

        return Ok(commentsDTO);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var comment = await CommentRepo.GetCommentByIdAsync(id);
        if (comment is null)
        {
            return NotFound();
        }
        return Ok(comment.ToDTO());
    }

    [HttpPost("{stockId:int}")]
    public async Task<IActionResult> CreateComment(
        [FromRoute] int stockId,
        CreateCommentDTO newComment
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
           
        var stockExists = await StockRepo.StockExists(stockId);
        if (!stockExists)
        {
            return BadRequest("Stock does not exists");
        }

         var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

         if (userId == null)
        {
            return Unauthorized("User is not authenticated.");
        }

         var user = await _userManager.FindByIdAsync(userId);

    
        var username = user?.UserName;

        if (username == null)
        {
            return Unauthorized("User is not authenticated.");
        }

        var AppUser = await _userManager.FindByNameAsync(username); 

        if (AppUser == null)
        {
            return Unauthorized("User is not authenticated.");
        }

        var commentModel = newComment.ToCommentFromCreate(stockId);

        commentModel.AppUserId = AppUser.Id;
        await CommentRepo.CreateCommentAsync(commentModel);
        return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToDTO());
    }

    [HttpPut("{stockId:int}")]
    public async Task<IActionResult> UpdateComment(
        [FromRoute] int stockId,
        UpdateCommentDTO comment
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var updatedCommet = await CommentRepo.UpdateCommentAsync(stockId, comment);

        if (updatedCommet is null)
        {
            return NotFound("Comment does not exists");
        }
        return Ok(updatedCommet.ToDTO());
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteComment([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var comment = await CommentRepo.DeleteCommentAsync(id);
        if (comment is null)
        {
            return NotFound("Comment does not exists");
        }
        return NoContent();
    }
}
