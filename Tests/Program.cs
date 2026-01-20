using Models;
using Models.Models;
using System.Net;
using System.Security.Cryptography;
using System.Text.Json;
using WebApiClient;

namespace Tests
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();
            TestWebClient();
            Console.ReadLine();
        }
        
        static void TestWebClient()
        {
            WebClient<MachineViewModel> webClient = new WebClient<MachineViewModel>();
            webClient.Schema = "http";
            webClient.Host = "localhost";
            webClient.Port = 5138;
            webClient.Path = "Api/Guest/GetMachineView";
            webClient.AddParameter("id", "15");
            MachineViewModel machine = webClient.Get();
            Console.WriteLine(machine.Machine.MachineName);
            Console.WriteLine(machine.Machine.MachineId);
            Console.WriteLine(machine.Machine.MachineDescription);
            Console.WriteLine(machine.SelectAmount);
        
        }

        static string GanerateSalt()//מחשב את המלח
        {
            byte[] saltByte = new byte[16];
            RandomNumberGenerator.Fill(saltByte);
            return Convert.ToBase64String(saltByte);
        }

        static string CalculateHash(string password, string salt)
        {
            string s = password + salt;
            byte[] pass = System.Text.Encoding.UTF8.GetBytes(s);//UTF8 טבלה של הסימנים(כל האותיות מכל השפות וסימנים כמו שטרודל והאשטאג ועוד)
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(pass);
                return Convert.ToBase64String(bytes);
            }
        }

        static void NewHash()
        {
            string pass = "Yona456!";
            string salt = GanerateSalt();
            Console.WriteLine(salt);
            string hash = CalculateHash(pass, salt);
            Console.WriteLine(hash);
        }

        static void TestWeb()
        {
            List<Currency> List = CurrencyListTest().Result;
            int count = 1;
            foreach (Currency currency in List)
            {
                Console.WriteLine($"{count} {currency.symbol} - {currency.name}");
                count++;
            }
            Console.Write("Select Currency Number From - ");
            int from = int.Parse(Console.ReadLine());
            Console.Write("Select Currency Number To - ");
            int to = int.Parse(Console.ReadLine());
            Console.Write("Enter Sum - ");
            double sum = double.Parse(Console.ReadLine());
            ConverResult r = GetResult(List[from - 1].symbol, List[to - 1].symbol, sum).Result;
            Console.WriteLine($"{r.result.amountToConvert} {r.result.from}  =  {Math.Round(r.result.convertedAmount, 2)} {r.result.to}");
            Console.ReadLine();
        }
        static void TestModelValidationClient()
        {
            Client client = new Client();
            client.ClientFirstName = "john";
            client.ClientLastName = "Css";
            client.ClientEmail = "orielaltit8@gmail.com";
            client.ClientPassword = "";


            Dictionary<string, List<string>> errors = client.AllErrors();
            if(client.IsValid == false)
            {
                foreach(var error in errors)
                {
                    foreach(var errorMsg in error.Value)
                    {
                        Console.WriteLine(errorMsg);
                    }
                }
            }



        }

        static  async Task<ConverResult> GetResult(string from, string to, double amount)
        {
            
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://currency-converter18.p.rapidapi.com/api/v1/convert?from={from}&to={to}&amount={amount}"),
                Headers =
                {
                    { "x-rapidapi-key", "0fed6f663fmshbdb9ad184fe6ae5p12fa92jsn1cd4294798e3" },
                    { "x-rapidapi-host", "currency-converter18.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ConverResult>(body);
            }
        }
        static async Task<List<Currency>> CurrencyListTest() 
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://currency-converter18.p.rapidapi.com/api/v1/supportedCurrencies"),
                Headers =
                {
                    { "x-rapidapi-key", "0fed6f663fmshbdb9ad184fe6ae5p12fa92jsn1cd4294798e3" },
                    { "x-rapidapi-host", "currency-converter18.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Currency>>(body);
            }
        }
    }
}
