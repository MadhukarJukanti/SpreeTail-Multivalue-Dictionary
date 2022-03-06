using System.Net;

namespace SpreeTail.MultiValueDictionary.Common.Helpers
{
    public class BasicResult
    {
        public HttpStatusCode HttpStatusCode { get; set; }

        public string ErrorMessage { get; set; }

        public BasicResult(HttpStatusCode httpStatusCode)
        {
            HttpStatusCode = httpStatusCode;
        }

        public BasicResult(string errorMessage)
        {
            ErrorMessage = errorMessage;
            HttpStatusCode = HttpStatusCode.BadRequest;
        }

        public BasicResult()
        {
        }
    }
}
