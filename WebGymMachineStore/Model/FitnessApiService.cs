using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace WebGymMachineStore
{
    public class FitnessApiService
    {
        HttpClient client;

        public FitnessApiService(HttpClient httpClient)
        {
            client = httpClient;
        }
        private readonly string _apiKey = "";
        private readonly string _model = "openrouter/free";
        private readonly string _url = "https://openrouter.ai/api/v1/chat/completions";
        private readonly string imagesApiKey = "";
        private readonly string googleApiKey = "";
        public async Task<List<Exercise>> GetExercisesForMachineAsync(string machineName)
        {
            // הגדרת כותרת האימות
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            // בניית גוף הבקשה
            // אנו מנחים את המודל ב-System Prompt להחזיר *רק* מערך JSON נקי
            var requestBody = new
            {
                model = _model,
                messages = new[]
                {
                    new
                    {
                        role = "system",
                        content = @"You are a fitness expert.
                        The user will provide a gym machine name.
                        You MUST respond ONLY with a raw JSON array of objects.
                        Each object must have a 'ExerciseName' (string) ,
                        'ExerciseImage (string)' and 'ExerciseDescription' (string) of an exercises
                        that can be done on this machine.
                        If you dont now any photo of this exercise return an empty string.
                        At least five exercises.    
                        Do not include markdown code blocks,
                        explanations, or any extra text."
                    },
                    new
                    {
                        role = "user",
                        content = $"Machine: {machineName}"
                    }
                }
            };

            string jsonBody = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(_url, content);
           // response.EnsureSuccessStatusCode(); // יזרוק שגיאה אם הבקשה נכשלה

            string responseJson = await response.Content.ReadAsStringAsync();

            // המרת התשובה מהשרת לאובייקט C#
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var apiResponse = JsonSerializer.Deserialize<OpenRouterResponse>(responseJson, options);

            string messageContent = apiResponse?.Choices?[0]?.Message?.Content;

            if (string.IsNullOrWhiteSpace(messageContent))
            {
                return new List<Exercise>();
            }

            // ניקוי של שאריות Markdown (לפעמים מודלים חינמיים מתעקשים להוסיף ```json למרות ההוראות)
            messageContent = messageContent.Trim();
            if (messageContent.StartsWith("```json", StringComparison.OrdinalIgnoreCase))
            {
                messageContent = messageContent.Substring(7);
            }
            else if (messageContent.StartsWith("```", StringComparison.OrdinalIgnoreCase))
            {
                messageContent = messageContent.Substring(3);
            }

            if (messageContent.EndsWith("```", StringComparison.OrdinalIgnoreCase))
            {
                messageContent = messageContent.Substring(0, messageContent.Length - 3);
            }

            // המרת ה-JSON של התרגילים חזרה לרשימה של אובייקטים
            var exercises = JsonSerializer.Deserialize<List<Exercise>>(messageContent.Trim(), options);

            return exercises ?? null;
        }
    }
}
