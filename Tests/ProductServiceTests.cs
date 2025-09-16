using CsvImportDemo.Models;
using CsvImportDemo.Services;
using CsvImportDemo.Repositories;
using CsvImportDemo.Validators;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CsvImportDemo.Tests
{
	[TestClass]
	public class ProductServiceTests
	{
		[TestMethod]
		public async Task ImportProductsFromCsv_Mocks_AddsAndSavesValidProducts()
		{
			// Arrange: Simulate CSV input
			var csv = "Id,Name,Price,Quantity,Sku,CreatedAt\n1,Apple,1.99,10,APL001,2025-09-13\n2,Banana,0.99,20,BNN001,2025-09-13\n3,Orange,2.49,15,ORG001,2025-09-13";
			var stream = new MemoryStream(Encoding.UTF8.GetBytes(csv));
			var fileMock = new Mock<Microsoft.AspNetCore.Http.IFormFile>();
			fileMock.Setup(f => f.OpenReadStream()).Returns(stream);
			fileMock.Setup(f => f.Length).Returns(stream.Length);

			var mockRepo = new Mock<IProductRepository>();
			var mockValidator = new Mock<IProductValidator>();

			// Simulate validator returns no errors for Apple and Orange, errors for Banana
			mockValidator.Setup(v => v.Validate(It.Is<Product>(p => p.Name == "Apple" || p.Name == "Orange")))
				.Returns(new List<string>());
			mockValidator.Setup(v => v.Validate(It.Is<Product>(p => p.Name == "Banana")))
				.Returns(new List<string> { "Invalid" });

			var service = new ProductService(mockValidator.Object, mockRepo.Object);

			// Act
			await service.ImportProductsFromCsv(fileMock.Object);

			// Assert
			mockRepo.Verify(r => r.AddAsync(It.Is<Product>(p => p.Name == "Apple")), Times.Once);
			mockRepo.Verify(r => r.AddAsync(It.Is<Product>(p => p.Name == "Orange")), Times.Once);
			mockRepo.Verify(r => r.AddAsync(It.Is<Product>(p => p.Name == "Banana")), Times.Never);
			mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
		}
	}
}
