namespace TodosApp.DataAccess
{
	using System;
	using System.Collections.Generic;
	using System.Configuration;
	using System.Data;
	using System.Data.SqlClient;

	using TodosModel;

	public class TodoRepository : ITodoRepository
	{
		SqlConnection connection;

		public TodoRepository()
		{
			connection =
				new SqlConnection(ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString);
		}

		public bool CreateTodo(TodoModel todoModel)
		{
			bool isCreated = false;
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
				isCreated = numberOfRowsAffected > 0 ? true : false;
				return isCreated;
			}
			catch (Exception ex)
			{
				// Log the exception here.
				return isCreated;
			}
			finally
			{
				connection.Close();
			}
		}

		public bool DeleteTodo(string Id)
		{
			bool isDeleted = false;
			int numberOfRowsAffected = 0;
			try
			{
				connection.Open();
				SqlCommand command = new SqlCommand(
					$"DELETE FROM TODO WHERE {Constants.ID} = {Constants.ID_PARAM};",
					connection);
				command.Parameters.Add(Constants.ID_PARAM, SqlDbType.NVarChar).Value = Id;
				numberOfRowsAffected = command.ExecuteNonQuery();
				isDeleted = numberOfRowsAffected > 0 ? true : false;
				return isDeleted;
			}
			catch (Exception e)
			{
				// Log the exception here.
				return isDeleted;
			}
			finally
			{
				connection.Close();
			}
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

				return outputList;
			}
			catch (Exception ex)
			{
				// Log the exception here.
				return null;
			}
			finally
			{
				connection.Close();
			}
		}

		public bool UpdateTodo(TodoModel newTodoModel)
		{
			bool isUpdated = false;
			int numberOfRowsAffected = 0;
			try
			{
				connection.Open();
				SqlCommand command = new SqlCommand(
					$"UPDATE TODO SET {Constants.IS_COMPLETED} = {Constants.IS_COMPLETED_PARAM}, {Constants.TITLE} = {Constants.TITLE_PARAM} where {Constants.ID} = {Constants.ID_PARAM};",
					connection);
				command.Parameters.Add(Constants.ID_PARAM, SqlDbType.NVarChar).Value = newTodoModel.Id;
				command.Parameters.Add(Constants.IS_COMPLETED_PARAM, SqlDbType.Bit).Value = newTodoModel.isComplete;
				command.Parameters.Add(Constants.TITLE_PARAM, SqlDbType.NVarChar).Value = newTodoModel.Title;
				numberOfRowsAffected = command.ExecuteNonQuery();
				isUpdated = numberOfRowsAffected > 0 ? true : false;
				return isUpdated;
			}
			catch (Exception ex)
			{
				// Log the exception here
				return isUpdated;
			}
			finally
			{
				connection.Close();
			}
		}
	}
}