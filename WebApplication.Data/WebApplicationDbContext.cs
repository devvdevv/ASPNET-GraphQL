using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApplication.Domain.Models;

namespace WebApplication.Data
{
    public class WebApplicationDbContext : DbContext
    {
        private readonly string _seedPath = $@"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Seed")}";

        // TODO: fixed issue cannot run migration with this constructor
        public WebApplicationDbContext(DbContextOptions<WebApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Person> Person { get; set; }

        public DbSet<Car> Car { get; set; }

        public DbSet<Licence> Licence { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=dotNetGraphQL;Username=postgres;Password=changeme")
                .LogTo(Console.WriteLine,
                    new[] { DbLoggerCategory.Database.Command.Name },   
                    LogLevel.Information)
                .EnableSensitiveDataLogging();

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasMany(x => x.Cars)
                .WithOne(x => x.Owner)
                .HasForeignKey("OwnById")
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Person>()
                .OwnsMany(x => x.Licences, li =>
                {
                    li.ToTable(nameof(Licence)).HasKey("Id");
                    li.Property<long>("Id");
                });

            modelBuilder.Entity<Car>()
                .Property(x => x.Name);

            modelBuilder.Entity<Car>()
                .Property(x => x.Brand);
        }

        public void Seed()
        {
            SeedPeople();
            SeedCars();
        }

        private void SeedPeople()
        {
            if (Person.Any())
            {
                return;
            }

            var people = GetPersonSeedData();
            Person.AddRange(people);
            base.SaveChanges();
        }

        private void SeedCars()
        {
            var random = new Random();

            if (Car.Any())
            {
                return;
            }

            var cars = GetCarSeedData();
            var people = Person.ToList();

            foreach (var person in people)
            {
                if (!cars.Any())
                    break;

                var numberOfCarsCanOwn = random.Next(0, 4);
                var carSize = cars.Count();
                var carsCanOwn = cars.Take(carSize > numberOfCarsCanOwn ? numberOfCarsCanOwn : carSize);
                cars = cars.Where(x => !carsCanOwn.Contains(x));
                person.Cars.AddRange(carsCanOwn);
            }

            base.SaveChanges();
        }

        public IEnumerable<Person> GetPersonSeedData()
        {
            var seedPath = $@"{_seedPath}/Person.csv";

            var data = new List<Person>();
            using var reader = new StreamReader(seedPath);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line is null)
                    break;

                var values = line.Split(',');
                data.Add(new Person(values[0], values[1]));
            }

            return data;
        }

        public IEnumerable<Car> GetCarSeedData()
        {
            var seedPath = $@"{_seedPath}/Car.csv";

            var data = new List<Car>();
            using var reader = new StreamReader(seedPath);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line is null)
                    break;

                var values = line.Split(',');
                var name = values[0];
                var brand = values[1];
                var vin = values[2];
                data.Add(new Car(name, vin, brand, null));
            }

            return data;
        }
    }
}