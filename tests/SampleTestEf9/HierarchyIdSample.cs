using Bogus;
using Microsoft.EntityFrameworkCore;

namespace SampleTestEf9
{
    public static class HierarchyIdSample
    {
        public static async Task Seed(PropertyPacketContext context)
        {
            var faker = new Faker<CategoryHierarchy>()
                .RuleFor(c => c.Name, f => f.Commerce.Categories(1)[0])
                .RuleFor(c => c.Description, f => f.Lorem.Sentence())
                // .RuleFor(c => c.Path, ...) // Don't set Path here, set it manually below
                .RuleFor(c => c.PictureId, f => f.Random.Int(1, 100))
                .RuleFor(c => c.PageSize, f => f.PickRandom(new[] { 10, 20, 30 }))
                .RuleFor(c => c.PageSizeOptions, "10,20,30")
                .RuleFor(c => c.ShowOnHomepage, f => f.Random.Bool())
                .RuleFor(c => c.IncludeInTopMenu, f => f.Random.Bool())
                .RuleFor(c => c.Published, f => f.Random.Bool())
                .RuleFor(c => c.Deleted, false)
                .RuleFor(c => c.DisplayOrder, f => f.Random.Int(0, 100))
                .RuleFor(c => c.CreatedOnUtc, f => f.Date.PastOffset(1).UtcDateTime)
                .RuleFor(c => c.UpdatedOnUtc, f => f.Date.RecentOffset(1).UtcDateTime)
                .RuleFor(c => c.PriceFrom, f => f.Random.Decimal(0, 50))
                .RuleFor(c => c.PriceTo, (f, c) => c.PriceFrom + f.Random.Decimal(1, 100))
                .RuleFor(c => c.CategoryTemplateId, f => f.Random.Int(1, 5));

            var list=new List<CategoryHierarchy>
            {
            };

            var pathList = new List<HierarchyId>
            {
                HierarchyId.Parse("/"),
                HierarchyId.Parse("/1/"),
                HierarchyId.Parse("/2/"),
                HierarchyId.Parse("/3/"),
                HierarchyId.Parse("/4/"),
                HierarchyId.Parse("/5/"),
                HierarchyId.Parse("/1/1/"),
                HierarchyId.Parse("/1/2/"),
                HierarchyId.Parse("/1/3/"),
                HierarchyId.Parse("/1/4/"),
                HierarchyId.Parse("/1/5/"),
                HierarchyId.Parse("/3/1/"),
                HierarchyId.Parse("/3/2/"),
                HierarchyId.Parse("/4/1/"),
                HierarchyId.Parse("/1/1/1/"),
                HierarchyId.Parse("/1/3/1/"),
                HierarchyId.Parse("/1/5/1/"),
                HierarchyId.Parse("/3/2/1/"),
                HierarchyId.Parse("/3/2/2/"),
                HierarchyId.Parse("/4/1/1/"),
                HierarchyId.Parse("/4/1/2/"),
                HierarchyId.Parse("/4/1/3/"),
                HierarchyId.Parse("/1/3/1/1/"),
                HierarchyId.Parse("/1/5/1/1/"),
                HierarchyId.Parse("/3/2/1/1/"),
                HierarchyId.Parse("/3/2/1/2/"),
                HierarchyId.Parse("/3/2/1/3/"),
                HierarchyId.Parse("/4/1/2/1/"),
                HierarchyId.Parse("/4/1/3/1/"),
                HierarchyId.Parse("/3/2/1/1/1/")
            };

            pathList.ForEach(path =>
            {
                var category = faker.Generate();
                category.Path = path; // Set Path from your list
                list.Add(category);
            });

            await context.AddRangeAsync(list);
        }

        public async static Task<CategoryHierarchy?> FindDirectAncestor(PropertyPacketContext context, string name)
             => await context.CategoryHierarchies
                 .SingleOrDefaultAsync(
                     ancestor => ancestor.Path == context.CategoryHierarchies
                         .Single(descendent => descendent.Name == name).Path
                         .GetAncestor(1));

        #region FindAllAncestors
        public static IQueryable<CategoryHierarchy> FindAllAncestors(PropertyPacketContext context, string name)
            => context.CategoryHierarchies.Where(
                    ancestor => context.CategoryHierarchies
                        .Single(
                            descendent =>
                                descendent.Name == name
                                && ancestor.Id != descendent.Id)
                        .Path.IsDescendantOf(ancestor.Path))
                .OrderByDescending(ancestor => ancestor.Path.GetLevel());
        #endregion

        #region FindDirectDescendents
        public static IQueryable<CategoryHierarchy> FindDirectDescendents(PropertyPacketContext context, string name)
            => context.CategoryHierarchies.Where(
                descendent => descendent.Path.GetAncestor(1) == context.CategoryHierarchies
                    .Single(ancestor => ancestor.Name == name).Path);
        #endregion

        #region FindAllDescendents
        public static IQueryable<CategoryHierarchy> FindAllDescendents(PropertyPacketContext context, string name)
            => context.CategoryHierarchies.Where(
                    descendent => descendent.Path.IsDescendantOf(
                        context.CategoryHierarchies
                            .Single(
                                ancestor =>
                                    ancestor.Name == name
                                    && descendent.Id != ancestor.Id)
                            .Path))
                .OrderBy(descendent => descendent.Path.GetLevel());
        #endregion

        #region LongoAndDescendents
        //var longoAndDescendents = await context.Halflings.Where(
        //        descendent => descendent.PathFromPatriarch.IsDescendantOf(
        //            context.Halflings.Single(ancestor => ancestor.Name == "Longo").PathFromPatriarch))
        //    .ToListAsync();
        #endregion

        #region GetReparentedValue  -- moving this to a new location
        //foreach (var descendent in longoAndDescendents)
        //{
        //    descendent.PathFromPatriarch
        //        = descendent.PathFromPatriarch.GetReparentedValue(
        //            mungo.PathFromPatriarch, ponto.PathFromPatriarch)!;
        //}
        //await context.SaveChangesAsync();
        #endregion

        #region FindCommonAncestor
        public static async Task<CategoryHierarchy?> FindCommonAncestor(PropertyPacketContext context, CategoryHierarchy first, CategoryHierarchy second)
            => await context.CategoryHierarchies
                .Where(
                    ancestor => first.Path.IsDescendantOf(ancestor.Path)
                                && second.Path.IsDescendantOf(ancestor.Path))
                .OrderByDescending(ancestor => ancestor.Path.GetLevel())
                .FirstOrDefaultAsync();
        #endregion

        #region AddDescendent
        public static async Task AddDescendent(PropertyPacketContext context, CategoryHierarchy ancestor, CategoryHierarchy descendent)
        {
            descendent.Path = ancestor.Path.GetReparentedValue(descendent.Path, ancestor.Path);
            await context.CategoryHierarchies.AddAsync(descendent);
            await context.SaveChangesAsync();
        }
        #endregion

        #region generate Path value for new CategoryHierarchy Path value
        public static HierarchyId GeneratePathValue(PropertyPacketContext context, string name)
        {
            var parent = context.CategoryHierarchies
                .SingleOrDefault(c => c.Name == name);
            if (parent == null)
            {
                return HierarchyId.Parse("/");
            }
            return parent.Path.GetReparentedValue(HierarchyId.Parse("/"), parent.Path);
        }
        #endregion

        #region create a new CategoryHierarchy 
        public static async Task AddNewCategoryNodDbe(PropertyPacketContext context, string parentPath, string newCategoryName)
        {
            //plugging into the existing hierarchy.

            // Find the parent category
            var parent = await context.CategoryHierarchies.SingleOrDefaultAsync(c => c.Path.ToString() == parentPath);

            if (parent == null)
            {
                throw new InvalidOperationException($"Parent category with path {parentPath} not found.");
            }

            // Find the next available child index
            var siblings = await context.CategoryHierarchies
                .Where(c => c.Path.GetAncestor(1) == parent.Path)
                .Select(c => c.Path)
                .ToListAsync();

            int nextChildIndex = 1;
            if (siblings.Any())
            {
                // Extract the last segment of each sibling's path and find the maximum
                var maxIndex = siblings
                    .Select(s => int.Parse(s.ToString().TrimEnd('/').Split('/').Last()))
                    .Max();
                nextChildIndex = maxIndex + 1;
            }

            // Create the new category
            var newCategory = new CategoryHierarchy
            {
                Name = newCategoryName,
                Description = $"Description for {newCategoryName}",
                Path = HierarchyId.Parse($"{parentPath}{nextChildIndex}/"),
                ShowOnHomepage = false,
                IncludeInTopMenu = true,
                Published = true,
                Deleted = false,
                DisplayOrder = nextChildIndex,
                CreatedOnUtc = DateTime.UtcNow,
                UpdatedOnUtc = DateTime.UtcNow,
                PriceFrom = 50.00m,
                PriceTo = 150.00m,
                PageSize = 20,
                PageSizeOptions = "10,20,30",
                CategoryTemplateId = 1,
                PictureId = 100
            };

            await context.CategoryHierarchies.AddAsync(newCategory);
            await context.SaveChangesAsync();

            Console.WriteLine($"Added new category: {newCategory.Name} ({newCategory.Path})");
            }
        #endregion

    }
}

