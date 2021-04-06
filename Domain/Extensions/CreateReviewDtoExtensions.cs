using System;
using BookWebDotNet.Domain.Dtos;
using BookWebDotNet.Domain.Entity;

namespace BookWebDotNet.Domain.Extensions
{
    public static class CreateReviewDtoExtensions
    {
        public static Review ToReview(this CreateReviewDto dto)
        {
            return new()
            {
                BookId = dto.BookId,
                Content = dto.Content,
                UserId = dto.UserId,
                ReviewId = Guid.NewGuid()
            };
        }
    }
}
