using System.Net;

namespace KoboPay.Logic.Dtos
{
    public class BaseResponseModel<T>
    {
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccessful { get; set; }
        public T Object { get; set; }
    }
}