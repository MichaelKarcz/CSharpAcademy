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

    internal async static Task<bool> CreateWorkerAsync(CreateWorkerRequest worker)
    {
        RestClientOptions options = new RestClientOptions(serviceAddress);
        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest("Worker");
        request.AddJsonBody(JsonConvert.SerializeObject(worker));
        var response = await client.ExecutePostAsync(request);

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return true;
        }
        else return false;
    }

    internal async static Task<List<WorkerResponse>> GetAllWorkersAsync()
    {
        RestClientOptions options = new RestClientOptions(serviceAddress);
        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest("Worker");
        var response = await client.ExecuteAsync(request);

        if (response.StatusCode == System.Net.HttpStatusCode.OK && !string.IsNullOrEmpty(response.Content))
        {
            string rawResponse = response.Content;
            List<WorkerResponse>? workers = JsonConvert.DeserializeObject<List<WorkerResponse>>(rawResponse);

            if (workers == null) workers = new List<WorkerResponse>();

            return workers;
        }
        else return new List<WorkerResponse>();
    }

    internal async static Task<WorkerResponse?> GetWorkerByIdAsync(int workerId)
    {
        RestClientOptions options = new RestClientOptions(serviceAddress);
        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest($"Worker/{workerId}");
        var response = await client.ExecuteAsync(request);
        if (response.StatusCode == System.Net.HttpStatusCode.OK && !string.IsNullOrEmpty(response.Content))
        {
            string? rawResponse = response.Content;
            WorkerResponse? worker = JsonConvert.DeserializeObject<WorkerResponse>(rawResponse);

            return worker;
        }
        else return null;
    }

    internal async static Task<WorkerResponse?> UpdateWorkerAsync(int workerId, UpdateWorkerRequest updateWorkerRequest)
    {
        RestClientOptions options = new RestClientOptions(serviceAddress);
        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest($"Worker/{workerId}", Method.Put);
        request.AddJsonBody(JsonConvert.SerializeObject(updateWorkerRequest));
        var response = await client.ExecutePutAsync(request);
        if (response.StatusCode == System.Net.HttpStatusCode.OK && !string.IsNullOrEmpty(response.Content))
        {
            string? rawResponse = response.Content;
            WorkerResponse? worker = JsonConvert.DeserializeObject<WorkerResponse>(rawResponse);

            return worker;
        }
        else return null;
    }

    internal async static Task<bool> DeleteWorkerAsync(int workerId)
    {
        RestClientOptions options = new RestClientOptions(serviceAddress);
        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest($"Worker/{workerId}", Method.Delete);
        var response = await client.ExecuteDeleteAsync(request);
        if (response.StatusCode == System.Net.HttpStatusCode.OK && !string.IsNullOrEmpty(response.Content))
        {
            return true;
        }
        else return false;
    }

    #endregion Worker Methods

    #region Shift Methods

    internal async static Task<bool> CreateShiftAsync(CreateShiftRequest shift)
    {
        RestClientOptions options = new RestClientOptions(serviceAddress);
        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest("Shift");
        request.AddJsonBody(JsonConvert.SerializeObject(shift));
        var response = await client.ExecutePostAsync(request);

        if (response.StatusCode == System.Net.HttpStatusCode.OK && !string.IsNullOrEmpty(response.Content))
        {
            return true;
        }
        else return false;
    }

    internal async static Task<List<ShiftResponse>> GetAllShiftsForWorkerAsync(WorkerResponse worker)
    {
        RestClientOptions options = new RestClientOptions(serviceAddress);
        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest($"Shift/{worker.Id}");
        var response = await client.ExecuteAsync(request);

        if (response.StatusCode == System.Net.HttpStatusCode.OK && !string.IsNullOrEmpty(response.Content))
        {
            string rawResponse = response.Content;
            List<ShiftResponse>? shifts = JsonConvert.DeserializeObject<List<ShiftResponse>>(rawResponse);

            if (shifts == null) shifts = new List<ShiftResponse>();

            return shifts;
        }
        else return new List<ShiftResponse>();
    }

    internal async static Task<ShiftResponse?> GetUnfinishedShiftForWorkerAsync(WorkerResponse worker)
    {
        RestClientOptions options = new RestClientOptions(serviceAddress);
        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest($"Shift/unfinished/{worker.Id}");
        var response = await client.ExecuteAsync(request);

        if (response.StatusCode == System.Net.HttpStatusCode.OK && !string.IsNullOrEmpty(response.Content))
        {
            string rawResponse = response.Content;
            ShiftResponse? unfinishedShift = JsonConvert.DeserializeObject<ShiftResponse>(rawResponse);
            return unfinishedShift;
        }
        else return null;
    }

    internal async static Task<ShiftResponse?> UpdateShiftAsync(int shiftId, UpdateShiftRequest updateShiftRequest)
    {
        RestClientOptions options = new RestClientOptions(serviceAddress);
        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest($"Shift/{shiftId}", Method.Put);
        request.AddJsonBody(JsonConvert.SerializeObject(updateShiftRequest));
        var response = await client.ExecutePutAsync(request);
        if (response.StatusCode == System.Net.HttpStatusCode.OK && !string.IsNullOrEmpty(response.Content))
        {
            string? rawResponse = response.Content;
            ShiftResponse? shift = JsonConvert.DeserializeObject<ShiftResponse>(rawResponse);

            return shift;
        }
        else return null;
    }

    internal async static Task<bool> DeleteShiftAsync(int shiftId)
    {
        RestClientOptions options = new RestClientOptions(serviceAddress);
        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest($"Shift/{shiftId}", Method.Delete);
        var response = await client.ExecuteDeleteAsync(request);
        if (response.StatusCode == System.Net.HttpStatusCode.OK && !string.IsNullOrEmpty(response.Content))
        {
            return true;
        }
        else return false;
    }

    #endregion Shift Methods
}
