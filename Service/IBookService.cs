
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
        /// <summary>Throws EntityNotFoundException when can't find book</summary> 
        public Task DeleteByIdAsync(Guid id);
        /// <summary>Throws EntityAlreadyExistsException when finds title in db</summary> 
        public Task<BookDto> CreateBookAsync(CreateBookDto dto);
        public Task<IEnumerable<BookDto>> FindByNameAsync(string str);
    }
}
