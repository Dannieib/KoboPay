using KoboPay.Data.Models;
using KoboPay.Logic.Dtos;

namespace KoboPay.Logic.Interfaces
{
    public interface ICourseService
    {
        Task<BaseResponseModel<CourseDto>> CreateCourseAsync(CourseDto course);
        Task<BaseResponseModel<List<Course>>> GetAllCourseAsync();
        Task<BaseResponseModel<GetCourseDto>> GetCourseByIdAsync(Guid courseId);
        Task<BaseResponseModel<CourseDto>> UpdateCourseAsync(Guid courseId, CourseDto course);
    }
}