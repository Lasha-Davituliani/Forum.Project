using Forum.Entities;
using System.Linq.Expressions;

namespace Forum.Contracts
{
    public interface ICommentRepository : ISavable
    {
        Task<List<CommentEntity>> GetAllCommentsAsync();
        Task<List<CommentEntity>> GetAllCommentsAsync(Expression<Func<CommentEntity, bool>> filter);
        Task<CommentEntity> GetSingleCommentAsync(Expression<Func<CommentEntity, bool>> filter);
        Task AddCommentAsync(CommentEntity entity);
        Task UpdateCommentAsync(CommentEntity entity);
        void DeleteComment(CommentEntity entity);
    }
}
