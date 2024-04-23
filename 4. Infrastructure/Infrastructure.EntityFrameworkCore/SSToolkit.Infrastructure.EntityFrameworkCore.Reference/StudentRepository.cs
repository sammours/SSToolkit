namespace SSToolkit.Infrastructure.EntityFrameworkCore.Reference
{
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    public class StudentRepository : EntityFrameworkRepository<Student>, IStudentRepository
    {
        private readonly DbContext dbContext;

        public StudentRepository(BaseDbContext dbContext) 
            : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<(DateTime date1, DateTime date2)> GetFirstLastAsync()
        {
            var dates = await this.dbContext.Set<Student>().GroupBy(x => x.CreateDate)
                  .Select(x => new FirstLastStudent
                  {
                      Date1 = x.OrderBy(x => x.CreateDate).Select(x => x.CreateDate).FirstOrDefault(),
                      Date2 = x.OrderByDescending(x => x.CreateDate).Select(x => x.CreateDate).FirstOrDefault()
                  }).FirstOrDefaultAsync();

            return (dates?.Date1 ?? DateTime.Now, dates?.Date2 ?? DateTime.Now);
        }
    }
}
