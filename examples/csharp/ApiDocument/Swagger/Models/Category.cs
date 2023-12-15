using System;

namespace ApiDocument.Models
{
    public class Category
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ParentId { get; set; }

        public string MPath { get; set; }

        public string Creator { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastModify { get; set; }
    }
}