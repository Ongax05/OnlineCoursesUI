using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dtos;

namespace OnlineCoursesUI.Interfaces
{
    public interface ICourseServices
    {
        Task<IEnumerable<CourseDto>> GetCourses ();
        Task<CourseWithEntities> GetCourseById(int Id);
        Task<bool> PostCourse (CourseDto courseDto);
    }
}