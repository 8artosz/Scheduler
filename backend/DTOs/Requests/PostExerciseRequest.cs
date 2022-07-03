using System;
namespace backend.DTOs.Requests
{
    public class PostExerciseRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}

