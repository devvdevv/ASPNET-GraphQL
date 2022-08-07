using LearnEFCore.Domain;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data
{
    public class LearnEFCoreContext : DbContext
    {
        public LearnEFCoreContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Samurai> Samurai { get; set; }

        public DbSet<Quote> Quote { get; set; }

        public DbSet<Battle> Battle { get; set; }

        public DbSet<Fighter> Fighter { get; set; }

        public DbSet<FighterFighter> FighterFighter { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Samurai>()
                .HasMany(s => s.Battles)
                .WithMany(b => b.Samurais)
                .UsingEntity<BattleSamurai>
                (bs => bs.HasOne<Battle>().WithMany(),
                    bs => bs.HasOne<Samurai>().WithMany())
                .Property(bs => bs.DateJoined)              // thêm 1 prop => payload
                .HasDefaultValueSql("getdate()");

            // Thay đổi tên column
            modelBuilder.Entity<BattleSamurai>()
                .Property(bs => bs.BattleId).HasColumnName("BattleId");

            modelBuilder.Entity<BattleSamurai>()
                .Property(bs => bs.SamuraiId).HasColumnName("SamuraiId");

            modelBuilder.Entity<Fighter>()
                .HasMany(x => x.Fighters)
                .WithMany(x => x.FighterMestres)
                .UsingEntity<FighterFighter>(
                    ff => ff.HasOne<Fighter>()
                        .WithMany()
                        .HasForeignKey(x => x.FighterId)
                        .IsRequired(false)
                        .OnDelete(DeleteBehavior.Restrict),
                    ff => ff.HasOne<Fighter>()
                        .WithMany()
                        .HasForeignKey(x => x.FighterMestreId)
                        .IsRequired(false)
                        .OnDelete(DeleteBehavior.Restrict));
        }

        public void Seed()
        {
            if (Samurai.Any())
            {
                return;
            }

            var samurais = (new List<string> { "Konicha", "Honda", "Matachuse", "Okamoto" })
                .Select(name => new Samurai { Name = name });
            Samurai.AddRange(samurais);
            SaveChanges();
        }
    }
}