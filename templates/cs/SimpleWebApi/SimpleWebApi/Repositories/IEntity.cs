namespace SimpleWebApi.Repositories
{
    /// <summary>
    /// 数据库实体
    /// </summary>
    /// <typeparam name="TKey">数据库实体的主键类型</typeparam>
    public interface IEntity<TKey>
    {
        /// <summary>
        /// 实体主键
        /// </summary>
        public TKey Id { get; set; }
    }
}
