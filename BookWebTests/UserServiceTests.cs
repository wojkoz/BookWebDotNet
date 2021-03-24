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
using BookWebDotNet.Domain.Exceptions;
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
        public async Task GetUserAsync_ShouldReturnUser_WhenUserExistsWithId()
        {
            //Arrange
            var guid = Guid.NewGuid();
            var mockSet = GenerateUserMockSet(guid);

            _userRepo.Users.Returns(mockSet);

            //Act
            var userDto = await _sut.GetUserAsync(guid);
            //Assert
            userDto.Should().NotBeNull();
            userDto.UserId.Should().Be(guid);
        }

        [Fact]
        public async Task GetUserAsync_ShouldThrowException_WhenUserNotExistsWithId()
        {
            //Arrange
            var guid = Guid.NewGuid();
            var mockSet = GenerateUserMockSet(null);
        
            _userRepo.Users.Returns(mockSet);
        
            //Act
            Func<Task> act = async () =>
            {
                await _sut.GetUserAsync(guid);
            };
            //Assert

            await act
                .Should()
                .ThrowAsync<EntityNotFoundException>()
                .WithMessage($"Couldn\'t find user with id = {guid}");
        }


        [Fact]
        public async Task GetUserAsync_ShouldReturnUser_WhenUserExistsWithEmail()
        {
            //Arrange
            const string email = "test@test.pl";
            var mockSet = GenerateUserMockSet(Guid.NewGuid());

            _userRepo.Users.Returns(mockSet);

            //Act
            var userDto = await _sut.GetUserAsync(email);
            //Assert
            userDto.Should().NotBeNull();
            userDto.Email.Should().Be(email);
        }

        [Fact]
        public async Task GetUserAsync_ShouldThrowException_WhenUserNotExistsWithEmail()
        {
            //Arrange
            const string email = "tes@ttttt.op";
            var mockSet = GenerateUserMockSet(null);

            _userRepo.Users.Returns(mockSet);

            //Act
            Func<Task> act = async () =>
            {
                await _sut.GetUserAsync(email);
            };
            //Assert

            await act
                .Should()
                .ThrowAsync<EntityNotFoundException>()
                .WithMessage($"Couldn\'t find user with email = {email}");
        }

        private static DbSet<User> GenerateUserMockSet(Guid? guid)
        {
            var data = GenerateUsers(guid).AsQueryable();
            var mockSet = Substitute.For<DbSet<User>, IQueryable<User>>();

            ((IQueryable<User>)mockSet).Provider.Returns(data.Provider);
            ((IQueryable<User>)mockSet).Expression.Returns(data.Expression);
            ((IQueryable<User>)mockSet).ElementType.Returns(data.ElementType);
            ((IQueryable<User>)mockSet).GetEnumerator().Returns(data.GetEnumerator());
            return mockSet;
        }


        public static IEnumerable<User> GenerateUsers(Guid? id)
        {
            return new List<User>
            {
                new()
                {
                    Email = "test@test.pl",
                    IsAdmin = false,
                    Name = "Joe",
                    Password = "empty",
                    Surname = "Doe",
                    UserId = id ?? Guid.NewGuid()
                },
                new ()
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
