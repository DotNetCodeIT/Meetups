using Albedo;
using AutoFixture.Idioms;
using AutoFixture.Xunit2;
using BaseUnitTests;
using Food.API.Dtos;
using Xunit;

namespace Food.API.UnitTests.DataTransferObjects
{
    public class FoodDtoTest
    {
        [Theory, AutoData]
        public void Test_Getter_And_Setters_FoodDto(WritablePropertyAssertion assertion)
        {
             assertion.Verify(new Properties<FoodDto>().Select(c => c.Id));
             assertion.Verify(new Properties<FoodDto>().Select(c => c.Name));
             assertion.Verify(new Properties<FoodDto>().Select(c => c.Calories));
             assertion.Verify(new Properties<FoodDto>().Select(c => c.Type));
             assertion.Verify(new Properties<FoodDto>().Select(c => c.Created));
        }
    }
}