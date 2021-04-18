using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HttpClient2
{
    public class Product
    {
        public string language { get; set; }
        public string language2 { get; set; }
    }

    public class Product2
    {
        public Product2(string a, string b)
        {
            language = a;
            language2 = b;
        }
        public string language { get; set; }
        public string language2 { get; set; }
    }

    class Program
    {
        static HttpClient client = new HttpClient();
        static void Main(string[] args)
        {

            //List<Product2> li = new List<Product2>();
            //li.Add(new Product2("太郎", "M"));
            //li.Add(new Product2("太郎", "M"));
            //string json = JsonConvert.SerializeObject(li);


            RunAsync().GetAwaiter().GetResult();
        }

        static async Task<Uri> CreateProductAsync(Product product)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "hello", product);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        static async Task<Product> CreateProductAsync2(Product product)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "hello", product);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<Product>();
            }
            return product;
        }


        static async Task<List<Product>> GetProductAsync(string path)
        {
            List<Product> product = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<List<Product>>();
            }
            return product;
        }

        static async Task RunAsync()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("http://127.0.0.1:8888/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                // Get the product
                var product = await GetProductAsync("http://127.0.0.1:8888/");
                Console.WriteLine(product[0].language);
                Console.WriteLine(product[0].language2);
                Console.WriteLine(product[1].language);
                Console.WriteLine(product[1].language2);

                var url = await CreateProductAsync2(product[0]);
                Console.WriteLine($"Created at {url}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
