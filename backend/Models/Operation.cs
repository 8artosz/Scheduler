namespace backend.Models
{
	public class Operation
	{
		public int IdOperation { get; set; }
		public string Description { get; set; }
		public int TimeSpent { get; set; }
		public Exercise Exercise { get; set; }
		public int IdExercise { get; set; }
	}
}

