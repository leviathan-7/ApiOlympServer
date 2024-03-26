using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ApiServer.Models
{
    public partial class olympicsContext : DbContext
    {
        public olympicsContext()
        {
        }

        public olympicsContext(DbContextOptions<olympicsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<CompetitorEvent> CompetitorEvents { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<GamesCity> GamesCities { get; set; }
        public virtual DbSet<GamesCompetitor> GamesCompetitors { get; set; }
        public virtual DbSet<Medal> Medals { get; set; }
        public virtual DbSet<NocRegion> NocRegions { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<PersonRegion> PersonRegions { get; set; }
        public virtual DbSet<Sport> Sports { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlite("Data Source=Database\\olympics.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("city");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CityName).HasColumnName("city_name");
            });

            modelBuilder.Entity<CompetitorEvent>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("competitor_event");

                entity.Property(e => e.CompetitorId).HasColumnName("competitor_id");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.MedalId).HasColumnName("medal_id");

                entity.HasOne(d => d.Competitor)
                    .WithMany()
                    .HasForeignKey(d => d.CompetitorId);

                entity.HasOne(d => d.Event)
                    .WithMany()
                    .HasForeignKey(d => d.EventId);

                entity.HasOne(d => d.Medal)
                    .WithMany()
                    .HasForeignKey(d => d.MedalId);
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("event");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.EventName).HasColumnName("event_name");

                entity.Property(e => e.SportId).HasColumnName("sport_id");

                entity.HasOne(d => d.Sport)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.SportId);
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.ToTable("games");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.GamesName).HasColumnName("games_name");

                entity.Property(e => e.GamesYear).HasColumnName("games_year");

                entity.Property(e => e.Season).HasColumnName("season");
            });

            modelBuilder.Entity<GamesCity>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("games_city");

                entity.Property(e => e.CityId).HasColumnName("city_id");

                entity.Property(e => e.GamesId).HasColumnName("games_id");

                entity.HasOne(d => d.City)
                    .WithMany()
                    .HasForeignKey(d => d.CityId);

                entity.HasOne(d => d.Games)
                    .WithMany()
                    .HasForeignKey(d => d.GamesId);
            });

            modelBuilder.Entity<GamesCompetitor>(entity =>
            {
                entity.ToTable("games_competitor");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.GamesId).HasColumnName("games_id");

                entity.Property(e => e.PersonId).HasColumnName("person_id");

                entity.HasOne(d => d.Games)
                    .WithMany(p => p.GamesCompetitors)
                    .HasForeignKey(d => d.GamesId);

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.GamesCompetitors)
                    .HasForeignKey(d => d.PersonId);
            });

            modelBuilder.Entity<Medal>(entity =>
            {
                entity.ToTable("medal");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.MedalName).HasColumnName("medal_name");
            });

            modelBuilder.Entity<NocRegion>(entity =>
            {
                entity.ToTable("noc_region");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Noc).HasColumnName("noc");

                entity.Property(e => e.RegionName).HasColumnName("region_name");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("person");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.FullName).HasColumnName("full_name");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.Weight).HasColumnName("weight");
            });

            modelBuilder.Entity<PersonRegion>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("person_region");

                entity.Property(e => e.PersonId).HasColumnName("person_id");

                entity.Property(e => e.RegionId).HasColumnName("region_id");

                entity.HasOne(d => d.Person)
                    .WithMany()
                    .HasForeignKey(d => d.PersonId);

                entity.HasOne(d => d.Region)
                    .WithMany()
                    .HasForeignKey(d => d.RegionId);
            });

            modelBuilder.Entity<Sport>(entity =>
            {
                entity.ToTable("sport");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.SportName).HasColumnName("sport_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
