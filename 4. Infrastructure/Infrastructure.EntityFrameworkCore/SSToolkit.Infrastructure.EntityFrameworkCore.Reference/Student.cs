namespace SSToolkit.Infrastructure.EntityFrameworkCore.Reference
{
    using SSToolkit.Domain.Repositories.Model;

    public class Student : Entity
    {
        public string FirstName { get; set; } = string.Empty;

        public DateTime CreateDate { get; set; } = DateTime.Now;
    }

#pragma warning disable SA1402 // File may only contain a single type
    public class FirstLastStudent
#pragma warning restore SA1402 // File may only contain a single type
    {
        public DateTime? Date1 { get; set; }

        public DateTime? Date2 { get; set; }
    }
}
