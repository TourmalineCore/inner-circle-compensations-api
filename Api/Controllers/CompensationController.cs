using Application;
using Application.Commands;
using Application.Dtos;
using Application.Services;
using Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Filters;

namespace Api.Controllers;

[Authorize]
[Route("api/compensations")]
public class CompensationController : Controller
{
    private readonly CompensationsService _compensationsService;
    private readonly CompensationHardDeletionCommand _compensationHardDeletionCommand;
    private readonly IInnerCircleHttpClient _client;

    public CompensationController(
        CompensationsService compensationsService, 
        IInnerCircleHttpClient client,
        CompensationHardDeletionCommand compensationHardDeletionCommand)
    {
        _compensationsService = compensationsService;
        _client = client;
        _compensationHardDeletionCommand = compensationHardDeletionCommand;
    }

    [RequiresPermission(UserClaimsProvider.CanRequestCompensations)]
    [HttpPost("create")]
    public async Task<List<long>> CreateAsync([FromBody] CompensationCreateDto dto)
    {
        var employee = await _client.GetEmployeeAsync(User.GetCorporateEmail());
        return await _compensationsService.CreateAsync(dto, employee);
    }

    [RequiresPermission(UserClaimsProvider.IsCompensationsHardDeleteAllowed)]
    [HttpDelete("{id}/hard-delete")]
    public async Task DeleteAsync([FromRoute] long id)
    {
        await _compensationHardDeletionCommand.ExecuteAsync(id);
    }

    [RequiresPermission(UserClaimsProvider.CanManageCompensations)]
    [HttpDelete("{id}/soft-delete")]
    public async Task SoftDeleteAsync([FromRoute] long id)
    {
        await _compensationHardDeletionCommand.ExecuteAsync(id);
    }

    [RequiresPermission(UserClaimsProvider.CanRequestCompensations)]
    [HttpGet("all")]
    public async Task<PersonalCompensationListDto> GetEmployeeCompensationsAsync()
    {
        var tenantId = User.GetTenantId();
        var employee = await _client.GetEmployeeAsync(User.GetCorporateEmail());
        return await _compensationsService.GetEmployeeCompensationsAsync(employee, tenantId);
    }

    [RequiresPermission(UserClaimsProvider.CanRequestCompensations)]
    [HttpGet("types")]
    public List<CompensationType> GetTypes()
    {
        return CompensationsService.GetTypes();
    }

    [RequiresPermission(UserClaimsProvider.CanManageCompensations)]
    [HttpGet("admin/all")]
    public async Task<AllCompensationsListDto> GetAdminAllAsync([FromQuery] int year, [FromQuery] int month)
    {
        var employees = await _client.GetEmployeesAsync();
        return await _compensationsService.GetAdminAllAsync(year, month, employees, User.GetTenantId());
    }

    [RequiresPermission(UserClaimsProvider.CanManageCompensations)]
    [HttpPut("mark-as-paid")]
    public async Task UpdateStatusAsync([FromBody] long[] compensationsIds)
    {
        await _compensationsService.UpdateStatusAsync(compensationsIds);
    }
}