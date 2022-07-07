using AutoFixture;
using Json.Repository.Entities;
using Json.Repository.Mappers;
using Xunit;

namespace Json.Repository.UnitTests.Repositories
{
    public class JsonDiscountDiscountMapperTests
    {
        private readonly JsonDiscountDiscountMapper discountMapper;

        private readonly Fixture fixture;

        public JsonDiscountDiscountMapperTests()
        {
            this.fixture = new Fixture();

            this.discountMapper = new JsonDiscountDiscountMapper();
        }

        [Fact]
        public void JsonDiscountDiscountMapperTests_Map_ValidEntity()
        {
            //Arrange
            var mapperInput = this.fixture.Create<JsonDiscount>();

            //Act
            var result = this.discountMapper.Map(mapperInput);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(mapperInput.Description, result.Description);
            Assert.Equal(mapperInput.DiscountTimeGateStart, result.DiscountTimeGateStart);
            Assert.Equal(mapperInput.DiscountTimeGateEnd, result.DiscountTimeGateEnd);
            Assert.Equal(mapperInput.DiscountPercentage, result.DiscountPercentage);
            Assert.Equal(mapperInput.DiscountType, result.DiscountType);
            Assert.Equal(mapperInput.DiscountBuyMoreItemsQty, result.DiscountBuyMoreItemsQty);
            Assert.Equal(mapperInput.DiscountBuyMoreItemsType, result.DiscountBuyMoreItemsType);
            Assert.Equal(mapperInput.Product, result.Product);
        }

        [Fact]
        public void DiscountRepositoryTests_ReadAllDiscounts_FileDoesNotExist()
        {
            //Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => this.discountMapper.Map(null));
        }
    }
}