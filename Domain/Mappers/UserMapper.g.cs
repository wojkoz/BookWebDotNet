using BookWebDotNet.Domain.Entity;

namespace BookWebDotNet.Domain.Entity
{
    public static partial class UserMapper
    {
        public static UserDto AdaptToDto(this User p1)
        {
            return p1 == null ? null : new UserDto()
            {
                UserId = p1.UserId,
                Name = p1.Name,
                Surname = p1.Surname,
                Email = p1.Email,
                IsAdmin = p1.IsAdmin
            };
        }
        public static UserDto AdaptTo(this User p2, UserDto p3)
        {
            if (p2 == null)
            {
                return null;
            }
            UserDto result = p3 ?? new UserDto();
            
            result.UserId = p2.UserId;
            result.Name = p2.Name;
            result.Surname = p2.Surname;
            result.Email = p2.Email;
            result.IsAdmin = p2.IsAdmin;
            return result;
            
        }
    }
}