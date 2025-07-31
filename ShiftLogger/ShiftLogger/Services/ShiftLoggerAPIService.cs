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

    internal static Worker GetWorkerById(int workerId)
    {
        RestClientOptions options = new RestClientOptions(serviceAddress);
        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest($"Worker/{workerId}");
        var response = client.ExecuteAsync(request);
        if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            string? rawResponse = response.Result.Content;
            if (string.IsNullOrEmpty(rawResponse)) return new Worker();
            Worker? worker = JsonConvert.DeserializeObject<Worker>(rawResponse);

            if (worker == null) return new Worker();

            return worker;
        }
        else return new Worker();
    }

    #endregion Worker Methods

    #region Shift Methods

    internal static List<Shift> GetAllShiftsForWorker(Worker worker)
    {
        RestClientOptions options = new RestClientOptions(serviceAddress);
        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest($"Shift/{worker.Id}");
        var response = client.ExecuteAsync(request);

        if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            string rawResponse = response.Result.Content;
            List<Shift>? shifts = JsonConvert.DeserializeObject<List<Shift>>(rawResponse);

            if (shifts == null) shifts = new List<Shift>();

            return shifts;
        }
        else return new List<Shift>();
    }

    #endregion Shift Methods
}
