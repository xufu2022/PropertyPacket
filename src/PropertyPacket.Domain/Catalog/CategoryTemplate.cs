using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyPacket.Domain.Catalog
{
    public partial class CategoryTemplate: BaseEntity
    {
        public required string Name { get; set; }

        public required string ViewPath { get; set; }

        public int DisplayOrder { get; set; }
    }
}
