using KoboPay.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace KoboPay.Logic.Dtos
{
    public class CourseDto
    {
        public Guid Id { get; set; }
        [ForeignKey("Departments")]
        public Guid DepartmentId { get; set; }
        public string CourseTitle { get; set; }
        public string CourseCode { get; set; }

        /*Not going into managing lecturers..
         Lecturers would be one-to-many courses*/
        public string CourseLecturer { get; set; }
    }

    public class GetCourseDto : Course {}
}