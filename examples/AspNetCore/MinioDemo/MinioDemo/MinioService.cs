using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Minio;

namespace MinioDemo
{
    public class MinioService
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

        public async ValueTask<T> GetAsync<T>(string key)
        {
            T result = default;
            var (bucketName, objectName) = GetObjectArgs(key);
            await _minioClient.GetObjectAsync(bucketName, objectName, async stream =>
            {
                DataContractJsonSerializer serializer = new(typeof(T));
                result = (T)serializer.ReadObject(stream);
            });
            Debug.Assert(result != null);
            return result;
        }

        public T Get<T>(string key) => GetAsync<T>(key).AsTask().ConfigureAwait(false).GetAwaiter().GetResult();
        
        public async ValueTask<string> GetAsync(string key, string fileName)
        {
            var (bucketName, objectName) = GetObjectArgs(key);
            await _minioClient.GetObjectAsync(bucketName, objectName, fileName);
            return fileName;
        }

        public string Get(string key, string fileName) =>
            GetAsync(key, fileName).AsTask().ConfigureAwait(false).GetAwaiter().GetResult();
        
        public async ValueTask SetAsync(string key, object value)
        {
            var (bucketName, objectName) = GetObjectArgs(key);
            await using MemoryStream memoryStream = new();
            DataContractJsonSerializer serializer = new(value.GetType());
            serializer.WriteObject(memoryStream, value);
            memoryStream.Position = 0;
            await memoryStream.FlushAsync();
            await _minioClient.PutObjectAsync(bucketName, objectName, memoryStream, memoryStream.Length,
                "application/json");
        }

        public void Set(string key, object value) =>
            SetAsync(key, value).AsTask().ConfigureAwait(false).GetAwaiter().GetResult();

        public async ValueTask<string> SetAsync(string key, string fileName)
        {
            var (bucketName, objectName) = GetObjectArgs(key);
            await _minioClient.PutObjectAsync(bucketName, objectName, fileName);
            return fileName;
        }

        public string Set(string key, string fileName) =>
            SetAsync(key, fileName).AsTask().GetAwaiter().GetResult();
        
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