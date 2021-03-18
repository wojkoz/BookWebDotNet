using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookWebDotNet.Domain.Dtos;
using BookWebDotNet.Domain.Entity;

namespace BookWebDotNet.Service
{
    interface IUserService
    {
        public Task<IEnumerable<UserDto>> GetAllUsersAsync();
        public Task<UserDto> GetUserAsync(Guid id);
        public Task<UserDto> GetUserAsync(string email);
        public Task<UserDto> CreateUserAsync(CreateUserDto createUserDto);
        public Task<UserDto> UpdateUserAsync(UserDto dto);
        public Task DeleteUserAsync(string email);
        public Task DeleteUserAsync(Guid id);
        public Task<bool> EmailExistsAsync(string email);

    }
}
