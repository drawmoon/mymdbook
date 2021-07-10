using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace MinioDemo
{
    public static class MinioServiceExtensions
    {
        public static async Task<T> GetJsonObjectAsync<T>(this IMinioService minioService, string key)
        {
            var stream = await minioService.GetAsync(key);
            DataContractJsonSerializer serializer = new(typeof(T));
            return (T)serializer.ReadObject(stream);
        }

        public static T GetJsonObject<T>(this IMinioService minioService, string key) =>
            GetJsonObjectAsync<T>(minioService, key).ConfigureAwait(false).GetAwaiter().GetResult();
        
        public static async ValueTask SetJsonObjectAsync(this IMinioService minioService, string key, object value)
        {
            await using MemoryStream memoryStream = new();
            DataContractJsonSerializer serializer = new(value.GetType());
            serializer.WriteObject(memoryStream, value);
            memoryStream.Position = 0;
            await memoryStream.FlushAsync();
            await minioService.SetAsync(key, memoryStream, "application/json");
        }

        public static void SetJsonObject(this IMinioService minioService, string key, object value) =>
            SetJsonObjectAsync(minioService, key, value).AsTask().ConfigureAwait(false).GetAwaiter().GetResult();
    }
}