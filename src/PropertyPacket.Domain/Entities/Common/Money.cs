﻿namespace PropertyTenants.Domain.Entities.Common
{
    public record Money
    {
        public decimal Amount { get; }

        public string Currency { get; }

        private Money()
        {
        }

        public Money(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }
    }
}
