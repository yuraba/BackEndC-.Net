using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Entities;
using WebApplication3.Model;

namespace WebApplication3.Controllers
{
    [Route("/api/[controller]")]

    public class ProductController : Controller
    {
        // private static List<Article> Articles = new List<Article>(new[]
        // {
        //     new Article() {Id = 1},
        //     new Article() {Id = 2},
        //     new Article() {Id = 3}
        // });
        private readonly Context _context;

        public ProductController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Article>>> Get()
        {
            return Ok(await _context.Articles.ToListAsync());
        }


        [HttpGet("{Id}")]
        public IActionResult Get(int Id)
        {
            var product = _context.Articles.SingleOrDefault(p => p.Id == Id);
            if (product==null)
            {
                return NotFound();
            }
            
            return Ok(product);
        }
      

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var art = await _context.Articles.FindAsync(Id);
            if (art == null)
            {
                return BadRequest("Article not found");
            }
            _context.Articles.Remove(art);
            await _context.SaveChangesAsync();
            return Ok();
        }

        private int NextProductId => _context.Articles.Local.Count == 0 ? 1 : _context.Articles.Max(i => i.Id) + 1;

        [HttpGet("NextProductId")]
        public int GetNextProductId()
        {
            return NextProductId;
        }

        [HttpPost]
        public async Task<ActionResult<List<Article>>> Post(Article product)
        {
            if (!ModelState.IsValid)
            {
                // return BadRequest(ModelState);
            }
            
            _context.Articles.Add(product);
            await _context.SaveChangesAsync();
            return Ok(await _context.Articles.ToListAsync());
        }
        

        // [HttpPost("AddProduct")]
        // public async Task<ActionResult> PostBody([FromBody] Article product) => Post(product);

        [HttpPut]
        public async Task<ActionResult<List<Article>>> Put(Article product)
        {
            var art = await _context.Articles.FindAsync(product.Id);
            if (art == null)
            {
                return BadRequest("Articles not found");
            }

            art.Title = product.Title;
            art.Body = product.Body;
            art.Image = product.Image;

            await _context.SaveChangesAsync();
            return Ok();
        }
        // [HttpPut("ChangeProduct")]
        // public IActionResult PutBody([FromBody] Article product) => Put(product);
        //

        // GET
        // public IActionResult Index()
        // {
        //     return Ok();
        // }
    }
}