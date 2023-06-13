
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestaurantApi.Controllers
{
    [Route("api/restaurant")]
    [ApiController]
    [Authorize]
    public class RestaurantController : ControllerBase
    {
        // dodaję akcję która będzie odpowiadać na zapytania typu
        // get i zwróci ona wszystkie rezultaty restauracji z naszej bazdy danych do klienta 

        private readonly IRestaurantService _restaurantService;
         
        public RestaurantController(IRestaurantService restaurantService) 
        {
            _restaurantService = restaurantService;
        }


        [HttpPut("{id}")]
        public ActionResult UpDate([FromBody]UpdateRestaurantDto dto, [FromRoute] int id)
        {
            
            _restaurantService.UpDate(id, dto);
                                   
            return Ok();

        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute]int id)
        {
           _restaurantService.Delete(id);

           return NoContent();
                      
        } 

        [HttpPost]
        [Authorize(Roles ="Admin,Manager")]
        
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto) // uzywajac mapera zmapuje dto od klienta do obiektu  typu restaurant 
        {
            var userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var id =_restaurantService.Create(dto);

            return Created($"/api/restaurant/{id}",null); // status created , uri i ciało odpowiedzi 

        }

        [HttpGet]
        [Authorize(Policy = "CreatedAtleast2Restaurants")]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll([FromQuery]  RestaurantQuery query)
        {
            var restaurantDtos = _restaurantService.GetAll(query);
            return Ok(restaurantDtos);
        }
         

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<RestaurantDto> Get([FromRoute]int id)
        {
            var restaurant = _restaurantService.GetById(id);
                                 
            
            return Ok(restaurant);
        }



    }
}
