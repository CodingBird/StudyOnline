using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using DataAccess.Abstract;
using System.Linq.Expressions;

namespace DataAccess.Concrete
{
    public abstract class RepositoryBase<TEntity>:IRepository<TEntity> where TEntity:class
    {
        public StudyDbContext DbContext { get; private set; }
        public DbSet<TEntity> DbSet { get; private set; }
        public RepositoryBase(StudyDbContext context)
        {
            this.DbContext = context;
            this.DbSet = this.DbContext.Set<TEntity>();
        }
        public IEnumerable<TEntity> Get()
        {

            IEnumerable<TEntity> entities =  this.DbSet.AsQueryable();
            return entities;
        }
        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            return this.DbSet.Where(filter).AsQueryable();
        }
        public IEnumerable<TEntity> Get<TOrderKey>(Expression<Func<TEntity, bool>> filter,
            int pageSize, int pageIndex, Expression<Func<TEntity, TOrderKey>> sortExpression, bool isAsc = true)
        {
            if (isAsc)
            {
                return this.DbSet.Where(filter)
                                .OrderBy(sortExpression)
                                .Skip(pageSize * (pageIndex - 1))
                                .Take(pageSize)
                                .AsQueryable();
            }
            else
            {
                return this.DbSet.Where(filter)
                                .OrderByDescending(sortExpression)
                                .Skip(pageSize * (pageIndex - 1))
                                .Take(pageSize)
                                .AsQueryable();
            }
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return this.DbSet.Where(predicate).Count();
        }
        public void Add(TEntity instance)
        {
            this.DbSet.Attach(instance);
            this.DbContext.Entry(instance).State = System.Data.EntityState.Added;
            this.DbContext.SaveChanges();
        }
        public void Update(TEntity instance)
        {
            this.DbSet.Attach(instance);
            this.DbContext.Entry(instance).State = System.Data.EntityState.Modified;
            this.DbContext.SaveChanges();
        }
        public void Delete(TEntity instance)
        {
            this.DbSet.Attach(instance);
            this.DbContext.Entry(instance).State = System.Data.EntityState.Deleted;
            this.DbContext.SaveChanges();
        }

        public void Dispose()
        {
            this.DbContext.Dispose();
        }
    }
}
