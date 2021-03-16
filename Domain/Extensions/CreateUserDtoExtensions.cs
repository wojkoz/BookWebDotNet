using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookWebDotNet.Domain.Dtos;
using BookWebDotNet.Domain.Entity;

namespace BookWebDotNet.Domain.Extensions
{
    public static class CreateUserDtoExtensions
    {
        public static User ConvertToUser(this CreateUserDto dto)
        {
            User user = new User
            {
                UserId = Guid.NewGuid(),
                Email = dto.Email,
                Surname = dto.Surname,
                IsAdmin = dto.IsAdmin,
                Name = dto.Name,
                Password = dto.Password
            };

            return user;
        }
    }
}
