using Forum.Contracts;
using Forum.Models;
using Forum.Service.Implementacions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Forum.API.Controllers
{
    [Produces("application/json")]
    [Route("api/comments")]
    [ApiController]
    [Authorize]
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

        [HttpPost("addComment")]
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

        [HttpPatch("{commentId:int}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int commentId, [FromBody] JsonPatchDocument<CommentForUpdatingDto> patchDocument)
        {
            await _commnetService.UpdateCommentAsync(commentId, patchDocument);

            _response.Result = patchDocument;
            _response.IsSuccess = true;
            _response.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
            _response.Message = "Comment updated successfully";

            return StatusCode(_response.StatusCode, _response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateComment([FromBody] CommentForUpdatingDto commentForUpdatingDto)
        {
            await _commnetService.UpdateCommentAsync(commentForUpdatingDto);

            _response.Result = commentForUpdatingDto;
            _response.IsSuccess = true;
            _response.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
            _response.Message = "Request completed successfully";

            return StatusCode(_response.StatusCode, _response);
        }
    }
}
