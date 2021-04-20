using System.IO;
using System.Threading.Tasks;

namespace MinioDemo
{
    public interface IMinioService
    {
        ValueTask GetAsync(string key, string fileName);

        void Get(string key, string fileName);

        ValueTask SetAsync(string key, string fileName);
        
        void Set(string key, string fileName);
        
        Task<Stream> GetAsync(string key);

        Stream Get(string key);

        ValueTask SetAsync(string key, Stream stream, string contentType = null);

        void Set(string key, Stream stream, string contentType = null);

        ValueTask RemoveAsync(string key);

        void Remove(string key);
    }
}