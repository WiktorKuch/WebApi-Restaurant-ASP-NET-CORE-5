using AutoMapper;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;

namespace RestaurantAPI
{
    public class RestaurantMappingProfile : Profile
    {
        public RestaurantMappingProfile()
        {
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(r => r.City, c => c.MapFrom(s => s.Address.City))
                .ForMember(r => r.Street, c => c.MapFrom(s => s.Address.Street))
                .ForMember(r => r.PostalCode, c => c.MapFrom(s => s.Address.PostalCode));

            CreateMap<Dish, DishDto>();           //w resturantDto używamy klasy DishDto ona
                                                  //również bedzie zmapowana na podstawie klasy Dish

            CreateMap<CreateRestaurantDto, Restaurant>() // w jaki sposób obiekt adresu dla restauracji
                                                         // bedzie zmapowany na podstawie modelu CreateRestaurantDto 
           .ForMember(r=>r.Address, c=>c.MapFrom(dto => new Address() 
            { City=dto.City , PostalCode = dto.PostalCode, Street = dto.Street })); // reszta właściwości zostanie zmapowana automatycznie

            CreateMap<CreateDishDto, Dish>();
            
        }
    }
}
 