using System.Net.Http.Headers;
using Application.Services.Options;
using Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
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

        var authHeader = _httpContextAccessor
            .HttpContext?
            .Request
            .Headers["Authorization"]
            .ToString();

        var debugTokenHeader = _httpContextAccessor
            .HttpContext?
            .Request
            .Headers["X-DEBUG-TOKEN"]
            .ToString();

        if (!string.IsNullOrEmpty(debugTokenHeader))
        {
            // for Karate tests
            _client.DefaultRequestHeaders.Add("X-DEBUG-TOKEN", debugTokenHeader);
        }
        else if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
        {
            var token = authHeader.Substring("Bearer ".Length).Trim();
            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(token);
        }
        //if (!string.IsNullOrEmpty(token))
        //{
        //    _client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(token);
        //}

        var response = await _client.GetStringAsync(link);

        return JsonConvert.DeserializeObject<List<Employee>>(response);
    }
}