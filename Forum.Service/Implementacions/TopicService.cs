using AutoMapper;
using Forum.Contracts;
using Forum.Entities;
using Forum.Models;
using Forum.Service.Exceptions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Forum.Service.Implementacions
{
    public class TopicService : ITopicService
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;

        public TopicService(ITopicRepository topicRepository, IHttpContextAccessor httpContextAccessor)
        {
            _topicRepository = topicRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = MappingInitializer.Initialize();

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

            var rawTodo = await _topicRepository.GetSingleTopicAsync(x => x.Id == Id);

            if (rawTodo is null)
                throw new TopicNotFoundException();

            if (rawTodo.AuthorId.Trim() == AuthenticatedUserId().Trim() || AuthenticatedUserRole().Trim() == "Admin")
            {
                _topicRepository.DeleteTopic(rawTodo);
                await _topicRepository.Save();
            }
            else
            {
                throw new UnauthorizedAccessException("Can`t delet different users todo!");
            }
        }

        public async Task<TopicForGettingDto> GetSingleTopicByUserId(int topicId, string userId)
        {
            if (topicId <= 0 || string.IsNullOrWhiteSpace(userId))
                throw new ArgumentNullException("Invalid argument passed!");

            if (AuthenticatedUserId().Trim() != userId.Trim())
                throw new UnauthorizedAccessException();

            var rawTodo = await _topicRepository.GetSingleTopicAsync(x => x.Id == topicId && x.AuthorId == userId);

            if (rawTodo is null)
                throw new TopicNotFoundException();

            var result = _mapper.Map<TopicForGettingDto>(rawTodo);
            return result;
        }

        public async Task<List<TopicForGettingDto>> GetTopicsOfUserAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentNullException("Invalid argument passed");
            if (AuthenticatedUserId().Trim() != userId.Trim())
                throw new UnauthorizedAccessException();

            var rowTodos = await _topicRepository.GetAllTopicsAsync(x => x.AuthorId.Trim() == userId.Trim());
            List<TopicForGettingDto> result = new();

            if (rowTodos.Count > 0)
                result = _mapper.Map<List<TopicForGettingDto>>(rowTodos);

            return result;
        }

        public Task UpdateTopicAsync(TopicForUpdatingDto topicForUpdatingDto)
        {
            throw new NotImplementedException();
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
