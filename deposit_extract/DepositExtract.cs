using System;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace deposit_extract
{
    public class DepositExtract
    {
        [FunctionName("DepositExtract")]
        public async Task RunAsync([TimerTrigger("0 2 * * *")]TimerInfo myTimer, ILogger log)
        {
            string apiKey = Environment.GetEnvironmentVariable("API_KEY");
            string apiUrl = Environment.GetEnvironmentVariable("API_URL");
            await GetResponseFromApi(apiUrl, apiKey);
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
        public static async Task<string> GetResponseFromApi(string url, string apiKey)
        {
            using (HttpClient client = new HttpClient())
            {
                // Ajouter l'en-t�te API-KEY � la demande
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("API-KEY", apiKey);

                // Faire la requ�te HTTP GET et r�cup�rer la r�ponse
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                return responseBody;
            }
        }

    }
}
