using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using RestaurantAPI.Entities;
using System;
using System.Collections.Generic;

namespace RestaurantAPI.Entities
{
    public class Restaurant
    {
        public int Id { get; set; }
        public  string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }   
        public bool HasDelivery { get; set; }
        public string ContactEmail { get; set; }
        public string ContactNumber { get; set; }
        public int? CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }

        public int AddressId { get; set; }  // na podstawie tej właściwości EF utworzy referencje do osobnej tabeli którą zaraz utworzymy
                                            // bedzzie reprezentować klucz obcy do tabeli z adresem 

        public virtual Address Address { get; set; } // wirtualne property aby łatwiej posługiwać się obiektem
                                                     // typu restaurant kiedy pobieżemy go z bazy danych , aby
                                                     // mieć bezpośredni dostęp do adresu danej restauracji jak i listy dań
                                                     // jeżeli moich zapytań dołącze te konkretne tabele 

        public virtual List<Dish> Dishes { get; set; }



    }
}
