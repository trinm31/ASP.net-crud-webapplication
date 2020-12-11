using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Model;

namespace WebApplication.Controllers
{
    [Route("api/Book")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        // GET
        public async Task<IActionResult> GetAll()
        {
            return Json(new{data = await _db.Books.ToListAsync()});
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var bookFromDb = await _db.Books.FirstOrDefaultAsync(u => u.Id == id);
            if (bookFromDb == null)
            {
                return Json(new {success = false, message = "Error while Deleting"});
            }

            _db.Books.Remove(bookFromDb);
            await _db.SaveChangesAsync();
            return Json(new {success = true, message = "Delete successful"});
        }
        
        
    }
}