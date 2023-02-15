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
    public class CourseService : ICourseService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public CourseService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<BaseResponseModel<CourseDto>> CreateCourseAsync(CourseDto course)
        {
            await ValidationHelper<CourseValidation, CourseDto>.Validate(course);
            var mapCourse = _mapper.Map<Course>(course);

            await _dataContext.AddAsync(mapCourse);
            if (await _dataContext.SaveChangesAsync() <= 0)
                return new BaseResponseModel<CourseDto>
                {
                    IsSuccessful = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "sorry, an error occurred while saving to database.",
                    Object = new()
                };

            return new BaseResponseModel<CourseDto>
            {
                IsSuccessful = true,
                Message = "Successfully created",
                StatusCode = HttpStatusCode.OK,
                Object = course
            };

        }

        public async Task<BaseResponseModel<CourseDto>> UpdateCourseAsync(Guid courseId, CourseDto course)
        {
            if (courseId == Guid.Empty)
            {
                throw new Exception("validation failed");
            }

            await ValidationHelper<CourseValidation, CourseDto>.Validate(course);
            var mapCourse = _mapper.Map<Course>(course);

            _dataContext.Update(mapCourse);
            if (await _dataContext.SaveChangesAsync() <= 0)
                return new BaseResponseModel<CourseDto>
                {
                    IsSuccessful = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "sorry, an error occurred while saving to database.",
                    Object = new()
                };

            return new BaseResponseModel<CourseDto>
            {
                IsSuccessful = true,
                Message = "Successfully created",
                StatusCode = HttpStatusCode.OK,
                Object = course
            };
        }

        public async Task<BaseResponseModel<GetCourseDto>> GetCourseByIdAsync(Guid courseId)
        {
            if (courseId == Guid.Empty)
            {
                throw new Exception("validation failed");
            }

            var get = await _dataContext.Courses.SingleOrDefaultAsync(x => x.Id == courseId);

            if (get is null)
            {
                return new BaseResponseModel<GetCourseDto>
                {
                    IsSuccessful = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "No such data exist",
                    Object = null
                };
            }

            var mapGetCourse = _mapper.Map<GetCourseDto>(get);
            return new BaseResponseModel<GetCourseDto>
            {
                IsSuccessful = true,
                Message = $"Course with id {courseId} successfully fetched",
                StatusCode = HttpStatusCode.OK,
                Object = mapGetCourse
            };
        }


        public async Task<BaseResponseModel<List<Course>>> GetAllCourseAsync()
        {
            var get = await _dataContext.Courses.Include(x=>x.Department).ToListAsync();

            return new BaseResponseModel<List<Course>>
            {
                IsSuccessful = true,
                Message = $"Courses successfully fetched",
                StatusCode = HttpStatusCode.OK,
                Object = get
            };
        }
    }
}