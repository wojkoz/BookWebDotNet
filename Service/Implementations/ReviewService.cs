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
    public class ReviewService : IReviewService
    {
        private readonly BookWebDbContext _repository;

        public ReviewService(BookWebDbContext repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<ReviewDto>> GetAllReviewsByBookId(Guid bookId)
        {
            var reviews = _repository.Reviews
                .Where((review => review.BookId.Equals(bookId)))
                .AsNoTracking()
                .AsEnumerable();

            var reviewsDto = reviews.Adapt<IEnumerable<ReviewDto>>();

            return Task.FromResult(reviewsDto);
        }

        public async Task<ReviewDto> GetReviewById(Guid id)
        {
            var review = await _repository.Reviews.FindAsync(id);

            return review?.AdaptToDto();
        }

        public async Task DeleteReview(Guid id)
        {
            var review = await _repository.Reviews.FindAsync(id);

            if (review is null)
            {
                throw new EntityNotFoundException($"Review with id {id} doesn't exists");
            }

            _repository.Reviews.Remove(review);
            await _repository.SaveChangesAsync();
        }

        public async Task<ReviewDto> CreateReview(CreateReviewDto createReviewDto)
        {
            var user = await _repository.Users.FindAsync(createReviewDto.UserId);
            var book = await _repository.Books.FindAsync(createReviewDto.BookId);

            if (user is null)
            {
                throw new EntityNotFoundException($"User with id {createReviewDto.UserId} doesn't exists");
            }
            if (book is null)
            {
                throw new EntityNotFoundException($"Book with id {createReviewDto.BookId} doesn't exists");
            }


            var review = createReviewDto.ToReview();

            await _repository.Reviews.AddAsync(review);
            await _repository.SaveChangesAsync();

            return review.AdaptToDto();
        }
    }
}
