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
    [Route("api/topics")]
    [ApiController]
    [Authorize]
    public class TopicController : Controller
    {
        private readonly ITopicService _topicService;
        private ApiResponse _response;
        public TopicController(ITopicService topicService)
        {
            _topicService = topicService;
            _response = new ApiResponse();
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> AllTopicsOfUser([FromRoute] string userId)
        {
            var result = await _topicService.GetTopicsOfUserAsync(userId);

            _response.Result = result;
            _response.IsSuccess = true;
            _response.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
            _response.Message = "Request completed successfully";

            return StatusCode(_response.StatusCode, _response);
        }

        [HttpGet("{userId}/ {topicId}")]
        [Authorize]
        public async Task<IActionResult> SingleTopicfUser([FromRoute] string userId, [FromRoute] int topicId)
        {
            var result = await _topicService.GetSingleTopicByUserId(topicId, userId);

            _response.Result = result;
            _response.IsSuccess = true;
            _response.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
            _response.Message = "Request completed successfully";

            return StatusCode(_response.StatusCode, _response);
        }

        [HttpPost("addTopic")]
        public async Task<IActionResult> AddTopic([FromBody] TopicForCreatingDto model)
        {
            await _topicService.AddTopicAsync(model);

            _response.Result = model;
            _response.IsSuccess = true;
            _response.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
            _response.Message = "Request completed successfully";

            return StatusCode(_response.StatusCode, _response);
        }

        [HttpDelete("{topicId}")]
        public async Task<IActionResult> DeleteTopic([FromRoute] int topicId)
        {
            await _topicService.DeleteTopicAsync(topicId);

            _response.Result = topicId;
            _response.IsSuccess = true;
            _response.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
            _response.Message = "Request completed successfully";

            return StatusCode(_response.StatusCode, _response);
        }

        [HttpPatch("{topicId:int}")]
        public async Task<IActionResult> UpdateTopic([FromRoute] int topicId, [FromBody] JsonPatchDocument<TopicForUpdatingDto> patchDocument)
        {
            await _topicService.UpdateTopicAsync(topicId, patchDocument);

            _response.Result = patchDocument;
            _response.IsSuccess = true;
            _response.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
            _response.Message = "Topic updated successfully";

            return StatusCode(_response.StatusCode, _response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTopic([FromBody] TopicForUpdatingDto topicForUpdatingDto)
        {
            await _topicService.UpdateTopicAsync(topicForUpdatingDto);

            _response.Result = topicForUpdatingDto;
            _response.IsSuccess = true;
            _response.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
            _response.Message = "Request completed successfully";

            return StatusCode(_response.StatusCode, _response);
        }
    }
}
