using System;
using System.IO;
using System.Text.Json;

namespace MinioDemo
{
    class Program
    {
        private const string Endpoint = "127.0.0.1:9000";
        private const string AccessKey = "AKIAIOSFODNN7EXAMPLE";
        private const string SecretKey = "wJalrXUtnFEMI/K7MDENG/bPxRfiCYEXAMPLEKEY";
        
        static void Main(string[] args)
        {
            try
            {
                MinioService minioService = new(Endpoint, AccessKey, SecretKey);
                
                Console.WriteLine("1. Set and Get Object");
                // Put Object
                minioService.Set("test&user/a1/user.json", new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "xl"
                });
                // Get Object
                var user = minioService.Get<User>("test&user/a1/user.json");
                Console.WriteLine($"{user.Id}  {user.Name}");
                // Remove File
                minioService.Remove("test&user/a1/user.json");

                Console.WriteLine("2. Set and Get File");
                var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tmp");
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                var fileName = Path.Combine(filePath, "user.json");
                File.WriteAllText(fileName, JsonSerializer.Serialize(user));
                // Put File
                minioService.Set("test&user/a2/user.json", fileName);
                // Get File
                var fileName2 = Path.Combine(filePath, "user2.json");
                minioService.Get("test&user/a2/user.json", fileName2);
                Console.WriteLine(File.ReadAllText(fileName2));
                // Remove File
                minioService.Remove("test&user/a2/user.json");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
    
    public class User
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}