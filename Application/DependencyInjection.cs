using Application.Commands;
using Application.Queries;
using Application.Queries.Contracts;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
  public static void AddApplication(this IServiceCollection services)
  {
    services.AddTransient<CompensationCreationCommand>();
    services.AddTransient<CompensationHardDeletionCommand>();
    services.AddTransient<CompensationsService>();
    services.AddTransient<ICompensationsQuery, CompensationsQuery>();
    services.AddTransient<IPersonalCompensationsQuery, PersonalCompensationsQuery>();
    services.AddTransient<CompensationStatusUpdateCommand>();
    services.AddTransient<IInnerCircleHttpClient, InnerCircleHttpClient>();
  }
}
