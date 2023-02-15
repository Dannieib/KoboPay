using System.ComponentModel.DataAnnotations.Schema;

namespace KoboPay.Data.Models
{
    public class StudentCourse:Auditable
    {
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }

        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }
    }
}
