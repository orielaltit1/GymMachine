using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiClient
{
    internal interface IWebClient<T>
    {
        // syncronic
        T Get();


        bool Post(T data);

        bool Post(T data, Stream file);

        bool Post(T data, List<Stream> file);

        Task<T>  GetAsync();

        Task<bool> PostAsync(T data);

        Task<bool> PostAsync(bool data, Stream file);

        Task<bool> PostAsync(T data, List<Stream> files);

    }
}
