using AutoMapper;
using KoboPay.Data.Models;
using KoboPay.Logic.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoboPay.Logic.Mapper
{
    public class Maps:Profile
    {
        public Maps()
        {
            //student maps
            CreateMap<Student, CreateUpdateStudentDto>().ReverseMap();
            CreateMap<Student, GetStudentDto>().ReverseMap();


            //department maps
            CreateMap<GetDepartmentDto, Department>().ReverseMap();
            CreateMap<CreateUpdateDepartment, Department>().ReverseMap();

            //course maps
            CreateMap<GetCourseDto,Course>().ReverseMap();
            CreateMap<CourseDto,Course>().ReverseMap();
        }
    }
}
