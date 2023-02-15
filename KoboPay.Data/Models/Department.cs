using System.ComponentModel.DataAnnotations.Schema;

namespace KoboPay.Data.Models
{
    public class Department:Auditable
    {
        public string DepartmentName { get; set; }
    }
}
