using MicroserviceBase.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using System;
namespace MicroserviceBase
{
    public class Startup
    {
        public static IServiceProvider ConfigureService()
        {
            var provider = new ServiceCollection()
                .AddSingleton<IFireAndForgetService, FireAndForgetService>()
                .AddSingleton<IMicroserviceBase, MicroserviceBase>()
                .AddSingleton<IHTTPCallService, HTTPCallService>()
                .AddSingleton<IRedisCacheService, RedisCacheService>()
                .BuildServiceProvider();
            return provider;
        }
    }
}
