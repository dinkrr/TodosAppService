namespace TodosApp.BLogic
{
	using System.Collections.Generic;

	using TodosModel;

	public interface ITodoManager
	{
		List<TodoModel> BringAllTodos();

		TodoModel CreateATodo(TodoModel todoModel);

		TodoModel EditATodo(string Id, TodoModel todoModel);

		int RemoveATodo(string id);
	}
}