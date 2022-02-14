using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProductsManagement.Application.Behaviours;
using ProductsManagement.Application.Features.Products.Commands.CreateProduct;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ProductsManagement.Application
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(provider => new Lazy<IMapper>(provider.GetService<IMapper>()));
        }
    }
}
