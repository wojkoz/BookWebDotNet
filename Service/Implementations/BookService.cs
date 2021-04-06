using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookWebDotNet.Domain.DbContext;
using BookWebDotNet.Domain.Dtos;
using BookWebDotNet.Domain.Entity;
using BookWebDotNet.Domain.Exceptions;
using BookWebDotNet.Domain.Extensions;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BookWebDotNet.Service.Implementations
{
    public class BookService : IBookService
    {
        private readonly BookWebDbContext _repository;

        public BookService(BookWebDbContext repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var dtoBooks = _repository.Books
                .AsNoTracking()
                .Adapt<IEnumerable<BookDto>>();
           
            return Task.FromResult(dtoBooks);
        }

        public async Task<BookDto> GetByIdAsync(Guid id)
        {
            var book = await _repository.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.BookId.Equals(id));

            return book?.AdaptToDto();
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var book = await _repository.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.BookId.Equals(id));
            
            if (book is null)
            {
                throw new EntityNotFoundException($"Couldn\'t find book with id = {id}");
            }

            _repository.Books.Remove(book);
            await _repository.SaveChangesAsync();
        }

        public async Task<BookDto> CreateBookAsync(CreateBookDto createBookDto)
        {
            var book = await _repository.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Title.Equals(createBookDto.Title));
            if (!(book is null))
            {
                throw new EntityAlreadyExistsException("Title duplication");
            }

            var newBook = createBookDto.ToBook();
            await _repository.Books.AddAsync(newBook);

            await _repository.SaveChangesAsync();
            return newBook.AdaptToDto();
        }

        public Task<IEnumerable<BookDto>> FindByNameAsync(string str)
        {
            if (str.Equals(""))
            {
                return Task.FromResult(Enumerable.Empty<BookDto>());
            }

            var books = _repository.Books
                .FromSqlRaw(
                    "SELECT Title, Author, Publisher, Year, Cover, BookId FROM Books m WHERE LOWER(m.title) LIKE TRIM(LOWER(CONCAT('%',TRIM({0}),'%')))",
                    str)
                .AsNoTracking()
                .AsEnumerable()
                .Select(book => book.AdaptToDto());

            return Task.FromResult(books);
        }
    }
}
