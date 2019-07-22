
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DependencyInjectionDemo
{
    public class MockDataSource
    {
        public static Task<IEnumerable<string>> GetAll()
        {
            var t = new Task<IEnumerable<string>>( () => {

                string[] data = { "mock1", "mock2" };
                return data;
            });
            t.Start();
            return t;
        }
    }
}