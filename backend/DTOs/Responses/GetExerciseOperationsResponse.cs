using System;
namespace backend.DTOs.Responses
{
    public class GetExerciseOperationsResponse
    {
        public int IdOperation { get; set; }
        public int TimeSpent { get; set; }
        public string Description { get; set; }
    }
}

