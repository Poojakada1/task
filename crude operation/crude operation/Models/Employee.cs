namespace crude_operation.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
       
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Department { get; set; }

        public string Email { get; set; }

        public long Salary { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
