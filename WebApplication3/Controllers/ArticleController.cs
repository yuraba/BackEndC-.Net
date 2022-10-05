using Microsoft.AspNetCore.Mvc;
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
        public IEnumerable<Article> Get() => _context.Articless;


        [HttpGet("{Id}")]
        public IActionResult Get(int Id)
        {
            var product = _context.Articless.SingleOrDefault(p => p.Id == Id);
            if (product==null)
            {
                return NotFound();
            }
            
            return Ok(product);
        }
      

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            _context.Articless.Remove(_context.Articless.SingleOrDefault(p => p.Id == Id));
            return Ok();
        }

        private int NextProductId => _context.Articless.Local.Count == 0 ? 1 : _context.Articless.Max(i => i.Id) + 1;

        [HttpGet("NextProductId")]
        public int GetNextProductId()
        {
            return NextProductId;
        }

        [HttpPost]
        public IActionResult Post(Article product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // product.Id = NextProductId;
            _context.Articless.Add(product);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }
        

        [HttpPost("AddProduct")]
        public IActionResult PostBody([FromBody] Article product) => Post(product);

        [HttpPut]
        public IActionResult Put(Article product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var storedProduct = _context.Articless.SingleOrDefault(p => p.Id == product.Id);
            if (storedProduct == null) return NotFound();
            storedProduct.Title = product.Title;
            storedProduct.Body = product.Body;
            return Ok(storedProduct);
        }
        [HttpPut("ChangeProduct")]
        public IActionResult PutBody([FromBody] Article product) => Put(product);
        

        // GET
        // public IActionResult Index()
        // {
        //     return Ok();
        // }
    }
}