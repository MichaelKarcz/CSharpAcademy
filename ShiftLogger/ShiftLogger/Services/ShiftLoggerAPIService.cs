using Newtonsoft.Json;
using RestSharp;
using ShiftLogger.Models;

namespace ShiftLogger.Services;
internal static class ShiftLoggerAPIService
{
    private static readonly string serviceAddress = "https://localhost:7099/api/";

    #region Worker Methods

    internal static bool CreateWorker(Worker worker)
    {
        RestClientOptions options = new RestClientOptions(serviceAddress);
        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest("Worker");
        request.AddJsonBody(JsonConvert.SerializeObject(worker));
        var response = client.ExecutePostAsync(request);

        if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return true;
        }
        if (response.Result.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            return false;
        }

        return false;
    }

    internal static List<Worker> GetAllWorkers()
    {
        RestClientOptions options = new RestClientOptions(serviceAddress);
        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest("Worker");
        var response = client.ExecuteAsync(request);

        if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            string rawResponse = response.Result.Content;
            List<Worker>? workers = JsonConvert.DeserializeObject<List<Worker>>(rawResponse);

            if (workers == null) workers = new List<Worker>();

            return workers;
        }

        else return new List<Worker>();
    }

    #endregion Worker Methods
}
