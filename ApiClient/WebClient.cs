using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApiClient
{
    public class WebClient<T> : IWebClient<T>
    {
        HttpClient httpClient; // מי שמעביר את הבקשות ומקבל את התשובות
        //אובייקט שמכיל את ההודעה שאנחנו רוצים לשלוח
        UriBuilder uriBuilder;//יודע להרכיב את הכתובת של הבקשה  
        

        public WebClient()
        {
            this.httpClient = new HttpClient();
            this.uriBuilder = new UriBuilder();
        }
        public string Schema 
        {
            set
            {
                this.uriBuilder.Scheme = value;
            } 

        }
        public string Host
        {
            set
            {
                this.uriBuilder.Host = value;
            }
        }
        public int Port
        {
            set
            {
                this.uriBuilder.Port = value;
            }
        }
        public string Path
        {
            set
            {
                this.uriBuilder.Path = value;
            }
        }
        public void AddParameter(string key, string value)
        {
            if(this.uriBuilder.Query != string.Empty)
            {
                this.uriBuilder.Query += "&" + key + "=" + value;
            }
            else
            {
                this.uriBuilder.Query += key + "=" + value;
            }

        }

        public T Get()//רוצים לקחת משהו מהווב סרוויס
        {
            using(HttpRequestMessage requestMessage = new HttpRequestMessage())
            {
                requestMessage.Method = HttpMethod.Get;//
                requestMessage.RequestUri = this.uriBuilder.Uri;
                using(HttpResponseMessage responseMessage = this.httpClient.SendAsync(requestMessage).Result)
                {
                    if(responseMessage.IsSuccessStatusCode == true)
                    {
                        string result = responseMessage.Content.ReadAsStringAsync().Result;
                        JsonSerializerOptions options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };

                        if (responseMessage.StatusCode == HttpStatusCode.NoContent)
                        {
                            return default(T);
                        }
                        T data = JsonSerializer.Deserialize<T>(result, options);
                        return data;
                    }
                    else
                    {
                        return default(T); 
                    }
                }
            }
        }

        public bool Post(T data)
        {
            using (HttpRequestMessage requestMessage = new HttpRequestMessage())
            {
                requestMessage.Method = HttpMethod.Post;//
                requestMessage.RequestUri = this.uriBuilder.Uri;//כתובת של בקשה
                string jsonData = JsonSerializer.Serialize(data);
                requestMessage.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                using(HttpResponseMessage response = this.httpClient.SendAsync(requestMessage).Result)
                {
                    return response.IsSuccessStatusCode;
                }

            }
        }

        public bool Post(T data, Stream file)
        {
            using (HttpRequestMessage requestMessage = new HttpRequestMessage())
            {
                requestMessage.Method = HttpMethod.Post;//
                requestMessage.RequestUri = this.uriBuilder.Uri;//כתובת של בקשה
                MultipartFormDataContent multiPartContent = new MultipartFormDataContent();
                string jsonData = JsonSerializer.Serialize(data);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                multiPartContent.Add(stringContent, "data");
                StreamContent fileContent = new StreamContent(file);
                multiPartContent.Add(fileContent, "file", "fileName");
                requestMessage.Content = multiPartContent;
                using (HttpResponseMessage responseMessage = this.httpClient.SendAsync(requestMessage).Result)
                {
                    return responseMessage.IsSuccessStatusCode;
                }
            }
        }

        public bool Post(T data, List<Stream> files)
        {
            using (HttpRequestMessage requestMessage = new HttpRequestMessage())
            {
                requestMessage.Method = HttpMethod.Post;//
                requestMessage.RequestUri = this.uriBuilder.Uri;//כתובת של בקשה
                MultipartFormDataContent multiPartContent = new MultipartFormDataContent();//מזוודה
                string jsonData = JsonSerializer.Serialize(data);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                multiPartContent.Add(stringContent, "data");
                foreach(var file in files)
                {
                    StreamContent fileContent = new StreamContent(file);
                    multiPartContent.Add(fileContent, "file", "fileName");

                }
                requestMessage.Content = multiPartContent;
                using(HttpResponseMessage responseMessage = this.httpClient.SendAsync(requestMessage).Result)
                {
                    return responseMessage.IsSuccessStatusCode;
                }
            }
        }

        public async Task<T> GetAsync()
        {
            using (HttpRequestMessage requestMessage = new HttpRequestMessage())
            {
                requestMessage.Method = HttpMethod.Get;//
                requestMessage.RequestUri = this.uriBuilder.Uri;
                using (HttpResponseMessage responseMessage = await this.httpClient.SendAsync(requestMessage))
                {
                    if (responseMessage.IsSuccessStatusCode == true)
                    {
                        string result = await responseMessage.Content.ReadAsStringAsync();
                        JsonSerializerOptions options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        T data = JsonSerializer.Deserialize<T>(result,options);
                        return data;
                    }
                    else
                    {
                        return default(T);
                    }
                }
            }
        }

        public async Task<bool> PostAsync(T data)
        {
            using (HttpRequestMessage requestMessage = new HttpRequestMessage())
            {
                requestMessage.Method = HttpMethod.Post;//
                requestMessage.RequestUri = this.uriBuilder.Uri;//כתובת של בקשה
                string jsonData = JsonSerializer.Serialize(data);
                requestMessage.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                using (HttpResponseMessage response = await this.httpClient.SendAsync(requestMessage))
                {
                    return response.IsSuccessStatusCode;
                }

            }
        }

        public async Task<bool> PostAsync(bool data, Stream file)
        {
            using (HttpRequestMessage requestMessage = new HttpRequestMessage())
            {
                requestMessage.Method = HttpMethod.Post;//
                requestMessage.RequestUri = this.uriBuilder.Uri;//כתובת של בקשה
                MultipartFormDataContent multiPartContent = new MultipartFormDataContent();
                string jsonData = JsonSerializer.Serialize(data);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                multiPartContent.Add(stringContent, "data");
                StreamContent fileContent = new StreamContent(file);
                multiPartContent.Add(fileContent, "file", "fileName");
                requestMessage.Content = multiPartContent;
                using (HttpResponseMessage responseMessage = this.httpClient.SendAsync(requestMessage).Result)
                {
                    return responseMessage.IsSuccessStatusCode;
                }
            }
        }

        public async Task<bool> PostAsync(T data, List<Stream> files)
        {
            using (HttpRequestMessage requestMessage = new HttpRequestMessage())
            {
                requestMessage.Method = HttpMethod.Post;//
                requestMessage.RequestUri = this.uriBuilder.Uri;//כתובת של בקשה
                MultipartFormDataContent multiPartContent = new MultipartFormDataContent();//מזוודה
                string jsonData = JsonSerializer.Serialize(data);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                multiPartContent.Add(stringContent, "data");
                foreach (var file in files)
                {
                    StreamContent fileContent = new StreamContent(file);
                    multiPartContent.Add(fileContent, "file", "fileName");

                }
                requestMessage.Content = multiPartContent;
                using (HttpResponseMessage responseMessage = this.httpClient.SendAsync(requestMessage).Result)
                {
                    return responseMessage.IsSuccessStatusCode;
                }
            }
        }

        
    }
}
