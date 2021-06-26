using Moq;
using Xunit;
using Inventory_Management.Server.Data;
using Inventory_Management.Server.Controllers;
using AutoMapper;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Inventory_Management.Server.Tests
{
    public class ProductControllerTests
    {

        Mock<IProductRepository> mockRepository = new();

        public readonly IMapper mapper;

        public readonly MapperConfiguration mockMapper;

        public ProductControllerTests()
        {
            mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductMapperProfile());
            });

            mapper= mockMapper.CreateMapper();

        }

        [Fact]
        public  async void GetProductByID()
        {
            //Arrange
            var productResult = new Product() {
                Id = 1,
                Name = "Redmi Note",
                Description = "Xiaomi & co",
                Price = 10000,
                DiscountedPrice = 9999,
                Quantity=10,
                Category = "mobile"
            };
            mockRepository.Setup(i => i.GetProductByIDAsync(1)).ReturnsAsync(productResult);

            var productControllerObj = new ProductController(mockRepository.Object, mapper);

            //Act

            var response = await productControllerObj.GetProductByID(1);

            //Assert
            Assert.True(productResult.Equals(response.Value));
        }

        [Fact]
        public async void GetProductsAsync()
        {
            //Arrange
            var productResult = new Product[2]{
                   new Product() {
                Id = 1,
                Name = "Redmi Note",
                Description = "Xiaomi & co",
                Price = 10000,
                DiscountedPrice = 9999,
                Quantity=10,
                Category = "mobile"
            },
                new Product() {
                Id = 2,
                Name = "Realme x7",
                Description = "Xiaomi & co",
                Price = 10000,
                DiscountedPrice = 9999,
                Quantity=10,
                Category = "mobile"
            }
            };

            mockRepository.Setup(i => i.GetProductsAync()).ReturnsAsync(productResult);

            var productControllerObj = new ProductController(mockRepository.Object, mapper);

            //Act
            var response = await productControllerObj.GetProducts();

            //Assert
            Assert.True(productResult.Equals(response.Value));

        }

        [Fact]
        public async void AddProduct()
        {
            //Arrange 
            var productModel = new ProductModel()
            {
                Name = "Redmi note 8",
                Description = "Xiaomi & company",
                Price = 10000,
                DiscountedPrice = 99999,
                Quantity = 10,
                Category = "Mobile"
            };

            var newProduct = new Product()
            {
                Id=1,
                Name = "Redmi note 8",
                Description = "Xiaomi & company",
                Price = 10000,
                DiscountedPrice = 99999,
                Quantity = 10,
                Category = "Mobile"
            };
            var newProductResult = new Product()
            {
                Id=1,
                Name = "Redmi note 8",
                Description = "Xiaomi & company",
                Price = 10000,
                DiscountedPrice = 99999,
                Quantity = 10,
                Category = "Mobile"
            };
            mockRepository.Setup(i => i.AddProductAsync(newProduct)).ReturnsAsync(newProductResult);

            var productControllerObj = new ProductController(mockRepository.Object, mapper);

            //Act

            var response = await productControllerObj.CreateProduct(productModel);

            //Assert

            Assert.IsType<OkObjectResult>(response.Result);

        }

        [Fact]
        public async void DeleteProduct()
        {
            //Arrange
            var productResult = new Product()
            {
                Id = 1,
                Name = "Redmi Note",
                Description = "Xiaomi & co",
                Price = 10000,
                DiscountedPrice = 9999,
                Quantity = 10,
                Category = "mobile"
            };
            mockRepository.Setup(i => i.GetProductByIDAsync(1)).ReturnsAsync(productResult);
            mockRepository.Setup(i => i.DeleteProductByIDAsync(1)).ReturnsAsync(true);

            var productControllerObj = new ProductController(mockRepository.Object, mapper);

            //Act
            var response = await productControllerObj.DeleteProduct(1);

            //Assert
            Assert.IsType<OkResult>(response);



        }

        [Fact]
        public async void UpdateProduct()
        {

            //Arrange
            var productResult = new Product()
            {
                Id = 1,
                Name = "Redmi Note",
                Description = "Xiaomi & co",
                Price = 10000,
                DiscountedPrice = 9999,
                Quantity = 10,
                Category = "mobile"
            };
            var updatedProduct = new Product()
            {
                Id = 1,
                Name = "IPhone 12",
                Description = "Apple",
                Price = 100000,
                DiscountedPrice = 99999,
                Quantity = 10,
                Category = "mobile"
            };
            mockRepository.Setup(i => i.GetProductByIDAsync(1)).ReturnsAsync(productResult);
            mockRepository.Setup(i => i.UpdateProductAsync(updatedProduct)).ReturnsAsync(updatedProduct);

            var productControllerObj = new ProductController(mockRepository.Object, mapper);

            //Act

            var response = await productControllerObj.UpdateProduct(updatedProduct);

            //Assert
            Assert.IsType<OkObjectResult>(response.Result);
        }



        }
    }
