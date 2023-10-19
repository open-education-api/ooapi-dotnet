using Microsoft.EntityFrameworkCore;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

public class CoreDBContext : DbContext
{
    public CoreDBContext(DbContextOptions<CoreDBContext> options) : base(options)
    {
    }

    public DbSet<Service> Services { get; set; }

    public DbSet<AcademicSession> AcademicSessions { get; set; }
    public IQueryable<AcademicSession> AcademicSessionsNoTracking { get => AcademicSessions.AsNoTracking(); }

    public DbSet<Association> Associations { get; set; }
    public IQueryable<Association> AssociationsNoTracking { get => Associations.AsNoTracking(); }

    public DbSet<Building> Buildings { get; set; }
    public IQueryable<Building> BuildingsNoTracking { get => Buildings.AsNoTracking(); }

    public DbSet<Component> Components { get; set; }
    public IQueryable<Component> ComponentsNoTracking { get => Components.AsNoTracking(); }

    public DbSet<ComponentOffering> ComponentOfferings { get; set; }
    public IQueryable<ComponentOffering> ComponentOfferingsNoTracking { get => ComponentOfferings.AsNoTracking(); }

    public DbSet<ComponentResult> ComponentResults { get; set; }
    public IQueryable<ComponentResult> ComponentResultsNoTracking { get => ComponentResults.AsNoTracking(); }

    public DbSet<Course> Courses { get; set; }
    public IQueryable<Course> CoursesNoTracking { get => Courses.AsNoTracking(); }

    public DbSet<CourseOffering> CourseOfferings { get; set; }
    public IQueryable<CourseOffering> CourseOfferingsNoTracking { get => CourseOfferings.AsNoTracking(); }

    public DbSet<CourseResult> CourseResults { get; set; }
    public IQueryable<CourseResult> CourseResultsNoTracking { get => CourseResults.AsNoTracking(); }

    public DbSet<EducationSpecification> EducationSpecifications { get; set; }
    public IQueryable<EducationSpecification> EducationSpecificationsNoTracking { get => EducationSpecifications.AsNoTracking(); }

    public DbSet<NewsFeed> NewsFeeds { get; set; }
    public IQueryable<NewsFeed> NewsFeedsNoTracking { get => NewsFeeds.AsNoTracking(); }

    public DbSet<NewsItem> NewsItems { get; set; }
    public IQueryable<NewsItem> NewsItemsNoTracking { get => NewsItems.AsNoTracking(); }

    public DbSet<Organization> Organizations { get; set; }
    public IQueryable<Organization> OrganizationsNoTracking { get => Organizations.AsNoTracking(); }

    public DbSet<Person> Persons { get; set; }
    public IQueryable<Person> PersonsNoTracking { get => Persons.AsNoTracking(); }

    public DbSet<Program> Programs { get; set; }
    public IQueryable<Program> ProgramsNoTracking { get => Programs.AsNoTracking(); }

    public DbSet<ProgramOffering> ProgramOfferings { get; set; }
    public IQueryable<ProgramOffering> ProgramOfferingsNoTracking { get => ProgramOfferings.AsNoTracking(); }

    public DbSet<ProgramResult> ProgramResults { get; set; }
    public IQueryable<ProgramResult> ProgramResultsNoTracking { get => ProgramResults.AsNoTracking(); }

    public DbSet<Room> Rooms { get; set; }
    public IQueryable<Room> RoomsNoTracking { get => Rooms.AsNoTracking(); }

    public DbSet<Address> Addresses { get; set; }
    public IQueryable<Address> AddressesNoTracking { get => Addresses.AsNoTracking(); }

    public DbSet<ConsumerRegistration> ConsumerRegistrations { get; set; }
    public IQueryable<ConsumerRegistration> ConsumerRegistrationsNoTracking { get => ConsumerRegistrations.AsNoTracking(); }
    public DbSet<Cost> Costs { get; set; }
    public IQueryable<Cost> CostsNoTracking { get => Costs.AsNoTracking(); }

    public DbSet<LanguageOfChoice> LanguageOfChoices { get; set; }
    public IQueryable<LanguageOfChoice> LanguageOfChoicesNoTracking { get => LanguageOfChoices.AsNoTracking(); }

    public DbSet<OtherCodes> OtherCodes { get; set; }
    public IQueryable<OtherCodes> OtherCodesNoTracking { get => OtherCodes.AsNoTracking(); }

    public DbSet<Group> Groups { get; set; }
    public IQueryable<Group> GroupsNoTracking { get => Groups.AsNoTracking(); }

    public DbSet<Consumer> Consumers { get; set; }
    public IQueryable<Consumer> ConsumersNoTracking { get => Consumers.AsNoTracking(); }

    public DbSet<v5.Models.Attribute> Attributes { get; set; }
    public IQueryable<v5.Models.Attribute> AttributesNoTracking { get => Attributes.AsNoTracking(); }


    /// <param name="modelBuilder"></param>
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
        modelBuilder.Entity<v5.Models.Attribute>().HasKey(c => new { c.Id, c.ModelTypeName, c.PropertyName, c.Language });
    }
}
