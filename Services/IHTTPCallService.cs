using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;

namespace MicroserviceBase
{
    public interface IHTTPCallService
    {
        void CallAsync(string Url);
        void PostAsync(string Content, string Url);
    }

    [Route("api/[controller]")]
    [ApiController]
    public class HTTPCallService : IHTTPCallService
    {
        
        public async void CallAsync(string Url)
        {
            Console.WriteLine(Url);
            using HttpResponseMessage res = await new HttpClient().GetAsync(Url);
            if (res.StatusCode.CompareTo(System.Net.HttpStatusCode.OK) == 1)
            {
                using HttpContent content = res.Content;
                string data = await content.ReadAsStringAsync();  // ReadAsStringAsync<Data>() belirlendiği zaman. Data bir sınıf.

                if (data != null)
                {
                    PostAsync(data, Url);
                    Console.WriteLine(data);
                }
            }
            else
            {
                Console.WriteLine("Bağlantı problemi.");
            }
        }

        public async void PostAsync(string Content, string Url)
        {
            using HttpResponseMessage post = await new HttpClient().PostAsJsonAsync(Url, Content);

            if (post.StatusCode.CompareTo(System.Net.HttpStatusCode.OK) == 1)
            {
                var response = await post.Content.ReadAsStringAsync();
                Console.WriteLine(response);
                Console.WriteLine("response");
            }
            Console.WriteLine("bitti");
        }
    }
}
