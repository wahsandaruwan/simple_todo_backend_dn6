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

        // Get one by id
        [HttpGet("GetById")]
        public async Task<ActionResult<Todo>> GetById(int id){
            var todo = await _context.Todos.FindAsync(id);
            if(todo == null){
                return NotFound();
            }

            return todo;
        }

        // Create a todo
        [HttpPost("Create")]
        public async Task<ActionResult<Todo>> Create(Todo todo){
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new {id = todo.TodoId}, todo);
        }
    }
}