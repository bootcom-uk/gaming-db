using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IGDB.Extensions
{
    public static class HttpRequestMessageExtensions
    {

        public async static Task<HttpRequestMessage> Clone(this HttpRequestMessage originalMessage)
        {
            if (originalMessage == null) { throw new ArgumentNullException(nameof(originalMessage)); }

            var message = new HttpRequestMessage(originalMessage.Method, originalMessage.RequestUri);

            // Copy the request's content (via a MemoryStream) into the cloned object
            var ms = new MemoryStream();
            if (originalMessage.Content != null)
            {
                await originalMessage.Content.CopyToAsync(ms).ConfigureAwait(false);
                ms.Position = 0;
                message.Content = new StreamContent(ms);

                // Copy the content headers
                if (originalMessage.Content.Headers != null)
                    foreach (var h in originalMessage.Content.Headers)
                        message.Content.Headers.Add(h.Key, h.Value);
            }


            message.Version = originalMessage.Version;

            foreach (KeyValuePair<string, IEnumerable<string>> header in originalMessage.Headers)
                message.Headers.TryAddWithoutValidation(header.Key, header.Value);

            return message;

        }
    }
}
