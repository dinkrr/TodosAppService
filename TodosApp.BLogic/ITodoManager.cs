namespace TodosApp.BLogic
{
	using System.Collections.Generic;

	using TodosModel;

	public interface ITodoManager
	{
		List<TodoModel> BringAllTodos();

		bool CreateATodo(TodoModel todoModel);

		bool EditATodo(TodoModel todoModel);

		bool RemoveATodo(string id);
	}
}