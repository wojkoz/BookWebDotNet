using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookWebDotNet.Domain.Entity;
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
    }
}
