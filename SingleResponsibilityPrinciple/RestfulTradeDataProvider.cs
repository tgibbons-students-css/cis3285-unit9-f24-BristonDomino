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
            List<string> tradeData = new List<string>();
            logger.LogInfo($"Fetching trade data from RESTful API at {url}");

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(url).Result;
                if (!response.IsSuccessStatusCode)
                {
                    logger.LogWarning($"Failed to retrieve data. Status code: {response.StatusCode}");
                    throw new Exception($"Failed to retrieve data from URL: {url}");
                }

                string jsonData = response.Content.ReadAsStringAsync().Result;
                tradeData = JsonSerializer.Deserialize<List<string>>(jsonData);

                return tradeData;
            }           
        }


       // public IEnumerable<string> GetTradeData()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
