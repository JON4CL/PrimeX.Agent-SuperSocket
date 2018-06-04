using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace Prime.Backend
{
    /// <summary>
    /// Formatter that allows content of type text/x-prime-message
    /// Allows for a single input parameter
    /// in the form of:
    /// 
    /// public byte[] RawData([FromBody] byte[] data)
    /// </summary>
    public class BinaryMessageFormatter : InputFormatter
    {
        public BinaryMessageFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/x-prime-message"));
        }

        /// <summary>
        /// Allow text/x-prime-message to be processed
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Boolean CanRead(InputFormatterContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (context.HttpContext.Request.ContentType == "application/x-prime-message") return true;
            return false;
        }

        /// <summary>
        /// Handle application/x-prime-message for byte[] results
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            var request = context.HttpContext.Request;
            if (context.HttpContext.Request.ContentType == "application/x-prime-message")
            {
                using (var ms = new MemoryStream(2048))
                {
                    await request.Body.CopyToAsync(ms);
                    var content = ms.ToArray();
                    return await InputFormatterResult.SuccessAsync(content);
                }
            }

            return await InputFormatterResult.FailureAsync();
        }
    }
}
