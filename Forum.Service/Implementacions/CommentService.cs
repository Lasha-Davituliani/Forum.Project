﻿using AutoMapper;
using Forum.Contracts;
using Forum.Entities;
using Forum.Models;
using Forum.Service.Exceptions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Forum.Service.Implementacions
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;

        public CommentService(ICommentRepository commentRepository, IHttpContextAccessor httpContextAccessor, Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager)
        {
            _commentRepository = commentRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = MappingInitializer.Initialize();
            _userManager = userManager;

        }
        public async Task AddCommentAsync(CommentForCreatingDto commentForCreatingDto)
        {
            if (commentForCreatingDto is null)
                throw new ArgumentNullException("Invalid argument passed!");

            if (commentForCreatingDto.AuthorId != AuthenticatedUserId())
                throw new UnauthorizedAccessException("Can`t add different users comment!");

            var topic = await _commentRepository.GetTopicAsync(commentForCreatingDto.TopicId);

            if (topic.Status == Status.Inactive)
                throw new InvalidOperationException("Cannot add a comment to an inactive topic.");

            var result = _mapper.Map<CommentEntity>(commentForCreatingDto);
            await _commentRepository.AddCommentAsync(result);

            await _commentRepository.Save();
        }

        public async Task DeleteCommentAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentNullException("Invalid argument passed");

            var rawComment = await _commentRepository.GetSingleCommentAsync(x => x.Id == id);

            if (rawComment is null)
                throw new CommentNotFoundException();

            if (rawComment.AuthorId.Trim() == AuthenticatedUserId().Trim() || AuthenticatedUserRole().Trim() == "Admin")
            {
                _commentRepository.DeleteComment(rawComment);
                await _commentRepository.Save();
            }
            else
            {
                throw new UnauthorizedAccessException("Can`t delet different users comment!");
            }
        }

        public async Task<List<CommentEntity>> GetAllCommentsAsync()
        {
            return await _commentRepository.GetAllCommentsAsync();
        }

        public async Task<List<CommentForGettingDto>> GetCommentsOfUserAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentNullException("Invalid argument passed");
            if (AuthenticatedUserId().Trim() != userId.Trim())
                throw new UnauthorizedAccessException();

            var rawComment = await _commentRepository.GetAllCommentsAsync(x => x.AuthorId.Trim() == userId.Trim());
            List<CommentForGettingDto> result = new();

            if (rawComment.Count > 0)
                result = _mapper.Map<List<CommentForGettingDto>>(rawComment);

            return result;
        }

        public async Task<CommentForGettingDto> GetSingleCommentByUserId(int commentId, string userId)
        {
            if (commentId <= 0 || string.IsNullOrWhiteSpace(userId))
                throw new ArgumentNullException("Invalid argument passed!");

            if (AuthenticatedUserId().Trim() != userId.Trim())
                throw new UnauthorizedAccessException();

            var rawComment = await _commentRepository.GetSingleCommentAsync(x => x.Id == commentId && x.AuthorId == userId);

            if (rawComment is null)
                throw new CommentNotFoundException();

            var result = _mapper.Map<CommentForGettingDto>(rawComment);
            return result;
        }

        public async Task UpdateCommentAsync(CommentForUpdatingDto commentForUpdatingDto)
        {            
            if (commentForUpdatingDto is null)
                throw new ArgumentNullException("Invalid argument passed");

            await _commentRepository.UpdateCommentAsync(_mapper.Map<CommentEntity>(commentForUpdatingDto));
            await _commentRepository.Save();

        }

        public async Task UpdateCommentAsync(int commentId, JsonPatchDocument<CommentForUpdatingDto> patchDocument)
        {
            if (commentId <= 0)
                throw new ArgumentException("Invalid argument passed");

            CommentEntity rawComment = await _commentRepository.GetSingleCommentAsync(x => x.Id == commentId);

            if (rawComment == null)
                throw new CommentNotFoundException();

            CommentForUpdatingDto commentToPatch = _mapper.Map<CommentForUpdatingDto>(rawComment);

            patchDocument.ApplyTo(commentToPatch);
            _mapper.Map(commentToPatch, rawComment);

            await _commentRepository.Save();
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
