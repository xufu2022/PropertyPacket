namespace PropertyTenants.Domain.Catalog
{
    public partial class CategoryTemplate: BaseEntity
    {
        public required string Name { get; set; }

        public required string ViewPath { get; set; }

        private int _displayOrder;
        public int DisplayOrder
        {
            get => _displayOrder;
            set => _displayOrder = value;
        }

        public bool IsActive
        {
            get => _displayOrder > 0; // Active if DisplayOrder > 0
            private set { } // Private setter to prevent external modification
        }
    }
}
