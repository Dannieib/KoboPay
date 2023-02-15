using KoboPay.Data.Models;
using KoboPay.Logic.Dtos;

namespace KoboPay.Logic.Interfaces
{
    public interface IDepartmentService
    {
        Task<BaseResponseModel<bool>> CreateDeptAsync(CreateUpdateDepartment dept);
        Task<BaseResponseModel<List<Department>>> GetAllCourseAsync();
        Task<BaseResponseModel<GetDepartmentDto>> GetDeptByIdAsync(Guid deptId);
        Task<BaseResponseModel<bool>> UpdateDeptAsync(Guid id, CreateUpdateDepartment dept);
    }
}