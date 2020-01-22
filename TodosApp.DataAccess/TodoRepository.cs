using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TodosModel;

namespace TodosApp.DataAccess
{

	public class TodoRepository : ITodoRepository
	{
		SqlConnection connection;

		public TodoRepository()
		{
			connection = new SqlConnection(Constants.CONNECTION_STRING);
		}

		public TodoModel CreateTodo(TodoModel todoModel)
		{
			TodoModel outputTodoModel = new TodoModel() { Id = Constants.NA, Title = Constants.NA, isComplete = false };

			int numberOfRowsAffected = 0;
			try
			{
				connection.Open();
				SqlCommand command = new SqlCommand(
					$"INSERT INTO TODO ({Constants.ID}, {Constants.TITLE}, {Constants.IS_COMPLETED}) VALUES ({Constants.ID_PARAM}, {Constants.TITLE_PARAM}, {Constants.IS_COMPLETED_PARAM});",
					connection);
				command.Parameters.Add(Constants.ID_PARAM, SqlDbType.NVarChar).Value = todoModel.Id;
				command.Parameters.Add(Constants.TITLE_PARAM, SqlDbType.NVarChar).Value = todoModel.Title;
				command.Parameters.Add(Constants.IS_COMPLETED_PARAM, SqlDbType.Bit).Value = todoModel.isComplete;
				numberOfRowsAffected = command.ExecuteNonQuery();
				if (numberOfRowsAffected == 1)
				{
					outputTodoModel = todoModel;
				}
			}
			catch (Exception ex)
			{
				return outputTodoModel;
			}
			finally
			{
				connection.Close();
			}

			return outputTodoModel;
		}

		public int DeleteTodo(string Id)
		{
			int numberOfRowsAffected = 0;
			try
			{
				connection.Open();
				SqlCommand command = new SqlCommand(
					$"DELETE FROM TODO WHERE {Constants.ID} = {Constants.ID_PARAM};",
					connection);
				command.Parameters.Add(Constants.ID_PARAM, SqlDbType.NVarChar).Value = Id;
				numberOfRowsAffected = command.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				return numberOfRowsAffected;
			}
			finally
			{
				connection.Close();
			}

			return numberOfRowsAffected;
		}

		public List<TodoModel> GetAllTodos()
		{
			List<TodoModel> outputList = new List<TodoModel>();
			DataTable todosDataTable = new DataTable();
			try
			{
				connection.Open();
				SqlCommand command = new SqlCommand("SELECT * FROM TODO;", connection);
				command.ExecuteNonQuery();

				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
				sqlDataAdapter.Fill(todosDataTable);

				foreach (DataRow row in todosDataTable.Rows)
				{
					outputList.Add(
						new TodoModel()
							{
								Id = Convert.ToString(row[Constants.ID]),
								Title = Convert.ToString(row[Constants.TITLE]),
								isComplete = Convert.ToBoolean(row[Constants.IS_COMPLETED])
							});
				}
			}
			catch (Exception ex)
			{
				List<TodoModel> errorList = new List<TodoModel>();
				errorList.Add(new TodoModel() { Id = Constants.NA, Title = Constants.NA, isComplete = false });
				return errorList;
			}
			finally
			{
				connection.Close();
			}

			return outputList;
		}

		public TodoModel UpdateTodo(string Id, TodoModel todoModel)
		{
			TodoModel outputTodoModel = new TodoModel();
			int numberOfRowsAffected = 0;
			try
			{
				connection.Open();
				SqlCommand command = new SqlCommand(
					$"UPDATE TODO SET {Constants.IS_COMPLETED} = {Constants.IS_COMPLETED_PARAM} where {Constants.ID} = {Constants.ID_PARAM};",
					connection);
				command.Parameters.Add(Constants.ID_PARAM, SqlDbType.NVarChar).Value = Id;
				command.Parameters.Add(Constants.IS_COMPLETED_PARAM, SqlDbType.Bit).Value = todoModel.isComplete;
				numberOfRowsAffected = command.ExecuteNonQuery();
				if (numberOfRowsAffected == 1)
				{
					outputTodoModel = todoModel;
				}
			}
			catch (Exception ex)
			{
				return outputTodoModel;
			}
			finally
			{
				connection.Close();
			}

			return outputTodoModel;
		}
	}
}