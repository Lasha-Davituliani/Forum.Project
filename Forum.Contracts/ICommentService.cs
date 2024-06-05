using Forum.Models;

namespace Forum.Contracts
{
    public  interface ICommentService
    {
        Task<List<CommentForGettingDto>> GetCommentsOfUserAsync(string userId);
        Task<CommentForGettingDto> GetSingleCommentByUserId(int commentId, string userId);
        Task DeleteCommentAsync(int id);
        Task AddCommentAsync(CommentForCreatingDto commentForCreatingDto);
        Task UpdateCommentAsync(CommentForUpdatingDto commentForUpdatingDto);
    }
}
