namespace SSToolkit.Infrastructure.EntityFrameworkCore.Reference
{
    using SSToolkit.Domain.Repositories.Model;

    public class Teacher : Entity<int>
    {
        public string FirstName { get; set; } = string.Empty;
    }
}
