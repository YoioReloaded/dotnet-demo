using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

using DependencyInjectionDemo.Controllers;
using DependencyInjectionDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DependencyInjectionDemo.Tests
{
    [TestClass]
    public class ValuesController_ReturnsCustomersArray
    {
        public ValuesController_ReturnsCustomersArray()
        {
        }

        [TestMethod]
        public void ItReturnsAllElementsInDataSource()
        {
            // Arrange
            ValuesController _controller = new ValuesController(new Mock_UnitOfWork());

            // Act
            var result = _controller.Get().Result.Result as OkObjectResult;
            
            // Assert
            Assert.IsNotNull(result, "The request result was null");

            var values = result.Value as List<Customer>;
            Assert.AreEqual<int>(2, values.Count);
            Assert.AreEqual<string>("Jane Doe", values[0].Name);
            Assert.AreEqual<string>("John Smith", values[1].Name);
        }
    }
}
