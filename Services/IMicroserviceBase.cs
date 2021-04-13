using MicroserviceBase.Services;
using Microsoft.Extensions.Caching.Distributed;
using System;

namespace MicroserviceBase
{
    public interface IMicroserviceBase
    {
        void Start(string Url);
    }

    public class MicroserviceBase : IMicroserviceBase
    {
        private readonly IFireAndForgetService _fireAndForgetService;
        private readonly IHTTPCallService _httpCallService;
        private readonly IRedisCacheService _redisCacheService;
        public MicroserviceBase(IFireAndForgetService fireAndForgetService, IHTTPCallService httpCallService, IRedisCacheService redisCacheService)
        {
            _fireAndForgetService = fireAndForgetService;
            _httpCallService = httpCallService;
            _redisCacheService = redisCacheService;
        }
        public void Start(string Url)
        {
            _redisCacheService.RedisCache();
            _httpCallService.CallAsync(Url);
            //_restApi.Get();
            //_httpCallService.PostAsync("deneme" , Url);
            _fireAndForgetService.FireAndForget();
        }
    }
}
