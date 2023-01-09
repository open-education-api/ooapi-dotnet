namespace ooapi.v5.core.Repositories;

using Microsoft.EntityFrameworkCore;
using ooapi.v5.core.Models.Many2Many;
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


    public DbSet<Service> Services { get; set; }

    public DbSet<AcademicSession> AcademicSessions { get; set; }
    public DbSet<Association> Associations { get; set; }
    public DbSet<Building> Buildings { get; set; }
    public DbSet<Component> Components { get; set; }
    public DbSet<ComponentOffering> ComponentOfferings { get; set; }

    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseOffering> CourseOfferings { get; set; }
    public DbSet<EducationSpecification> EducationSpecifications { get; set; }
    public DbSet<NewsFeed> NewsFeeds { get; set; }
    public DbSet<NewsItem> NewsItems { get; set; }
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Program> Programs { get; set; }
    public DbSet<ProgramOffering> ProgramOfferings { get; set; }
    public DbSet<Room> Rooms { get; set; }

    public DbSet<Address> Addresses { get; set; }
    //public DbSet<Consumer> Consumers { get; set; }
    public DbSet<ConsumerRegistration> ConsumerRegistrations { get; set; }
    
    public DbSet<Cost> Costs { get; set; }
    public DbSet<LanguageOfChoice> LanguageOfChoices { get; set; }
    public DbSet<OtherCodes> OtherCodes { get; set; }
    public DbSet<Group> Groups { get; set; }

    // Many2Many Costs
    public DbSet<ProgramOfferingsCosts> ProgramOfferingsCosts { get; set; }
    public DbSet<CourseOfferingsCosts> CourseOfferingsCosts { get; set; }
    public DbSet<ComponentOfferingsCosts> ComponentOfferingsCosts { get; set; }

    // Many2Many News
    //public DbSet<NewsItemsNewsFeeds> NewsItemsNewsFeeds { get; set; }
    
    // Many2Many Consumers
    //public DbSet<AcademicSessionsConsumers> AcademicSessionsConsumers { get; set; }
    //public DbSet<AssociationsConsumers> AssociationsConsumers { get; set; }
    //public DbSet<BuildingsConsumers> BuildingsConsumers { get; set; }
    //public DbSet<ComponentOfferingsConsumers> ComponentOfferingsConsumers { get; set; }
    //public DbSet<ComponentsConsumers> ComponentsConsumers { get; set; }
    //public DbSet<CourseOfferingsConsumers> CourseOfferingsConsumers { get; set; }
    //public DbSet<CoursesConsumers> CoursesConsumers { get; set; }
    //public DbSet<GroupsConsumers> GroupsConsumers { get; set; }
    //public DbSet<NewsFeedsConsumers> NewsFeedsConsumers  { get; set; }
    //public DbSet<NewsItemsConsumers> NewsItemsConsumers { get; set; }
    //public DbSet<OrganizationsConsumers> OrganizationsConsumers{ get; set; }
    //public DbSet<PersonsConsumers> PersonsConsumers { get; set; }
    //public DbSet<ProgramOfferingsConsumers> ProgramOfferingsConsumers { get; set; }
    //public DbSet<ProgramsConsumers> ProgramsConsumers { get; set; }
    //public DbSet<RoomsConsumers> RoomsConsumers { get; set; }
    //public DbSet<ServicesConsumers> ServicesConsumers{ get; set; }

    // Consumers
    public DbSet<AcademicSessionConsumer> AcademicSessionConsumers { get; set; }
    public DbSet<AssociationConsumer> AssociationConsumers { get; set; }
    public DbSet<BuildingConsumer> BuildingConsumers { get; set; }
    public DbSet<ComponentOfferingConsumer>  ComponentOfferingConsumers{ get; set; }
    public DbSet<ComponentConsumer> ComponentConsumers { get; set; }
    public DbSet<CourseOfferingConsumer> CourseOfferingConsumers { get; set; }
    public DbSet<CourseConsumer> CourseConsumers { get; set; }
    public DbSet<EducationSpecificationConsumer> EducationSpecificationConsumers { get; set; }
    public DbSet<GroupConsumer> GroupConsumers { get; set; }
    public DbSet<NewsFeedConsumer> NewsFeedConsumers { get; set; }
    public DbSet<NewsItemConsumer> NewsItemConsumers { get; set; }
    public DbSet<OrganizationConsumer> OrganizationConsumers { get; set; }
    public DbSet<PersonConsumer> PersonConsumers { get; set; }
    public DbSet<ProgramOfferingConsumer> ProgramOfferingConsumers { get; set; }
    public DbSet<ProgramConsumer> ProgramConsumers { get; set; }
    public DbSet<RoomConsumer> RoomConsumers { get; set; }
    public DbSet<ServiceConsumer> ServiceConsumers { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("ooapiv5");

        modelBuilder.Entity<Service>().HasKey(c => c.ServiceId);

        modelBuilder.Entity<AcademicSession>().HasKey(c => c.AcademicSessionId);
        modelBuilder.Entity<Association>().HasKey(c => c.AssociationId);
        modelBuilder.Entity<Building>().HasKey(c => c.BuildingId);

        modelBuilder.Entity<Component>().HasKey(c => c.ComponentId);
        //modelBuilder.Entity<Component>().HasOne(c => c.Course).WithMany().OnDelete(DeleteBehavior.Cascade);


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
        //modelBuilder.Entity<Consumer>().HasKey(c => c.ConsumerId);
        modelBuilder.Entity<ConsumerRegistration>().HasKey(c => c.ConsumerKey);
        modelBuilder.Entity<Cost>().HasKey(c => c.CostId);
        modelBuilder.Entity<LanguageOfChoice>().HasKey(c => c.LanguageOfChoiceId);
        modelBuilder.Entity<OtherCodes>().HasKey(c => c.OtherCodesId);


        // Many2Many Costs
        modelBuilder.Entity<ProgramOfferingsCosts>().HasKey(c => new { c.ProgramOfferingId, c.CostId });
        modelBuilder.Entity<CourseOfferingsCosts>().HasKey(c => new { c.CourseOfferingId, c.CostId });
        modelBuilder.Entity<ComponentOfferingsCosts>().HasKey(c => new { c.ComponentOfferingId, c.CostId });

        // Many2Many News
        //modelBuilder.Entity<NewsItemsNewsFeeds>().HasKey(c => new { c.NewsItemId, c.NewsFeedId});
        
        // Many2Many Consumers
        //modelBuilder.Entity<AcademicSessionsConsumers>().HasKey(c => new { c.AcademicSessionId , c.ConsumerId });
        //modelBuilder.Entity<AssociationsConsumers>().HasKey(c => new { c.AssociationId, c.ConsumerId });
        //modelBuilder.Entity<BuildingsConsumers>().HasKey(c => new { c.BuildingId, c.ConsumerId });
        //modelBuilder.Entity<ComponentOfferingsConsumers>().HasKey(c => new { c.ComponentOfferingId, c.ConsumerId });
        //modelBuilder.Entity<ComponentsConsumers>().HasKey(c => new { c.ComponentId, c.ConsumerId });
        //modelBuilder.Entity<CourseOfferingsConsumers>().HasKey(c => new { c.CourseOfferingId, c.ConsumerId });
        //modelBuilder.Entity<CoursesConsumers>().HasKey(c => new { c.CourseId, c.ConsumerId });
        //modelBuilder.Entity<GroupsConsumers>().HasKey(c => new { c.GroupId, c.ConsumerId });
        //modelBuilder.Entity<NewsFeedsConsumers>().HasKey(c => new { c.NewsFeedId, c.ConsumerId });
        //modelBuilder.Entity<NewsItemsConsumers>().HasKey(c => new { c.NewsItemId, c.ConsumerId });
        //modelBuilder.Entity<OrganizationsConsumers>().HasKey(c => new { c.OrganizationId, c.ConsumerId });
        //modelBuilder.Entity<PersonsConsumers>().HasKey(c => new { c.PersonId, c.ConsumerId });
        //modelBuilder.Entity<ProgramOfferingsConsumers>().HasKey(c => new { c.ProgramOfferingId, c.ConsumerId });
        //modelBuilder.Entity<ProgramsConsumers>().HasKey(c => new { c.ProgramId, c.ConsumerId });
        //modelBuilder.Entity<RoomsConsumers>().HasKey(c => new { c.RoomId, c.ConsumerId });
        //modelBuilder.Entity<ServicesConsumers>().HasKey(c => new { c.ServiceId, c.ConsumerId });

        // Consumers
        modelBuilder.Entity<AcademicSessionConsumer>().HasKey(c => new { c.AcademicSessionId, c.ConsumerKey, c.PropertyName });
        modelBuilder.Entity<AcademicSessionConsumer>().HasOne(c => c.AcademicSession).WithMany().OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AssociationConsumer>().HasKey(c => new { c.AssociationId, c.ConsumerKey, c.PropertyName });
        modelBuilder.Entity<AssociationConsumer>().HasOne(c => c.Association).WithMany().OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<BuildingConsumer>().HasKey(c => new { c.BuildingId, c.ConsumerKey, c.PropertyName });
        modelBuilder.Entity<BuildingConsumer>().HasOne(c => c.Building).WithMany().OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ComponentOfferingConsumer>().HasKey(c => new { c.ComponentOfferingId, c.ConsumerKey, c.PropertyName });
        modelBuilder.Entity<ComponentOfferingConsumer>().HasOne(c => c.ComponentOffering).WithMany().OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ComponentConsumer>().HasKey(c => new { c.ComponentId, c.ConsumerKey, c.PropertyName });
        modelBuilder.Entity<ComponentConsumer>().HasOne(c => c.Component).WithMany().OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CourseOfferingConsumer>().HasKey(c => new { c.CourseOfferingId, c.ConsumerKey, c.PropertyName });
        modelBuilder.Entity<CourseOfferingConsumer>().HasOne(c => c.CourseOffering).WithMany().OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CourseConsumer>().HasKey(c => new { c.CourseId, c.ConsumerKey, c.PropertyName });
        modelBuilder.Entity<CourseConsumer>().HasOne(c => c.Course).WithMany().OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<EducationSpecificationConsumer>().HasKey(c => new { c.EducationSpecificationId, c.ConsumerKey, c.PropertyName });
        modelBuilder.Entity<EducationSpecificationConsumer>().HasOne(c => c.EducationSpecification).WithMany().OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<GroupConsumer>().HasKey(c => new { c.GroupId, c.ConsumerKey, c.PropertyName });
        modelBuilder.Entity<GroupConsumer>().HasOne(c => c.Group).WithMany().OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<NewsFeedConsumer>().HasKey(c => new { c.NewsFeedId, c.ConsumerKey, c.PropertyName });
        modelBuilder.Entity<NewsFeedConsumer>().HasOne(c => c.NewsFeed).WithMany().OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<NewsItemConsumer>().HasKey(c => new { c.NewsItemId, c.ConsumerKey, c.PropertyName });
        modelBuilder.Entity<NewsItemConsumer>().HasOne(c => c.NewsItem).WithMany().OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrganizationConsumer>().HasKey(c => new { c.OrganizationId, c.ConsumerKey, c.PropertyName });
        modelBuilder.Entity<OrganizationConsumer>().HasOne(c => c.Organization).WithMany().OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<PersonConsumer>().HasKey(c => new { c.PersonId, c.ConsumerKey, c.PropertyName });
        modelBuilder.Entity<PersonConsumer>().HasOne(c => c.Person).WithMany().OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ProgramOfferingConsumer>().HasKey(c => new { c.ProgramOfferingId, c.ConsumerKey, c.PropertyName });
        modelBuilder.Entity<ProgramOfferingConsumer>().HasOne(c => c.ProgramOffering).WithMany().OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ProgramConsumer>().HasKey(c => new { c.ProgramId, c.ConsumerKey, c.PropertyName });
        modelBuilder.Entity<ProgramConsumer>().HasOne(c => c.Program).WithMany().OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<RoomConsumer>().HasKey(c => new { c.RoomId, c.ConsumerKey, c.PropertyName });
        modelBuilder.Entity<RoomConsumer>().HasOne(c => c.Room).WithMany().OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ServiceConsumer>().HasKey(c => new { c.ServiceId, c.ConsumerKey, c.PropertyName });
        modelBuilder.Entity<ServiceConsumer>().HasOne(c => c.Service).WithMany().OnDelete(DeleteBehavior.Cascade);


    }

}
