namespace TodosAppService.Tests
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Net;
	using System.Web.Http;
	using System.Web.Http.Results;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using Moq;

	using TodosApp.BLogic;
	using TodosApp.Controllers;

	using TodosModel;

	[TestClass]
	public class TodosControllerTest
	{
		List<TodoModel> expectedTodos;

		Mock<ITodoManager> mockTodosManager;

		ToDosController toDosController;

		[TestMethod]
		public void Delete_Fail()
		{
			var todosContentResult = toDosController.Delete("a123b134") as NegotiatedContentResult<HttpError>;
			var actualMessage = todosContentResult.Content.Message;
			var actualStatusCode = todosContentResult.StatusCode;

			Assert.AreEqual(Constants.expectedDelFailMessage, actualMessage);
			Assert.AreEqual(HttpStatusCode.NotFound, actualStatusCode);
		}

		[TestMethod]
		public void Delete_Successful()
		{
			var todosContentResult = toDosController.Delete("a123b123") as OkNegotiatedContentResult<string>;
			var actualMessage = todosContentResult.Content;

			Assert.AreEqual(Constants.expectedDelSuccessMessage, actualMessage);
		}

		[TestMethod]
		public void GetAll_Successful()
		{
			var todosContentResult = toDosController.Get() as OkNegotiatedContentResult<List<TodoModel>>;
			var actualTodos = todosContentResult.Content;

			Assert.AreEqual(expectedTodos[0].Id, actualTodos[0].Id);
			Assert.AreEqual(expectedTodos[0].Title, actualTodos[0].Title);
			Assert.AreEqual(expectedTodos[0].isComplete, actualTodos[0].isComplete);
		}

		[TestInitialize]
		public void InitializeTestData()
		{
			expectedTodos = GetExpectedTodosData();

			mockTodosManager = new Mock<ITodoManager>();
			toDosController = new ToDosController(mockTodosManager.Object);

			mockTodosManager.Setup(mockedObject => mockedObject.BringAllTodos()).Returns(expectedTodos);

			mockTodosManager.Setup(mockedObject => mockedObject.CreateATodo(It.IsAny<TodoModel>())).Returns(
				(TodoModel newTodo) =>
					{
						if (newTodo == null)
						{
							return false;
						}

						expectedTodos.Add(newTodo);
						return true;
					});

			mockTodosManager.Setup(mockedObject => mockedObject.EditATodo(It.IsAny<TodoModel>())).Returns(
				(TodoModel newTodo) =>
					{
						if (newTodo == null)
						{
							return false;
						}

						var todo = expectedTodos.Where(singleTodo => singleTodo.Id == newTodo.Id).FirstOrDefault();
						if (todo == null)
						{
							return false;
						}

						todo.Title = newTodo.Title;
						todo.isComplete = newTodo.isComplete;

						return true;
					});

			mockTodosManager.Setup(mockedObject => mockedObject.RemoveATodo(It.IsAny<string>())).Returns(
				(string guid) =>
					{
						if (guid == null)
						{
							return false;
						}

						var todo = expectedTodos.Where(singleTodo => singleTodo.Id == guid).FirstOrDefault();
						if (todo == null)
						{
							return false;
						}

						expectedTodos.Remove(todo);
						return true;
					});
		}

		[TestMethod]
		public void Post_Fail()
		{
			TodoModel newTodo = GetTodo(null, null);
			var todosContentResult = toDosController.Post(newTodo) as NegotiatedContentResult<HttpError>;
			var actualMessage = todosContentResult.Content.Message;
			var actualStatusCode = todosContentResult.StatusCode;

			Assert.AreEqual(Constants.expectedPostFailMessage, actualMessage);
			Assert.AreEqual(HttpStatusCode.BadRequest, actualStatusCode);
		}

		[TestMethod]
		public void Post_Successful()
		{
			TodoModel newTodo = GetTodo("a321b456", "Test2", false);
			var todosContentResult = toDosController.Post(newTodo) as OkNegotiatedContentResult<string>;
			var actualMessage = todosContentResult.Content;

			Assert.AreEqual(Constants.expectedPostSuccessMessage, actualMessage);
		}

		[TestMethod]
		public void Put_Fail()
		{
			TodoModel newTodo = GetTodo("a123b134", "New Title For Test1", true);
			var todosContentResult = toDosController.Put(newTodo) as NegotiatedContentResult<HttpError>;
			var actualMessage = todosContentResult.Content.Message;
			var actualStatusCode = todosContentResult.StatusCode;

			Assert.AreEqual(Constants.expectedPutFailMessage, actualMessage);
			Assert.AreEqual(HttpStatusCode.BadRequest, actualStatusCode);
		}

		[TestMethod]
		public void Put_Successful()
		{
			TodoModel updatedTodo = GetTodo("a123b123", "New Title For Test1", true);
			var todosContentResult = toDosController.Put(updatedTodo) as OkNegotiatedContentResult<string>;
			var actualMessage = todosContentResult.Content;

			Assert.AreEqual(Constants.expectedPutSuccessMessage, actualMessage);
		}

		private List<TodoModel> GetExpectedTodosData()
		{
			List<TodoModel> todoList = new List<TodoModel>();
			todoList.Add(new TodoModel() { Id = "a123b123", Title = "Test1", isComplete = false });
			return todoList;
		}

		private TodoModel GetTodo(string guid, string title, bool isComplete = false)
		{
			if ((guid == null || title == null) && isComplete == false)
			{
				return null;
			}

			return new TodoModel() { Id = guid, Title = title, isComplete = isComplete };
		}
	}
}