using Microsoft.EntityFrameworkCore;
using ooapi.v5.Models;
using System.Diagnostics.CodeAnalysis;
using ooapi.v5.core.Repositories.Interfaces;
using Attribute = ooapi.v5.Models.Attribute;

namespace ooapi.v5.core.Repositories;

[ExcludeFromCodeCoverage(Justification = "Configuration class")]
public class CoreDBContext : DbContext, ICoreDbContext
{
    public CoreDBContext(DbContextOptions<CoreDBContext> options) : base(options)
    {
    }

    public DbSet<Service> Services { get; set; } = default!;

    public DbSet<AcademicSession> AcademicSessions { get; set; } = default!;
    public IQueryable<AcademicSession> AcademicSessionsNoTracking { get => AcademicSessions.AsNoTracking(); }

    public DbSet<Association> Associations { get; set; } = default!;
    public IQueryable<Association> AssociationsNoTracking { get => Associations.AsNoTracking(); }

    public DbSet<Building> Buildings { get; set; } = default!;
    public IQueryable<Building> BuildingsNoTracking { get => Buildings.AsNoTracking(); }

    public DbSet<Component> Components { get; set; } = default!;
    public IQueryable<Component> ComponentsNoTracking { get => Components.AsNoTracking(); }

    public DbSet<ComponentOffering> ComponentOfferings { get; set; } = default!;
    public IQueryable<ComponentOffering> ComponentOfferingsNoTracking { get => ComponentOfferings.AsNoTracking(); }

    public DbSet<ComponentResult> ComponentResults { get; set; } = default!;
    public IQueryable<ComponentResult> ComponentResultsNoTracking { get => ComponentResults.AsNoTracking(); }

    public DbSet<Course> Courses { get; set; } = default!;
    public IQueryable<Course> CoursesNoTracking { get => Courses.AsNoTracking(); }

    public DbSet<CourseOffering> CourseOfferings { get; set; } = default!;
    public IQueryable<CourseOffering> CourseOfferingsNoTracking { get => CourseOfferings.AsNoTracking(); }

    public DbSet<CourseResult> CourseResults { get; set; } = default!;
    public IQueryable<CourseResult> CourseResultsNoTracking { get => CourseResults.AsNoTracking(); }

    public DbSet<EducationSpecification> EducationSpecifications { get; set; } = default!;
    public IQueryable<EducationSpecification> EducationSpecificationsNoTracking { get => EducationSpecifications.AsNoTracking(); }

    public DbSet<NewsFeed> NewsFeeds { get; set; } = default!;
    public IQueryable<NewsFeed> NewsFeedsNoTracking { get => NewsFeeds.AsNoTracking(); }

    public DbSet<NewsItem> NewsItems { get; set; } = default!;
    public IQueryable<NewsItem> NewsItemsNoTracking { get => NewsItems.AsNoTracking(); }

    public DbSet<Organization> Organizations { get; set; } = default!;
    public IQueryable<Organization> OrganizationsNoTracking { get => Organizations.AsNoTracking(); }

    public DbSet<Person> Persons { get; set; } = default!;
    public IQueryable<Person> PersonsNoTracking { get => Persons.AsNoTracking(); }

    public DbSet<Program> Programs { get; set; } = default!;
    public IQueryable<Program> ProgramsNoTracking { get => Programs.AsNoTracking(); }

    public DbSet<ProgramOffering> ProgramOfferings { get; set; } = default!;
    public IQueryable<ProgramOffering> ProgramOfferingsNoTracking { get => ProgramOfferings.AsNoTracking(); }

    public DbSet<ProgramResult> ProgramResults { get; set; } = default!;
    public IQueryable<ProgramResult> ProgramResultsNoTracking { get => ProgramResults.AsNoTracking(); }

    public DbSet<Room> Rooms { get; set; } = default!;
    public IQueryable<Room> RoomsNoTracking { get => Rooms.AsNoTracking(); }

    public DbSet<Address> Addresses { get; set; } = default!;
    public IQueryable<Address> AddressesNoTracking { get => Addresses.AsNoTracking(); }

    public DbSet<ConsumerRegistration> ConsumerRegistrations { get; set; } = default!;
    public IQueryable<ConsumerRegistration> ConsumerRegistrationsNoTracking { get => ConsumerRegistrations.AsNoTracking(); }
    public DbSet<Cost> Costs { get; set; } = default!;
    public IQueryable<Cost> CostsNoTracking { get => Costs.AsNoTracking(); }

    public DbSet<LanguageOfChoice> LanguageOfChoices { get; set; } = default!;
    public IQueryable<LanguageOfChoice> LanguageOfChoicesNoTracking { get => LanguageOfChoices.AsNoTracking(); }

    public DbSet<OtherCodes> OtherCodes { get; set; } = default!;
    public IQueryable<OtherCodes> OtherCodesNoTracking { get => OtherCodes.AsNoTracking(); }

    public DbSet<Group> Groups { get; set; } = default!;
    public IQueryable<Group> GroupsNoTracking { get => Groups.AsNoTracking(); }

    public DbSet<Consumer> Consumers { get; set; } = default!;
    public IQueryable<Consumer> ConsumersNoTracking { get => Consumers.AsNoTracking(); }

    public DbSet<Attribute> Attributes { get; set; } = default!;
    public IQueryable<Attribute> AttributesNoTracking { get => Attributes.AsNoTracking(); }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("ooapiv5");

        modelBuilder.Entity<Service>().HasKey(c => c.ServiceId);

        modelBuilder.Entity<AcademicSession>().HasKey(c => c.AcademicSessionId);

        modelBuilder.Entity<Association>().HasKey(c => c.AssociationId);
        modelBuilder.Entity<Building>().HasKey(c => c.BuildingId);
        modelBuilder.Entity<Component>().HasKey(c => c.ComponentId);
        modelBuilder.Entity<ComponentOffering>().HasKey(c => c.OfferingId);
        modelBuilder.Entity<ComponentResult>().HasKey(c => c.ResultId);
        modelBuilder.Entity<Course>().HasKey(c => c.CourseId);
        modelBuilder.Entity<CourseOffering>().HasKey(c => c.OfferingId);
        modelBuilder.Entity<CourseResult>().HasKey(c => c.ResultId);
        modelBuilder.Entity<EducationSpecification>().HasKey(c => c.EducationSpecificationId);
        modelBuilder.Entity<Group>().HasKey(c => c.GroupId);
        modelBuilder.Entity<NewsFeed>().HasKey(c => c.NewsFeedId);
        modelBuilder.Entity<NewsItem>().HasKey(c => c.NewsItemId);
        modelBuilder.Entity<Organization>().HasKey(c => c.OrganizationId);
        modelBuilder.Entity<Person>().HasKey(c => c.PersonId);
        modelBuilder.Entity<Program>().HasKey(c => c.ProgramId);
        modelBuilder.Entity<ProgramOffering>().HasKey(c => c.OfferingId);
        modelBuilder.Entity<ProgramResult>().HasKey(c => c.ResultId);
        modelBuilder.Entity<Room>().HasKey(c => c.RoomId);
        modelBuilder.Entity<Address>().HasKey(c => c.AddressId);
        modelBuilder.Entity<ConsumerRegistration>().HasKey(c => c.ConsumerKey);
        modelBuilder.Entity<Cost>().HasKey(c => c.CostId);
        modelBuilder.Entity<LanguageOfChoice>().HasKey(c => c.LanguageOfChoiceId);
        modelBuilder.Entity<OtherCodes>().HasKey(c => c.OtherCodesId);

        // Consumers
        modelBuilder.Entity<Consumer>().HasKey(c => new { c.Id, c.ModelTypeName, c.ConsumerKey, c.PropertyName });
        // Attributes (LanguageTypedProperties)
        modelBuilder.Entity<Attribute>().HasKey(c => new { c.Id, c.ModelTypeName, c.PropertyName, c.Language });
    }
}