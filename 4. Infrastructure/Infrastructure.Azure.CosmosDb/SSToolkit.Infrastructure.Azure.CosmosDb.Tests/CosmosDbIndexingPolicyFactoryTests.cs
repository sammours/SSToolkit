namespace SSToolkit.Infrastructure.Azure.CosmosDb.Tests
{
    using System.Linq;
    using Microsoft.Azure.Cosmos;
    using Shouldly;
    using Xunit;

    public class CosmosDbIndexingPolicyFactoryTests
    {
        [Fact]
        public void IndexingPolicyPaths_Test()
        {
            // default
            var index = CosmosDbIndexingPolicyFactory.Create(false, IndexingMode.Lazy);
            index.ShouldNotBeNull();
            index.Automatic.ShouldBeFalse();
            index.IndexingMode.ShouldBe(IndexingMode.Lazy);

            index = CosmosDbIndexingPolicyFactory.Create();
            index.ShouldNotBeNull();
            index.Automatic.ShouldBeTrue();
            index.IndexingMode.ShouldBe(IndexingMode.Consistent);

            // Included Paths
            index.AddIncludedPath<IndexingPolicyStub>("/FirstIndex/*");
            index.AddIncludedPath<IndexingPolicyStub>(x => x.SecondIndex);

            index.ShouldNotBeNull();
            index.IncludedPaths.ShouldNotBeNull();
            index.IncludedPaths.Count().ShouldBe(2);
            index.IncludedPaths.First().Path.ShouldBe("/FirstIndex/*");
            index.IncludedPaths.Last().Path.ShouldBe("/SecondIndex/*");


            // Excluded Paths
            index.AddExcludedPath<IndexingPolicyStub>("/FirstIndex/*");
            index.AddExcludedPath<IndexingPolicyStub>(x => x.SecondIndex);

            index.ShouldNotBeNull();
            index.ExcludedPaths.ShouldNotBeNull();
            index.ExcludedPaths.Count().ShouldBe(2);
            index.ExcludedPaths.First().Path.ShouldBe("/FirstIndex/*");
            index.ExcludedPaths.Last().Path.ShouldBe("/SecondIndex/*");

            // Composite Paths
            index.AddCompositePath<IndexingPolicyStub>("/FirstIndex", "/SecondIndex");
            index.ShouldNotBeNull();
            index.CompositeIndexes.ShouldNotBeNull();
            index.CompositeIndexes.Count().ShouldBe(1);
            index.CompositeIndexes.First().Count().ShouldBe(2);
            index.CompositeIndexes.First().First().Path.ShouldBe("/FirstIndex");
            index.CompositeIndexes.First().First().Order.ShouldBe(CompositePathSortOrder.Ascending);
            index.CompositeIndexes.First().Last().Path.ShouldBe("/SecondIndex");
            index.CompositeIndexes.First().Last().Order.ShouldBe(CompositePathSortOrder.Ascending);

            index = CosmosDbIndexingPolicyFactory.Create();
            index.AddCompositePath<IndexingPolicyStub>(("/SecondIndex", CompositePathSortOrder.Descending), ("/FirstIndex", CompositePathSortOrder.Descending));
            index.ShouldNotBeNull();
            index.CompositeIndexes.ShouldNotBeNull();
            index.CompositeIndexes.Count().ShouldBe(1);
            index.CompositeIndexes.First().Count().ShouldBe(2);
            index.CompositeIndexes.First().First().Path.ShouldBe("/SecondIndex");
            index.CompositeIndexes.First().First().Order.ShouldBe(CompositePathSortOrder.Descending);
            index.CompositeIndexes.First().Last().Path.ShouldBe("/FirstIndex");
            index.CompositeIndexes.First().Last().Order.ShouldBe(CompositePathSortOrder.Descending);

            index = CosmosDbIndexingPolicyFactory.Create();
            index.AddCompositePath<IndexingPolicyStub>((x => x.FirstIndex, CompositePathSortOrder.Descending), (x => x.SecondIndex, CompositePathSortOrder.Descending));
            index.ShouldNotBeNull();
            index.CompositeIndexes.ShouldNotBeNull();
            index.CompositeIndexes.Count().ShouldBe(1);
            index.CompositeIndexes.First().Count().ShouldBe(2);
            index.CompositeIndexes.First().First().Path.ShouldBe("/FirstIndex");
            index.CompositeIndexes.First().First().Order.ShouldBe(CompositePathSortOrder.Descending);
            index.CompositeIndexes.First().Last().Path.ShouldBe("/SecondIndex");
            index.CompositeIndexes.First().Last().Order.ShouldBe(CompositePathSortOrder.Descending);

            // Spatial Path
            index.AddSpatialPath<IndexingPolicyStub>("/FirstIndex/*");
            var spatialPath = new SpatialPath()
            {
                Path = "/SecondIndex/*",
                BoundingBox = new BoundingBoxProperties { Xmax = 20, Xmin = 10, Ymax = 20, Ymin = 10 },
            };
            spatialPath.SpatialTypes.Add(SpatialType.Point);
            spatialPath.SpatialTypes.Add(SpatialType.LineString);
            index.AddSpatialPath<IndexingPolicyStub>(spatialPath);

            index.SpatialIndexes.ShouldNotBeNull();
            index.SpatialIndexes.Count().ShouldBe(2);
            index.SpatialIndexes.First().BoundingBox.ShouldNotBeNull();
            index.SpatialIndexes.First().BoundingBox.Xmax.ShouldBe(10);
            index.SpatialIndexes.First().BoundingBox.Xmin.ShouldBe(0);
            index.SpatialIndexes.First().BoundingBox.Ymax.ShouldBe(10);
            index.SpatialIndexes.First().BoundingBox.Ymin.ShouldBe(0);
            index.SpatialIndexes.First().Path.ShouldBe("/FirstIndex/*");
            index.SpatialIndexes.First().SpatialTypes.ShouldContain(SpatialType.Point);
            index.SpatialIndexes.First().SpatialTypes.ShouldContain(SpatialType.MultiPolygon);
            index.SpatialIndexes.First().SpatialTypes.ShouldContain(SpatialType.Polygon);
            index.SpatialIndexes.First().SpatialTypes.ShouldContain(SpatialType.LineString);

            index.SpatialIndexes.Last().BoundingBox.ShouldNotBeNull();
            index.SpatialIndexes.Last().BoundingBox.Xmax.ShouldBe(20);
            index.SpatialIndexes.Last().BoundingBox.Xmin.ShouldBe(10);
            index.SpatialIndexes.Last().BoundingBox.Ymax.ShouldBe(20);
            index.SpatialIndexes.Last().BoundingBox.Ymin.ShouldBe(10);
            index.SpatialIndexes.Last().Path.ShouldBe("/SecondIndex/*");
            index.SpatialIndexes.Last().SpatialTypes.ShouldContain(SpatialType.Point);
            index.SpatialIndexes.Last().SpatialTypes.ShouldContain(SpatialType.LineString);
            index.SpatialIndexes.Last().SpatialTypes.ShouldNotContain(SpatialType.MultiPolygon);
            index.SpatialIndexes.Last().SpatialTypes.ShouldNotContain(SpatialType.Polygon);

            index.HasSpatialPath().ShouldBeTrue();
            index = CosmosDbIndexingPolicyFactory.Create();
            index.HasSpatialPath().ShouldBeFalse();
        }
    }

#pragma warning disable SA1402 // File may only contain a single type
    public class IndexingPolicyStub : CosmosDbEntity
#pragma warning restore SA1402 // File may only contain a single type
    {
        public string FirstIndex { get; set; } = string.Empty;

        public string SecondIndex { get; set; } = string.Empty;
    }
}