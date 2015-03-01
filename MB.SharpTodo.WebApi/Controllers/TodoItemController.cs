using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using MB.SharpTodo.Core.Application;
using MB.SharpTodo.Core.Domain;

namespace MB.SharpTodo.WebApi.Controllers
{
    public class TodoItemController : ApiController
    {
        private readonly ISharpTodoService _service;

        public TodoItemController(ISharpTodoService service)
        {
            _service = service;
            //var service2 = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ISharpTodoService));        }
        }

        // GET: api/TodoItem
        public IQueryable<TodoItem> GetTodoItems()
        {
            return _service.FindAllTodoItems().ToList().AsQueryable(); // force lazy loading
        }

        // GET: api/TodoItem/5
        [ResponseType(typeof(TodoItem))]
        public IHttpActionResult GetTodoItem(int id)
        {
            TodoItem todoItem = _service.FindTodoItemById(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            return Ok(todoItem);
        }

        // PUT: api/TodoItem/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTodoItem(int id, TodoItem todoItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            _service.UpdateTodoItem(todoItem);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TodoItem
        [ResponseType(typeof(TodoItem))]
        public IHttpActionResult PostTodoItem(TodoItem todoItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _service.AddTodoItem(todoItem);

            return CreatedAtRoute("DefaultApi", new { id = todoItem.Id }, todoItem);
        }

        // DELETE: api/TodoItem/5
        [ResponseType(typeof(TodoItem))]
        public IHttpActionResult DeleteTodoItem(int id)
        {
            TodoItem todoItem = _service.FindTodoItemById(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _service.RemoveTodoItem(todoItem);

            return Ok(todoItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _service.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}