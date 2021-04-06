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
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet("/by-book/{id}")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviewsByBookId(Guid id)
        {
            var reviews = await _reviewService.GetAllReviewsByBookId(id);

            return Ok(reviews);
        }

        [HttpGet("/{id}")]
        public async Task<ActionResult<ReviewDto>> GetReviewById(Guid id)
        {
            var review = await _reviewService.GetReviewById(id);

            if (review is null)
            {
                return NotFound();
            }

            return Ok(review);
        }

        [HttpDelete("/{id}")]
        public async Task<ActionResult> DeleteReviewById(Guid id)
        {
            try
            {
                await _reviewService.DeleteReview(id);

                return Ok();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ReviewDto>> CreateReview([FromBody] CreateReviewDto dto)
        {
            try
            {
                var review = await _reviewService.CreateReview(dto);
                return Ok(review);
            }
            catch (EntityNotFoundException e)
            {
                return Conflict(new {message = e.Message});
            }
        }
    }
}
