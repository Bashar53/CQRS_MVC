using Employee.Core;
using Employee.Core.Behavior;
using Employee.Core.Mapping;
using Employee.Infrastructure.Persistence;
using Employee.Repository.Concrete;
using Employee.Repository.Interface;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.IoC.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection MapCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EmployeeDbContext>(option => option.UseSqlServer
        (configuration.GetConnectionString("default")));

        services.AddAutoMapper(typeof(MappingExtension).Assembly);
        services.AddTransient<IEmployeeRepository, EmployeeRepository>();

        services.AddTransient<ICountryRepository, CountryRepository>();
        services.AddTransient<IStateRepository, StateRepository>();

        services.AddValidatorsFromAssembly(typeof(ICore).Assembly);
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(typeof(ICore).Assembly);
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });
        return services;
    }
}
