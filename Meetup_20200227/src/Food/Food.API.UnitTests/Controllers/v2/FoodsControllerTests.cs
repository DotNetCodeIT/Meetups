using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BaseUnitTests;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Food.API.Dtos;
using Food.API.Entities;
using Food.API.Models;
using Food.API.Repositories;
using Food.API.v2.Controllers;
using Xunit;

namespace Food.API.UnitTests.Controllers.v2
{
    public class FoodsControllerTests : BaseAutoMockedTest<FoodsController>
    {
        [Fact]
        public void Get_should_return_foods()
        {
            // Given
            var foods = Enumerable.Repeat(new FoodEntity(), 5);
            var foodDtos = Enumerable.Repeat(new FoodDto(), 5);

            var queryParameters = new QueryParameters();
            GetMock<IFoodRepository>().Setup(x => x.GetAll(queryParameters)).Returns(foods.AsQueryable());
            GetMock<IMapper>().Setup(x => x.Map<FoodDto>(It.IsAny<FoodEntity>())).Returns(foodDtos.First());
            GetMock<IMapper>().Setup(x => x.Map<IEnumerable<FoodDto>>(It.IsAny<List<FoodEntity>>())).Returns(foodDtos);

            var controller = ClassUnderTest;

            // Ensure the controller can add response headers
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();

            // When
            var result = controller.GetAllFoods(ApiVersion.Default, queryParameters);

            // Then
            result.Should().BeOfType<OkObjectResult>();
        }
    }
}
