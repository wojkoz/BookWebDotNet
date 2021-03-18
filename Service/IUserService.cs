using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookWebDotNet.Domain.Dtos;
using BookWebDotNet.Domain.Entity;
using BookWebDotNet.Domain.Exceptions;

namespace BookWebDotNet.Service
{
    public interface IUserService
    {
        public Task<IEnumerable<UserDto>> GetAllUsersAsync();
        /// <summary>Throws EntityNotFoundException when can't find user</summary> 
        public Task<UserDto> GetUserAsync(Guid id);
        /// <summary>Throws EntityNotFoundException when can't find user</summary> 
        public Task<UserDto> GetUserAsync(string email);
        /// <summary>Throws EntityAlreadyExistsException when finds email in db</summary> 
        public Task<UserDto> CreateUserAsync(CreateUserDto createUserDto);
        /// <summary>Throws EntityNotFoundException when can't find user</summary> 
        public Task<UserDto> UpdateUserAsync(UserDto dto);
        /// <summary>Throws EntityNotFoundException when can't find user</summary> 
        public Task DeleteUserAsync(Guid id);

    }
}
