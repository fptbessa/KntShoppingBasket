using AutoFixture;
using Json.Repository.Entities;
using Json.Repository.Mappers;
using Xunit;

namespace Json.Repository.UnitTests.Repositories
{
    public class JsonProductProductMapperTests
    {
        private readonly JsonProductProductMapper productMapper;

        private readonly Fixture fixture;

        public JsonProductProductMapperTests()
        {
            this.fixture = new Fixture();

            this.productMapper = new JsonProductProductMapper();
        }

        [Fact]
        public void JsonProductProductMapperTests_Map_ValidEntity()
        {
            //Arrange
            var mapperInput = this.fixture.Create<JsonProduct>();

            //Act
            var result = this.productMapper.Map(mapperInput);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(mapperInput.Name, result.Name);
            Assert.Equal(mapperInput.Price, result.Price);
        }

        [Fact]
        public void DiscountRepositoryTests_ReadAllDiscounts_FileDoesNotExist()
        {
            //Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => this.productMapper.Map(null));
        }
    }
}