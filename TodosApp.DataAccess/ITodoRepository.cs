using System.Collections.Generic;
using TodosModel;

namespace TodosApp.DataAccess
{
	public interface ITodoRepository
	{
		TodoModel CreateTodo(TodoModel todoModel);

		TodoModel UpdateTodo(string Id, TodoModel todoModel);

		List<TodoModel> GetAllTodos();

		int DeleteTodo(string id);
	}
}
