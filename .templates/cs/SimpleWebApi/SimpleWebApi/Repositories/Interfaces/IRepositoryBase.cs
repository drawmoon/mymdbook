using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApi.Repositories.Interfaces
{
    /// <summary>
    /// 通用的仓储接口
    /// </summary>
    /// <typeparam name="TDbContext">数据库上下文</typeparam>
    /// <typeparam name="TEntity">实现了 <see cref="IEntity{TKey}"/> 的数据库实体</typeparam>
    /// <typeparam name="TKey">数据库实体的主键类型</typeparam>
    public interface IRepositoryBase<TDbContext, TEntity, in TKey>
        where TDbContext : DbContext
        where TEntity : class, IEntity<TKey>, new()
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 全部实体
        /// </summary>
        DbSet<TEntity> Entities { get; }

        /// <summary>
        /// 全部实体（不跟踪）
        /// </summary>
        IQueryable<TEntity> DetachedEntities { get; }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> GetAllAsync();

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <param name="includes">指定包含的导航属性</param>
        /// <returns></returns>
        Task<List<TEntity>> GetAllAsync(params string[] includes);

        /// <summary>
        /// 根据主键获取数据
        /// </summary>
        /// <param name="id">实体主键</param>
        /// <returns></returns>
        Task<TEntity> GetAsync(TKey id);

        /// <summary>
        /// 根据主键获取数据
        /// </summary>
        /// <param name="id">实体主键</param>
        /// <param name="includes">指定包含的导航属性</param>
        /// <returns></returns>
        Task<TEntity> GetAsync(TKey id, params string[] includes);

        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        Task<TEntity> InsertAsync(TEntity entity);

        /// <summary>
        /// 新增多条记录
        /// </summary>
        /// <param name="entities">实体集合</param>
        /// <returns></returns>
        Task InsertRangeAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity">实体</param>
        void Update(TEntity entity);

        /// <summary>
        /// 更新多条实体
        /// </summary>
        /// <param name="entities">实体集合</param>
        void UpdateRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="id">实体主键</param>
        void Delete(TKey id);

        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <param name="ids">实体主键集合</param>
        void DeleteRange(IEnumerable<TKey> ids);

        /// <summary>
        /// 事物
        /// </summary>
        /// <returns></returns>
        Task<IDbContextTransaction> BeginTransactionAsync();

        /// <summary>
        /// 提交更改
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();
    }
}
