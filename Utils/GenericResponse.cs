using System.Net;

namespace ApiBrasil.Utils
{
    public class GenericResponse<T> where T : class
    {
        public HttpStatusCode HttpCode { get; set; }   

        public T Data { get; set; }
        
        public string? ErrorMessage { get; set; } = string.Empty;
    }
}
