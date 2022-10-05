using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Entities;
using WebApplication3.Model;

namespace WebApplication3.Controllers;

    [Route("/comment/[controller]")]
    [Authorize]
public class CommentController : Controller

{
    // private static List<Coment> Comments = new List<Coment>(new[]
    //     {
    //         new Coment() {CommentId = 1},
    //         new Coment() {CommentId = 2},
    //         new Coment() {CommentId = 3}
    //     });
    private readonly Context _context;

    public CommentController(Context context)
    {
        _context = context;
    }

        [HttpGet]
        public IEnumerable<Coment> Get() =>  _context.Comments;


        [HttpGet("{Id}")]
        public IActionResult Get(int Id)
        {
            var product = _context.Comments.SingleOrDefault(p => p.Id == Id);
            if (product==null)
            {
                return NotFound();
            }
            
            return Ok(product);
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            _context.Comments.Remove(_context.Comments.SingleOrDefault(p => p.Id == Id));
            return Ok();
        }

        private int NextProductId => _context.Comments.Local.Count == 0 ? 1 : _context.Comments.Max(i => i.Id) + 1;

        [HttpGet("NextCommentId")]
        public int GetNextProductId()
        {
            return NextProductId;
        }

        [HttpPost]
        public IActionResult Post(Coment product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            product.Id = NextProductId;
            _context.Comments.Add(product);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }

        [HttpPost("AddComment")]
        public IActionResult PostBody([FromBody] Coment product) => Post(product);

        [HttpPut]
        public IActionResult Put(Coment product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var storedProduct = _context.Comments.SingleOrDefault(p => p.Id == product.Id);
            if (storedProduct == null) return NotFound();
            storedProduct.Comment = product.Comment;
            storedProduct.User = product.User;
            return Ok(storedProduct);
        }
        [HttpPut("ChangeComment")]
        public IActionResult PutBody([FromBody] Coment product) => Put(product);
        
    }
