using Forum.Contracts;
using Forum.Data;
using Forum.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Forum.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddCommentAsync(CommentEntity entity)
        {
            if (entity != null)
            {
                await _context.Comments.AddAsync(entity);
            }
        }

        public async Task<int> CountAsync(Expression<Func<CommentEntity, bool>> predicate)
        {
            return await _context.Comments
                .CountAsync(predicate);
        }

        public void DeleteComment(CommentEntity entity)
        {
            if (entity != null)
            {
                _context.Comments.Remove(entity);
            }
        }

        public async Task<List<CommentEntity>> GetAllCommentsAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<List<CommentEntity>> GetAllCommentsAsync(Expression<Func<CommentEntity, bool>> filter)
        {
            return await _context.Comments
                .Where(filter)
                .ToListAsync();
        }

        public async Task<CommentEntity> GetSingleCommentAsync(Expression<Func<CommentEntity, bool>> filter)
        {
            return await _context.Comments.FirstOrDefaultAsync(filter);
        }

        public async Task<TopicEntity> GetTopicAsync(int topicId)
        {
            return await _context.Topics.FindAsync(topicId);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCommentAsync(CommentEntity entity)
        {
            if (entity != null)
            {
                var commentToUpdate = await _context.Comments.FirstOrDefaultAsync(x => x.Id == entity.Id);

                if (commentToUpdate != null)
                {
                    commentToUpdate.TopicId = entity.TopicId;
                    commentToUpdate.AuthorId = entity.AuthorId;
                    commentToUpdate.Content = entity.Content;
                    commentToUpdate.CreationDate = entity.CreationDate;

                    _context.Comments.Update(commentToUpdate);
                }

            }
        }
    }
}
