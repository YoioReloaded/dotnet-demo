using System;

namespace DependencyInjectionDemo.Models
{
    public interface IUnitOfWork : IDisposable 
    {
        IRepository<EntityType> Get<EntityType>() where EntityType : class;
        
        void Commit();
    }

    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : class
    {
        TContext context { get; }
    }
}