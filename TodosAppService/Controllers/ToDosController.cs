using System.Web.Http;
using TodosApp.BLogic;
using TodosModel;

namespace TodosApp.Controllers
{
	public class ToDosController : ApiController
	{
		private ITodoManager todoManager;

		public ToDosController()
		{
			todoManager = new TodoManager();
		}

		/// <summary>
		/// Delete the ToDo
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public IHttpActionResult Delete(string id)
		{
			return Ok(todoManager.RemoveATodo(id));
		}

		/// <summary>
		/// Get All the List of ToDos
		/// </summary>
		/// <returns></returns>
		public IHttpActionResult Get()
		{
			return Ok(todoManager.BringAllTodos());
		}

		/// <summary>
		/// Create a new ToDo
		/// </summary>
		/// <param name="todoValue"></param>
		/// <returns></returns>
		public IHttpActionResult Post([FromBody] TodoModel todoValue)
		{
			return Ok(todoManager.CreateATodo(todoValue));
		}

		/// <summary>
		/// Edit a ToDo
		/// </summary>
		/// <param name="id"></param>
		/// <param name="todoValue"></param>
		/// <returns></returns>
		public IHttpActionResult Put(string id, [FromBody] TodoModel todoValue)
		{
			return Ok(todoManager.EditATodo(id, todoValue));
		}
	}
}