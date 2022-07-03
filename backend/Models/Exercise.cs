using System.Collections.Generic;

namespace backend.Models
{
	public class Exercise
	{
		public int IdExercise { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Status { get; set; }
		public ICollection<Operation> Operations { get; set; }
	}
}

