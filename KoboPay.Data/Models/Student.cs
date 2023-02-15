using System.ComponentModel.DataAnnotations.Schema;

namespace KoboPay.Data.Models
{
    public class Student: Auditable
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public int Age { get; set; }

        [ForeignKey("Departments")]
        public Guid DepartmentId { get; set; }


        [ForeignKey("id")]
        public virtual ICollection<StudentCourse> Courses { get; set; }
    }
}
