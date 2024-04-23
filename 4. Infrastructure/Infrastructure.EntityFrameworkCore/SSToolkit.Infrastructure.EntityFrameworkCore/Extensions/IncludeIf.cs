namespace SSToolkit.Infrastructure.EntityFrameworkCore.Extensions
{
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using SSToolkit.Domain.Repositories;
    using SSToolkit.Domain.Repositories.Model;

    public static partial class EntityFrameworkExtensions
    {
        /// <summary>
        /// Include if includes have been provided to <see cref="IFindOptions<T>"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IQueryable<T> IncludeIf<T>(
            this IQueryable<T> source,
            IFindOptions<T>? options)
            where T : class, IEntity
        {
            if (options == null || !options.ShouldInclude())
            {
                return source as IQueryable<T>;
            }

            foreach (var include in options.GetIncludes())
            {
                if (include.Expression != null)
                {
                    source = source.Include(include.Expression);
                }
                else if (include.Path is not null)
                {
                    source = source.Include(include.Path);
                }
            }

            return source;
        }
    }
}
