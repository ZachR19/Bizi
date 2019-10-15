namespace Bizi.Domain
{
    public class EmployeeModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName { get { return $"{FirstName} {LastName}"; } }

        public int BusinessID { get; set; }
    }
}
