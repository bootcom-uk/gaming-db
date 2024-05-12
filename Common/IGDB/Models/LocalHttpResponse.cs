using System.Net;

namespace IGDB.Models
{
    public class LocalHttpResponse
    {

        public HttpResponseMessage? WebResponse { get; set; }

        public string? BearerToken {  get; set; } 

    }
}
