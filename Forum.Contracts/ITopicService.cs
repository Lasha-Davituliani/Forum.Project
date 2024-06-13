using Forum.Entities;
using Forum.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace Forum.Contracts
{
    public interface ITopicService
    {
        Task<List<TopicEntity>> GetAllTopicsAsync();
        Task<List<TopicForGettingDto>> GetTopicsOfUserAsync(string userId);
        Task<TopicForGettingDto> GetSingleTopicByUserId(int topicId, string userId);
        Task DeleteTopicAsync(int Id);
        Task AddTopicAsync(TopicForCreatingDto topicForCreatingDto);
        Task UpdateTopicAsync(TopicForUpdatingDto topicForUpdatingDto);
        Task UpdateTopicAsync(int topicId, JsonPatchDocument<TopicForUpdatingDto> patchDocument);
    }
}
