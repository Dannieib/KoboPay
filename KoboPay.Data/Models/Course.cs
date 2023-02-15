using System.ComponentModel.DataAnnotations.Schema;

namespace KoboPay.Data.Models
{
    public class Course: Auditable
    {
        public Guid DepartmentId { get; set; }
        public string CourseTitle { get; set; }
        public string CourseCode { get; set; }
        public string CourseLecturer { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }
    }
}
