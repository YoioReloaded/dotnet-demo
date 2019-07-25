using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DependencyInjectionDemo.Models
{
    public class EfRepository<EntityType> : IRepository<EntityType> where EntityType : class
    {
        private readonly DbSet<EntityType> _dbSet;

        private DbSet<EntityType> dbSet
        {
            get { return this._dbSet; }
        }

        public EfRepository(DbContext context)
        {
            this._dbSet = context.Set<EntityType>();
        }

        void IRepository<EntityType>.Add(EntityType entity)
        {
            this.dbSet.Add(entity);
        }

        void IRepository<EntityType>.Delete(EntityType entity)
        {
            throw new NotImplementedException();
        }

        Task<List<EntityType>> IRepository<EntityType>.Get(Expression<Func<EntityType, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        Task<List<EntityType>> IRepository<EntityType>.GetAll()
        {
            return this.dbSet.ToListAsync();
        }

        void IRepository<EntityType>.Update(EntityType entity)
        {
            throw new NotImplementedException();
        }
    }
}