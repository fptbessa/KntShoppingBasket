using AutoFixture;
using Domain.Model;
using Json.Repository.Mappers;
using Json.Repository.Repositories.Product;
using Moq;
using Newtonsoft.Json;
using System.IO.Abstractions;
using Xunit;

namespace Json.Repository.UnitTests.Repositories
{
    public class ProductRepositoryTests
    {
        private readonly Mock<JsonProductProductMapper> mockProductMapper;
        private readonly Mock<IFileSystem> mockFileSystem;

        private readonly ProductRepository productRepository;

        private readonly Fixture fixture;

        public ProductRepositoryTests()
        {
            this.mockProductMapper = new Mock<JsonProductProductMapper>();
            this.mockFileSystem = new Mock<IFileSystem>();

            this.fixture = new Fixture();

            this.productRepository = new ProductRepository(
                mockProductMapper.Object,
                mockFileSystem.Object,
                this.fixture.Create<string>());
        }

        [Fact]
        public void ProductRepositoryTests_ReadAllProducts_FileExists()
        {
            //Arrange
            var resultFromMapper = this.fixture.CreateMany<Product>().ToList();
            var resultFromJsonRead = JsonConvert.SerializeObject(resultFromMapper);

            this.mockFileSystem
                .Setup(method => method.File.ReadAllText(It.IsAny<string>()))
                .Returns(resultFromJsonRead);

            //Act
            var result = this.productRepository.GetAllProducts();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(resultFromMapper.Count, result.Count);

            mockFileSystem.Verify(method => method.File.ReadAllText(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void ProductRepositoryTests_ReadAllProducts_FileDoesNotExist()
        {
            //Arrange
            this.mockFileSystem
                .Setup(method => method.File.ReadAllText(It.IsAny<string>()))
                .Throws<FileNotFoundException>();

            //Act & Assert
            Assert.Throws<FileNotFoundException>(() => this.productRepository.GetAllProducts());
        }
    }
}