using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using RodosApi.Domain;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace RodosApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Coating> Coatings { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Door> Doors { get; set; }
        public DbSet<DoorModel> DoorModels { get; set; }
        public DbSet<DoorHandle> DoorHandles { get; set; }
        public DbSet<FurnitureType> FurnitureTypes { get; set; }
        public DbSet<Hinges> Hinges { get; set; }
        public DbSet<Maker> Makers { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<TypeOfDoor> TypesOfDoors { get; set; }
        public DbSet<TypeOfHinge> TypesOfHinges { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<DeliveryStatus> DeliveryStatuses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderHinges> OrderHinges { get; set; }
        public DbSet<OrderDoorHandle> OrderDoorHandles { get; set; }
        public DbSet<OrderDoor> OrderDoors { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Door>().HasOne(s => s.Category).WithOne();
            builder.Entity<Door>().HasOne(s => s.DoorModel).WithOne().OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Door>().HasOne(s => s.Coating).WithOne();
            builder.Entity<Door>().HasOne(s => s.Collection).WithOne().OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Door>().HasOne(s => s.Color).WithOne().OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Door>().HasOne(s => s.Maker).WithOne().OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Door>().HasOne(s => s.DoorHandle).WithOne().OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Door>().HasOne(s => s.Hinges).WithOne().OnDelete(DeleteBehavior.NoAction);

            builder.Entity<OrderHinges>().HasOne(s => s.Hinges).WithMany(s => s.OrderHinges).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<OrderHinges>().HasOne(s => s.Order).WithMany(s => s.Hinges).OnDelete(DeleteBehavior.NoAction);

            builder.Entity<OrderDoorHandle>().HasOne(s => s.DoorHandle).WithMany(c=>c.OrderDoorHandle).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<OrderDoorHandle>().HasOne(s => s.Order).WithMany(c=>c.DoorHandles).OnDelete(DeleteBehavior.NoAction);

            builder.Entity<OrderDoor>().HasOne(s => s.Door).WithMany(s => s.OrderDoors).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<OrderDoor>().HasOne(s => s.Order).WithMany(s => s.Doors).OnDelete(DeleteBehavior.NoAction);


            AddSeedData(builder);
        }


        private void AddSeedData(ModelBuilder builder)
        {
            builder.Entity<Category>().HasData(new Category[]
            {
                new Category(){CategoryId= 1,Name = "Furniture" },
                new Category(){CategoryId= 2,Name = "Door" },
            });

            builder.Entity<Coating>().HasData(new Coating[]
            {
                new Coating(){Id = 1, Name ="Metal" },
                new Coating(){Id = 2, Name ="Wood" }
            });

            builder.Entity<Collection>().HasData(new Collection[]
            {
                new Collection(){CollectionId = 1, Name ="Basic street" },
                new Collection(){CollectionId = 2, Name ="White king" }
            });

            builder.Entity<Color>().HasData(new Color[]
            {
                new Color(){Id = 1, Name ="Red" },
                new Color(){Id = 2, Name ="Blue" }
            });

            builder.Entity<DoorModel>().HasData(new DoorModel[]
            {
                new DoorModel(){Id = 1, Name ="Bas 001" },
                new DoorModel(){Id = 2, Name ="White winter 002" }
            });

            builder.Entity<FurnitureType>().HasData(new FurnitureType[]
            {
                new FurnitureType(){FurnitureId = 1, Name ="Door Handle" },
                new FurnitureType(){FurnitureId= 2, Name ="Hinges" }
            });

            builder.Entity<Country>().HasData(new Country[]
            {
                new Country(){CountryId = 1, Name ="Ukraine" },
                new Country(){CountryId= 2, Name ="Italia" }
            });

            builder.Entity<Maker>().HasData(new Maker[]
           {
                new Maker(){MakerId = 1, Name ="Rodos", CountryId = 1 },
                new Maker(){MakerId= 2, Name ="Mario", CountryId = 2 }
           });

            builder.Entity<Material>().HasData(new Material[]
           {
                new Material(){MaterialId = 1, Name ="Chrome" },
                new Material(){MaterialId= 2, Name ="Diamond" }
           });

            builder.Entity<TypeOfDoor>().HasData(new TypeOfDoor[]
            {
                new TypeOfDoor(){Id = 1, Name ="Entrance door" },
                new TypeOfDoor(){Id= 2, Name ="Interior door" }
            });

            builder.Entity<TypeOfHinge>().HasData(new TypeOfHinge[]
            {
                new TypeOfHinge(){TypeOfHingeId = 1, Name ="Mortise looks" },
                new TypeOfHinge(){TypeOfHingeId= 2, Name ="Other looks" }
            });

            builder.Entity<DoorHandle>().HasData(new DoorHandle[]
            {
                new DoorHandle(){FurnitureTypeId = 1, CategoryId = 1, ColorId = 1, DoorHandleId = 1, MakerId = 2, MaterialId = 1, Name = "Forme Door handle italia", Price = 245.34m},
                new DoorHandle(){FurnitureTypeId = 1, CategoryId = 1, ColorId = 2, DoorHandleId = 2, MakerId = 1, MaterialId = 2, Name = "Door handle water+ Rodos", Price = 450.54m},
            });

            builder.Entity<Hinges>().HasData(new Hinges[]
            {
                new Hinges(){FurnitureTypeId = 1, CategoryId = 1, HingesId =1, MakerId = 2, MaterialId = 1, Name = "Magnetic look", Price = 700.23m , TypeOfHingesId =2},
                new Hinges(){FurnitureTypeId = 1, CategoryId = 1, HingesId = 2,  MakerId = 1, MaterialId = 2, Name = "Deffault look", Price = 104.94m,TypeOfHingesId =1},
            });

            builder.Entity<Door>().HasData(

                 new Door()
                 {
                     Description = "White death door",
                     DoorId = 1,
                     Name = "winter aid",
                     Price = 5000.72m,
                     CategoryId = 2,
                     HingesId = 1,
                     MakerId = 2,
                     CoatingId = 1,
                     CollectionId = 1,
                     ColorId = 2,
                     DoorHandleId = 1,
                     TypeOfDoorId = 1,
                     DoorModelId = 1
                 });

            builder.Entity<Client>().HasData(new Client[] 
            {
                new Client(){ClientId =1, Address = "ms 1 street",Name = "Oleg", LastName = "Krigan",Phone = "380500653293",Email = "OlegKick@ukr.net" },
                new Client(){ClientId =2, Address = "BubleCity5 street",Name = "Finn", LastName = "Mortens",Phone = "",Email = "ADventureTime@ukr.net" }
            });

            builder.Entity<DeliveryStatus>().HasData(new DeliveryStatus[]{
                new DeliveryStatus(){Id = 1, Name = "Preparing for delivery"},
                new DeliveryStatus(){Id = 2, Name = "In delivery process"},
                new DeliveryStatus(){Id = 3, Name = "Was delivered"},
            });
            builder.Entity<IdentityRole>().HasData(new IdentityRole[]
            {
                new IdentityRole(){
                Id = Guid.NewGuid().ToString(),
                Name = "Admin"},

                new IdentityRole()
                {
                Id = Guid.NewGuid().ToString(),
                Name = "User"}
            });
        }
    }
}
