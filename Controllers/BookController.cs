using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookWebDotNet.Domain.Dtos;
using BookWebDotNet.Domain.Entity;
using BookWebDotNet.Domain.Exceptions;
using BookWebDotNet.Service;
using Microsoft.AspNetCore.Mvc;

namespace BookWebDotNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _service;

        public BookController(IBookService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetAllBooksAsync()
        {
            var books = await _service.GetAllBooksAsync();

            return Ok(books);
        }

        [HttpPost]
        public async Task<ActionResult<BookDto>> CreateBookAsync([FromBody] CreateBookDto dto)
        {
            try
            {
                var book = await _service.CreateBookAsync(dto);

                return CreatedAtAction(nameof(GetAllBooksAsync), new { id = book.BookId }, book);
            }
            catch (EntityAlreadyExistsException e)
            {
                return Conflict(new {message = e.Message});
            }
        }

        [HttpGet("/api/[controller]/{id}")]
        public async Task<ActionResult<BookDto>> GetBookByIdAsync(Guid id)
        {
            var book = await _service.GetByIdAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpDelete("/api/[controller]/{id}")]
        public async Task<ActionResult> DeleteBookByIdAsync(Guid id)
        {
            try
            {
                await _service.DeleteByIdAsync(id);

                return Ok();
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("/api/[controller]/search-title/{title}")]
        public async Task<ActionResult<IEnumerable<BookDto>>> SearchBookByTitleAsync(string title)
        {
            var books = await _service.FindByNameAsync(title);

            return Ok(books);
        }
    }
}
