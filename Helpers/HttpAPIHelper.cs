using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CourseWorkApplication.Helpers
{
    public class HttpAPIHelper<T> : IHttpAPIHelper<T>
    {
        private static readonly string BaseURl = "https://localhost:7139/api/";
        private static readonly HttpClient Client = new HttpClient() { BaseAddress = new Uri(BaseURl) };

        public async Task<T[]> GetMultipleItemsRequest(string apiUrl)
        {
            T[] result = null;
            var response = await Client.GetAsync(apiUrl).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    result = JsonConvert.DeserializeObject<T[]>(x.Result);
                });
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                response.Content?.Dispose();
                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }
            return result;
        }


        public async Task<T> GetSingleItemRequest(string apiUrl)
        {
            var result = default(T);
            var response = await Client.GetAsync(apiUrl).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync().ContinueWith(x =>
                {
                    result = JsonConvert.DeserializeObject<T>(x?.Result);
                });

            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                response.Content?.Dispose();
                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }
            return result;
        }

        public async Task<T> PostRequest(string apiUrl, T postObject)
        {
            T result = default(T);

            //var Content = JsonConvert.SerializeObject(postObject, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Serialize });
            //var buffer = System.Text.Encoding.UTF8.GetBytes(Content);
            //var ByteContent = new ByteArrayContent(buffer);
            //ByteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            //var response = Client.PostAsync(BaseURl + apiUrl, ByteContent).Result;

            //var json = JsonConvert.SerializeObject(postObject);
            //var data = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            //var response = await Client.PostAsync(BaseURl + apiUrl, data);

            //var json = Newtonsoft.Json.JsonConvert.SerializeObject(postObject);
            //var data = new System.Net.Http.StringContent(json, System.Text.Encoding.UTF8, "application/json");
            //var response = await Client.PostAsJsonAsync<T>(apiUrl, postObject);
            var response = await Client.PostAsJsonAsync(apiUrl, postObject);

            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    result = JsonConvert.DeserializeObject<T>(x.Result);
                });
            }
            else
            {
                var cont = await response.Content.ReadAsStringAsync();
                response.Content?.Dispose();
                throw new HttpRequestException($"{response.StatusCode}:{cont}");
            }

            return result;
        }

        public async Task<T> PutRequest(string apiUrl, T putObject)
        {
            T result = default(T);

            var Content = JsonConvert.SerializeObject(putObject);
            var buffer = System.Text.Encoding.UTF8.GetBytes(Content);
            var ByteContent = new ByteArrayContent(buffer);
            ByteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await Client.PutAsync(apiUrl, ByteContent).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    result = JsonConvert.DeserializeObject<T>(x.Result);
                });
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                response.Content?.Dispose();
                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }

            return result;
        }

        public async Task<T> DeleteRequest(Uri apiUrl)
        {
            T result = default(T);
            var response = await Client.DeleteAsync(apiUrl).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync().ContinueWith(x =>
                {
                    result = JsonConvert.DeserializeObject<T>(x?.Result);
                });

            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                response.Content?.Dispose();
                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }

            return result;
        }

        public async Task<T> GetSingleItemRequest(string apiUrl, T sendObject)
        {
            T result = default(T);

            var Content = JsonConvert.SerializeObject(sendObject);
            var buffer = System.Text.Encoding.UTF8.GetBytes(Content);
            var ByteContent = new ByteArrayContent(buffer);
            ByteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await Client.PostAsync(BaseURl + apiUrl, ByteContent).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    result = JsonConvert.DeserializeObject<T>(x.Result);
                });
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                response.Content?.Dispose();
                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }

            return result;
        }


        public async Task<T> AuthenticationRequest(string apiUrl, T postObject)
        {
            T result = default(T);

            var Content = JsonConvert.SerializeObject(postObject);
            var buffer = System.Text.Encoding.UTF8.GetBytes(Content);
            var ByteContent = new ByteArrayContent(buffer);
            ByteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await Client.PostAsync(BaseURl + apiUrl, ByteContent).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    result = JsonConvert.DeserializeObject<T>(x.Result);
                });
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                response.Content?.Dispose();
                throw new HttpRequestException($"{response.StatusCode}:{content}");
            }

            return result;
        }
    }
}
