using AutoMapper;
using Forum.Contracts;
using Forum.Entities;
using Forum.Models;
using Forum.Service.Exceptions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using System.Security.Claims;

namespace Forum.Service.Implementacions
{
    public class TopicService : ITopicService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ITopicRepository _topicRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;

        public TopicService(ITopicRepository topicRepository, ICommentRepository commentRepository, IHttpContextAccessor httpContextAccessor, Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager)
        {
            _topicRepository = topicRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = MappingInitializer.Initialize();
            _userManager = userManager;
            _commentRepository = commentRepository;

        }
        public async Task AddTopicAsync(TopicForCreatingDto topicForCreatingDto)
        {
           if(topicForCreatingDto is null)
                throw new ArgumentNullException("Invalid argument passed!");

           if(topicForCreatingDto.AuthorId != AuthenticatedUserId())
                throw new UnauthorizedAccessException("Can`t add different users topic!");

            var result = _mapper.Map<TopicEntity>(topicForCreatingDto);
            await _topicRepository.AddTopicAsync(result);

            var user = await _userManager.FindByIdAsync(topicForCreatingDto.AuthorId);
            await _userManager.UpdateAsync(user);

            await _topicRepository.Save();
        }

        public async Task DeleteTopicAsync(int Id)
        {
            if (Id <= 0)
                throw new ArgumentNullException("Invalid argument passed");

            var rawTopics = await _topicRepository.GetSingleTopicAsync(x => x.Id == Id);

            if (rawTopics is null)
                throw new TopicNotFoundException();

            if (rawTopics.AuthorId.Trim() == AuthenticatedUserId().Trim() || AuthenticatedUserRole().Trim() == "Admin")
            {
                _topicRepository.DeleteTopic(rawTopics);
                await _topicRepository.Save();
            }
            else
            {
                throw new UnauthorizedAccessException("Can`t delet different users topic!");
            }
        }

        public async Task<List<TopicForGettingDto>> GetAllTopicsAsync()
        {
            var topics = await _topicRepository.GetAllTopicsWithCommentCountsAsync();
            return _mapper.Map<List<TopicForGettingDto>>(topics);
        }

        public async Task<TopicForGettingDto> GetSingleTopicByUserId(int topicId, string userId)
        {
            if (topicId <= 0 || string.IsNullOrWhiteSpace(userId))
                throw new ArgumentNullException("Invalid argument passed!");

            if (AuthenticatedUserId().Trim() != userId.Trim())
                throw new UnauthorizedAccessException();

            var rawTopic = await _topicRepository.GetSingleTopicAsync(x => x.Id == topicId && x.AuthorId == userId);


            if (rawTopic is null)
                throw new TopicNotFoundException();

            var result = _mapper.Map<TopicForGettingDto>(rawTopic);
            
            return result;
        }

        public async Task<List<TopicForGettingDto>> GetTopicsOfUserAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentNullException("Invalid argument passed");

            if (AuthenticatedUserId().Trim() != userId.Trim())
                throw new UnauthorizedAccessException();

            var rawTopics = await _topicRepository.GetAllTopicsAsync(x => x.AuthorId.Trim() == userId.Trim());
            var topicDtos = new List<TopicForGettingDto>();

            foreach (var rawTopic in rawTopics)
            {
                var topicDto = _mapper.Map<TopicForGettingDto>(rawTopic);
                topicDto.CommentCount = await _commentRepository.CountAsync(c => c.TopicId == rawTopic.Id);

                var user = await _userManager.FindByIdAsync(rawTopic.AuthorId);
                topicDto.AuthorName = user?.UserName;

                topicDtos.Add(topicDto);
            }

            return topicDtos;
        }

        public async Task UpdateTopicAsync(TopicForUpdatingDto topicForUpdatingDto)
        {
            
            if (topicForUpdatingDto is null)
                throw new ArgumentNullException("Invalid argument passed");

            await _topicRepository.UpdateTopicAsync(_mapper.Map<TopicEntity>(topicForUpdatingDto));
            await _topicRepository.Save();

        }

        public async Task UpdateTopicAsync(int topicId, JsonPatchDocument<TopicForUpdatingDto> patchDocument)
        {
            if (topicId <= 0)
                throw new ArgumentException("Invalid argument passed");

            TopicEntity rawTopics = await _topicRepository.GetSingleTopicAsync(x => x.Id == topicId);

            if (rawTopics == null)
                throw new TopicNotFoundException();

            TopicForUpdatingDto topicToPatch = _mapper.Map<TopicForUpdatingDto>(rawTopics);

            patchDocument.ApplyTo(topicToPatch);
            _mapper.Map(topicToPatch, rawTopics);

            await _topicRepository.Save();
        }

        private string AuthenticatedUserId()
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                return result;
            }
            else
            {
                throw new UnauthorizedAccessException("Can`t get credentials of unauthorized user!");
            }
        }

        private string AuthenticatedUserRole()
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
                return result;
            }
            else
            {
                throw new UnauthorizedAccessException("Can`t get credentials of unauthorized user!");
            }
        }
    }
}
