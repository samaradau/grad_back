﻿using System;
using DemoLab.Identity;

namespace DemoLab.Services.Security
{
    internal class UserContext : IUserContext
    {
        private readonly ApplicationUser _applicationUser;

        public UserContext(ApplicationUser applicationUser)
        {
            _applicationUser = applicationUser ?? throw new ArgumentNullException(nameof(applicationUser));
        }

        public string IdentityId => _applicationUser.Id;

        public Guid Id => _applicationUser.DomainId;
    }
}
