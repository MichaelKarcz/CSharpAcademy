using Newtonsoft.Json;
using RestSharp;
using ShiftLogger.Models;

namespace ShiftLogger.Services;
internal static class ShiftLoggerAPIService
{
    private static readonly string serviceAddress = "http://localhost:7099/api/";

    #region Worker Methods

    internal static List<Worker> GetAllWorkers()
    {
        var options = new RestClientOptions(serviceAddress);
        RestClient Client = new RestClient(options);
        RestRequest request = new RestRequest("Worker");
        var response = Client.ExecuteAsync(request);

        if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            string rawResponse = response.Result.Content;
            var serialize = JsonConvert.DeserializeObject<Workers>(rawResponse);

            List<Worker> workers = serialize.WorkersList;

            Console.Write("Success!");
            if (workers != null && workers.Count == 0)
            {
                Console.WriteLine("No workers to return");
            }

            return workers;
        }

        else return new List<Worker>();
    }

    #endregion Worker Methods
}
