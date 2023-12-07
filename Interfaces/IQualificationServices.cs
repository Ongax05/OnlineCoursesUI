using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dtos;

namespace OnlineCoursesUI.Interfaces
{
    public interface IQualificationServices
    {
        Task<bool> PostQualification (QualificationDto qualificationDto);
        Task<AverageDto> GetAverageQualificationByCourse(int CourseId);
        Task<List<QualificationDto>> GetQualificationsByUser(int UserId);
    }
}