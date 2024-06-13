using Forum.Contracts;
using Forum.Data;
using Forum.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Forum.Repositories
{
    public class TopicRepository : ITopicRepository
    {
        private readonly ApplicationDbContext _context;

        public TopicRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddTopicAsync(TopicEntity entity)
        {
            if (entity != null)
            {
                await _context.Topics.AddAsync(entity);
            }
        }

        public void DeleteTopic(TopicEntity entity)
        {
            if (entity != null)
            {
                _context.Topics.Remove(entity);
            }
        }

        public async Task<List<TopicEntity>> GetAllTopicsAsync()
        {
            return await _context.Topics
                .Include(t => t.Comments) 
                .ToListAsync();
        }

        public async Task<List<TopicEntity>> GetAllTopicsWithCommentCountsAsync()
        {
            return await _context.Topics
                .Include(t => t.Comments) 
                .Select(t => new TopicEntity
                {
                    Id = t.Id,                    
                    Description = t.Description,
                    CreationDate = t.CreationDate,
                    AuthorId = t.AuthorId,
                    Author = t.Author,
                    Comments = t.Comments, 
                    CommentCount = t.Comments.Count 
                })
                .ToListAsync();
        }

        public async Task<List<TopicEntity>> GetAllTopicsAsync(Expression<Func<TopicEntity, bool>> filter)
        {
            return await _context.Topics
               .Where(filter)
               .ToListAsync();
        }

        public async Task<TopicEntity> GetSingleTopicAsync(Expression<Func<TopicEntity, bool>> filter)
        {
            return await _context.Topics.FirstOrDefaultAsync(filter);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTopicAsync(TopicEntity entity)
        {
            if (entity != null)
            {
                var topicToUpdate = await _context.Topics.FirstOrDefaultAsync(x => x.Id == entity.Id);
                if (topicToUpdate != null)
                {
                    topicToUpdate.Title = entity.Title;
                    topicToUpdate.Description = entity.Description;
                    topicToUpdate.Status = entity.Status;                    
                    topicToUpdate.AuthorId = entity.AuthorId;
                    topicToUpdate.CreationDate = entity.CreationDate;
                    topicToUpdate.State = entity.State;

                    _context.Topics.Update(topicToUpdate);
                }

            }
        }
    }
}
