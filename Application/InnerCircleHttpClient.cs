using System.Net.Http.Headers;
using Application.Services.Options;
using Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using Newtonsoft.Json;

namespace Application;

public class InnerCircleHttpClient : IInnerCircleHttpClient
{
    private readonly HttpClient _client;
    private readonly InnerCircleServiceUrls _urls;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public InnerCircleHttpClient(
        IOptions<InnerCircleServiceUrls> urls,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _client = new HttpClient();
        _urls = urls.Value;
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
        var link = $"{_urls.EmployeesServiceUrl}/internal/get-employees";

        var header = Environment.GetEnvironmentVariable("REQUEST_HEADER_NAME");

        var token = _httpContextAccessor
            .HttpContext!
            .Request
            .Headers[header]
            .ToString();

        if (header == "X-DEBUG-TOKEN")
        {
            _client.DefaultRequestHeaders.Add("X-DEBUG-TOKEN", token.Replace("Bearer ", ""));
        }
        else
        {
            _client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(token);
        }

        var response = await _client.GetStringAsync(link);

        return JsonConvert.DeserializeObject<List<Employee>>(response);
    }
}