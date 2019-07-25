using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DependencyInjectionDemo.Models
{

    public class UnitOfWork : IUnitOfWork, IUnitOfWork<DbContext>
    {
        private readonly string _connectionString;
        private DbContext _context;
        public DbContext context
        {
            get { return null != this._context ? this._context : this.createContext(this._connectionString); }
        }

        private Dictionary<Type, object> _repositories;

        private CustomersContext createContext(string connectionString)
        {
            if (0 != connectionString.Trim().Length)
            {
                var optBuilder = new DbContextOptionsBuilder<CustomersContext>();
                optBuilder.UseSqlServer(connectionString);

                return new CustomersContext(optBuilder.Options);
            }
            else
            {
                return null;
            }
        }

        public UnitOfWork(string connectionString)
        {
            this._connectionString = connectionString;
            this._context = createContext(connectionString);
        }

        public void Commit()
        {
            this.context.SaveChanges();
        }

        public IRepository<EntityType> Get<EntityType>() where EntityType : class
        {
            if (_repositories == null) _repositories = new Dictionary<Type, object>();

            var type = typeof(EntityType);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new EfRepository<EntityType>(this.context);
            }
            return (EfRepository<EntityType>)_repositories[type];
        }

        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}