namespace SSToolkit.Infrastructure.EntityFrameworkCore.Reference
{
    using SSToolkit.Domain.Repositories;

    public interface IStudentRepository : IRepository<Student>
    {
        Task<(DateTime date1, DateTime date2)> GetFirstLastAsync();
    }
}
