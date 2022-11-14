using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Consistency
{
    public class GenericHttpResponse<T> where T: class
    {

        public GenericHttpResponse(T data = null)
        {
            Data = data;
            IsSuccess = data!=null;
        }
        [JsonPropertyName("success")]
        public bool IsSuccess { get; private set; }

        [JsonPropertyName("data")]
        public T? Data { get; private set; }
    }
}
