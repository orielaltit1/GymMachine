using System.Text.Json.Serialization;

namespace WebGymMachineStore
{
    public class Exercise
    {
        [JsonPropertyName("ExerciseName")]
        public string ExerciseName { get; set; }

        [JsonPropertyName("ExerciseDescription")]
        public string ExerciseDescription { get; set; }

        [JsonPropertyName("ExerciseImage")]
        public string ExerciseImage { get; set; }
    }

    public class OpenRouterResponse
    {
        [JsonPropertyName("choices")]
        public List<Choice> Choices { get; set; }
    }

    public class Choice
    {
        [JsonPropertyName("message")]
        public Message Message { get; set; }
    }

    public class Message
    {
        [JsonPropertyName("content")]
        public string Content { get; set; }
    }

}
