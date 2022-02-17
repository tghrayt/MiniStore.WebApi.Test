using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniStore.Context;
using MiniStore.Controllers;
using MiniStore.Dto;
using MiniStore.Models;
using MiniStore.Repositories;
using MiniStore.Services;
using Moq;
using System;
using Xunit;

namespace MiniStoreTest
{
    public class CategoriesController
    {
        private readonly Mock<ICategoryService> _categoryService;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<ILogger<CategoryController>> _logger;
        private readonly CategoryController _categoryController;

        public CategoriesController()
        {

            _categoryService = new Mock<ICategoryService>();
            _mapper = new Mock<IMapper>();
            _logger = new Mock<ILogger<CategoryController>>();

            _categoryController = new CategoryController(_categoryService.Object, _mapper.Object, _logger.Object);

        }

        [Fact]
        public void GetAllCategories_WhenCalled_ReturnsOkResult()
        {
            // Arrange

            // Act
            var okResult = _categoryController.GetAllCategories();
            // Assert
            Assert.IsType<ObjectResult>(okResult.Result as ObjectResult);
            //Assert.Equals(okResult.Value)
        }


        [Fact]
        public void GetCatgoryByID_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            int codeCategorie = 2;
            _categoryService.Setup(r => r.GetCatgoryByID(It.IsAny<int>()))
                .Callback<int>(x => codeCategorie = x);


            // Act
            var code = 2;
            var categoryReturned = _categoryController.GetCatgoryByID(code);
            _categoryService.Verify(x => x.GetCatgoryByID(It.IsAny<int>()), Times.Once);

            // Assert

            Assert.Equal(code, codeCategorie);
        }

        [Fact]
        public void AddCategory_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            Category categorie = null;
            _categoryService.Setup(r => r.AddCategory(It.IsAny<Category>()))
                .Callback<Category>(x => categorie = x);


            // Act
           
            var categoryDto = new CategoryDto
            {
                CategoryId = 4,
                CategoryName = "Test_Name"
            };
            var categoryReturned = _categoryController.AddCategory(categoryDto);
            _categoryService.Verify(x => x.AddCategory(It.IsAny<Category>()), Times.Once);


            // Assert

            Assert.True(categoryReturned.IsCompleted);
        }


       


    }
}
