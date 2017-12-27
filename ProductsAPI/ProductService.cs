using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Repository;

namespace ProductsAPI
{
    public interface IProductService
    {
        void AddProduct(Product product, List<Category> categories);
        Task<List<Department>> GetDepts();
        Task<List<Category>> GetCategories();
    }

    public class ProductService : IProductService
    {
        void IProductService.AddProduct(Product product, List<Category> categories)
        {
        }

        async Task<List<Department>> IProductService.GetDepts()
        {
            var serializer = new DataContractJsonSerializer(typeof(List<Department>));

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            Task<Stream> streamTask = client.GetStreamAsync("http://localhost:51263/api/category/get");

            List<Department> depts = serializer.ReadObject(await streamTask) as List<Department>;
            return depts;
        }

        async Task<List<Category>> IProductService.GetCategories()
        {
            var serializer = new DataContractJsonSerializer(typeof(List<Category>));

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            Task<Stream> streamTask = client.GetStreamAsync("http://localhost:51263/api/category/get");

            List<Category> depts = serializer.ReadObject(await streamTask) as List<Category>;
            return depts;
        }
    }
}
