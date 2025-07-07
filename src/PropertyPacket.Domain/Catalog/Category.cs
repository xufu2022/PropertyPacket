using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyPacket.Domain.Catalog
{
    public partial class Category : BaseEntity
    {
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public required string Description { get; set; }

        public int? ParentCategoryId { get; set; }

        public Category? Parent { get; set; }
        public IList<Category> Children { get; set; } = new List<Category>();
        public int PictureId { get; set; }

        public int PageSize { get; set; }

        public string PageSizeOptions { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show the category on home page
        /// </summary>
        public bool ShowOnHomepage { get; set; }

        public bool IncludeInTopMenu { get; set; }

        public bool Published { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity has been deleted
        /// </summary>
        public bool Deleted { get; set; }

        public int DisplayOrder { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance update
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }

        public decimal PriceFrom { get; set; }

        /// <summary>
        /// Gets or sets the "to" price
        /// </summary>
        public decimal PriceTo { get; set; }

        public int CategoryTemplateId { get; set; }
    }
}
