using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.models;
using api.DTOs.Comment;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetCommentsAsync();
        Task<Comment?> GetCommentByIdAsync(int id);
        Task<Comment> CreateCommentAsync(Comment comment);
        Task<Comment?> UpdateCommentAsync(int id, Comment comment);
        Task<Comment?> DeleteCommentAsync(int id);
    }
}