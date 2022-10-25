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



    public DbSet<Group> Groups { get; set; }



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

        modelBuilder.Entity<Offering>().HasKey(c => c.OfferingId);
        modelBuilder.Entity<Address>().HasKey(c => c.AddressId);
        modelBuilder.Entity<Consumer>().HasKey(c => c.ConsumerId);
        modelBuilder.Entity<Cost>().HasKey(c => c.CostId);
        modelBuilder.Entity<Geolocation>().HasKey(c => c.GeolocationId);
        modelBuilder.Entity<LanguageOfChoice>().HasKey(c => c.LanguageOfChoiceId);
        modelBuilder.Entity<OtherCodes>().HasKey(c => c.OtherCodesId);
        modelBuilder.Entity<PrimaryCode>().HasKey(c => c.PrimaryCodeId);
        modelBuilder.Entity<ProgramResultStudyLoad>().HasKey(c => c.ProgramResultStudyLoadId);
        modelBuilder.Entity<RoomGeolocation>().HasKey(c => c.RoomGeolocationId);


        modelBuilder.Entity<LanguageTypedString>().HasKey(c => c.LanguageTypedStringId);
        modelBuilder.Entity<List<LanguageTypedString>>().HasNoKey();

        modelBuilder.Entity<Author>().HasNoKey();
        modelBuilder.Entity<Resource>().HasNoKey();

        modelBuilder.Entity<List<string>>().HasNoKey();
    }

}
