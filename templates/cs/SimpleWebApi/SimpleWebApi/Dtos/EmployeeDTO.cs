namespace SimpleWebApi.Dtos
{
    /// <summary>
    /// 员工信息表DTO
    /// </summary>
    public class EmployeeDTO
    {
        /// <summary>
        /// 员工Id
        /// </summary>
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
