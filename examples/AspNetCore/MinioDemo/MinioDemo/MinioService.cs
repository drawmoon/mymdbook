using System;
using System.IO;
using System.Threading.Tasks;
using Minio;

namespace MinioDemo
{
    public class MinioService : IMinioService
    {
        private readonly MinioClient _minioClient;
        
        public MinioService(string endpoint, string accessKey, string secretKey, bool useSSL = false)
        {
            _minioClient = new MinioClient(endpoint, accessKey, secretKey);
            if (useSSL)
            {
                _minioClient.WithSSL();
            }
        }
        
        public async ValueTask GetAsync(string key, string fileName)
        {
            var (bucketName, objectName) = GetObjectArgs(key);
            await _minioClient.GetObjectAsync(bucketName, objectName, fileName);
        }

        public void Get(string key, string fileName) =>
            GetAsync(key, fileName).AsTask().ConfigureAwait(false).GetAwaiter().GetResult();

        public async ValueTask SetAsync(string key, string fileName)
        {
            var (bucketName, objectName) = GetObjectArgs(key);
            await _minioClient.PutObjectAsync(bucketName, objectName, fileName);
        }

        public void Set(string key, string fileName) =>
            SetAsync(key, fileName).AsTask().GetAwaiter().GetResult();
        
        public async ValueTask<Stream> GetAsync(string key)
        {
            MemoryStream stream = new();
            var (bucketName, objectName) = GetObjectArgs(key);
            await _minioClient.GetObjectAsync(bucketName, objectName, async sm =>
            {
                await sm.CopyToAsync(stream);
                stream.Position = 0;
                await stream.FlushAsync();
            });
            return stream;
        }

        public Stream Get(string key) =>
            GetAsync(key).AsTask().ConfigureAwait(false).GetAwaiter().GetResult();
        
        public async ValueTask SetAsync(string key, Stream stream, string contentType = null)
        {
            var (bucketName, objectName) = GetObjectArgs(key);
            await _minioClient.PutObjectAsync(bucketName, objectName, stream, stream.Length, contentType);
        }

        public void Set(string key, Stream stream, string contentType = null) =>
            SetAsync(key, stream, contentType).AsTask().ConfigureAwait(false).GetAwaiter().GetResult();
        
        public async ValueTask RemoveAsync(string key)
        {
            var (bucketName, objectName) = GetObjectArgs(key);
            await _minioClient.RemoveObjectAsync(bucketName, objectName);
        }

        public void Remove(string key) => RemoveAsync(key).AsTask().ConfigureAwait(false).GetAwaiter().GetResult();

        private static (string bucketName, string objectName) GetObjectArgs(string key)
        {
            var split = key.Split("&");
            if (split.Length != 2)
            {
                throw new NotSupportedException("Key is not a string composed of '{bucketName}&{objectName}'");
            }
            return (split[0], split[1]);
        }
    }
}