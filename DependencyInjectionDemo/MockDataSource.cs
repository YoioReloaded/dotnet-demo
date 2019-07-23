using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

using DependencyInjectionDemo.Models;

namespace DependencyInjectionDemo
{
    public class MockDataSource
    {

        static readonly CustomersContext db = MockDataSource.createContext();

        private static CustomersContext createContext()
        {
            var optBuilder = new DbContextOptionsBuilder<CustomersContext>();
            optBuilder.UseInMemoryDatabase("CustomersList");

            if (null == db || 0 == db.Customers.CountAsync().Result)
            {

                var context = new CustomersContext(optBuilder.Options);

                Customer[] customers = {
                    new Customer { Id = 1, Name = "Jane Doe", Account = 100.0M },
                    new Customer { Id = 2, Name = "John smith", Account = 99.0M },
                };
                context.AddRange(customers);
                context.SaveChanges();

                return context;
            }
            else
            {
                return db;
            }
        }

        public static async Task<IEnumerable<Customer>> GetAll() => await db.Customers.ToListAsync();
    }
}