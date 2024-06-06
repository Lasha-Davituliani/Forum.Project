using Forum.Contracts;
using Forum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Forum.API.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commnetService;
        private ApiResponse _response;
        public CommentController(ICommentService commentService)
        {
            _commnetService = commentService;
            _response = new ApiResponse();
        }

        [HttpGet("{userId}/comment")]
        public async Task<IActionResult> AllCommentsOfUser([FromRoute] string userId)
        {
            var result = await _commnetService.GetCommentsOfUserAsync(userId);

            _response.Result = result;
            _response.IsSuccess = true;
            _response.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
            _response.Message = "Request completed successfully";

            return StatusCode(_response.StatusCode, _response);
        }

        [HttpGet("{userId}/ {commentId}")]
        [Authorize]
        public async Task<IActionResult> SingleCommentOfUser([FromRoute] string userId, [FromRoute] int commentId)
        {
            var result = await _commnetService.GetSingleCommentByUserId(commentId, userId);

            _response.Result = result;
            _response.IsSuccess = true;
            _response.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
            _response.Message = "Request completed successfully";

            return StatusCode(_response.StatusCode, _response);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddComment([FromBody] CommentForCreatingDto model)
        {
            await _commnetService.AddCommentAsync(model);

            _response.Result = model;
            _response.IsSuccess = true;
            _response.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
            _response.Message = "Request completed successfully";

            return StatusCode(_response.StatusCode, _response);
        }

        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int commentId)
        {
            await _commnetService.DeleteCommentAsync(commentId);

            _response.Result = commentId;
            _response.IsSuccess = true;
            _response.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
            _response.Message = "Request completed successfully";

            return StatusCode(_response.StatusCode, _response);
        }
    }
}
