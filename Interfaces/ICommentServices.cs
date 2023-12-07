using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dtos;

namespace OnlineCoursesUI.Interfaces
{
    public interface ICommentServices
    {
        Task<bool> PostComment (CommentDto commentDto);
        Task<List<CommentDto>> GetCommentsByCourse(int CourseId);
        Task<List<CommentDto>> GetCommentsByUser(int UserId);
    }
}