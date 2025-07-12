﻿namespace PropertyPacket.Domain.Directory
{
    public partial class Currency
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string CurrencyCode { get; set; }

        public required string DisplayLocale { get; set; }

        public string? CustomFormatting { get; set; }

        public decimal Rate { get; set; }

        public bool LimitedToStores { get; set; }

        public bool Published { get; set; }

        public int DisplayOrder { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public DateTime UpdatedOnUtc { get; set; }

        public int RoundingTypeId { get; set; }

        //public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
    }
}
