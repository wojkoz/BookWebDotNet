using BookWebDotNet.Domain.DbContext;
using BookWebDotNet.Domain.Entity;
using BookWebDotNet.Service.Implementations;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookWebDotNet.Domain.Exceptions;
using Xunit;
using Xunit.Abstractions;

namespace BookWebTests
{
    public class UserServiceTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly UserService _sut;
        private readonly BookWebDbContext _userRepo = Substitute.For<BookWebDbContext>();

        public UserServiceTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _sut = new UserService(_userRepo);
        }

        [Fact]
        public async Task GetUserAsync_ShouldReturnUser_WhenUserExistsWithId()
        {
            //Arrange
            var guid = Guid.NewGuid();
            var data = GenerateUsers(guid);
            var mockSet = DbSetMock.GenerateMockSet(data);

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
            var data = GenerateUsers(null);
            var mockSet = DbSetMock.GenerateMockSet(data);

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
            var data = GenerateUsers(Guid.NewGuid());
            var mockSet = DbSetMock.GenerateMockSet(data);

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
            var data = GenerateUsers(null);
            var mockSet = DbSetMock.GenerateMockSet(data);

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

        [Fact]
        public async Task UpdateUserAsync_ShouldUpdateUser_WhenExists()
        {
            //Arrange
            var guid = Guid.NewGuid();
            var data = GenerateUsers(guid);

            var mockSet = DbSetMock.GenerateMockSet(data);

            const string email = "update@email.com";
            const string name = "TestName";
            const string surname = "TestSurname";
            UserDto dto = new()
            {
                UserId = guid,
                Email = email,
                IsAdmin = false,
                Name = name,
                Surname = surname
            };

            _userRepo.Users.Returns(mockSet);
            

            //Act
            var updatedUser = await _sut.UpdateUserAsync(dto);
            
            //Assert
            updatedUser.Should().NotBeNull();
            updatedUser.UserId.Should().Be(guid);
            updatedUser.Email.Should().Be(email);
            updatedUser.Name.Should().Be(name);
            updatedUser.Surname.Should().Be(surname);
        }

        [Fact]
        public async Task UpdateUserAsync_ShouldThrowException_WhenUserNotExists()
        {
            //Arrange
            var guid = Guid.NewGuid();
            var data = GenerateUsers(guid);

            var mockSet = DbSetMock.GenerateMockSet(data);

            const string email = "update@email.com";
            const string name = "TestName";
            const string surname = "TestSurname";
            UserDto dto = new()
            {
                UserId = Guid.NewGuid(),
                Email = email,
                IsAdmin = false,
                Name = name,
                Surname = surname
            };

            _userRepo.Users.Returns(mockSet);


            //Act
            Func<Task> act = async () =>
            {
                await _sut.UpdateUserAsync(dto);
            };

            //Assert
            await act
                .Should()
                .ThrowAsync<EntityNotFoundException>()
                .WithMessage($"Couldn\'t find user with id = {dto.UserId}");
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
