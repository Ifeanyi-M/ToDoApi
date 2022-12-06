using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Serialization;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoController : Controller
    {
        private readonly WebAPIDbContext dbContext;

        public ToDoController(WebAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        [Route("{page}")]
        public async Task <ActionResult<List<ToDoItems>>> GetToDos(int page)
        {
            if (dbContext.ToDos == null)
                return NotFound();

            var pageOutcome = 3f;
            var noOfPages = Math.Ceiling(dbContext.ToDos.Count() / pageOutcome);


            var thingsToDo = await dbContext.ToDos
                .Skip((page - 1) * (int)pageOutcome)
                .Take((int)pageOutcome)
                .ToListAsync();

            var response = new ToDoResponse
            {
                toDoItems = thingsToDo,
                CurrentPage = page,
                Pages = (int)pageOutcome

            };

            return Ok(response);
            
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> SearchTodo([FromRoute] Guid id)
        {
            var lookForToDo = await dbContext.ToDos.FindAsync(id);
            if(lookForToDo == null)
            {
                return NotFound();
            }
            return Ok(lookForToDo);
        }

        [HttpPost]
        public async Task <IActionResult> AddTodo(AddToDoRequest addToDoRequest)
        {
            var toDoItem = new ToDoItems()
            {
                Id = Guid.NewGuid(),
                Email = addToDoRequest.Email,
                ToDo = addToDoRequest.ToDo,
                PageNumber = addToDoRequest.PageNumber
            };

            await dbContext.ToDos.AddAsync(toDoItem);
            await dbContext.SaveChangesAsync();

            return Ok(toDoItem);
        }


        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateTodo([FromRoute] Guid id, UpdateTodoRequest updateTodoRequest)
        {
           var someToDo = await dbContext.ToDos.FindAsync(id);

            if(someToDo != null)
            {
                someToDo.ToDo = updateTodoRequest.ToDo;
                someToDo.Email = updateTodoRequest.Email;
                someToDo.PageNumber = updateTodoRequest.PageNumber;

                await dbContext.SaveChangesAsync();

                return Ok(someToDo);


            }
            return NotFound();
        }


        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteTodo([FromRoute] Guid id)
        {
            var todoToDelete = await dbContext.ToDos.FindAsync(id);
            
            if(todoToDelete != null)
            {
                dbContext.Remove(todoToDelete);
                await dbContext.SaveChangesAsync();
                return Ok(todoToDelete);
            }
            return NotFound();
        }
    }
}
