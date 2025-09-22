using Application.Services.Options;
using Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Options;

namespace Application;

public class InnerCircleHttpClient : IInnerCircleHttpClient
{
    private readonly HttpClient _client;
    private readonly InnerCircleServiceUrls _urls;
    private readonly AuthenticationOptions _authOptions;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<InnerCircleHttpClient> _logger;

    public InnerCircleHttpClient(
        IOptions<InnerCircleServiceUrls> urls,
        IOptions<AuthenticationOptions> authOptions,
        ILogger<InnerCircleHttpClient> logger,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _client = new HttpClient();
        _urls = urls.Value;
        _logger = logger;
        _authOptions = authOptions.Value;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Employee> GetEmployeeAsync(string corporateEmail)
    {
        var link = $"{_urls.EmployeesServiceUrl}/internal/get-employee?corporateEmail={corporateEmail}";
        var response = await _client.GetStringAsync(link);

        return JsonConvert.DeserializeObject<Employee>(response);
    }

    public async Task<List<Employee>> GetEmployeesAsync()
    {
        _logger.LogInformation($"GetEmployeesAsync: EmployeesServiceUrl = {_urls.EmployeesServiceUrl}");

        var headerName = _authOptions.IsDebugTokenEnabled ? "X-DEBUG-TOKEN" : "Authorization";
        _logger.LogInformation($"GetEmployeesAsync: Using header = {headerName}");

        if (_httpContextAccessor.HttpContext == null)
        {
            _logger.LogError("GetEmployeesAsync: HttpContext is NULL.");
            throw new InvalidOperationException("HttpContext is not available.");
        }

        if (!_httpContextAccessor.HttpContext.Request.Headers.TryGetValue(headerName, out var tokenValue))
        {
            _logger.LogError($"GetEmployeesAsync: Header '{headerName}' not found in request.");
            throw new InvalidOperationException($"Header '{headerName}' not found in request.");
        }

        var token = tokenValue.ToString();
        _logger.LogInformation($"GetEmployeesAsync: Token = {token}");

        var link = $"{_urls.EmployeesServiceUrl}/internal/get-employees";
        _logger.LogInformation($"GetEmployeesAsync: Request URL = {link}");
        try
        {

            _client.DefaultRequestHeaders.Add(headerName, token);

            _logger.LogInformation("GetEmployeesAsync: Sending request to EmployeesService.");
            var response = await _client.GetStringAsync(link);
            _logger.LogInformation($"GetEmployeesAsync: Response received: {response}");

            var employees = JsonConvert.DeserializeObject<List<Employee>>(response);
            _logger.LogInformation($"GetEmployeesAsync: Deserialized {employees?.Count ?? 0} employees.");
            return employees;
        }
        catch (HttpRequestException httpEx)
        {
            _logger.LogError(httpEx, "GetEmployeesAsync: HTTP request failed.");
            throw;
        }
        catch (JsonException jsonEx)
        {
            _logger.LogError(jsonEx, "GetEmployeesAsync: Failed to deserialize response.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetEmployeesAsync: Unexpected error occurred.");
            throw;
        }
    }
}