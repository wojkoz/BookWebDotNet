using BookWebDotNet.Domain.DbContext;
using BookWebDotNet.Domain.Entity;
using BookWebDotNet.Service.Implementations;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BookWebTests
{
    public class UserServiceTests
    {
        private readonly UserService _sut;
        private readonly BookWebDbContext _userRepo = Substitute.For<BookWebDbContext>();

        public UserServiceTests()
        {
            _sut = new UserService(_userRepo);
        }

        [Fact]
        public async Task GetUserAsync_ShouldReturnUser_WhenUserExists()
        {
            //Arrange
            var guid = Guid.NewGuid();
            var data = GenerateUsers(guid).AsQueryable();
            var mockSet = Substitute.For<DbSet<User>, IQueryable<User>>();

            ((IQueryable<User>)mockSet).Provider.Returns(data.Provider);
            ((IQueryable<User>)mockSet).Expression.Returns(data.Expression);
            ((IQueryable<User>)mockSet).ElementType.Returns(data.ElementType);
            ((IQueryable<User>)mockSet).GetEnumerator().Returns(data.GetEnumerator());

            _userRepo.Users.Returns(mockSet);

            //Act
            var userDto = await _sut.GetUserAsync(guid);
            //Assert
            userDto.Should().NotBeNull();
            userDto.UserId.Should().Be(guid);
        }

        public static IEnumerable<User> GenerateUsers(Guid? id)
        {
            return new List<User>
            {
                new User
                {
                    Email = "test@test.pl",
                    IsAdmin = false,
                    Name = "Joe",
                    Password = "empty",
                    Surname = "Doe",
                    UserId = id ?? Guid.NewGuid()
                },
                new User
                {
                    Email = "test2@tes2t.pl",
                    IsAdmin = false,
                    Name = "Joe2",
                    Password = "empty2",
                    Surname = "Doe2",
                    UserId = Guid.NewGuid()
                }
            };
        }
    }
}
