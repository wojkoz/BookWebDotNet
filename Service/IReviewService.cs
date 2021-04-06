

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookWebDotNet.Domain.Dtos;
using BookWebDotNet.Domain.Entity;

namespace BookWebDotNet.Service
{
    public interface IReviewService
    {
        public Task<IEnumerable<ReviewDto>> GetAllReviewsByBookId(Guid bookId);
        public Task<ReviewDto> GetReviewById(Guid id);
        /// <summary>Throws EntityNotFoundException when can't find review</summary> 
        public Task DeleteReview(Guid id);
        public Task<ReviewDto> CreateReview(CreateReviewDto createReviewDto);
    }
}
