using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApplication.Domain.Models;

namespace WebApplication.Data
{
    public class WebApplicationDbContext : DbContext
    {
        // TODO: fixed issue cannot run migration with this constructor
        //public WebApplicationDbContext(DbContextOptions<WebApplicationDbContext> options)
        //    : base(options)
        //{
        //}

        public DbSet<Person> Person { get; set; }

        public DbSet<Car> Car { get; set; }

        public DbSet<Licence> Licence { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog=WebApplicationData")
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
            if (Person.Any())
                return;

            var people = GetPersonSeedData();
            Person.AddRange(people);
            base.SaveChanges();
        }

        public IEnumerable<Person> GetPersonSeedData()
        {
            var basePath = $@"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Seed")}";
            var seedPath = $@"{basePath}/Person.csv";

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
    }
}