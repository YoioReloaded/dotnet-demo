using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DependencyInjectionDemo.Models
{

    public class Mock_UnitOfWork : IUnitOfWork, IUnitOfWork<DbContext>
    {
        static readonly CustomersContext db = Mock_UnitOfWork.createContext();
        private DbContext _context;
        public DbContext context
        {
            get { return this._context; }
        }

        private Dictionary<Type, object> _repositories;

        private static CustomersContext createContext()
        {
            var optBuilder = new DbContextOptionsBuilder<CustomersContext>();
            optBuilder.UseInMemoryDatabase("CustomersList");

            if (null == db || 0 == db.Customers.CountAsync().Result)
            {
                var _ctx = new CustomersContext(optBuilder.Options);

                Customer[] customers = {
                    new Customer { Id = 1, Name = "Jane Doe", Account = 100.0M },
                    new Customer { Id = 2, Name = "John smith", Account = 99.0M },
                };
                _ctx.AddRange(customers);
                _ctx.SaveChanges();

                return _ctx;
            }
            else
            {
                return db;
            }
        }

        public Mock_UnitOfWork()
        {
            this._context = Mock_UnitOfWork.createContext();
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