
using BookWebDotNet.Domain.Entity;

namespace BookWebDotNet.Domain.Extensions
{
    public static class UserDtoExtensions
    {
        public static User ToUser(this UserDto dto, string pass)
        {
            return new User
            {
                Email = dto.Email,
                IsAdmin = dto.IsAdmin,
                Name = dto.Name,
                Password = pass,
                Surname = dto.Surname,
                UserId = dto.UserId
            };
        }
    }
}
