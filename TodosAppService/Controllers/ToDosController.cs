namespace TodosApp.Controllers
{
	using System.Collections.Generic;
	using System.Net;
	using System.Web.Http;

	using TodosApp.BLogic;

	using TodosModel;

	public class ToDosController : ApiController
	{
		private ITodoManager todoManager;

		public ToDosController(ITodoManager todoManager)
		{
			this.todoManager = todoManager;
		}

		/// <summary>
		/// Delete the ToDo
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpDelete]
		[Route("api/Todos/{id}")]
		public IHttpActionResult Delete(string id)
		{
			bool isDeleted = todoManager.RemoveATodo(id);
			if (!isDeleted)
			{
				string errorMessage = "Error occurred while deleting the Todo, Kindly see logs.";
				HttpError error = new HttpError(errorMessage);
				return Content(HttpStatusCode.NotFound, error);
			}

			string message = "Todo is Deleted successfully";
			return Ok(message);
		}

		/// <summary>
		/// Get All the List of ToDos
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public IHttpActionResult Get()
		{
			List<TodoModel> todosList = todoManager.BringAllTodos();
			if (todosList == null)
			{
				string errorMessage = "Unable to retrieve products, Kindly see logs.";
				HttpError error = new HttpError(errorMessage);
				return Content(HttpStatusCode.NotFound, error);
			}

			return Ok(todosList);
		}

		/// <summary>
		/// Create a new ToDo
		/// </summary>
		/// <param name="todoValue"></param>
		/// <returns></returns>
		[HttpPost]
		public IHttpActionResult Post([FromBody] TodoModel todoValue)
		{
			bool isCreated = todoManager.CreateATodo(todoValue);
			if (!isCreated)
			{
				string errorMessage = "Unable to Create the Todo, Kindly see the logs.";
				HttpError error = new HttpError(errorMessage);
				return Content(HttpStatusCode.BadRequest, error);
			}

			string message = "Todo is created successfully";
			return Ok(message);
		}

		/// <summary>
		/// Edit a ToDo
		/// </summary>
		/// <param name="id"></param>
		/// <param name="todoValue"></param>
		/// <returns></returns>
		[HttpPut]
		public IHttpActionResult Put([FromBody] TodoModel todoValue)
		{
			bool isUpdated = todoManager.EditATodo(todoValue);
			if (!isUpdated)
			{
				string errorMessage = "Unable to update the Todo, Kindly see the logs.";
				HttpError error = new HttpError(errorMessage);
				return Content(HttpStatusCode.BadRequest, error);
			}

			string message = "Todo is updated successfully";
			return Ok(message);
		}
	}
}