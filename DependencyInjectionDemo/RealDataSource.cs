using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

using DependencyInjectionDemo.Models;

namespace DependencyInjectionDemo
{
    public class RealDataSource
    {
        static string _connectionString = "";
        public static string connectionString
        {
            set
            {
                _connectionString = value;
                db = createContext();
            }
        }

        static CustomersContext? db;

        private static CustomersContext createContext()
        {
            if (0 != _connectionString.Trim().Length)
            {
                var optBuilder = new DbContextOptionsBuilder<CustomersContext>();
                optBuilder.UseSqlServer(_connectionString);

                return new CustomersContext(optBuilder.Options);
            }
            else
            {
                return null;
            }
        }

        public static async Task<IEnumerable<Customer>> GetAll() => db != null ? await db.Customers.ToListAsync() : new List<Customer>();
    }
}