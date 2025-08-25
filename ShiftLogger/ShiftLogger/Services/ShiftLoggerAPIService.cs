using Newtonsoft.Json;
using RestSharp;
using ShiftLogger.Contracts.Responses.Shifts;
using ShiftLogger.Contracts.Responses.Workers;
using ShiftLogger.Contracts.Requests.Shifts;
using ShiftLogger.Contracts.Requests.Workers;

namespace ShiftLogger.Services;
internal static class ShiftLoggerAPIService
{
    private static readonly string serviceAddress = "https://localhost:7099/api/";

    #region Worker Methods

    internal static bool CreateWorker(CreateWorkerRequest worker)
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

    internal static List<WorkerResponse> GetAllWorkers()
    {
        RestClientOptions options = new RestClientOptions(serviceAddress);
        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest("Worker");
        var response = client.ExecuteAsync(request);

        if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            string rawResponse = response.Result.Content;
            List<WorkerResponse>? workers = JsonConvert.DeserializeObject<List<WorkerResponse>>(rawResponse);

            if (workers == null) workers = new List<WorkerResponse>();

            return workers;
        }
        else return new List<WorkerResponse>();
    }

    internal static WorkerResponse GetWorkerById(int workerId)
    {
        RestClientOptions options = new RestClientOptions(serviceAddress);
        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest($"Worker/{workerId}");
        var response = client.ExecuteAsync(request);
        if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            string? rawResponse = response.Result.Content;
            if (string.IsNullOrEmpty(rawResponse)) return new WorkerResponse();
            WorkerResponse? worker = JsonConvert.DeserializeObject<WorkerResponse>(rawResponse);

            if (worker == null) return new WorkerResponse();

            return worker;
        }
        else return new WorkerResponse();
    }

    #endregion Worker Methods

    #region Shift Methods

    internal static List<ShiftResponse> GetAllShiftsForWorker(WorkerResponse worker)
    {
        RestClientOptions options = new RestClientOptions(serviceAddress);
        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest($"Shift/{worker.Id}");
        var response = client.ExecuteAsync(request);

        if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            string rawResponse = response.Result.Content;
            List<ShiftResponse>? shifts = JsonConvert.DeserializeObject<List<ShiftResponse>>(rawResponse);

            if (shifts == null) shifts = new List<ShiftResponse>();

            return shifts;
        }
        else return new List<ShiftResponse>();
    }

    #endregion Shift Methods
}
