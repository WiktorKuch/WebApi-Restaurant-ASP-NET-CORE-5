

using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantAPI
{
    public class RestaurantSeeder
    {
        private readonly RestaurantDbContext _dbContext;
        public RestaurantSeeder(RestaurantDbContext dbContext ) 
        { 
            _dbContext=dbContext;

        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect()) // sprawdzimy połączenie do bazy danych - upewniam sie czy
                                                  // połączenie do bazy danych moze zostać nawiązane 
            {
                if (_dbContext.Database.IsRelational())
                {
                    var pendingMigrations = _dbContext.Database.GetPendingMigrations();//dla projektu webApi sprawdzenie czy są jakieś istniejące migracje
                    if (pendingMigrations != null && pendingMigrations.Any()) //jeśli tak to odpowiedni update tej bazy danych za pomocą EF
                    {
                        _dbContext.Database.Migrate();
                    }
                }


                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();

                }


                if(!_dbContext.Restaurants.Any()) //spawdzam czy tabela z restauracjami jest pusta - czy jest w tej tabeli jakikolwiek wiersz 
                {

                    var restaurants = GetRestaurants();
                    _dbContext.Restaurants.AddRange(restaurants);
                    _dbContext.SaveChanges(); // po zapisie zmian na konteksie baz danych EF powinien
                                              // dodac res jak i powiązane z nia encje do konkretnych tabel 

                }
            }
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                 new Role()
                {
                    Name = "Manager"
                },
                  new Role()
                {
                    Name = "Admin"
                },

                  
            };
            return roles;
        }

        private IEnumerable<Restaurant> GetRestaurants() // metoda zw kolekcje restauracji - ta metoda zwroci
                                                         // restauracje ktore zawsze beda istnieć w tabeli restaurant 
        {
            var resturants = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name = "KFC",
                    Category = "Fast food",
                    Description = "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered",
                    ContactEmail = "contact@kfc.com",
                    HasDelivery = true,

                    Dishes = new List<Dish>
                    {
                        new Dish()
                        {
                            Name = "Nashville Hot Chicken",
                            Price = 10.30M,

                        },

                        new Dish()
                        {
                            Name = "Chicken Nuggets",
                            Price = 5.30M,
                        },
                    },

                    Address = new Address()
                    {
                        City = "Kraków",
                        Street = "Długa 5",
                        PostalCode = "30-001"
                    }
                },
                new Restaurant()
                {
                    Name = "McDonald",
                    Category = "Fast food",
                    Description = "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises",
                    ContactEmail = "contact@mcdonald.com",
                    HasDelivery = true,
                    Address = new Address()
                    {
                        City ="Kraków",
                        Street = "Szewska 2",
                        PostalCode = "30-001"
                    }
                }
            };
            return resturants;

        }
    }
}
