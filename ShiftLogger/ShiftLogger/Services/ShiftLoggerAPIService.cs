using Newtonsoft.Json;
using RestSharp;
using ShiftLogger.Contracts.Responses.Shifts;
using ShiftLogger.Contracts.Responses.Workers;
using ShiftLogger.Contracts.Requests.Shifts;
using ShiftLogger.Contracts.Requests.Workers;
using Spectre.Console;

namespace ShiftLogger.Console.Services;
internal static class ShiftLoggerApiService
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

    internal static WorkerResponse? GetWorkerById(int workerId)
    {
        RestClientOptions options = new RestClientOptions(serviceAddress);
        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest($"Worker/{workerId}");
        var response = client.ExecuteAsync(request);
        if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            string? rawResponse = response.Result.Content;
            if (string.IsNullOrEmpty(rawResponse)) return null;
            WorkerResponse? worker = JsonConvert.DeserializeObject<WorkerResponse>(rawResponse);

            return worker;
        }
        else return null;
    }

    internal static WorkerResponse? UpdateWorker(int workerId, UpdateWorkerRequest updateWorkerRequest)
    {
        RestClientOptions options = new RestClientOptions(serviceAddress);
        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest($"Worker/{workerId}", Method.Put);
        request.AddJsonBody(JsonConvert.SerializeObject(updateWorkerRequest));
        var response = client.ExecutePutAsync(request);
        if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            string? rawResponse = response.Result.Content;
            if (string.IsNullOrEmpty(rawResponse)) return null;
            WorkerResponse? worker = JsonConvert.DeserializeObject<WorkerResponse>(rawResponse);

            return worker;
        }
        else return null;
    }

    internal static bool DeleteWorker(int workerId)
    {
        RestClientOptions options = new RestClientOptions(serviceAddress);
        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest($"Worker/{workerId}", Method.Delete);
        var response = client.ExecuteDeleteAsync(request);
        if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            string? rawResponse = response.Result.Content;
            if (string.IsNullOrEmpty(rawResponse)) return false;
        }
        return true;
    }

    #endregion Worker Methods

    #region Shift Methods

    internal static bool CreateShift(CreateShiftRequest shift)
    {
        RestClientOptions options = new RestClientOptions(serviceAddress);
        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest("Shift");
        request.AddJsonBody(JsonConvert.SerializeObject(shift));
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

    internal static ShiftResponse? GetUnfinishedShiftForWorker(WorkerResponse worker)
    {
        RestClientOptions options = new RestClientOptions(serviceAddress);
        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest($"Shift/unfinished/{worker.Id}");
        var response = client.ExecuteAsync(request);

        if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            string rawResponse = response.Result.Content;
            ShiftResponse? unfinishedShift = JsonConvert.DeserializeObject<ShiftResponse>(rawResponse);
            return unfinishedShift;
        }
        else return null;
    }

    internal static ShiftResponse? UpdateShift(int shiftId, UpdateShiftRequest updateShiftRequest)
    {
        RestClientOptions options = new RestClientOptions(serviceAddress);
        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest($"Shift/{shiftId}", Method.Put);
        request.AddJsonBody(JsonConvert.SerializeObject(updateShiftRequest));
        var response = client.ExecutePutAsync(request);
        if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            string? rawResponse = response.Result.Content;
            if (string.IsNullOrEmpty(rawResponse)) return null;
            ShiftResponse? shift = JsonConvert.DeserializeObject<ShiftResponse>(rawResponse);

            return shift;
        }
        else return null;
    }

    internal static bool DeleteShift(int shiftId)
    {
        RestClientOptions options = new RestClientOptions(serviceAddress);
        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest($"Shift/{shiftId}", Method.Delete);
        var response = client.ExecuteDeleteAsync(request);
        if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            string? rawResponse = response.Result.Content;
            if (string.IsNullOrEmpty(rawResponse)) return false;
        }
        return true;
    }

    #endregion Shift Methods
}
