using System.Collections.Generic;

namespace Bizi.Domain
{
    public class BusinessModel
    {
        public string Name { get; set; }

        public string Owner { get; set; }

        public List<EmployeeModel> Employees { get; set; } = new List<EmployeeModel>();

        public int NumOfEmployees { get { return Employees.Count; } }

        public int BusinessID { get; }
    }
}
