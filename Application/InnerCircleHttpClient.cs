using System.Text.Json;
using Application.Services.Options;
using Core;
using Microsoft.Extensions.Options;

namespace Application;

public class InnerCircleHttpClient : IInnerCircleHttpClient
{
    private readonly HttpClient _client;
    private readonly InnerCircleServiceUrls _urls;

    public InnerCircleHttpClient(IOptions<InnerCircleServiceUrls> urls)
    {
        _client = new HttpClient();
        _urls = urls.Value;
    }

    public async Task<Employee> GetEmployeeAsync(string corporateEmail)
    {
        var link = $"{_urls.SalaryServiceUrl}/internal/get-employee?corporateEmail={corporateEmail}";
        var response = await _client.GetStringAsync(link);

        return JsonSerializer.Deserialize<Employee>(response);
    }

    public async Task<List<Employee>> GetEmployeesAsync()
    {
        var link = $"{_urls.SalaryServiceUrl}/internal/get-employees";
        var response = await _client.GetStringAsync(link);

        return JsonSerializer.Deserialize<List<Employee>>(response);
    }
}