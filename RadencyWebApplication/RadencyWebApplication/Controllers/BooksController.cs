using Microsoft.AspNetCore.Mvc;
using RadencyWebApplication.Models.Db;
using System;
using System.Linq;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RadencyWebApplication.Controllers
{
    [Route("api")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        ApiContext _context;
        public BooksController(ApiContext apiContext)
        {
            _context = apiContext;
        }
        Random rnd = new Random();

        //Book b = new Book() { Author = rnd.Next(1,15).ToString(), Content = "dfdfgdfdf", Cover = "asad", Genre = "aads", Title = rnd.Next(1, 15).ToString() };
        //_context.Books.Add(b);
        //_context.SaveChanges();
        //Rating r1 = new Rating() { Score = 45, Book = b };
        //Rating r2 = new Rating() { Score = 15, Book = b };
        //Review rev = new Review() { Book = b, Message = "message", Reviewer = "reviwer" };

        //_context.Ratings.Add(r1);
        //_context.Ratings.Add(r2);
        //_context.Reviews.Add(rev);
        //_context.SaveChanges();
        // GET: api/<ValuesController>
        [HttpGet("[controller]")]

        public IResult Get()
        {
            string order = HttpContext.Request.Query["order"].ToString();

            var result = from book in _context.Books
                         join review in _context.Reviews on book.Id equals review.BookId
                         group review by book into k
                         select new
                         {
                             book = k.Key,
                             reviews = k.Count()
                         };


            var res = from r in result
                      join rating in _context.Ratings on r.book.Id equals rating.BookId into gR                     
                      select new
                      {
                          id = r.book.Id,
                          title = r.book.Title,
                          author = r.book.Author,
                          rating = gR.Average(r => r.Score),
                          reviewsNumber = r.reviews
                      };

            res = order == "author" ?
                res.OrderBy(r => r.author) 
                : order == "title" ?
                res.OrderBy(r => r.title) 
                : res;

            return Results.Json(res);
        }

        // GET api/<ValuesController>/5
        [HttpGet("[controller]/{id}")]
        public IResult Get(int id)
        {
            Book b = new Book() { Author = rnd.Next(1, 15).ToString(), Content = "dfdfgdfdf", Cover = "asad", Genre = "aads", Title = rnd.Next(1, 15).ToString() };
            _context.Books.Add(b);
            _context.SaveChanges();
            Rating r1 = new Rating() { Score = 45, Book = b };
            Rating r2 = new Rating() { Score = 15, Book = b };
            Review rev = new Review() { Book = b, Message = "message", Reviewer = "reviwer" };
            Review rev1 = new Review() { Book = b, Message = "message", Reviewer = "reviwer" };

            _context.Ratings.Add(r1);
            _context.Ratings.Add(r2);
            _context.Reviews.Add(rev);
            _context.Reviews.Add(rev1);
            _context.SaveChanges();
            //var result = _context?.Books?.FirstOrDefault(b => b.Id == id);

            var result = _context?.Books.Where(b => b.Id == id).
                GroupJoin(_context.Reviews,
                b => b.Id,
                r => r.BookId,
                (b, r) => new
                {
                    b, r
                }).ToList();
            var res = from r in result
                      join rating in _context.Ratings on r.b.Id equals rating.BookId into gR
                      select new
                      {
                          id = r.b.Id,
                          title = r.b.Title,
                          author = r.b.Author,
                          rating = gR.Average(r => r.Score),
                          reviews = r.r
                      };
            return Results.Json(res);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
