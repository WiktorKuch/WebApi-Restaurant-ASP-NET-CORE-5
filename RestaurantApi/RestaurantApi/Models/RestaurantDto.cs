using System.Collections.Generic;

namespace RestaurantAPI.Models
{
    public class RestaurantDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool HasDelivery { get; set; }

        


        //dodatkowo dodałem informację o adresie w sposób
        //spłaszczony czyli bez dodatkowej referencji do osobnego obiektu 
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        // adres restauracji bedzie zawarty w tych trzech właściwościach zamiast osobnego obiektu 

        public List<DishDto> Dishes { get; set; }
    }
}
