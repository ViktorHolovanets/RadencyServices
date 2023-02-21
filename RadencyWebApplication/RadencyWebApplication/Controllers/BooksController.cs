using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.Query.Internal;
using RadencyWebApplication.Models.Db;
using RadencyWebApplication.Models.Entity;
using System;
using System.Drawing;
using System.Linq;
using System.Text;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RadencyWebApplication.Controllers
{
    [Route("api")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        ILogger<BooksController> _logger;
        ApiContext _context;
        private readonly IConfiguration _config;
        public BooksController(ApiContext apiContext, IConfiguration config, ILogger<BooksController> logger)
        {
            _context = apiContext;
            _config = config;
            _logger = logger;
        }

        [HttpGet("[controller]")]
        public async Task<IActionResult> Get()
        {
            var request = HttpContext.Request;
            WriteLogRequest(request);
            string order = request.Query["order"].ToString();
            var res = GetAllBooks();
            if (res == null)
            {
                _logger.LogWarning($"No related data to books");
            }
            res = order == "author" ?
                res.OrderBy(r => r.author)
                : order == "title" ?
                res.OrderBy(r => r.title)
                : res;
            return Ok(res.ToList());
        }
        [HttpGet("recommended")]
        public async Task<IResult> GetRecommended()
        {
            var request = HttpContext.Request;
            WriteLogRequest(request);
            string genre = request.Query["genre"].ToString();
            var res = GetAllBooks(genre);
            res = res.OrderByDescending(b => b.rating).Take(10);
            return Results.Json(res, statusCode: 200);
        }

        [HttpGet("[controller]/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var request = HttpContext.Request;
            WriteLogRequest(request);


            var result = _context.Books.Where(b => b.Id == id).
               GroupJoin(_context.Reviews,
               book => book.Id,
               review => review.BookId,
               (book, review) => new
               {
                   book,
                   review = review.Select(rew => new { rew.Id, rew.Message, rew.Reviewer }),
               }).ToList()
               .GroupJoin(_context.Ratings,
               book => book.book.Id,
               rating => rating.BookId,
               (book, rating) => new
               {
                   id = book.book.Id,
                   title = book.book.Title,
                   author = book.book.Author,
                   cover = book.book.Cover,
                   content = book.book.Content,
                   genre= book.book.Genre,
                   rating = rating.Average(r => r.Score),
                   reviews = book.review
               }).ToList();
            if (result.Count() < 1)
            {
                _logger.LogWarning($"No related data to a book with ID {id} ");
                return BadRequest(new { message = "Not fount book" });
            }
            return Ok(result.ToList());
        }


        [HttpPost("[controller]/save")]
        public async Task<IResult> Post(Book book)
        {
            var request = HttpContext.Request;
            WriteLogRequest(request);
            if (!ModelState.IsValid)
            {
                return Results.BadRequest();
            }
            var temp = await _context.Books.FirstOrDefaultAsync(b => b.Id == book.Id);
            if (temp == null)
            {
                _context?.Books.Add(book);
                _logger.LogInformation($"Created a new book with ID {book.Id}");
                temp = book;
            }
            else
            {
                temp.Title = book.Title;
                temp.Author = book.Author;
                temp.Cover = book.Cover;
                temp.Content = book.Content;
                temp.Genre = book.Genre;
            }
            await _context.SaveChangesAsync();
            return Results.Json(temp, statusCode: 201);
        }


        [HttpPut("[controller]/{id}/review")]
        public async Task<IResult> SetReview(int id, Review review)
        {
            var request = HttpContext.Request;
            WriteLogRequest(request);
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null || !ModelState.IsValid)
            {
                _logger.LogWarning("Bad requst");
                return Results.BadRequest();
            }
            review.Book = book;
            _context.Reviews.Add(review);
            _context.SaveChanges();
            return Results.Json(new { id = review.Id }, statusCode: 201);
        }
        [HttpPut("[controller]/{id}/rate")]
        public async Task<IResult> SetRating(int id, Rating rating)
        {
            var request = HttpContext.Request;
            WriteLogRequest(request);
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (book == null || !ModelState.IsValid)
                return Results.BadRequest();
            rating.Book = book;
            _context.Ratings.Add(rating);
            _context.SaveChanges();
            return Results.Json(new { id = rating.Id }, statusCode: 201);
        }


        [HttpDelete("[controller]/{id}")]
        public async Task<IResult> Delete(int id)
        {
            var request = HttpContext.Request;
            WriteLogRequest(request);
            string secret = HttpContext.Request.Query["secret"].ToString();
            var secretConfig = _config["secretKey"];
            if (secret == secretConfig)
            {
                var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
                if (book != null)
                {
                    _context.Books.Remove(book);
                    return Results.Ok(new { message = "Success" });
                }
                return Results.BadRequest(new { message = "Book not found" });
            }
            return Results.BadRequest(new { message = "Invalid secret code" });
        }
        private IQueryable<BookInfo> GetAllBooks(string? genre = null)
        {

            var result = from book in _context.Books
                         where !String.IsNullOrEmpty(genre) ? book.Genre == genre : 1 == 1
                         join review in _context.Reviews on book.Id equals review.BookId
                         group review by book into k
                         select new
                         {
                             book = k.Key,
                             reviews = k.Count()
                         };

            var res = from r in result
                      join rating in _context.Ratings on r.book.Id equals rating.BookId into gR
                      select new BookInfo()
                      {
                          id = r.book.Id,
                          title = r.book.Title,
                          author = r.book.Author,
                          cover=r.book.Cover,
                          rating = (decimal)gR.Average(r => r.Score),
                          reviewsNumber = r.reviews
                      };
            return res;

        }
        private void WriteLogRequest(HttpRequest request)
        {
            string h = "Headers\n";
            foreach (var header in request.Headers)
            {
                h += ($"{header.Key}\t{header.Value}\n");
            }
            _logger.LogInformation(h);
            _logger.LogInformation($"Path: {request.Path}");
            _logger.LogInformation($"Query string: {request.QueryString}");

        }
    }
}
