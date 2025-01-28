using Api.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Services;
using Xunit;
using Application;
using Application.Commands;
using Application.Queries;
using Application.Queries.Contracts;
using Core;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Application.Dtos;

namespace Tests.Api
{
    public class CompensationControllerTests
    {
        private readonly CompensationsDbContext _context;
        private readonly CompensationController _controller;

        private readonly CompensationsService _compensationsService;

        private readonly CompensationCreationCommand _compensationCreationCommand;
        private readonly ICompensationsQuery _compensationsQuery;
        private readonly IPersonalCompensationsQuery _personalCompensationsQuery;
        
        private readonly Mock<CompensationStatusUpdateCommand> _compensationStatusUpdateCommandMock;
        private readonly Mock<IInnerCircleHttpClient> _iInnerCircleHttpClientMock;
        private readonly Mock<CompensationHardDeletionCommand> _compensationHardDeletionCommandMock;

        private readonly Employee _testEmployee = new Employee()
        {
            Id = 1,
            TenantId = 1,
            CorporateEmail = "test@tourmalinecore.com",
            FullName = "Test Test Test"
        };

        private readonly CompensationCreateDto _compensationsCreateDto = new CompensationCreateDto()
        {
            Compensations = new List<CompensationDto>()
            {
                new CompensationDto()
                {
                    Amount = 100,
                    Comment = "Test",
                    IsPaid = false,
                    Quantity = 2,
                    TypeId = 1
                },
                new CompensationDto()
                {
                    Amount = 300,
                    Comment = "Test",
                    IsPaid = true,
                    Quantity = 1,
                    TypeId = 2
                }
            },
            CompensationRequestedForYearAndMonth = "2024-12-31"
        };

        public CompensationControllerTests()
        {
            // Init inMemory database
            var options = new DbContextOptionsBuilder<CompensationsDbContext>()
                .UseInMemoryDatabase(databaseName: "CompensationsDatabase")
                .Options;
            // Init CompensationService
            _context = new CompensationsDbContext(options);
            _compensationCreationCommand = new CompensationCreationCommand(_context);
            _compensationsQuery = new CompensationsQuery(_context);
            _personalCompensationsQuery = new PersonalCompensationsQuery(_context);
            _compensationStatusUpdateCommandMock = new Mock<CompensationStatusUpdateCommand>(_context);
            _compensationsService = new CompensationsService(
                _compensationCreationCommand,
                _compensationsQuery,
                _personalCompensationsQuery,
                _compensationStatusUpdateCommandMock.Object
                );
            // Add other mocks
            _iInnerCircleHttpClientMock = new Mock<IInnerCircleHttpClient>();
            _compensationHardDeletionCommandMock = new Mock<CompensationHardDeletionCommand>(_context);
            //Init controller
            _controller = new CompensationController(
                _compensationsService,
                _iInnerCircleHttpClientMock.Object,
                _compensationHardDeletionCommandMock.Object
                );
            // Add fake claims to httpcontext
            var claims = new List<Claim>
            {
                new Claim("tenantId", "1"),
                new Claim("corporateEmail", "test@tourmalinecore.com"),
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var user = new ClaimsPrincipal(identity);

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(x => x.User).Returns(user);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext.Object
            };
            
            _iInnerCircleHttpClientMock
                .Setup(x => x.GetEmployeeAsync("test@tourmalinecore.com"))
                .ReturnsAsync(_testEmployee);
        }

        [Fact]
        public async Task GetAdminAllCompensationsAsync_ShouldReturnCorrectTotal()
        {
            await _controller.CreateAsync(_compensationsCreateDto);
            var createdCompensationsList = await _controller.GetAdminAllAsync(2024, 12);
            Assert.Equal(500, createdCompensationsList.TotalAmount);
            Assert.Equal(200, createdCompensationsList.TotalUnpaidAmount);
        }

        [Fact]
        public async Task GetAllCompensationsAsync_ShouldReturnCorrectTotal()
        {
            await _controller.CreateAsync(_compensationsCreateDto);
            var createdCompensationsList = await _controller.GetEmployeeCompensationsAsync();
            Assert.Equal(400, createdCompensationsList.TotalUnpaidAmount);
        }
    }
}
