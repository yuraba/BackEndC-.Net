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
        public async Task<ActionResult<List<Article>>> Get(CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Ok("Action was cancelled");
            }
            return Ok(await _context.Articles.ToListAsync());
        }


        [HttpGet("{Id}")]
        public IActionResult Get(int Id, CancellationToken cancellationToken = default)
        {
            var product = _context.Articles.SingleOrDefault(p => p.Id == Id);
            if (cancellationToken.IsCancellationRequested)
            {
                return Ok("Action was cancelled");
            }
            if (product==null)
            {
                return NotFound();
            }
            
            return Ok(product);
        }
        
        [HttpGet("/user/{name}")]
        public  async Task<ActionResult<List<Article>>> Get(string name, CancellationToken cancellationToken = default)
        {
            var a = await _context.Articles.Where(p => p.CreatedBy == name).ToListAsync();
            if (cancellationToken.IsCancellationRequested)
            {
                return Ok("Action was cancelled");
            }
            return Ok(a);
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
        public async Task<ActionResult<List<Article>>> Post([FromBody]Article product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            _context.Articles.Add(product);
            await _context.SaveChangesAsync();
            return Ok(await _context.Articles.ToListAsync());
        }
        

        // [HttpPost("AddProduct")]
        // public async Task<ActionResult> PostBody([FromBody] Article product) => Post(product);

        [HttpPut("{Id}")]
        public async Task<ActionResult<List<Article>>> bm([FromBody]Article product)
        {
            var art = await _context.Articles.FindAsync(product.Id);
            if (art == null)
            {
                return BadRequest("Articles not found");
            }

            art.title = product.title;
            art.body = product.body;
            art.image = product.image;
            art.IsApproved = product.IsApproved;

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