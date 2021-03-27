using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookWebDotNet.Domain.DbContext;
using BookWebDotNet.Domain.Dtos;
using BookWebDotNet.Domain.Entity;
using BookWebDotNet.Domain.Exceptions;
using BookWebDotNet.Domain.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BookWebDotNet.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly BookWebDbContext  _repository ;

        public UserService(BookWebDbContext repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var dtos = _repository
                .Users
                .AsNoTracking()
                .Select(user => user.AdaptToDto());
            return await Task.FromResult(dtos);
        }
        
        public async Task<UserDto> GetUserAsync(Guid id)
        {
            var dto = _repository
                .Users
                .AsNoTracking()
                .FirstOrDefault(user => user.UserId.Equals(id));

            if (dto is null)
            {
                throw new EntityNotFoundException($"Couldn\'t find user with id = {id}");
            }

            return await Task.FromResult(dto.AdaptToDto());
        }

        public async Task<UserDto> GetUserAsync(string email)
        {
            var dto = _repository
                .Users
                .AsNoTracking()
                .SingleOrDefault(user => user.Email == email)
                ?.AdaptToDto();

            if (dto is null)
            {
                throw new EntityNotFoundException($"Couldn\'t find user with email = {email}");
            }

            return await Task.FromResult(dto);
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
        {
            var emailExists = await EmailExistsAsync(createUserDto.Email);

            if (emailExists)
            {
                throw new EntityAlreadyExistsException("Email duplication");
            }

            var user = createUserDto.ConvertToUser();

            _repository.Add(user);

            await _repository.SaveChangesAsync();

            return await Task.FromResult(user.AdaptToDto());
        }

        public async Task<UserDto> UpdateUserAsync(UserDto dto)
        {
            var user = _repository.Users.AsNoTracking().FirstOrDefault(u => u.UserId.Equals(dto.UserId));

            if (user is null)
            {
                throw new EntityNotFoundException($"Couldn\'t find user with id = {dto.UserId}");
            }

            //_repository.Entry(user).CurrentValues.SetValues(dto.ToUser(user.Password));
            _repository.Update(dto.ToUser(user.Password));

            await _repository.SaveChangesAsync();

            return await Task.FromResult(dto);

        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = _repository.Users.AsNoTracking().FirstOrDefault(item => item.UserId.Equals(id));

            if (user is null)
            {
                throw new EntityNotFoundException($"Couldn\'t find user with id = {id}");
            }

            _repository.Remove(user);
            await _repository.SaveChangesAsync();
        }

        private async Task<bool> EmailExistsAsync(string email)
        {
            var userFound = _repository.Users.FirstOrDefault(user => user.Email == email);

            return await Task.FromResult(!(userFound is null));
        }
    }
}
