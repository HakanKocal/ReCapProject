using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Extensions
{
    public static  class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDepencencyResolvers(this IServiceCollection servicesCollection ,ICoreModule[] modules)
        {
            foreach (var module in modules)
            {
                module.Load(servicesCollection);
            }
            return ServiceTool.Create(servicesCollection);
        }
    }
}
