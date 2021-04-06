using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookWebDotNet.Domain.DbContext;
using BookWebDotNet.Domain.Dtos;
using BookWebDotNet.Domain.Entity;
using BookWebDotNet.Domain.Exceptions;
using BookWebDotNet.Service.Implementations;
using FluentAssertions;
using MockQueryable.NSubstitute;
using NSubstitute;
using Xunit;

namespace BookWebTests
{
    public class BookServiceTests
    {
        private readonly BookService _sut;
        private readonly BookWebDbContext _bookRepo = Substitute.For<BookWebDbContext>();

        public BookServiceTests()
        {
            _sut = new BookService(_bookRepo);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnBook_WhenExistsWithId()
        {
            //Arrange
            var guid = Guid.NewGuid();
            var data = GenerateBooks(guid);
            var mocks = data.AsQueryable().BuildMockDbSet();

            _bookRepo.Books.Returns(mocks);
            //Act
            var book = await _sut.GetByIdAsync(guid);
            //Assert
            book.Should().NotBeNull();
            book.BookId.Should().Be(guid);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenBookNotExistsWithId()
        {
            //Arrange
            var data = GenerateBooks(null);
            var mocks = data.AsQueryable().BuildMockDbSet();
            _bookRepo.Books.Returns(mocks);

            //Act
            var book = await _sut.GetByIdAsync(Guid.NewGuid());

            //Assert
            book.Should().BeNull();
        }

        [Fact]
        public async Task CreateBookAsync_ShouldReturnBookDto_WhenSuccessfullyCreateBook()
        {
            //Arrange
            var data = GenerateBooks(null);
            var mocks = data.AsQueryable().BuildMockDbSet();
            _bookRepo.Books.Returns(mocks);

            const string author = "Mat Mat";
            const string cover = "www.g.com";
            const string publisher = "Best B";
            const string title = "Super Ultra Flower";
            const int year = 2020;
            var createBookDto = new CreateBookDto
            {
                Author = author,
                Cover = cover,
                Publisher = publisher,
                Title = title,
                Year = year
            };

            //Act
            var book = await _sut.CreateBookAsync(createBookDto);

            //Assert
            book.Should().NotBeNull();
            book.Author.Should().Be(author);
            book.Cover.Should().Be(cover);
            book.Publisher.Should().Be(publisher);
            book.Title.Should().Be(title);
            book.Year.Should().Be(year);
        }

        [Fact]
        public async Task CreateBookAsync_ShouldThrowException_WhenTitleIsDuplicated()
        {
            //Arrange
            var data = GenerateBooks(null);
            var mocks = data.AsQueryable().BuildMockDbSet();
            _bookRepo.Books.Returns(mocks);

            const string author = "Mat Mat";
            const string cover = "www.g.com";
            const string publisher = "Best B";
            const string title = "Super Dragon";
            const int year = 2020;
            var createBookDto = new CreateBookDto
            {
                Author = author,
                Cover = cover,
                Publisher = publisher,
                Title = title,
                Year = year
            };

            //Act
            Func<Task> act = async () =>
            {
                await _sut.CreateBookAsync(createBookDto);
            };

            //Assert
            await act.Should()
                .ThrowAsync<EntityAlreadyExistsException>()
                .WithMessage("Title duplication");
        }

        private static IEnumerable<Book> GenerateBooks(Guid? id)
        {
            return new List<Book>
            {
                new()
                {
                    BookId = id ?? Guid.NewGuid(),
                    Author = "John T.",
                    Cover = "www.img.com",
                    Publisher = "Best Pub",
                    Title = "Super Dragon",
                    Year = 2011
                },
                new()
                {
                    BookId = Guid.NewGuid(),
                    Author = "Anna G.",
                    Cover = "www.img-flower.com",
                    Publisher = "Best Pub",
                    Title = "Nice flower",
                    Year = 1990
                }
            };
        }
    }
}
