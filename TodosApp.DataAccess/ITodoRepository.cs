namespace TodosApp.DataAccess
{
	using System.Collections.Generic;

	using TodosModel;

	public interface ITodoRepository
	{
		TodoModel CreateTodo(TodoModel todoModel);

		int DeleteTodo(string id);

		List<TodoModel> GetAllTodos();

		TodoModel UpdateTodo(string Id, TodoModel todoModel);
	}
}