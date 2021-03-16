using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BookWebDotNet.Domain.Dtos;
using BookWebDotNet.Domain.Entity;
using BookWebDotNet.Domain.Extensions;

namespace BookWebDotNet.Service.Implementations
{
    public class UserServiceImplementation : UserService
    {
        private readonly IList<User>  _repository = new List<User>
        {
            new User
            {
                Email = "email@meail.pl",
                IsAdmin = false, Name = "john",
                Password = "pass", Surname = "Doe",
                UserId = Guid.NewGuid()
            }
            ,
            new User
            {
                Email = "email@meail.pl",
                IsAdmin = false,
                Name = "Adam",
                Password = "pass",
                Surname = "Doe",
                UserId = Guid.NewGuid()

            }
        };
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var dtos = _repository.Select(user => user.AdaptToDto());
            return await Task.FromResult(dtos);
        }

        public async Task<UserDto> GetUserAsync(Guid id)
        {
            var dto = _repository
                .SingleOrDefault(user => user.UserId.Equals(id))
                .AdaptToDto();

            return await Task.FromResult(dto);
        }

        public async Task<UserDto> GetUserAsync(string email)
        {
            var dto = _repository
                .SingleOrDefault(user => user.Email == email)
                .AdaptToDto();

            return await Task.FromResult(dto);
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = createUserDto.ConvertToUser();

            _repository.Add(user);

            return await Task.FromResult(user.AdaptToDto());
        }

        public async Task<UserDto> UpdateUserAsync(UserDto dto)
        {
            int index = -1;

            for (int i = 0; i < _repository.Count; i++)
            {
                if (_repository[i].UserId.Equals(dto.UserId))
                {
                    index = i;
                    break;
                }
            }

            if (index < 0)
            {
                // TODO: throw exception with message "no user with that id"
            }

            var updatedUser = _repository[index] with
            {
                Surname = dto.Surname,
                Email = dto.Email,
                Name = dto.Name,
                IsAdmin = dto.IsAdmin
            };

            _repository[index] = updatedUser;

            return await Task.FromResult(dto);

        }

        public async Task DeleteUserAsync(string email)
        {
            var user = _repository.FirstOrDefault(item => item.Email == email);

            var isDeleted = _repository.Remove(user);

            if (!isDeleted)
            {
                // TODO: throw exception with message "no user with that email"
            }

            await Task.Delay(100);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = _repository.FirstOrDefault(item => item.UserId.Equals(id));

            var isDeleted = _repository.Remove(user);

            if (!isDeleted)
            {
                // TODO: throw exception with message "no user with that email"
            }

            await Task.Delay(100);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            var userFound = _repository.FirstOrDefault(user => user.Email == email);

            return await Task.FromResult(userFound is null);
        }
    }
}
