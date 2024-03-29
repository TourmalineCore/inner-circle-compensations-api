﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Dtos;
using Application.Services;
using Core;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Filters;
using Application;

namespace Api.Controllers;

[Authorize]
[Route("api/compensations")]
public class CompensationController : Controller
{
    private readonly CompensationsService _compensationsService;
    private readonly  IInnerCircleHttpClient _client;

    public CompensationController(CompensationsService compensationsService, IInnerCircleHttpClient client)
    {
        _compensationsService = compensationsService;
        _client = client;
    }

    [RequiresPermission(UserClaimsProvider.CanRequestCompensations)]
    [HttpPost("create")]
    public async Task CreateAsync([FromBody] CompensationCreateDto dto)
    {
        var employee = await _client.GetEmployeeAsync(User.GetCorporateEmail());
        await _compensationsService.CreateAsync(dto, employee);
    }

    [RequiresPermission(UserClaimsProvider.CanRequestCompensations)]
    [HttpGet("all")]
    public async Task<PersonalCompensationListDto> GetEmployeeCompensationsAsync()
    {
        var employee = await _client.GetEmployeeAsync(User.GetCorporateEmail());
        return await _compensationsService.GetEmployeeCompensationsAsync(employee);
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
        return await _compensationsService.GetAdminAllAsync(year, month, employees);
    }

    [RequiresPermission(UserClaimsProvider.CanManageCompensations)]
    [HttpPut("admin/update")]
    public async Task UpdateStatusAsync([FromBody] long[] compensationsIds)
    {
        await _compensationsService.UpdateStatusAsync(compensationsIds);
    }
}
