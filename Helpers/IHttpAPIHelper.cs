using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkApplication.Helpers
{
    public interface IHttpAPIHelper<T>
    {
        Task<T> GetSingleItemRequest(string apiUrl);
        Task<T[]> GetMultipleItemsRequest(string apiUrl);
        Task<T> GetSingleItemRequest(string apiUrl, T sendObject);
        Task<T> PostRequest(string apiUrl, T postObject);
        Task<T> PutRequest(string apiUrl, T putObject);
        Task<T> DeleteRequest(string apiUrl);
    }
}
