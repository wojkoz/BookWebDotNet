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
    }
}
