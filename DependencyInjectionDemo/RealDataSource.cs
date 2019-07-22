using System.Collections.Generic;
using System.Threading.Tasks;

namespace DependencyInjectionDemo
{
    public class RealDataSource
    {
        public static Task<IEnumerable<string>> GetAll()
        {
            var t = new Task<IEnumerable<string>>( () => {

                string[] data = { "value1", "value2" };
                return data;
            });
            t.Start();
            return t;
        }
    }
}