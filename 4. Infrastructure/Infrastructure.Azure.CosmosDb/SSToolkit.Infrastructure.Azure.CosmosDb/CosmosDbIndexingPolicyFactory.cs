namespace SSToolkit.Infrastructure.Azure.CosmosDb
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.Azure.Cosmos;
    using SSToolkit.Domain.Repositories.Model;
    using SSToolkit.Fundamental.Extensions;

    public static class CosmosDbIndexingPolicyFactory
    {
        /// <summary>
        /// Create Indexing policy
        /// </summary>
        /// <param name="automatic"></param>
        /// <param name="indexingMode"></param>
        /// <returns></returns>
        public static IndexingPolicy Create(bool automatic = true, IndexingMode indexingMode = IndexingMode.Consistent)
            => new()
            {
                Automatic = automatic,
                IndexingMode = indexingMode
            };

        /// <summary>
        /// Add excluded path when it is not already added
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source">The indexing policy</param>
        /// <param name="expression">Property expression</param>
        /// <returns>The indexing policy with excluded path</returns>
        public static IndexingPolicy AddExcludedPath<TEntity>(this IndexingPolicy source, Expression<Func<TEntity, string>> expression)
            where TEntity : CosmosDbEntity, IEntity<string>, IStateEntity
        {
            if (source == null)
            {
                return new IndexingPolicy();
            }

            if (expression == null)
            {
                return source;
            }

            return source.AddExcludedPath<TEntity>($"/{expression.Body.GetPropertyName<TEntity>()}/*");
        }

        /// <summary>
        /// Add excluded path when it is not already added
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source">The indexing policy</param>
        /// <param name="path">The path (Should start with /)</param>
        /// <returns>The indexing policy with excluded path</returns>
        public static IndexingPolicy AddExcludedPath<TEntity>(this IndexingPolicy source, string path)
            where TEntity : CosmosDbEntity, IEntity<string>, IStateEntity
        {
            if (source == null)
            {
                return new IndexingPolicy();
            }

            if (path.IsNullOrEmpty())
            {
                return source;
            }

            if (!source.ExcludedPaths.SafeAny(x => x.Path == path))
            {
                source.ExcludedPaths.Add(new ExcludedPath
                {
                    Path = path
                });
            }

            return source;
        }

        /// <summary>
        /// Add included path when it is not already added
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source">The indexing policy</param>
        /// <param name="expression">Property expression</param>
        /// <returns>The indexing policy with included path</returns>
        public static IndexingPolicy AddIncludedPath<TEntity>(this IndexingPolicy source, Expression<Func<TEntity, string>> expression)
            where TEntity : CosmosDbEntity, IEntity<string>, IStateEntity
        {
            if (source == null)
            {
                return new IndexingPolicy();
            }

            if (expression == null)
            {
                return source;
            }

            return source.AddIncludedPath<TEntity>($"/{expression.Body.GetPropertyName<TEntity>()}/*");
        }

        /// <summary>
        /// Add included path when it is not already added
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source">The indexing policy</param>
        /// <param name="path">The path (Should start with /)</param>
        /// <returns>The indexing policy with included path</returns>
        public static IndexingPolicy AddIncludedPath<TEntity>(this IndexingPolicy source, string path)
            where TEntity : CosmosDbEntity, IEntity<string>, IStateEntity
        {
            if (source == null)
            {
                return new IndexingPolicy();
            }

            if (path.IsNullOrEmpty())
            {
                return source;
            }

            if (!source.IncludedPaths.SafeAny(x => x.Path == path))
            {
                source.IncludedPaths.Add(new IncludedPath
                {
                    Path = path
                });
            }

            return source;
        }

        /// <summary>
        /// Add composite path
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source">The indexing policy</param>
        /// <param name="expression">Property expression</param>
        /// <returns>The indexing policy with included path</returns>
        public static IndexingPolicy AddCompositePath<TEntity>(this IndexingPolicy source, Expression<Func<TEntity, string>> expression)
            where TEntity : CosmosDbEntity, IEntity<string>, IStateEntity
        {
            if (source == null)
            {
                return new IndexingPolicy();
            }

            if (expression == null)
            {
                return source;
            }

            return source.AddIncludedPath<TEntity>($"/{expression.Body.GetPropertyName<TEntity>()}/*");
        }

        /// <summary>
        /// Add composite path with default order Ascending for all paths
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source">The indexing policy</param>
        /// <param name="paths">Path params</param>
        /// <returns>The indexing policy with composite path</returns>
        public static IndexingPolicy AddCompositePath<TEntity>(this IndexingPolicy source, params string[] paths)
            where TEntity : CosmosDbEntity, IEntity<string>, IStateEntity
        {
            if (source == null)
            {
                return new IndexingPolicy();
            }

            if (paths.IsNullOrEmpty())
            {
                return source;
            }

            var compositePaths = paths.Select(path => new CompositePath() { Path = path, Order = CompositePathSortOrder.Ascending });
            if (compositePaths.SafeAny())
            {
                source.CompositeIndexes.Add(new Collection<CompositePath>(compositePaths.ToList()));
            }

            return source;
        }

        /// <summary>
        /// Add composite path
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source">The indexing policy</param>
        /// <param name="paths">Path params</param>
        /// <returns>The indexing policy with composite path</returns>
        public static IndexingPolicy AddCompositePath<TEntity>(this IndexingPolicy source, params (Expression<Func<TEntity, string>> expression, CompositePathSortOrder order)[] paths)
            where TEntity : CosmosDbEntity, IEntity<string>, IStateEntity
        {
            if (source == null)
            {
                return new IndexingPolicy();
            }

            if (paths.IsNullOrEmpty())
            {
                return source;
            }

            return source.AddCompositePath<TEntity>(paths.Select(x => ($"/{x.expression.Body.GetPropertyName<TEntity>()}", x.order)).ToArray());
        }

        /// <summary>
        /// Add composite path
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source">The indexing policy</param>
        /// <param name="paths">Path params</param>
        /// <returns>The indexing policy with composite path</returns>
        public static IndexingPolicy AddCompositePath<TEntity>(this IndexingPolicy source, params (string path, CompositePathSortOrder order)[] paths)
            where TEntity : CosmosDbEntity, IEntity<string>, IStateEntity
        {
            if (source == null)
            {
                return new IndexingPolicy();
            }

            if (paths.IsNullOrEmpty())
            {
                return source;
            }

            var compositePaths = paths.Select(x => new CompositePath() { Path = x.path, Order = x.order });
            if (compositePaths.SafeAny())
            {
                source.CompositeIndexes.Add(new Collection<CompositePath>(compositePaths.ToList()));
            }

            return source;
        }

        /// <summary>
        /// Check if has Spatial Path (Spatial path are used only with Geospatial container configuration)
        /// </summary>
        /// <param name="source"></param>
        /// <returns><<c>true</c> if has spatial path; otherwise, <c>false</c></returns>
        public static bool HasSpatialPath(this IndexingPolicy source)
        {
            return source != null && source.SpatialIndexes.SafeAny(x => x.Path.IsNotNullOrEmpty());
        }

        /// <summary>
        /// Add spatial path
        /// <para>
        /// Default values <br />
        /// <br />
        /// </para>
        /// <code>
        /// "boundingBox": {
        ///     Xmin: 0,
        ///     Ymin: 0,
        ///     Xmax: 10
        ///     Ymax: 10
        /// },
        /// "types": [
        ///     "Point",
        ///     "Polygon",
        ///     "MultiPolygon",
        ///     "LineString"
        /// ]
        /// </code>
        /// <br> <see href="https://docs.microsoft.com/en-us/azure/cosmos-db/sql/sql-query-geospatial-index" /></br>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source">The indexing policy</param>
        /// <param name="path">The path</param>
        /// <returns>The indexing policy with spatial path</returns>
        public static IndexingPolicy AddSpatialPath<TEntity>(this IndexingPolicy source, string path)
            where TEntity : CosmosDbEntity, IEntity<string>, IStateEntity
        {
            if (source == null)
            {
                return new IndexingPolicy();
            }

            if (path.IsNullOrEmpty())
            {
                return source;
            }

            var spatialPath = new SpatialPath()
            {
                Path = path,
                BoundingBox = new BoundingBoxProperties()
                {
                    Xmin = 0,
                    Ymin = 0,
                    Xmax = 10,
                    Ymax = 10
                }
            };

            spatialPath.SpatialTypes.Add(SpatialType.Point);
            spatialPath.SpatialTypes.Add(SpatialType.LineString);
            spatialPath.SpatialTypes.Add(SpatialType.Polygon);
            spatialPath.SpatialTypes.Add(SpatialType.MultiPolygon);

            source.SpatialIndexes.Add(spatialPath);

            return source;
        }

        /// <summary>
        /// Add spatial path
        /// <br> <see href="https://docs.microsoft.com/en-us/azure/cosmos-db/sql/sql-query-geospatial-index" /></br>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source">The indexing policy</param>
        /// <param name="spatialPath">The spatial path</param>
        /// <returns>The indexing policy with spatial path</returns>
        public static IndexingPolicy AddSpatialPath<TEntity>(this IndexingPolicy source, SpatialPath spatialPath)
            where TEntity : CosmosDbEntity, IEntity<string>, IStateEntity
        {
            if (source == null)
            {
                return new IndexingPolicy();
            }

            if (spatialPath == null)
            {
                return source;
            }

            source.SpatialIndexes.Add(spatialPath);

            return source;
        }
    }
}