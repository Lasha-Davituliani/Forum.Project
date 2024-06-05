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
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;
        public async Task AddCommentAsync(CommentForCreatingDto commentForCreatingDto)
        {
            if (commentForCreatingDto is null)
                throw new ArgumentNullException("Invalid argument passed!");

            if (commentForCreatingDto.UserId != AuthenticatedUserId())
                throw new UnauthorizedAccessException("Can`t add different users comment!");

            var result = _mapper.Map<CommentEntity>(commentForCreatingDto);
            await _commentRepository.AddCommentAsync(result);

            var user = await _userManager.FindByIdAsync(commentForCreatingDto.UserId);
            await _userManager.UpdateAsync(user);

            await _commentRepository.Save();
        }

        public async Task DeleteCommentAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentNullException("Invalid argument passed");

            var rawTodo = await _commentRepository.GetSingleCommentAsync(x => x.Id == id);

            if (rawTodo is null)
                throw new CommentNotFoundException();

            if (rawTodo.AuthorId.Trim() == AuthenticatedUserId().Trim() || AuthenticatedUserRole().Trim() == "Admin")
            {
                _commentRepository.DeleteComment(rawTodo);
                await _commentRepository.Save();
            }
            else
            {
                throw new UnauthorizedAccessException("Can`t delet different users comment!");
            }
        }

        public async Task<List<CommentForGettingDto>> GetCommentsOfUserAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentNullException("Invalid argument passed");
            if (AuthenticatedUserId().Trim() != userId.Trim())
                throw new UnauthorizedAccessException();

            var rowTodos = await _commentRepository.GetAllCommentsAsync(x => x.AuthorId.Trim() == userId.Trim());
            List<CommentForGettingDto> result = new();

            if (rowTodos.Count > 0)
                result = _mapper.Map<List<CommentForGettingDto>>(rowTodos);

            return result;
        }

        public async Task<CommentForGettingDto> GetSingleCommentByUserId(int commentId, string userId)
        {
            if (commentId <= 0 || string.IsNullOrWhiteSpace(userId))
                throw new ArgumentNullException("Invalid argument passed!");

            if (AuthenticatedUserId().Trim() != userId.Trim())
                throw new UnauthorizedAccessException();

            var rawTodo = await _commentRepository.GetSingleCommentAsync(x => x.Id == commentId && x.AuthorId == userId);

            if (rawTodo is null)
                throw new CommentNotFoundException();

            var result = _mapper.Map<CommentForGettingDto>(rawTodo);
            return result;
        }

        public Task UpdateCommentAsync(CommentForUpdatingDto commentForUpdatingDto)
        {
            throw new NotImplementedException();
            //if (todoForUpdatingDto is null)
            //    throw new ArgumentNullException("Invalid argument passed");

            //await _todoRepository.UpdateTodoAsync(_mapper.Map<TodoEntity>(todoForUpdatingDto));
            ////await _todoRepository.Save();

        }

        //public async Task UpdateTodoAsync(int todoId, JsonPatchDocument<TodoForUpdatingDto> patchDocument)
        //{
        //    if (todoId <= 0)
        //        throw new ArgumentException("Invalid argument passed");

        //    //Check if todo exists
        //    TodoEntity rawTodo = await _todoRepository.GetSingleTodoAsync(x => x.Id == todoId);

        //    if (rawTodo == null)
        //        throw new TodoNotFoundException();

        //    TodoForUpdatingDto todoToPatch = _mapper.Map<TodoForUpdatingDto>(rawTodo);

        //    patchDocument.ApplyTo(todoToPatch);
        //    _mapper.Map(todoToPatch, rawTodo);

        //    //await _todoRepository.Save();
        //}

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
