using KoboPay.Data.Models;
using KoboPay.Logic.Dtos;

namespace KoboPay.Logic.Interfaces
{
    public interface IStudentService
    {
        Task AddStudentCourses(List<StudentCourseDto> model);
        Task UpdateStudentCourse(Guid id, StudentCourseDto model);
        Task<BaseResponseModel<Guid>> CreateStudentAsync(CreateUpdateStudentDto student);
        Task<BaseResponseModel<List<Student>>> GetAllStudentAsync();
        Task<BaseResponseModel<GetStudentDto>> GetStudentByIdAsync(Guid deptId);
        Task<BaseResponseModel<bool>> UpdateStudentAsync(Guid id, CreateUpdateStudentDto student);
    }
}