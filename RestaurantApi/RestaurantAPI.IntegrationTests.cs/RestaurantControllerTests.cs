﻿using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Moq;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using RestaurantAPI.Entities;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using RestaurantAPI.Models;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Authorization.Policy;
using RestaurantAPI.IntegrationTests.Helpers;

namespace RestaurantAPI.IntegrationTests
{
    public class RestaurantControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private HttpClient _client;
        private WebApplicationFactory<Startup> _factory;

        public RestaurantControllerTests(WebApplicationFactory<Startup> factory )
        {
            //var factory = new WebApplicationFactory<Startup>();
            _factory = factory
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var dbContextOptions = services
                        .SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<RestaurantDbContext>));

                        services.Remove(dbContextOptions);

                        services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();

                        services.AddMvc(option => option.Filters.Add(new FakeUserFilter()));

                        services.AddDbContext<RestaurantDbContext>(options => options.UseInMemoryDatabase("RestaurantDb"));
                    });

                });
                
            _client=_factory.CreateClient();

        }

        private void SeedRestaurant(Restaurant restaurant)
        {
            var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<RestaurantDbContext>();

            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task Delete_ForNonRestaurantOwner_ReturnsForbidden()
        {
            //arrange 

            var restaurant = new Restaurant()
            {
                CreatedById = 900
                
            };

            //seed
            SeedRestaurant(restaurant);
            //act 
            var response = await _client.DeleteAsync("/api/restaurant/" + restaurant.Id);

            //assert 
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Forbidden);
        }


        [Fact]
        public async Task Delete_ForRestaurantOwner_ReturnsNoContent()
        {
            //arrange 

            var restaurant = new Restaurant()
            {
                CreatedById = 1,
                Name = "Test",
            };

            //seed
            SeedRestaurant(restaurant);
            //act 
            var response = await _client.DeleteAsync("/api/restaurant/" + restaurant.Id);

            //assert 
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Delete_ForNonExistingRestaurant_ReturnsNotFound()
        {
            //act
            var response = await _client.DeleteAsync("/api/restaurant/987");
            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);

        } 



        [Fact]
        public async Task CreateRestaurant_WithValidModel_ReturnsCreatedStatus()
        {
            //arrange
            var model = new CreateRestaurantDto()
            {
                Name = "TestRestauran",
                City = "Kraków",
                Street = "Długa 5"
            };



            var httpContent = model.ToJsonHttpContent(); 

            //act 
            var response = await _client.PostAsync("/api/restaurant", httpContent);
            //assert 
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
            response.Headers.Location.Should().NotBeNull();
        }

        [Fact]
        public async Task Createrestaurant_WithInValidModel_ReturnsBadRequest()
        {
            //arrange
            var model = new CreateRestaurantDto()
            {
                ContactEmail = "test@test.com",
                Description = "test desc",
                ContactNumber = "999 888 777"
            };

            var httpContent = model.ToJsonHttpContent();
            //act 
            var response = await _client.PostAsync("/api/restaurant", httpContent);

            //assert 
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);


        }

        [Theory]
        [InlineData("pageSize=5&pageNumber=1")]
        [InlineData("pageSize=15&pageNumber=2")]
        [InlineData("pageSize=10&pageNumber=3")]
        public async Task GetAll_WithQueryParameters_ReturnsOkResult(string queryParams)
        {
            //arrange

            //var factory = new WebApplicationFactory<Startup>();
            //var client = factory.CreateClient();

            //act
            var response = await _client.GetAsync("/api/restaurant?"+queryParams);


            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        }
        [Theory]
        [InlineData("pageSize=100&pageNumber=3")]
        [InlineData("pageSize=11&pageNumber=1")]
        [InlineData(null)]
        [InlineData("")]
        public async Task GetAll_WithInvalidQueryparams_ReturnsbadRequest(string queryParams)
        {
            //var factory = new WebApplicationFactory<Startup>();
            //var client = factory.CreateClient();

            var response = await _client.GetAsync("/api/restaurant?" + queryParams);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);  


        }
        
        
    }
}
