using api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    public class CommentController : ControllerBase {
        private readonly ICommentRepository CommentRepo;
        public CommentController(ICommentRepository commentRepository)
        {
            CommentRepo = commentRepository;      
        }

        [HttpGet]   
        public async Task<ActionResult> GetComments(int stockId) {
        }

     }
}
