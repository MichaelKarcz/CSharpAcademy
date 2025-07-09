using Newtonsoft.Json;
using RestSharp;
using ShiftLogger.Models;

namespace ShiftLogger.Services
{
    internal static class ShiftLoggerAPIService
    {
        private static readonly string serviceAddress = "https://localhost/7099/api/";

        #region Worker Methods

        internal static List<Worker> GetAllWorkers()
        {
            RestClient Client = new RestClient(serviceAddress);
            RestRequest request = new RestRequest("/Worker/");
            var response = Client.ExecuteAsync(request);

            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawResponse = response.Result.Content;
                var serialize = JsonConvert.DeserializeObject<Workers>(rawResponse);

                List<Worker> workers = serialize.WorkersList;

                return workers;
            }

            else return new List<Worker>();
        }

        #endregion Worker Methods
    }
}
