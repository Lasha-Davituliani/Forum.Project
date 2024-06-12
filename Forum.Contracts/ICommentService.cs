using Forum.Entities;
using Forum.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace Forum.Contracts
{
    public  interface ICommentService
    {
        Task<List<CommentEntity>> GetAllCommentsAsync();
        Task<List<CommentForGettingDto>> GetCommentsOfUserAsync(string userId);
        Task<CommentForGettingDto> GetSingleCommentByUserId(int commentId, string userId);
        Task DeleteCommentAsync(int id);
        Task AddCommentAsync(CommentForCreatingDto commentForCreatingDto);
        Task UpdateCommentAsync(CommentForUpdatingDto commentForUpdatingDto);
        Task UpdateCommentAsync(int commentId, JsonPatchDocument<CommentForUpdatingDto> patchDocument);
    }
}
