using System.IO.Compression;

namespace DwapiCentral.Ct
{
    public class GzipDecompressionMiddleware
    {
        private readonly RequestDelegate _next;

        public GzipDecompressionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.ContainsKey("Content-Encoding") &&
                context.Request.Headers["Content-Encoding"].ToString().Contains("gzip"))
            {
                using (var decompressionStream = new GZipStream(context.Request.Body, CompressionMode.Decompress))
                {
                    var buffer = new MemoryStream();
                    await decompressionStream.CopyToAsync(buffer);
                    buffer.Seek(0, SeekOrigin.Begin);
                    context.Request.Body = buffer;
                }
            }

            await _next(context);
        }
    }
}
