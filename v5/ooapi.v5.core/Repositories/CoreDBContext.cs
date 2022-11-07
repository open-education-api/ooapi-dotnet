namespace ooapi.v5.core.Repositories;

using Microsoft.EntityFrameworkCore;
using ooapi.v5.Helpers;
using ooapi.v5.Models;
using System;

public class CoreDBContext : DbContext
{
    public CoreDBContext(DbContextOptions<CoreDBContext> options) : base(options)

    {
        //
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        builder.Properties<DateOnly>()
            .HaveConversion<DateOnlyConverter>()
            .HaveColumnType("date");

        builder.Properties<DateOnly?>()
            .HaveConversion<NullableDateOnlyConverter>()
            .HaveColumnType("date");
    }



    public DbSet<AcademicSession> AcademicSessions { get; set; }
    public DbSet<Association> Associations { get; set; }
    public DbSet<Building> Buildings { get; set; }
    public DbSet<Component> Components { get; set; }
    public DbSet<ComponentOffering> ComponentOfferings { get; set; }

    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseOffering> CourseOfferings { get; set; }
    public DbSet<NewsFeed> NewsFeeds { get; set; }
    public DbSet<NewsItem> NewsItems { get; set; }
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Program> Programs { get; set; }
    public DbSet<ProgramOffering> ProgramOfferings { get; set; }
    public DbSet<Room> Rooms { get; set; }

    public DbSet<Address> Addresses { get; set; }
    public DbSet<Consumer> Consumers { get; set; }
    public DbSet<Cost> Costs { get; set; }
    public DbSet<Geolocation> Geolocations { get; set; }
    public DbSet<LanguageOfChoice> LanguageOfChoices { get; set; }
    public DbSet<OtherCodes> OtherCodes { get; set; }
    public DbSet<PrimaryCode> PrimaryCodes { get; set; }
    public DbSet<StudyLoadDescriptor> StudyLoadDescriptors { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<RoomGeolocation> RoomGeolocations { get; set; }

    public DbSet<Resource> Resources { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("ooapiv5");

        modelBuilder.Entity<AcademicSession>().HasKey(c => c.AcademicSessionId);
        modelBuilder.Entity<Association>().HasKey(c => c.AssociationId);
        modelBuilder.Entity<Building>().HasKey(c => c.BuildingId);
        modelBuilder.Entity<Component>().HasKey(c => c.ComponentId);
        modelBuilder.Entity<ComponentOffering>().HasKey(c => c.OfferingId);

        modelBuilder.Entity<Course>().HasKey(c => c.CourseId);
        modelBuilder.Entity<CourseOffering>().HasKey(c => c.OfferingId);
        modelBuilder.Entity<EducationSpecification>().HasKey(c => c.EducationSpecificationId);
        modelBuilder.Entity<Group>().HasKey(c => c.GroupId);
        modelBuilder.Entity<NewsFeed>().HasKey(c => c.NewsFeedId);
        modelBuilder.Entity<NewsItem>().HasKey(c => c.NewsItemId);
        modelBuilder.Entity<Organization>().HasKey(c => c.OrganizationId);
        modelBuilder.Entity<Person>().HasKey(c => c.PersonId);
        modelBuilder.Entity<Program>().HasKey(c => c.ProgramId);
        modelBuilder.Entity<ProgramOffering>().HasKey(c => c.OfferingId);
        modelBuilder.Entity<Room>().HasKey(c => c.RoomId);

        //??modelBuilder.Entity<Offering>().HasKey(c => c.OfferingId);
        modelBuilder.Entity<Address>().HasKey(c => c.AddressId);
        modelBuilder.Entity<Consumer>().HasKey(c => c.ConsumerId);
        modelBuilder.Entity<Cost>().HasKey(c => c.CostId);
        modelBuilder.Entity<Geolocation>().HasKey(c => c.GeolocationId);
        modelBuilder.Entity<LanguageOfChoice>().HasKey(c => c.LanguageOfChoiceId);
        modelBuilder.Entity<OtherCodes>().HasKey(c => c.OtherCodesId);
        modelBuilder.Entity<PrimaryCode>().HasKey(c => c.PrimaryCodeId);
        modelBuilder.Entity<StudyLoadDescriptor>().HasKey(c => c.StudyLoadDescriptorId);
        modelBuilder.Entity<RoomGeolocation>().HasKey(c => c.RoomGeolocationId);

        modelBuilder.Entity<Resource>().HasKey(c => c.ResourceId);

        //modelBuilder.Entity<LanguageTypedString>().HasKey(c => c.LanguageTypedStringId);
        //modelBuilder.Entity<List<LanguageTypedString>>().HasNoKey();

        //modelBuilder.Entity<Author>().HasKey(c => c.AuthorId);

        //modelBuilder.Entity<List<string>>().HasNoKey();
    }

}
