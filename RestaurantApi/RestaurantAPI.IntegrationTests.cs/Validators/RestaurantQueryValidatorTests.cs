using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using RestaurantAPI.Models.Validators;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace RestaurantAPI.IntegrationTests.Validators
{
    
    public class RestaurantQueryValidatorTests
    {

        public static IEnumerable<object[]> GetSampleValidData()
        {
            var list = new List<RestaurantQuery>()
            {

                new RestaurantQuery()
                {
                PageNumber = 1,
                PageSize = 10,
                },

                new RestaurantQuery()
                {
                PageNumber = 2,
                PageSize = 15,
                },

                new RestaurantQuery()
                {
                PageNumber = 22,
                PageSize = 5,
                SortBy = nameof(Restaurant.Name)
                },

                new RestaurantQuery()
                {
                PageNumber = 12,
                PageSize = 15,
                SortBy = nameof(Restaurant.Category)
                },

                };
            return list.Select(g => new object[] { g });

        }


        public static IEnumerable<object[]> GetSampleInValidData()
        {
            var list = new List<RestaurantQuery>()
            {

                new RestaurantQuery()
                {
                PageNumber = 0,
                PageSize = 10,
                },

                new RestaurantQuery()
                {
                PageNumber = 2,
                PageSize = 7,
                },

                new RestaurantQuery()
                {
                PageNumber = 22,
                PageSize = 5,
                SortBy = nameof(Restaurant.ContactEmail)
                },

                new RestaurantQuery()
                {
                PageNumber = 12,
                PageSize = 15,
                SortBy = nameof(Restaurant.ContactNumber)
                },

                };
            return list.Select(g => new object[] { g });

        }

        [Theory]
        [MemberData(nameof(GetSampleValidData))]
        public void Validate_ForCorrectModel_ReturnsSuccess( RestaurantQuery model )
        {
            //arrange
            var validator = new RestaurantQueryValidator();
            

            //act 
            var result = validator.TestValidate(model);
            //assert
            result.ShouldNotHaveAnyValidationErrors(); 

        }

        [Theory]
        [MemberData(nameof(GetSampleInValidData))]
        public void Validate_ForInCorrectModel_ReturnsFailure(RestaurantQuery model)
        {
            //arrange
            var validator = new RestaurantQueryValidator();


            //act 
            var result = validator.TestValidate(model);
            //assert
            result.ShouldHaveAnyValidationError();

        }



    }
}
