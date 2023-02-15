using KoboPay.Data.Models;

namespace KoboPay.Logic.Dtos
{
    public class GetStudentDto:Student{}

    public class CreateUpdateStudentDto
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public int Age { get; set; }
        public Guid DepartmentId { get; set; }
    }
}