﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodosModel
{
	public class TodoModel
	{
		/// <summary>
		/// This is the id of one Todo
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// This is the Title
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Tells if the Todo is completed or not
		/// </summary>
		public bool isComplete { get; set; }
	}
}