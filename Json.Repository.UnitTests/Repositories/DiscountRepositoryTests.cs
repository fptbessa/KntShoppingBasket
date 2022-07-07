using AutoFixture;
using Domain.Model;
using Json.Repository.Mappers;
using Json.Repository.Repositories.Discount;
using Moq;
using Newtonsoft.Json;
using System.IO.Abstractions;
using Xunit;

namespace Json.Repository.UnitTests.Repositories
{
    public class DiscountRepositoryTests
    {
        private readonly Mock<JsonDiscountDiscountMapper> mockDiscountMapper;
        private readonly Mock<IFileSystem> mockFileSystem;

        private readonly DiscountRepository discountRepository;

        private readonly Fixture fixture;

        public DiscountRepositoryTests()
        {
            this.mockDiscountMapper = new Mock<JsonDiscountDiscountMapper>();
            this.mockFileSystem = new Mock<IFileSystem>();

            this.fixture = new Fixture();

            this.discountRepository = new DiscountRepository(
                mockDiscountMapper.Object,
                mockFileSystem.Object,
                this.fixture.Create<string>());
        }

        [Fact]
        public void DiscountRepositoryTests_ReadAllDiscounts_FileExists()
        {
            //Arrange
            var resultFromMapper = this.fixture.CreateMany<Discount>().ToList();
            var resultFromJsonRead = JsonConvert.SerializeObject(resultFromMapper);

            this.mockFileSystem
                .Setup(method => method.File.ReadAllText(It.IsAny<string>()))
                .Returns(resultFromJsonRead);

            //Act
            var result = this.discountRepository.GetAllDiscounts();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(resultFromMapper.Count, result.Count);

            mockFileSystem.Verify(method => method.File.ReadAllText(It.IsAny<string>()), Times.Once);
        }

        //Validating things other than happy paths
        [Fact]
        public void DiscountRepositoryTests_ReadAllDiscounts_FileDoesNotExist()
        {
            //Arrange
            this.mockFileSystem
                .Setup(method => method.File.ReadAllText(It.IsAny<string>()))
                .Throws<FileNotFoundException>();

            //Act & Assert
            Assert.Throws<FileNotFoundException>(() => this.discountRepository.GetAllDiscounts());
        }
    }
}