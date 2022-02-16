using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Razor_BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Razor_BookStore.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await  _db.Book.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var bookFromDB = await _db.Book.FirstOrDefaultAsync(u => u.Id == id);
            if(bookFromDB == null)
            {
                return Json(new { success = false, message = "Error while deleteing" });
            }
            _db.Book.Remove(bookFromDB);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete success" });
        }
    }
}
