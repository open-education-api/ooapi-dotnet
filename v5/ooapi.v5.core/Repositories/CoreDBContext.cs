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
    public DbSet<LanguageOfChoice> LanguageOfChoices { get; set; }
    public DbSet<OtherCodes> OtherCodes { get; set; }
    public DbSet<StudyLoadDescriptor> StudyLoadDescriptors { get; set; }
    public DbSet<Group> Groups { get; set; }


    public DbSet<OrganizationAddress> OrganizationsAddresses { get; set; }
    public DbSet<ComponentAddress> ComponentsAddresses { get; set; }
    public DbSet<ComponentOfferingAddress>  ComponentOfferingsAddresses { get; set; }
    public DbSet<CourseAddress> CoursesAddresses { get; set; }
    public DbSet<CourseOfferingAddress> CourseOfferingsAddresses { get; set; }
    public DbSet<ProgramAddress> ProgramsAddresses { get; set; }
    public DbSet<ProgramOfferingAddress> ProgramOfferingsAddresses { get; set; }


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
        modelBuilder.Entity<LanguageOfChoice>().HasKey(c => c.LanguageOfChoiceId);
        modelBuilder.Entity<OtherCodes>().HasKey(c => c.OtherCodesId);
        modelBuilder.Entity<StudyLoadDescriptor>().HasKey(c => c.StudyLoadDescriptorId);



        modelBuilder.Entity<OrganizationAddress>().HasKey(bc => new { bc.OrganizationId, bc.AddressId });
        modelBuilder.Entity<OrganizationAddress>().HasOne(bc => bc.Organization).WithMany(b => b.OrganizationAddresses).HasForeignKey(bc => bc.OrganizationId);
        modelBuilder.Entity<OrganizationAddress>().HasOne(bc => bc.Address).WithMany(c => c.OrganizationAddresses).HasForeignKey(bc => bc.AddressId);

        modelBuilder.Entity<ComponentAddress>().HasKey(bc => new { bc.ComponentId, bc.AddressId });
        modelBuilder.Entity<ComponentAddress>().HasOne(bc => bc.Component).WithMany(b => b.ComponentsAddresses).HasForeignKey(bc => bc.ComponentId);
        modelBuilder.Entity<ComponentAddress>().HasOne(bc => bc.Address).WithMany(c => c.ComponentsAddresses).HasForeignKey(bc => bc.AddressId);

        modelBuilder.Entity<ComponentOfferingAddress>().HasKey(bc => new { bc.ComponentOfferingId, bc.AddressId });
        modelBuilder.Entity<ComponentOfferingAddress>().HasOne(bc => bc.ComponentOffering).WithMany(b => b.ComponentOfferingsAddresses).HasForeignKey(bc => bc.ComponentOfferingId);
        modelBuilder.Entity<ComponentOfferingAddress>().HasOne(bc => bc.Address).WithMany(c => c.ComponentOfferingsAddresses).HasForeignKey(bc => bc.AddressId);

        modelBuilder.Entity<CourseAddress>().HasKey(bc => new { bc.CourseId, bc.AddressId });
        modelBuilder.Entity<CourseAddress>().HasOne(bc => bc.Course).WithMany(b => b.CoursesAddresses).HasForeignKey(bc => bc.CourseId);
        modelBuilder.Entity<CourseAddress>().HasOne(bc => bc.Address).WithMany(c => c.CoursesAddresses).HasForeignKey(bc => bc.AddressId);

        modelBuilder.Entity<CourseOfferingAddress>().HasKey(bc => new { bc.CourseOfferingId, bc.AddressId });
        modelBuilder.Entity<CourseOfferingAddress>().HasOne(bc => bc.CourseOffering).WithMany(b => b.CourseOfferingsAddresses).HasForeignKey(bc => bc.CourseOfferingId);
        modelBuilder.Entity<CourseOfferingAddress>().HasOne(bc => bc.Address).WithMany(c => c.CourseOfferingsAddresses).HasForeignKey(bc => bc.AddressId);

        modelBuilder.Entity<ProgramAddress>().HasKey(bc => new { bc.ProgramId, bc.AddressId });
        modelBuilder.Entity<ProgramAddress>().HasOne(bc => bc.Program).WithMany(b => b.ProgramsAddresses).HasForeignKey(bc => bc.ProgramId);
        modelBuilder.Entity<ProgramAddress>().HasOne(bc => bc.Address).WithMany(c => c.ProgramsAddresses).HasForeignKey(bc => bc.AddressId);

        modelBuilder.Entity<ProgramOfferingAddress>().HasKey(bc => new { bc.ProgramOfferingId, bc.AddressId });
        modelBuilder.Entity<ProgramOfferingAddress>().HasOne(bc => bc.ProgramOffering).WithMany(b => b.ProgramOfferingsAddresses).HasForeignKey(bc => bc.ProgramOfferingId);
        modelBuilder.Entity<ProgramOfferingAddress>().HasOne(bc => bc.Address).WithMany(c => c.ProgramOfferingsAddresses).HasForeignKey(bc => bc.AddressId);


    }

}
