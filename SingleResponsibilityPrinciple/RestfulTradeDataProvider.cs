using SingleResponsibilityPrinciple.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple
{
    public class RestfulTradeDataProvider : ITradeDataProvider
    {
        private readonly string url;
        private readonly ILogger logger;

        public RestfulTradeDataProvider(string url, ILogger logger)
        {
            this.url = url;
            this.logger = logger;
        }

        public IEnumerable<string> GetTradeData()
        {
            return Task.Run(async () =>
            {
                List<string> tradeData = new List<string>();
                logger.LogInfo("Reading trades from RESTful API: " + url);

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (!response.IsSuccessStatusCode)
                    {
                        logger.LogWarning($"Failed to retrieve data. Status code: {response.StatusCode}");
                        throw new Exception($"Failed to retrieve data from URL: {url}");
                    }

                    string jsonContent = await response.Content.ReadAsStringAsync();
                    tradeData = JsonSerializer.Deserialize<List<string>>(jsonContent);

                    return tradeData;
                }
            }).Result;
        }
    }
}