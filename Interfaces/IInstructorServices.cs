using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dtos;

namespace OnlineCoursesUI.Interfaces
{
    public interface IInstructorServices
    {
        Task<InstructorDto> GetInstructorByName(string Name);
    }
}