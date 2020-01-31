namespace TodosApp.BLogic
{
	using System.Collections.Generic;

	using TodosApp.DataAccess;

	using TodosModel;

	public class TodoManager : ITodoManager
	{
		private ITodoRepository todoRepository;

		public TodoManager(ITodoRepository todoRepository)
		{
			this.todoRepository = todoRepository;
		}

		public List<TodoModel> BringAllTodos()
		{
			return todoRepository.GetAllTodos();
		}

		public bool CreateATodo(TodoModel todoModel)
		{
			if (todoModel == null)
			{
				return false;
			}
			return todoRepository.CreateTodo(todoModel);
		}

		public bool EditATodo(TodoModel todoModel)
		{
			if (todoModel == null)
			{
				return false;
			}
			return todoRepository.UpdateTodo(todoModel);
		}

		public bool RemoveATodo(string id)
		{
			if (id == null)
			{
				return false;
			}
			return todoRepository.DeleteTodo(id);
		}
	}
}