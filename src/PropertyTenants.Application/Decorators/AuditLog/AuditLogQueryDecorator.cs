﻿using System.Text.Json;
using PropertyTenants.Application.Common.Queries;

namespace PropertyTenants.Application.Decorators.AuditLog
{
    [Mapping(Type = typeof(AuditLogAttribute))]
    public class AuditLogQueryDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        private readonly IQueryHandler<TQuery, TResult> _handler;

        public AuditLogQueryDecorator(IQueryHandler<TQuery, TResult> handler)
        {
            _handler = handler;
        }

        public Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken = default)
        {
            string queryJson = JsonSerializer.Serialize(query);
            Console.WriteLine($"Query of type {query.GetType().Name}: {queryJson}");
            return _handler.HandleAsync(query, cancellationToken);
        }
    }
}