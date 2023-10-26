using SimpleWebApi.Repositories;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleWebApi.Models.Management
{
    /// <summary>
    /// 员工信息表
    /// </summary>
    public class Employee : IEntity<int>
    {
        /// <summary>
        /// 员工Id
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
		public string Name { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
		public int Age { get; set; }
    }
}
