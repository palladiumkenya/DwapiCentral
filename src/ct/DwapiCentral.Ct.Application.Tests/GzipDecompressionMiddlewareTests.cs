using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Application.Tests
{
    [TestFixture]
    public class GzipDecompressionMiddlewareTests
    {
        private HttpClient _client;

        [Test]
        public async Task TestGzipCompressionAndDecompression()
        {
            // Arrange
            string originalData = "Test data to compress";
            byte[] originalBytes = Encoding.UTF8.GetBytes(originalData);

            // Compress the data using GZIP
            using (var compressedStream = new MemoryStream())
            using (var gzipStream = new GZipStream(compressedStream, CompressionMode.Compress))
            {
                gzipStream.Write(originalBytes, 0, originalBytes.Length);
                gzipStream.Close();

                // Set up the request with the compressed data
                var requestContent = new ByteArrayContent(compressedStream.ToArray());
                requestContent.Headers.Add("Content-Encoding", "gzip");

                // Act
                var response = await _client.PostAsync("/your-api-endpoint", requestContent);

                // Assert
                Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);

                // Decompress the response body
                using (var decompressedStream = new MemoryStream())
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                using (var gzipStreams = new GZipStream(responseStream, CompressionMode.Decompress))
                {
                    gzipStreams.CopyTo(decompressedStream);
                    decompressedStream.Seek(0, SeekOrigin.Begin);
                    var decompressedData = Encoding.UTF8.GetString(decompressedStream.ToArray());

                    // Assert that the decompressed data matches the original data
                    Assert.AreEqual(originalData, decompressedData);
                }
            }
        }
    }
}