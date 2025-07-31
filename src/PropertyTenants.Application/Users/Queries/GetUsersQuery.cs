﻿using PropertyTenants.Application.Queries;
using PropertyTenants.Domain.Entities.Clients;
using PropertyTenants.Domain.Repositories;

namespace PropertyTenants.Application.Users.Queries
{
    public class GetUsersQuery : IQuery<List<User>>
    {
        public bool IncludeClaims { get; set; }
        public bool IncludeUserRoles { get; set; }
        public bool IncludeRoles { get; set; }
        public bool AsNoTracking { get; set; }
    }

    internal class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, List<User>>
    {
        private readonly IUserRepository _userRepository;

        public GetUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> HandleAsync(GetUsersQuery query, CancellationToken cancellationToken = default)
        {
            var db = _userRepository.Get(new UserQueryOptions
            {
                IncludeClaims = query.IncludeClaims,
                IncludeUserRoles = query.IncludeUserRoles,
                IncludeRoles = query.IncludeRoles,
                AsNoTracking = query.AsNoTracking,
            });

            return await _userRepository.ToListAsync(db);
        }
    }
}
