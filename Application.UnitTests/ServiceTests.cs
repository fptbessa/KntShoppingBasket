using AutoFixture;
using Domain.Model;
using Json.Repository.Repositories.Discount;
using Json.Repository.Repositories.Product;
using Moq;
using Service;
using Xunit;

namespace Application.UnitTests
{
    public class ServiceTests
    {
        private readonly Mock<IDiscountRepository> mockDiscountRepository;
        private readonly Mock<IProductRepository> mockProductRepository;

        private readonly Fixture fixture;

        private readonly BillService billService;

        public ServiceTests()
        {
            //Using AutoMoq to mock the injected dependencies SOLID principle - D
            this.mockDiscountRepository = new Mock<IDiscountRepository>();
            this.mockProductRepository = new Mock<IProductRepository>();

            this.billService = new BillService(mockProductRepository.Object, mockDiscountRepository.Object);

            //Using autofixture to generate our inputs for the tests.
            //Functionality should often be agnostic from specific input
            this.fixture = new Fixture();
        }

        [Fact]
        public void ServiceTests_ShopForProducts_NoDiscounts()
        {
            //Arrange
            var productRepositoryItems = this.fixture.CreateMany<Product>().ToList();

            var discountRepositoryItems = new List<Discount>();

            var productsToBuy = productRepositoryItems
                .Select(productRepositoryItem => productRepositoryItem.Name.ToLower() ?? string.Empty)
                .ToList();

            this.mockProductRepository
                .Setup(method => method.GetAllProducts())
                .Returns(productRepositoryItems);

            this.mockDiscountRepository
                .Setup(method => method.GetAllDiscounts())
                .Returns(discountRepositoryItems);

            //Act
            var resultingBill = this.billService.ShopForProducts(productsToBuy);

            //Assert
            Assert.NotNull(resultingBill);
            Assert.Equal(productRepositoryItems.Count, resultingBill.Products.Count);
            Assert.Empty(resultingBill.AppliedDiscounts);
            Assert.Empty(resultingBill.Warnings);

            mockProductRepository.Verify(method => method.GetAllProducts(), Times.Once);
            mockDiscountRepository.Verify(method => method.GetAllDiscounts(), Times.Once);
        }

        [Fact]
        public void ServiceTests_ShopForProducts_Discounts()
        {
            var productRepositoryItems = this.fixture.CreateMany<Product>().ToList();

            var discountRepositoryItems = this.fixture
                .Build<Discount>()
                .With(prop => prop.Product, productRepositoryItems.First().Name)
                .With(prop => prop.DiscountType, DiscountType.TimeGate)
                .With(prop => prop.DiscountTimeGateStart, DateTime.Now.AddDays(-1))
                .With(prop => prop.DiscountTimeGateEnd, DateTime.Now.AddDays(1))
                .CreateMany(1)
                .ToList();

            var productsToBuy = productRepositoryItems
                .Select(productRepositoryItem => productRepositoryItem.Name.ToLower() ?? string.Empty)
                .ToList();

            this.mockProductRepository
                .Setup(method => method.GetAllProducts())
                .Returns(productRepositoryItems);

            this.mockDiscountRepository
                .Setup(method => method.GetAllDiscounts())
                .Returns(discountRepositoryItems);

            //Act
            var resultingBill = this.billService.ShopForProducts(productsToBuy);

            //Assert
            Assert.NotNull(resultingBill);
            Assert.Equal(productRepositoryItems.Count, resultingBill.Products.Count);
            Assert.Equal(discountRepositoryItems.Count, resultingBill.AppliedDiscounts.Count);
            Assert.Empty(resultingBill.Warnings);

            mockProductRepository.Verify(method => method.GetAllProducts(), Times.Once);
            mockDiscountRepository.Verify(method => method.GetAllDiscounts(), Times.Once);
        }

        [Fact]
        public void ServiceTests_ShopForProducts_NoSellableProducts()
        {
            var productRepositoryItems = this.fixture.CreateMany<Product>().ToList();

            var discountRepositoryItems = this.fixture.CreateMany<Discount>().ToList();

            this.mockProductRepository
                .Setup(method => method.GetAllProducts())
                .Returns(productRepositoryItems);

            this.mockDiscountRepository
                .Setup(method => method.GetAllDiscounts())
                .Returns(discountRepositoryItems);

            //Arrange
            var productsToBuy = this.fixture.CreateMany<string>(1);

            //Act
            var resultingBill = this.billService.ShopForProducts(productsToBuy);

            //Assert
            Assert.NotNull(resultingBill);
            Assert.Empty(resultingBill.Products);
            Assert.Empty(resultingBill.AppliedDiscounts);
            Assert.Single(resultingBill.Warnings);

            mockProductRepository.Verify(method => method.GetAllProducts(), Times.Once);
            mockDiscountRepository.Verify(method => method.GetAllDiscounts(), Times.Once);
        }
    }
}