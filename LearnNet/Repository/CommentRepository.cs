using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.models;
using api.data;
using Microsoft.EntityFrameworkCore;
using api.DTOs.Comment;
using api.Mappers;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AplicationDBContext _context;
        public CommentRepository(AplicationDBContext context)
        {
         _context = context;   
        }
        public async Task<List<Comment>> GetCommentsAsync()
        {
            return await _context.Comments.ToListAsync();
        }
        public Task<Comment?> GetCommentByIdAsync(int id)
        {
            return _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<Comment> CreateCommentAsync(CreateCommentRequestDto comment)
        {
            var commentModel = comment.ToCommentFromCreateDTO();
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<Comment?> UpdateCommentAsync(int id, UpdateCommentRequestDto comment)
        {
            var commentModel = await _context.Comments.FindAsync(id);
            if (commentModel == null)
            {
                return null;
            }
            commentModel.Title = comment.Title;
            commentModel.Content = comment.Content;
            commentModel.CreatedOn = comment.CreatedOn;
            commentModel.StockId = comment.StockId;

            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<Comment?> DeleteCommentAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return null;
            }
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;

        }
    }
}