using System;
using System.Collections.Generic;
using System.Text;
using Albedo;
using AutoFixture.Idioms;
using AutoFixture.Xunit2;
using BaseUnitTests;
using Food.API.Entities;
using Xunit;

namespace Food.API.UnitTests.Models
{
	public class FoodTest
	{
		[Theory, AutoData]
		public void Test_food_auto_properties(WritablePropertyAssertion assertion)
		{
			assertion.Verify(new Properties<FoodEntity>().Select(c => c.Id));
			assertion.Verify(new Properties<FoodEntity>().Select(c => c.Name));
			assertion.Verify(new Properties<FoodEntity>().Select(c => c.Calories));
			assertion.Verify(new Properties<FoodEntity>().Select(c => c.Type));
			assertion.Verify(new Properties<FoodEntity>().Select(c => c.Created));
		}
	}
}
