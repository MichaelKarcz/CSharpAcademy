using Newtonsoft.Json;
using RestSharp;
using ShiftLogger.Contracts.Responses.Shifts;
using ShiftLogger.Contracts.Responses.Workers;
using ShiftLogger.Contracts.Requests.Shifts;
using ShiftLogger.Contracts.Requests.Workers;
using ShiftLogger.Console.Helpers;

namespace ShiftLogger.Console.Services;
internal class ShiftLoggerApiService
{
    private readonly string serviceAddress = "https://localhost:7099/api/";

    #region Worker Methods

    internal async Task<ServiceResult<WorkerResponse>> CreateWorkerAsync(CreateWorkerRequest worker)
    {
        RestClientOptions options = new RestClientOptions(serviceAddress);
        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest("Worker");
        request.AddJsonBody(JsonConvert.SerializeObject(worker));

        try
        {
            var response = await client.ExecutePostAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK && !string.IsNullOrEmpty(response.Content))
            {
                try
                {
                    WorkerResponse? addedWorker = JsonConvert.DeserializeObject<WorkerResponse>(response.Content);
                    return new ServiceResult<WorkerResponse>
                    {
                        Success = true,
                        Data = addedWorker
                    };
                }
                catch (JsonException ex)
                {
                    return new ServiceResult<WorkerResponse>
                    {
                        Success = false,
                        ErrorMessage = $"Failed to parse created worker data: {ex.Message}"
                    };
                }
            }
            else
            {
                return new ServiceResult<WorkerResponse>
                {
                    Success = false,
                    ErrorMessage = DisplayHelper.GetFormattedErrorMessage(response.StatusCode, response.Content),
                    StatusCode = (int)response.StatusCode
                };
            }
        }
        catch (HttpRequestException ex)
        {
            return new ServiceResult<WorkerResponse>
            {
                Success = false,
                ErrorMessage = $"Network error while creating worker: {ex.Message}"
            };
        }
        catch (TaskCanceledException ex)
        {
            return new ServiceResult<WorkerResponse>
            {
                Success = false,
                ErrorMessage = $"Request timeout while creating worker: {ex.Message}"
            };
        }
        catch (Exception ex)
        {
            return new ServiceResult<WorkerResponse>
            {
                Success = false,
                ErrorMessage = $"Unexpected error while creating worker: {ex.Message}"
            };
        }
    }

    internal async Task<ServiceResult<List<WorkerResponse>>> GetAllWorkersAsync()
    {
        RestClientOptions options = new RestClientOptions(serviceAddress);
        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest("Worker");
        
        try
        {
            var response = await client.ExecuteAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK && !string.IsNullOrEmpty(response.Content))
            {
                try
                {
                    string rawResponse = response.Content;
                    List<WorkerResponse>? workers = JsonConvert.DeserializeObject<List<WorkerResponse>>(rawResponse);

                    if (workers == null) workers = new List<WorkerResponse>();

                    return new ServiceResult<List<WorkerResponse>>
                    {
                        Success = true,
                        Data = workers
                    };
                }
                catch (JsonException ex)
                {
                    return new ServiceResult<List<WorkerResponse>>
                    {
                        Success = false,
                        ErrorMessage = $"Failed to parse retrieved worker data: {ex.Message}"
                    };
                }

            }
            else
            {
                return new ServiceResult<List<WorkerResponse>>
                {
                    Success = false,
                    ErrorMessage = DisplayHelper.GetFormattedErrorMessage(response.StatusCode, response.Content),
                    StatusCode = (int)response.StatusCode
                };
            }
        }
        catch (HttpRequestException ex)
        {
            return new ServiceResult<List<WorkerResponse>>
            {
                Success = false,
                ErrorMessage = $"Network error while retrieving the workers: {ex.Message}"
            };
        }
        catch (TaskCanceledException ex)
        {
            return new ServiceResult<List<WorkerResponse>>
            {
                Success = false,
                ErrorMessage = $"Request timeout while retrieving the workers: {ex.Message}"
            };
        }
        catch (Exception ex)
        {
            return new ServiceResult<List<WorkerResponse>>
            {
                Success = false,
                ErrorMessage = $"Unexpected error while retrieving the workers: {ex.Message}"
            };
        }
    }

    internal async Task<ServiceResult<WorkerResponse?>> GetWorkerByIdAsync(int workerId)
    {
        RestClientOptions options = new RestClientOptions(serviceAddress);
        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest($"Worker/{workerId}");
        
        try
        {
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK && !string.IsNullOrEmpty(response.Content))
            {
                try
                {
                    string? rawResponse = response.Content;
                    WorkerResponse? worker = JsonConvert.DeserializeObject<WorkerResponse>(rawResponse);

                    return new ServiceResult<WorkerResponse?>
                    {
                        Success = true,
                        Data = worker
                    };
                }
                catch (JsonException ex)
                {
                    return new ServiceResult<WorkerResponse?>
                    {
                        Success = false,
                        ErrorMessage = $"Failed to parse retrieved worker data: {ex.Message}"
                    };
                }

            }
            else
            {
                return new ServiceResult<WorkerResponse?>
                {
                    Success = false,
                    ErrorMessage = DisplayHelper.GetFormattedErrorMessage(response.StatusCode, response.Content),
                    StatusCode = (int)response.StatusCode
                };
            }

        }
        catch (HttpRequestException ex)
        {
            return new ServiceResult<WorkerResponse?>
            {
                Success = false,
                ErrorMessage = $"Network error while retrieving the worker: {ex.Message}"
            };
        }
        catch (TaskCanceledException ex)
        {
            return new ServiceResult<WorkerResponse?>
            {
                Success = false,
                ErrorMessage = $"Request timeout while retrieving the worker: {ex.Message}"
            };
        }
        catch (Exception ex)
        {
            return new ServiceResult<WorkerResponse?>
            {
                Success = false,
                ErrorMessage = $"Unexpected error while retrieving the worker: {ex.Message}"
            };
        }
    }

    internal async Task<ServiceResult<WorkerResponse?>> UpdateWorkerAsync(int workerId, UpdateWorkerRequest updateWorkerRequest)
    {
        RestClientOptions options = new RestClientOptions(serviceAddress);
        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest($"Worker/{workerId}", Method.Put);
        request.AddJsonBody(JsonConvert.SerializeObject(updateWorkerRequest));

        try
        {
            var response = await client.ExecutePutAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK && !string.IsNullOrEmpty(response.Content))
            {
                try
                {
                    string? rawResponse = response.Content;
                    WorkerResponse? worker = JsonConvert.DeserializeObject<WorkerResponse>(rawResponse);

                    return new ServiceResult<WorkerResponse?>
                    {
                        Success = true,
                        Data = worker
                    };
                }
                catch (JsonException ex)
                {
                    return new ServiceResult<WorkerResponse?>
                    {
                        Success = false,
                        ErrorMessage = $"Failed to parse updated worker data: {ex.Message}"
                    };
                }
            }
            else
            {
                return new ServiceResult<WorkerResponse?>
                {
                    Success = false,
                    ErrorMessage = DisplayHelper.GetFormattedErrorMessage(response.StatusCode, response.Content),
                    StatusCode = (int)response.StatusCode
                };
            }

        }
        catch (HttpRequestException ex)
        {
            return new ServiceResult<WorkerResponse?>
            {
                Success = false,
                ErrorMessage = $"Network error while updating the worker: {ex.Message}"
            };
        }
        catch (TaskCanceledException ex)
        {
            return new ServiceResult<WorkerResponse?>
            {
                Success = false,
                ErrorMessage = $"Request timeout while updating the worker: {ex.Message}"
            };
        }
        catch (Exception ex)
        {
            return new ServiceResult<WorkerResponse?>
            {
                Success = false,
                ErrorMessage = $"Unexpected error while updating the worker: {ex.Message}"
            };
        }
    }
    // -------------------------------------------------------------------------------------------------------
    internal async Task<ServiceResult<string>> DeleteWorkerAsync(int workerId)
    {
        RestClientOptions options = new RestClientOptions(serviceAddress);
        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest($"Worker/{workerId}", Method.Delete);
        
        try
        {
            var response = await client.ExecuteDeleteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK && !string.IsNullOrEmpty(response.Content))
            {
                try
                {
                    string? serviceMessageResponse = JsonConvert.DeserializeObject<string>(response.Content);
                    return new ServiceResult<string>
                    {
                        Success = true,
                        Data = serviceMessageResponse
                    };
                }
                catch (JsonException ex)
                {
                    return new ServiceResult<string>
                    {
                        Success = false,
                        ErrorMessage = $"Failed to parse the result of the worker deletion: {ex.Message}"
                    };
                }
            }
            else
            {
                return new ServiceResult<string>
                {
                    Success = false,
                    ErrorMessage = DisplayHelper.GetFormattedErrorMessage(response.StatusCode, response.Content),
                    StatusCode = (int)response.StatusCode
                };
            }
        }
        catch (HttpRequestException ex)
        {
            return new ServiceResult<string>
            {
                Success = false,
                ErrorMessage = $"Network error while deleting the worker: {ex.Message}"
            };
        }
        catch (TaskCanceledException ex)
        {
            return new ServiceResult<string>
            {
                Success = false,
                ErrorMessage = $"Request timeout while deleting the worker: {ex.Message}"
            };
        }
        catch (Exception ex)
        {
            return new ServiceResult<string>
            {
                Success = false,
                ErrorMessage = $"Unexpected error while deleting the worker: {ex.Message}"
            };
        }
    }

    #endregion Worker Methods

    #region Shift Methods

    internal async Task<ServiceResult<ShiftResponse>> CreateShiftAsync(CreateShiftRequest shift)
    {
        RestClientOptions options = new RestClientOptions(serviceAddress);
        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest("Shift");
        request.AddJsonBody(JsonConvert.SerializeObject(shift));
        
        try
        {
            var response = await client.ExecutePostAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK && !string.IsNullOrEmpty(response.Content))
            {
                try
                {
                    string? rawResponse = response.Content;
                    ShiftResponse? createdShift = JsonConvert.DeserializeObject<ShiftResponse>(rawResponse);

                    return new ServiceResult<ShiftResponse>
                    {
                        Success = true,
                        Data = createdShift
                    };
                }
                catch (JsonException ex)
                {
                    return new ServiceResult<ShiftResponse>
                    {
                        Success = false,
                        ErrorMessage = $"Failed to parse created shift data: {ex.Message}"
                    };
                }
            }
            else
            {
                return new ServiceResult<ShiftResponse>
                {
                    Success = false,
                    ErrorMessage = DisplayHelper.GetFormattedErrorMessage(response.StatusCode, response.Content),
                    StatusCode = (int)response.StatusCode
                };
            }
        }
        catch (HttpRequestException ex)
        {
            return new ServiceResult<ShiftResponse>
            {
                Success = false,
                ErrorMessage = $"Network error while adding the shift: {ex.Message}"
            };
        }
        catch (TaskCanceledException ex)
        {
            return new ServiceResult<ShiftResponse>
            {
                Success = false,
                ErrorMessage = $"Request timeout while adding the shift: {ex.Message}"
            };
        }
        catch (Exception ex)
        {
            return new ServiceResult<ShiftResponse>
            {
                Success = false,
                ErrorMessage = $"Unexpected error while adding the shift: {ex.Message}"
            };
        }
    }

    internal async Task<ServiceResult<List<ShiftResponse>>> GetAllShiftsForWorkerAsync(WorkerResponse worker)
    {
        RestClientOptions options = new RestClientOptions(serviceAddress);
        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest($"Shift/{worker.Id}");

        try
        {
            var response = await client.ExecuteAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK && !string.IsNullOrEmpty(response.Content))
            {
                try
                {
                    string rawResponse = response.Content;
                    List<ShiftResponse>? shifts = JsonConvert.DeserializeObject<List<ShiftResponse>>(rawResponse);

                    if (shifts == null) shifts = new List<ShiftResponse>();

                    return new ServiceResult<List<ShiftResponse>>
                    {
                        Success = true,
                        Data = shifts
                    };
                }
                catch (JsonException ex)
                {
                    return new ServiceResult<List<ShiftResponse>>
                    {
                        Success = false,
                        ErrorMessage = $"Failed to parse retrieved shift data: {ex.Message}"
                    };
                }
            }
            else
            {
                return new ServiceResult<List<ShiftResponse>>
                {
                    Success = false,
                    ErrorMessage = DisplayHelper.GetFormattedErrorMessage(response.StatusCode, response.Content),
                    StatusCode = (int)response.StatusCode
                };
            }
        }
        catch (HttpRequestException ex)
        {
            return new ServiceResult<List<ShiftResponse>>
            {
                Success = false,
                ErrorMessage = $"Network error while retrieving the shifts: {ex.Message}"
            };
        }
        catch (TaskCanceledException ex)
        {
            return new ServiceResult<List<ShiftResponse>>
            {
                Success = false,
                ErrorMessage = $"Request timeout while retrieving the shifts: {ex.Message}"
            };
        }
        catch (Exception ex)
        {
            return new ServiceResult<List<ShiftResponse>>
            {
                Success = false,
                ErrorMessage = $"Unexpected error while retrieving the shifts: {ex.Message}"
            };
        }

    }

    internal async Task<ServiceResult<ShiftResponse?>> GetUnfinishedShiftForWorkerAsync(WorkerResponse worker)
    {
        RestClientOptions options = new RestClientOptions(serviceAddress);
        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest($"Shift/unfinished/{worker.Id}");
        
        try
        {
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK && !string.IsNullOrEmpty(response.Content))
            {
                try
                {
                    string rawResponse = response.Content;
                    ShiftResponse? unfinishedShift = JsonConvert.DeserializeObject<ShiftResponse>(rawResponse);
                    return new ServiceResult<ShiftResponse?>
                    {
                        Success = true,
                        Data = unfinishedShift
                    };
                }
                catch (JsonException ex)
                {
                    return new ServiceResult<ShiftResponse?>
                    {
                        Success = false,
                        ErrorMessage = $"Failed to parse retrieved shift data: {ex.Message}"
                    };
                }
            }
            else
            {
                return new ServiceResult<ShiftResponse?>
                {
                    Success = false,
                    ErrorMessage = DisplayHelper.GetFormattedErrorMessage(response.StatusCode, response.Content),
                    StatusCode = (int)response.StatusCode
                };
            }
        }
        catch (HttpRequestException ex)
        {
            return new ServiceResult<ShiftResponse?>
            {
                Success = false,
                ErrorMessage = $"Network error while retrieving the shift: {ex.Message}"
            };
        }
        catch (TaskCanceledException ex)
        {
            return new ServiceResult<ShiftResponse?>
            {
                Success = false,
                ErrorMessage = $"Request timeout while retrieving the shift: {ex.Message}"
            };
        }
        catch (Exception ex)
        {
            return new ServiceResult<ShiftResponse?>
            {
                Success = false,
                ErrorMessage = $"Unexpected error while retrieving the shift: {ex.Message}"
            };
        }
    }

    internal async Task<ServiceResult<ShiftResponse?>> UpdateShiftAsync(int shiftId, UpdateShiftRequest updateShiftRequest)
    {
        RestClientOptions options = new RestClientOptions(serviceAddress);
        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest($"Shift/{shiftId}", Method.Put);
        request.AddJsonBody(JsonConvert.SerializeObject(updateShiftRequest));
        
        try
        {
            var response = await client.ExecutePutAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK && !string.IsNullOrEmpty(response.Content))
            {
                try
                {
                    string? rawResponse = response.Content;
                    ShiftResponse? shift = JsonConvert.DeserializeObject<ShiftResponse>(rawResponse);

                    return new ServiceResult<ShiftResponse?>
                    {
                        Success = true,
                        Data = shift
                    };
                }
                catch (JsonException ex)
                {
                    return new ServiceResult<ShiftResponse?>
                    {
                        Success = false,
                        ErrorMessage = $"Failed to parse retrieved shift data: {ex.Message}"
                    };
                }
            }
            else
            {
                return new ServiceResult<ShiftResponse?>
                {
                    Success = false,
                    ErrorMessage = DisplayHelper.GetFormattedErrorMessage(response.StatusCode, response.Content),
                    StatusCode = (int)response.StatusCode
                };
            }
        }
        catch (HttpRequestException ex)
        {
            return new ServiceResult<ShiftResponse?>
            {
                Success = false,
                ErrorMessage = $"Network error while updating the shift: {ex.Message}"
            };
        }
        catch (TaskCanceledException ex)
        {
            return new ServiceResult<ShiftResponse?>
            {
                Success = false,
                ErrorMessage = $"Request timeout while updating the shift: {ex.Message}"
            };
        }
        catch (Exception ex)
        {
            return new ServiceResult<ShiftResponse?>
            {
                Success = false,
                ErrorMessage = $"Unexpected error while updating the shift: {ex.Message}"
            };
        }
    }

    internal async Task<ServiceResult<string>> DeleteShiftAsync(int shiftId)
    {
        RestClientOptions options = new RestClientOptions(serviceAddress);
        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest($"Shift/{shiftId}", Method.Delete);
        
        try
        {
            var response = await client.ExecuteDeleteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK && !string.IsNullOrEmpty(response.Content))
            {
                try
                {
                    string? serviceMessageResponse = JsonConvert.DeserializeObject<string>(response.Content);
                    return new ServiceResult<string>
                    {
                        Success = true,
                        Data = serviceMessageResponse
                    };
                }
                catch (JsonException ex)
                {
                    return new ServiceResult<string>
                    {
                        Success = false,
                        ErrorMessage = $"Failed to parse the result of the shift deletion: {ex.Message}"
                    };
                }
            }
            else
            {
                return new ServiceResult<string>
                {
                    Success = false,
                    ErrorMessage = DisplayHelper.GetFormattedErrorMessage(response.StatusCode, response.Content),
                    StatusCode = (int)response.StatusCode
                };
            }
        }
        catch (HttpRequestException ex)
        {
            return new ServiceResult<string>
            {
                Success = false,
                ErrorMessage = $"Network error while deleting the shift: {ex.Message}"
            };
        }
        catch (TaskCanceledException ex)
        {
            return new ServiceResult<string>
            {
                Success = false,
                ErrorMessage = $"Request timeout while deleting the shift: {ex.Message}"
            };
        }
        catch (Exception ex)
        {
            return new ServiceResult<string>
            {
                Success = false,
                ErrorMessage = $"Unexpected error while deleting the shift: {ex.Message}"
            };
        }
    }

    #endregion Shift Methods
}
