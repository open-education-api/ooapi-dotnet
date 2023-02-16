namespace ooapi.v5.core.Repositories;

using Microsoft.EntityFrameworkCore;

using ooapi.v5.Helpers;
using ooapi.v5.Models;
using System;
using System.IO;
using System.Reflection.Emit;
using System.Xml;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

public class CoreDBContext : DbContext
{
    public CoreDBContext(DbContextOptions<CoreDBContext> options) : base(options)

    {
        //
    }


    public DbSet<Service> Services { get; set; }

    public DbSet<AcademicSession> AcademicSessions { get; set; }
    public DbSet<Association> Associations { get; set; }
    public DbSet<Building> Buildings { get; set; }
    public DbSet<Component> Components { get; set; }
    public DbSet<ComponentOffering> ComponentOfferings { get; set; }
    public DbSet<ComponentResult> ComponentResults { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseOffering> CourseOfferings { get; set; }
    public DbSet<CourseResult> CourseResults { get; set; }
    public DbSet<EducationSpecification> EducationSpecifications { get; set; }
    public DbSet<NewsFeed> NewsFeeds { get; set; }
    public DbSet<NewsItem> NewsItems { get; set; }
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Program> Programs { get; set; }
    public DbSet<ProgramOffering> ProgramOfferings { get; set; }
    public DbSet<ProgramResult> ProgramResults { get; set; }
    public DbSet<Room> Rooms { get; set; }

    public DbSet<Address> Addresses { get; set; }
    public DbSet<ConsumerRegistration> ConsumerRegistrations { get; set; }

    public DbSet<Cost> Costs { get; set; }
    public DbSet<LanguageOfChoice> LanguageOfChoices { get; set; }
    public DbSet<OtherCodes> OtherCodes { get; set; }
    public DbSet<Group> Groups { get; set; }


    public DbSet<Consumer> Consumers { get; set; }

    public DbSet<v5.Models.Attribute> Attributes { get; set; }




    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("ooapiv5");

        modelBuilder.Entity<Service>().HasKey(c => c.ServiceId);

        modelBuilder.Entity<AcademicSession>().HasKey(c => c.AcademicSessionId);
        //modelBuilder.Entity<AcademicSession>().HasOne(c => c.Parent).WithMany().HasForeignKey(c => c.AcademicSessionId);
        //modelBuilder.Entity<AcademicSession>().HasOne(c => c.Parent).WithOne(x=>x.AcademicSessionId);

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
