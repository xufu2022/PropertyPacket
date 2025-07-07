using Microsoft.EntityFrameworkCore;
using PropertyPacket.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyPacket.Infrastructure.Repositories
{
    public class CategoryRepository
    {
        private readonly PropertyPacketContext _context;

        public CategoryRepository(PropertyPacketContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllDescendantsAsync(int categoryId, bool includeDeleted = false)
        {
            var result = new List<Category>();
            var queue = new Queue<Category>();
            var root = await _context.Categories
                .Include(c => c.Children)
                .FirstOrDefaultAsync(c => c.Id == categoryId && (includeDeleted || !c.Deleted));

            if (root == null) return result;
            result.Add(root);
            queue.Enqueue(root);

            while (queue.Any())
            {
                var current = queue.Dequeue();
                var children = current.Children
                    .Where(c => includeDeleted || !c.Deleted)
                    .ToList();

                foreach (var child in children)
                {
                    result.Add(child);
                    queue.Enqueue(child);
                }
            }

            return result;
        }

        public async Task<List<Category>> GetAllAncestorsAsync(int categoryId, bool includeDeleted = false)
        {
            var result = new List<Category>();
            var current = await _context.Categories
                .Include(c => c.Parent)
                .FirstOrDefaultAsync(c => c.Id == categoryId && (includeDeleted || !c.Deleted));

            while (current?.Parent != null)
            {
                current = current.Parent;
                if (includeDeleted || !current.Deleted)
                {
                    result.Add(current);
                }
            }

            return result.OrderBy(c => c.DisplayOrder).ToList();
        }

        public async Task<List<Category>> GetSubTreeAsync(int categoryId, bool includeDeleted = false)
        {
            return await GetAllDescendantsAsync(categoryId, includeDeleted);
        }

        public async Task<List<Category>> GetRootCategoriesAsync(bool includeDeleted = false)
        {
            return await _context.Categories
                .Where(c => c.ParentCategoryId == null && (includeDeleted || !c.Deleted))
                .OrderBy(c => c.DisplayOrder)
                .ToListAsync();
        }

        public async Task MoveCategoryAsync(int categoryId, int? newParentId)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == categoryId);

            if (category == null)
                throw new Exception("Category not found");

            if (newParentId.HasValue)
            {
                var descendants = await GetAllDescendantsAsync(categoryId, true);
                if (descendants.Any(d => d.Id == newParentId))
                    throw new Exception("Cannot move a category to its own descendant");
            }

            category.ParentCategoryId = newParentId;
            category.UpdatedOnUtc = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        public async Task<int> AddCategoryAsync(Category categoryDto)
        {
            if (string.IsNullOrWhiteSpace(categoryDto.Name))
                throw new ArgumentException("Name is required.");
            if (string.IsNullOrWhiteSpace(categoryDto.Description))
                throw new ArgumentException("Description is required.");

            if (categoryDto.ParentCategoryId.HasValue)
            {
                var parent = await _context.Categories
                    .FirstOrDefaultAsync(c => c.Id == categoryDto.ParentCategoryId && !c.Deleted);
                if (parent == null)
                    throw new ArgumentException("Parent category does not exist or is deleted.");
            }

            var category = new Category
            {
                Name = categoryDto.Name,
                Description = categoryDto.Description,
                ParentCategoryId = categoryDto.ParentCategoryId,
                PictureId = categoryDto.PictureId,
                PageSize = categoryDto.PageSize,
                PageSizeOptions = categoryDto.PageSizeOptions,
                ShowOnHomepage = categoryDto.ShowOnHomepage,
                IncludeInTopMenu = categoryDto.IncludeInTopMenu,
                Published = categoryDto.Published,
                Deleted = false,
                DisplayOrder = categoryDto.DisplayOrder,
                CreatedOnUtc = DateTime.UtcNow,
                UpdatedOnUtc = DateTime.UtcNow,
                PriceFrom = categoryDto.PriceFrom,
                PriceTo = categoryDto.PriceTo,
                CategoryTemplateId = categoryDto.CategoryTemplateId
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return category.Id;
        }
    }
}
