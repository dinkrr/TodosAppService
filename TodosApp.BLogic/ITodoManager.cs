using System.Collections.Generic;
using TodosModel;

namespace TodosApp.BLogic
{
	public interface ITodoManager
	{
		TodoModel CreateATodo(TodoModel todoModel);

		TodoModel EditATodo(string Id, TodoModel todoModel);

		List<TodoModel> BringAllTodos();

		int RemoveATodo(string id);
	}
}
