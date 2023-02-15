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
    public class StudentService : IStudentService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly IDepartmentService _departmentService;
        private readonly ICourseService _courseService;

        public StudentService(DataContext dataContext, IMapper mapper, 
            IDepartmentService departmentService, ICourseService courseService)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _departmentService = departmentService;
            _courseService = courseService;
        }

        public async Task<BaseResponseModel<Guid>> CreateStudentAsync(CreateUpdateStudentDto student)
        {
            await ValidationHelper<StudentValidation, CreateUpdateStudentDto>.Validate(student);
            var mapStudent = _mapper.Map<Student>(student);

            var department = await _departmentService.GetDeptByIdAsync(student.DepartmentId);
            if(department.Object is null)
            {
                throw new ArgumentNullException("Sorry, no department exist for specified identifier");
            }

            var checkEmailExistence = await _dataContext.Students.FirstOrDefaultAsync(x => x.EmailAddress == student.EmailAddress);
            if (checkEmailExistence is not null)
            {
                throw new ArgumentNullException("Sorry, User with specified email address already exist.");
            }

            var add = await _dataContext.AddAsync(mapStudent);
            if (await _dataContext.SaveChangesAsync() <= 0)
                return new BaseResponseModel<Guid>
                {
                    IsSuccessful = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "sorry, an error occurred while saving to database.",
                    Object = Guid.Empty
                };

            return new BaseResponseModel<Guid>
            {
                IsSuccessful = true,
                Message = "Successfully created",
                StatusCode = HttpStatusCode.OK,
                Object = add.Entity.Id
            };

        }

        public async Task<BaseResponseModel<bool>> UpdateStudentAsync(Guid id, CreateUpdateStudentDto student)
        {
            if (id == Guid.Empty)
            {
                throw new Exception("validation failed");
            }

            await ValidationHelper<StudentValidation, CreateUpdateStudentDto>.Validate(student);
            var mapUpdateStudent = _mapper.Map<Department>(student);

            _dataContext.Update(mapUpdateStudent);
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

        public async Task<BaseResponseModel<GetStudentDto>> GetStudentByIdAsync(Guid deptId)
        {
            if (deptId == Guid.Empty)
            {
                throw new Exception("validation failed");
            }

            var get = await _dataContext.Departments.SingleOrDefaultAsync(x => x.Id == deptId);

            if (get is null)
            {
                return new BaseResponseModel<GetStudentDto>
                {
                    IsSuccessful = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "No such record exist",
                    Object = new()
                };
            }

            var mapGetdept = _mapper.Map<GetStudentDto>(get);
            return new BaseResponseModel<GetStudentDto>
            {
                IsSuccessful = true,
                Message = $"Course with id {deptId} successfully fetched",
                StatusCode = HttpStatusCode.OK,
                Object = mapGetdept
            };
        }

        public async Task<BaseResponseModel<List<Student>>> GetAllStudentAsync()
        {
            var get = await _dataContext.Students
                .Include(x => x.Courses).ToListAsync();

            return new BaseResponseModel<List<Student>>
            {
                IsSuccessful = true,
                Message = $"Courses successfully fetched",
                StatusCode = HttpStatusCode.OK,
                Object = get
            };
        }

        public async Task AddStudentCourses(List<StudentCourseDto> model)
        {
            foreach (var item in model)
            {
                var course = await _courseService.GetCourseByIdAsync(item.CourseId);
                if(course.Object == null)
                {
                    throw new ArgumentNullException($"Course Id: {item.CourseId} does not exist.");
                }
            }

            await _dataContext.AddRangeAsync(model);
            if(await _dataContext.SaveChangesAsync() <= 0)
            {
                throw new Exception("Failed to effect changes to database");
            }
        }

        public async Task UpdateStudentCourse(Guid id, StudentCourseDto model)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Failed to validate identifier");
            }

            var get = await _dataContext.studentCourses.FindAsync(id);
            if (get == null)
            {
                throw new Exception("no such record exist for specified id");

                var course = await _courseService.GetCourseByIdAsync(model.CourseId);
                if (course.Object == null)
                {
                    throw new ArgumentNullException($"Course Id: {model.CourseId} does not exist.");
                }

                _dataContext.Update(model);
                if (await _dataContext.SaveChangesAsync() <= 0)
                {
                    throw new Exception("Failed to effect changes to database");
                }
            }
        }
    }
}
