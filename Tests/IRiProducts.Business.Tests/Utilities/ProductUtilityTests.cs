using System;
using System.Linq;
using IRiProducts.Business.Models.Csv;
using IRiProducts.Business.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IRiProducts.Business.Tests.Utilities
{
    public class ProductUtilityTests
    {
        [TestClass]
        public class FilterByCodeType
        {
            private static IProductUtility _productUtility;

            [ClassInitialize]
            public static void Setup(TestContext context)
            {
                _productUtility = new ProductUtility();
            }

            [TestMethod]
            public void ReturnsValid()
            {
                // Arrange
                var retailerProduct1 = new RetailerProduct { Id = 18886, RetailerName = "DDS", RetailerProductCode = "93482745", RetailerProductCodeType = "Barcode", DateReceived = new DateTime(2010, 05, 16) };
                var retailerProduct2 = new RetailerProduct { Id = 18886, RetailerName = "Woolworths", RetailerProductCode = "F8CE71964FAC90E59164FDB6AA19B10A", RetailerProductCodeType = "Woolworths Ref", DateReceived = new DateTime(2017, 05, 09) };
                var retailerProduct3 = new RetailerProduct { Id = 18886, RetailerName = "Woolworths", RetailerProductCode = "017E9562042C3E9F0E1D200A8C915052", RetailerProductCodeType = "Woolworths Ref", DateReceived = new DateTime(2017, 10, 03) };
                var retailerProduct4 = new RetailerProduct { Id = 18886, RetailerName = "Coles", RetailerProductCode = "93482745", RetailerProductCodeType = "Barcode", DateReceived = new DateTime(2006, 04, 23) };
                var retailProducts = new RetailerProduct[] { retailerProduct1, retailerProduct2, retailerProduct3, retailerProduct4 };
                var iriProducts = new IRiProduct[] { new IRiProduct { Id = 18886, Name = "FISH OIL" } };

                // Act
                var filteredProducts = _productUtility.FilterByCodeType(retailProducts, iriProducts);

                // Assert
                // Assert that retailerProduct1 and retailerProduct3 are the latest in their RetailerProductCodeType group of Barcode
                var filteredProduct1 = filteredProducts[0];
                Assert.AreEqual(retailerProduct1.RetailerProductCode, filteredProduct1.Code);
                Assert.AreEqual(retailerProduct1.RetailerProductCodeType, filteredProduct1.CodeType);
                Assert.AreEqual(retailerProduct1.DateReceived, filteredProduct1.DateReceived);
                Assert.AreEqual(retailerProduct1.Id, filteredProduct1.Id);
                Assert.AreEqual(retailerProduct1.RetailerName, filteredProduct1.RetailerName);

                var filteredProduct2 = filteredProducts[1];
                Assert.AreEqual(retailerProduct3.RetailerProductCode, filteredProduct2.Code);
                Assert.AreEqual(retailerProduct3.RetailerProductCodeType, filteredProduct2.CodeType);
                Assert.AreEqual(retailerProduct3.DateReceived, filteredProduct2.DateReceived);
                Assert.AreEqual(retailerProduct3.Id, filteredProduct2.Id);
                Assert.AreEqual(retailerProduct3.RetailerName, filteredProduct2.RetailerName);
            }

            [TestMethod]
            public void ReturnsValidCount()
            {
                // Arrange
                var retailerProduct1 = new RetailerProduct { Id = 18886, RetailerName = "Woolworths", RetailerProductCode = "F8CE71964FAC90E59164FDB6AA19B10A", RetailerProductCodeType = "Woolworths Ref", DateReceived = new DateTime(2017, 05, 09) };
                var retailerProduct2 = new RetailerProduct { Id = 18886, RetailerName = "Woolworths", RetailerProductCode = "017E9562042C3E9F0E1D200A8C915052", RetailerProductCodeType = "Woolworths Ref", DateReceived = new DateTime(2017, 10, 03) };
                var retailProducts = new RetailerProduct[] { retailerProduct1, retailerProduct2 };
                var iriProducts = new IRiProduct[] { new IRiProduct { Id = 18886, Name = "FISH OIL" } };

                // Act
                var filteredProducts = _productUtility.FilterByCodeType(retailProducts, iriProducts);

                // Assert
                // Assert that there are only two items as there are two items in filteredProducts as there are only two RetailerProductCodeType
                Assert.AreEqual(filteredProducts.Count, 1);
            }

            [TestMethod]
            public void ReturnsNameNotAvailable()
            {
                // Arrange
                var retailerProduct1 = new RetailerProduct { Id = 18886, RetailerName = "Woolworths", RetailerProductCode = "F8CE71964FAC90E59164FDB6AA19B10A", RetailerProductCodeType = "Woolworths Ref", DateReceived = new DateTime(2017, 05, 09) };
                var retailerProduct2 = new RetailerProduct { Id = 18886, RetailerName = "Woolworths", RetailerProductCode = "017E9562042C3E9F0E1D200A8C915052", RetailerProductCodeType = "Woolworths Ref", DateReceived = new DateTime(2017, 10, 03) };
                var retailProducts = new RetailerProduct[] { retailerProduct1, retailerProduct2 };
                var iriProducts = new IRiProduct[] { new IRiProduct { Id = 124511, Name = "FISH OIL" } };
                var notAvailable = Constants.Product.NameNotAvailable;

                // Act
                var filteredProducts = _productUtility.FilterByCodeType(retailProducts, iriProducts);

                // Assert
                // Assert that all product names equal to notAvailable as the id is not in iriProducts 
                Assert.IsTrue(filteredProducts.All(p => p.Name == notAvailable));
            }
        }
    }
}