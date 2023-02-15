using KoboPay.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoboPay.Logic.Dtos
{
    public class GetDepartmentDto: Department
    {
    }

    public class CreateUpdateDepartment
    {
        public string DepartmentName { get; set; }
    }
}
