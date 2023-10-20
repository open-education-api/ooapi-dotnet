using Microsoft.EntityFrameworkCore;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories.Interfaces;

public interface ICoreDbContext : IDisposable
{
    DbSet<Service> Services { get; set; }

    DbSet<AcademicSession> AcademicSessions { get; set; }
    IQueryable<AcademicSession> AcademicSessionsNoTracking { get; }

    DbSet<Association> Associations { get; set; }
    IQueryable<Association> AssociationsNoTracking { get; }

    DbSet<Building> Buildings { get; set; }
    IQueryable<Building> BuildingsNoTracking { get; }

    DbSet<Component> Components { get; set; }
    IQueryable<Component> ComponentsNoTracking { get; }

    DbSet<ComponentOffering> ComponentOfferings { get; set; }
    IQueryable<ComponentOffering> ComponentOfferingsNoTracking { get; }

    DbSet<ComponentResult> ComponentResults { get; set; }
    IQueryable<ComponentResult> ComponentResultsNoTracking { get; }

    DbSet<Course> Courses { get; set; }
    IQueryable<Course> CoursesNoTracking { get; }

    DbSet<CourseOffering> CourseOfferings { get; set; }
    IQueryable<CourseOffering> CourseOfferingsNoTracking { get; }

    DbSet<CourseResult> CourseResults { get; set; }
    IQueryable<CourseResult> CourseResultsNoTracking { get; }

    DbSet<EducationSpecification> EducationSpecifications { get; set; }
    IQueryable<EducationSpecification> EducationSpecificationsNoTracking { get; }

    DbSet<NewsFeed> NewsFeeds { get; set; }
    IQueryable<NewsFeed> NewsFeedsNoTracking { get; }

    DbSet<NewsItem> NewsItems { get; set; }
    IQueryable<NewsItem> NewsItemsNoTracking { get; }

    DbSet<Organization> Organizations { get; set; }
    IQueryable<Organization> OrganizationsNoTracking { get; }

    DbSet<Person> Persons { get; set; }
    IQueryable<Person> PersonsNoTracking { get; }

    DbSet<Program> Programs { get; set; }
    IQueryable<Program> ProgramsNoTracking { get; }

    DbSet<ProgramOffering> ProgramOfferings { get; set; }
    IQueryable<ProgramOffering> ProgramOfferingsNoTracking { get; }

    DbSet<ProgramResult> ProgramResults { get; set; }
    IQueryable<ProgramResult> ProgramResultsNoTracking { get; }

    DbSet<Room> Rooms { get; set; }
    IQueryable<Room> RoomsNoTracking { get; }

    DbSet<Address> Addresses { get; set; }
    IQueryable<Address> AddressesNoTracking { get; }

    DbSet<ConsumerRegistration> ConsumerRegistrations { get; set; }
    IQueryable<ConsumerRegistration> ConsumerRegistrationsNoTracking { get; }


    DbSet<Cost> Costs { get; set; }
    IQueryable<Cost> CostsNoTracking { get; }

    DbSet<LanguageOfChoice> LanguageOfChoices { get; set; }
    IQueryable<LanguageOfChoice> LanguageOfChoicesNoTracking { get; }

    DbSet<OtherCodes> OtherCodes { get; set; }
    IQueryable<OtherCodes> OtherCodesNoTracking { get; }

    DbSet<Group> Groups { get; set; }
    IQueryable<Group> GroupsNoTracking { get; }

    DbSet<Consumer> Consumers { get; set; }
    IQueryable<Consumer> ConsumersNoTracking { get; }

    DbSet<v5.Models.Attribute> Attributes { get; set; }
    IQueryable<v5.Models.Attribute> AttributesNoTracking { get; }

    DbSet<TEntity> Set<TEntity>() where TEntity : class;
}