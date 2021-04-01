
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookWebDotNet.Domain.Dtos;
using BookWebDotNet.Domain.Entity;

namespace BookWebDotNet.Service
{
    public interface IBookService
    {
        public Task<IEnumerable<BookDto>> GetAllBooksAsync();
        public Task<BookDto> GetByIdAsync(Guid id);
        public Task DeleteByIdAsync(Guid id);
        public Task<BookDto> CreateBookAsync(CreateBookDto dto);
        public Task<IEnumerable<BookDto>> FindByNameAsync(string str);
    }
}
