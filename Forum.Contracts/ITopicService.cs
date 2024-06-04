using Forum.Models;

namespace Forum.Contracts
{
    public interface ITopicService
    {
        Task<List<TopicForGettingDto>> GetTopicsOfUserAsync(string userId);
        Task<TopicForGettingDto> GetSingleTopicByUserId(int topicId, string userId);
        Task DeleteTopicAsync(int Id);
        Task AddTopicAsync(TopicForCreatingDto topicForCreatingDto);
        Task UpdateTopicAsync(TopicForUpdatingDto topicForUpdatingDto);
    }
}
