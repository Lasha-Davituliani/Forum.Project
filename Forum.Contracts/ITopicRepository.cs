using Forum.Entities;
using System.Linq.Expressions;

namespace Forum.Contracts
{
    public interface ITopicRepository : ISavable
    {
        Task<List<TopicEntity>> GetAllTopicsAsync();
        Task<List<TopicEntity>> GetAllTopicsAsync(Expression<Func<TopicEntity, bool>> filter);
        Task<TopicEntity> GetSingleTopicAsync(Expression<Func<TopicEntity, bool>> filter);
        Task AddTopicAsync(TopicEntity entity);
        Task UpdateTopicAsync(TopicEntity entity);
        void DeleteTopic(TopicEntity entity);
    }
}
