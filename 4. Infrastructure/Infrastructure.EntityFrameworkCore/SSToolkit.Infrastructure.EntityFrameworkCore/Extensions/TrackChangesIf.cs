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
        public static IQueryable<T> TrackChangesIf<T>(
            this IQueryable<T> source,
            IFindOptions<T>? options)
            where T : class, IEntity
        {
            return options == null || !options.TrackChanges ? source : source.AsNoTracking();
        }
    }
}
