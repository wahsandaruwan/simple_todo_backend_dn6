using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        // -----Db context instance-----
        private readonly ApiDbContext _context;

        // -----Constructor-----
        public TodoController(ApiDbContext context){
            _context = context;
        }

        // -----All API endpoints-----

        // Get all todos
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Todo>>> GetAll(){
            return await _context.Todos.ToListAsync();
        }
    }
}