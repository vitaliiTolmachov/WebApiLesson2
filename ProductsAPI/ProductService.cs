using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Repository;

namespace ProductsAPI
{
    public interface IProductService
    {
        Task<T> Get<T>(string url, string param);
    }

    public class ProductService : IProductService
    {
        async Task<T> IProductService.Get<T>(string url, string param)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                string response = await client.GetStringAsync($"{url}/{param}");
                if (!string.IsNullOrEmpty(response))
                {
                    T result = JsonConvert.DeserializeObject<T>(response);
                    return result;
                }else
                {
                    return default(T);
                }
            }
            
        }
    }
}
