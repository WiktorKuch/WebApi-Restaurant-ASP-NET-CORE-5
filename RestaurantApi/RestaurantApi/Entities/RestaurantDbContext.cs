using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Entities   // klasa reprezentująca baze danych - zawiera właściwości odwzorowujące tabele w bazie danych 
{
    public class RestaurantDbContext : DbContext
    {
        //private string _connectionString = "Server=(localdb)\\Local;Database=RestaurantDb;Trusted_Connection=True;";
        //"Server=(localdb)\\mssqllocaldb;Database=RestaurantDb;Trusted_Connection=True;";

        //odwzorowanie 3 tabel na podstawie utworzonych klas 
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options)
        {

        }



        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        


        // w tej klasie - skonfigurowanie takich rzeczy jak połączenie do bazy
        // danych czy też dodatkowe właściwości które powinny zawierać kolumny bezpośrednio w bazie danych




        //jesli chcemy określić ,że nazwa restauracji jest kolumną wymaganą w bazie danych musimy nadpisać metode onmodelcreating która przyjmuje obiekt modelbuilder 
        // na tym obiekcie wywołujemy Entity< jaką encję chce skonfigurowac > Dla tej encji chcemy skonfigurować właściwość property name ze jest wymagana oraz jej
        // max długość.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();

            modelBuilder.Entity<Role>()
                .Property(u => u.Name)
                .IsRequired();


            modelBuilder.Entity<Restaurant>()
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(25);

            modelBuilder.Entity<Dish>()
                .Property(r => r.Name)
                .IsRequired();

            modelBuilder.Entity<Address>()
                .Property(d=>d.City)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Address>()
                .Property(a=>a.Street)
                .IsRequired()
                .HasMaxLength(50);

           
        }

        // skonfigurowanie połączenia do bazy danych poprzez nadpisanie onConfiguring 
        // jakiego typu bazy danych chce używać oraz jak powinno wyglądać połączenie do tej bazy danych 



        // aby na podstawie tej klasy utworzyć baze danych trzeba utworzyć migrację która wskaże EntityFramework wszystkie kroki które
        // są niezbędne aby odwzorować tą klasę w bazie danych - tools - manager nuget - console -> add-migration Init 

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
       */
    }
}
