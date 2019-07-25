using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DependencyInjectionDemo.Models
{
    public interface IRepository<EntityType> where EntityType : class
    {
        Task<List<EntityType>> GetAll();

        Task<List<EntityType>> Get(Expression<Func<EntityType, bool>> predicate);

        void Add(EntityType entity);

        void Delete(EntityType entity);

        void Update(EntityType entity);
    }
}