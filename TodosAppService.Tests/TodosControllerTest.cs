using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodosApp.BLogic;
using TodosApp.Controllers;
using TodosApp.DataAccess;
using TodosModel;

namespace TodosAppService.Tests
{
	[TestClass]
	public class TodosControllerTest
	{
		[TestMethod]
		public void GetAll_ReturnsAllTodos()
		{
			//Arrange
			List<TodoModel> testTodosList = new List<TodoModel>();
			testTodosList.Add(
						new TodoModel()
						{
							Id = "68deea7d-b995-3aea-5d95-a2ffda43efd8",
							Title = "Three",
							isComplete = false
						});
			var toDosController = new ToDosController(new TodoManager(new TodoRepository()));

			//Act
			List<TodoModel> dataResults = GetResultsFromController(toDosController);

			//Assert
			Assert.AreEqual(testTodosList[0].Id, dataResults[0].Id);
			Assert.AreEqual(testTodosList[0].Title, dataResults[0].Title);
			Assert.AreEqual(testTodosList[0].isComplete, dataResults[0].isComplete);
		}

		private List<TodoModel> GetResultsFromController(ToDosController toDosController)
		{
			var httpActionResult = toDosController.Get() as OkNegotiatedContentResult<List<TodoModel>>;
			return httpActionResult.Content;
		}
	}
}
