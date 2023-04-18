// Local directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        [HttpGet("GetById/{id}")]
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

        // Update a todo
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, Todo todo){
            if(id != todo.TodoId){
                return BadRequest();
            }

            _context.Entry(todo).State = EntityState.Modified;

            try{
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException){
                if(!TodoExist(id)){
                    return NotFound();
                }
                else{
                    throw;
                }
            }

            return NoContent();
        }

        // Delete a todo
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // -----All utilities-----
        // Exist todo
        private bool TodoExist(int id){
            return _context.Todos.Any(e => e.TodoId == id);
        }
    }
}