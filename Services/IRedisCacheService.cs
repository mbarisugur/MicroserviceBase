using StackExchange.Redis;
using System;

namespace MicroserviceBase.Services
{
    public interface IRedisCacheService
    {
        void RedisCache();
    }
    public class RedisCacheService : IRedisCacheService
    {
        public void RedisCache()
        {
            ConnectionMultiplexer redisConnection = ConnectionMultiplexer.Connect("localhost");
            if (redisConnection.IsConnected)
            {
                IDatabase db = redisConnection.GetDatabase();
                db.StringSet("foo2", "bar2");

                string val = db.StringGet("foo2");

                Console.WriteLine("output : " + val);
            }
            else
            {
                Console.WriteLine("Redis bağlanmadı.");
            }
            
           
        }
    }
}