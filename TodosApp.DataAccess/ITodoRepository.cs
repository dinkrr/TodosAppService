namespace TodosApp.DataAccess
{
	using System.Collections.Generic;

	using TodosModel;

	public interface ITodoRepository
	{
		bool CreateTodo(TodoModel todoModel);

		bool DeleteTodo(string id);

		List<TodoModel> GetAllTodos();

		bool UpdateTodo(TodoModel todoModel);
	}
}