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

			[TestMethod]
			public async Task ImportProductsFromCsv_ValidSingleProduct_AddsAndSaves()
			{
				var csv = "Id,Name,Price,Quantity,Sku,CreatedAt\n1,Peach,2.99,5,PCH001,2025-09-16";
				var stream = new MemoryStream(Encoding.UTF8.GetBytes(csv));
				var fileMock = new Mock<Microsoft.AspNetCore.Http.IFormFile>();
				fileMock.Setup(f => f.OpenReadStream()).Returns(stream);
				fileMock.Setup(f => f.Length).Returns(stream.Length);

				var mockRepo = new Mock<IProductRepository>();
				var mockValidator = new Mock<IProductValidator>();
				mockValidator.Setup(v => v.Validate(It.IsAny<Product>())).Returns(new List<string>());
				var service = new ProductService(mockValidator.Object, mockRepo.Object);

				await service.ImportProductsFromCsv(fileMock.Object);

				mockRepo.Verify(r => r.AddAsync(It.Is<Product>(p => p.Name == "Peach")), Times.Once);
				mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
			}

			[TestMethod]
			public async Task ImportProductsFromCsv_ValidMultipleProducts_AllAddedAndSaved()
			{
				var csv = "Id,Name,Price,Quantity,Sku,CreatedAt\n1,Grape,1.49,12,GRP001,2025-09-16\n2,Mango,3.99,8,MNG001,2025-09-16";
				var stream = new MemoryStream(Encoding.UTF8.GetBytes(csv));
				var fileMock = new Mock<Microsoft.AspNetCore.Http.IFormFile>();
				fileMock.Setup(f => f.OpenReadStream()).Returns(stream);
				fileMock.Setup(f => f.Length).Returns(stream.Length);

				var mockRepo = new Mock<IProductRepository>();
				var mockValidator = new Mock<IProductValidator>();
				mockValidator.Setup(v => v.Validate(It.IsAny<Product>())).Returns(new List<string>());
				var service = new ProductService(mockValidator.Object, mockRepo.Object);

				await service.ImportProductsFromCsv(fileMock.Object);

				mockRepo.Verify(r => r.AddAsync(It.Is<Product>(p => p.Name == "Grape")), Times.Once);
				mockRepo.Verify(r => r.AddAsync(It.Is<Product>(p => p.Name == "Mango")), Times.Once);
				mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
		}
	}
}
