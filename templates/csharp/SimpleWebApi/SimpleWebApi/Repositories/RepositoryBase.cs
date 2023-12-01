using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SimpleWebApi.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApi.Repositories
{
    /// <summary>
    /// 通用的仓储接口实现
    /// </summary>
    /// <typeparam name="TDbContext">数据库上下文</typeparam>
    /// <typeparam name="TEntity">实现了 <see cref="IEntity{TKey}"/> 的数据库实体</typeparam>
    /// <typeparam name="TKey">数据库实体的主键类型</typeparam>
	public class RepositoryBase<TDbContext, TEntity, TKey> : IRepositoryBase<TDbContext, TEntity, TKey>
        where TDbContext : DbContext
        where TEntity : class, IEntity<TKey>, new()
        where TKey : IEquatable<TKey>
    {
        protected TDbContext DbContext;

        public RepositoryBase(TDbContext dbContext)
        {
            DbContext = dbContext;
        }

        /// <summary>
        /// 全部实体
        /// </summary>
        public DbSet<TEntity> Entities => DbContext.Set<TEntity>();

        /// <summary>
        /// 全部实体（不跟踪）
        /// </summary>
        public IQueryable<TEntity> DetachedEntities => Entities.AsNoTracking();

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public Task<List<TEntity>> GetAllAsync()
        {
            return Entities.ToListAsync();
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <param name="includes">指定包含的导航属性</param>
        /// <returns></returns>
        public Task<List<TEntity>> GetAllAsync(params string[] includes)
        {
            var query = Entities.AsQueryable();
            query = includes.Aggregate(query, (current, include) => current.Include(include));
            return query.ToListAsync();
        }

        /// <summary>
        /// 根据主键获取数据
        /// </summary>
        /// <param name="id">实体主键</param>
        /// <returns></returns>
        public Task<TEntity> GetAsync(TKey id)
        {
            var query = Entities.AsQueryable();
            return query.SingleOrDefaultAsync(c => c.Id.Equals(id));
        }

        /// <summary>
        /// 根据主键获取数据
        /// </summary>
        /// <param name="id">实体主键</param>
        /// <param name="includes">指定包含的导航属性</param>
        /// <returns></returns>
        public Task<TEntity> GetAsync(TKey id, params string[] includes)
        {
            var query = Entities.AsQueryable();
            query = includes.Aggregate(query, (current, include) => current.Include(include));
            return query.SingleOrDefaultAsync(c => c.Id.Equals(id));
        }

        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            var entityEntry = await Entities.AddAsync(entity);
            return entityEntry.Entity;
        }

        /// <summary>
        /// 新增多条记录
        /// </summary>
        /// <param name="entities">实体集合</param>
        /// <returns></returns>
        public Task InsertRangeAsync(IEnumerable<TEntity> entities)
        {
            return Entities.AddRangeAsync(entities);
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity">实体</param>
        public void Update(TEntity entity)
        {
            Entities.Update(entity);
        }

        /// <summary>
        /// 更新多条实体
        /// </summary>
        /// <param name="entities">实体集合</param>
        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            Entities.UpdateRange(entities);
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="id">实体主键</param>
        public void Delete(TKey id)
        {
            var entity = new TEntity { Id = id };
            Entities.Attach(entity);
            Entities.Remove(entity);
        }

        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <param name="ids">实体主键集合</param>
        public void DeleteRange(IEnumerable<TKey> ids)
        {
            foreach (var id in ids)
            {
                Delete(id);
            }
        }

        /// <summary>
        /// 事物
        /// </summary>
        /// <returns></returns>
        public Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return DbContext.Database.BeginTransactionAsync();
        }

        /// <summary>
        /// 提交更改
        /// </summary>
        /// <returns></returns>
        public Task<int> SaveChangesAsync()
        {
            return DbContext.SaveChangesAsync();
        }
    }
}
