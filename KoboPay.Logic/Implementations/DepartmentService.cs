using AutoMapper;
using KoboPay.Data.DataAccess;
using KoboPay.Data.Models;
using KoboPay.Logic.Dtos;
using KoboPay.Logic.Helpers;
using KoboPay.Logic.Helpers.Validations;
using KoboPay.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace KoboPay.Logic.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public DepartmentService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<BaseResponseModel<bool>> CreateDeptAsync(CreateUpdateDepartment dept)
        {
            await ValidationHelper<DepartmentValidation, CreateUpdateDepartment>.Validate(dept);
            var mapDept = _mapper.Map<Department>(dept);

            await _dataContext.AddAsync(mapDept);
            if (await _dataContext.SaveChangesAsync() <= 0)
                return new BaseResponseModel<bool>
                {
                    IsSuccessful = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "sorry, an error occurred while saving to database.",
                    Object = false
                };

            return new BaseResponseModel<bool>
            {
                IsSuccessful = true,
                Message = "Successfully created",
                StatusCode = HttpStatusCode.OK,
                Object = true
            };

        }

        public async Task<BaseResponseModel<bool>> UpdateDeptAsync(Guid id, CreateUpdateDepartment dept)
        {
            if (id == Guid.Empty)
            {
                throw new Exception("validation failed");
            }

            await ValidationHelper<DepartmentValidation, CreateUpdateDepartment>.Validate(dept);
            var mapDept = _mapper.Map<Department>(dept);

            _dataContext.Update(mapDept);
            if (await _dataContext.SaveChangesAsync() <= 0)
                return new BaseResponseModel<bool>
                {
                    IsSuccessful = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "sorry, an error occurred while saving to database.",
                    Object = false
                };

            return new BaseResponseModel<bool>
            {
                IsSuccessful = true,
                Message = "Successfully updated",
                StatusCode = HttpStatusCode.OK,
                Object = true
            };
        }

        public async Task<BaseResponseModel<GetDepartmentDto>> GetDeptByIdAsync(Guid deptId)
        {
            if (deptId == Guid.Empty)
            {
                throw new Exception("validation failed");
            }

            var get = await _dataContext.Departments.SingleOrDefaultAsync(x => x.Id == deptId);

            if (get is null)
            {
                return new BaseResponseModel<GetDepartmentDto>
                {
                    IsSuccessful = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "No such record exist",
                    Object = null
                };
            }

            var mapGetdept = _mapper.Map<GetDepartmentDto>(get);
            return new BaseResponseModel<GetDepartmentDto>
            {
                IsSuccessful = true,
                Message = $"Course with id {deptId} successfully fetched",
                StatusCode = HttpStatusCode.OK,
                Object = mapGetdept
            };
        }

        public async Task<BaseResponseModel<List<Department>>> GetAllCourseAsync()
        {
            var get = await _dataContext.Departments.ToListAsync();

            return new BaseResponseModel<List<Department>>
            {
                IsSuccessful = true,
                Message = $"Courses successfully fetched",
                StatusCode = HttpStatusCode.OK,
                Object = get
            };
        }
    }
}
