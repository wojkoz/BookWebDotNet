using System;
using BookWebDotNet.Domain.Dtos;
using BookWebDotNet.Domain.Entity;

namespace BookWebDotNet.Domain.Extensions
{
    public static class CreateBookDtoExtensions
    {
        public static Book ToBook(this CreateBookDto dto)
        {
            return new()
            {
                BookId = Guid.NewGuid(),
                Author = dto.Author,
                Cover = dto.Cover,
                Publisher = dto.Publisher,
                Title = dto.Title,
                Year = dto.Year
            };
        }
    }
}
