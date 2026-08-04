using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using OEAPI.Infrastructure.Data.Entities;

namespace OEAPI.Infrastructure.Data.Context;

/// <summary>
///     The main database context for the OEAPI implementation.
/// </summary>
/// <remarks>
///     Initializes a new instance of the OEAPI database context.
/// </remarks>
/// <param name="options">
///     The database context options. Deliberately typed as the non-generic <see cref="DbContextOptions" />
///     rather than <c>DbContextOptions&lt;OEAPIDbContext&gt;</c> so that design-time-only subclasses
///     (<see cref="SqlServerOEAPIDbContext" />, <see cref="PostgreSqlOEAPIDbContext" />) can pass their
///     own typed options through - each subclass gives EF Core's migration tooling a distinct context
///     identity, so SQL Server and PostgreSQL can each keep an independent migration history/snapshot
///     instead of silently overwriting a single shared one. Runtime DI (<c>AddDbContext&lt;OEAPIDbContext&gt;</c>
///     in Program.cs) is unaffected: <c>DbContextOptions&lt;OEAPIDbContext&gt;</c> still satisfies this
///     parameter, since it derives from the non-generic type.
/// </param>
public class OEAPIDbContext(DbContextOptions options) : DbContext(options)
{

    // DbSet properties for each entity type

    /// <summary>
    ///     Gets or sets the academic sessions.
    /// </summary>
    public DbSet<AcademicSessionEntity> AcademicSessions { get; set; }

    /// <summary>
    ///     Gets or sets the addresses.
    /// </summary>
    public DbSet<AddressEntity> Addresses { get; set; }

    /// <summary>
    ///     Gets or sets the buildings.
    /// </summary>
    public DbSet<BuildingEntity> Buildings { get; set; }

    /// <summary>
    ///     Gets or sets the courses.
    /// </summary>
    public DbSet<CourseEntity> Courses { get; set; }

    /// <summary>
    ///     Gets or sets the course offerings.
    /// </summary>
    public DbSet<CourseOfferingEntity> CourseOfferings { get; set; }

    /// <summary>
    ///     Gets or sets the course offering associations.
    /// </summary>
    public DbSet<CourseOfferingAssociationEntity> CourseOfferingAssociations { get; set; }

    /// <summary>
    ///     Gets or sets the historical/future versions of courses (the spec's <c>timelineOverrides</c>
    ///     mechanism).
    /// </summary>
    public DbSet<TimelineOverrideCourseEntity> CourseTimelineOverrides { get; set; }

    /// <summary>
    ///     Gets or sets the documents.
    /// </summary>
    public DbSet<DocumentEntity> Documents { get; set; }

    /// <summary>
    ///     Gets or sets the groups.
    /// </summary>
    public DbSet<GroupEntity> Groups { get; set; }

    /// <summary>
    ///     Gets or sets the learning components.
    /// </summary>
    public DbSet<LearningComponentEntity> LearningComponents { get; set; }

    /// <summary>
    ///     Gets or sets the learning component offerings.
    /// </summary>
    public DbSet<LearningComponentOfferingEntity> LearningComponentOfferings { get; set; }

    /// <summary>
    ///     Gets or sets the learning component offering associations.
    /// </summary>
    public DbSet<LearningComponentOfferingAssociationEntity> LearningComponentOfferingAssociations { get; set; }

    /// <summary>
    ///     Gets or sets the memberships.
    /// </summary>
    public DbSet<MembershipEntity> Memberships { get; set; }

    /// <summary>
    ///     Gets or sets the organisations.
    /// </summary>
    public DbSet<OrganisationEntity> Organisations { get; set; }

    /// <summary>
    ///     Gets or sets the persons.
    /// </summary>
    public DbSet<PersonEntity> Persons { get; set; }

    /// <summary>
    ///     Gets or sets the programmes.
    /// </summary>
    public DbSet<ProgrammeEntity> Programmes { get; set; }

    /// <summary>
    ///     Gets or sets the programme offerings.
    /// </summary>
    public DbSet<ProgrammeOfferingEntity> ProgrammeOfferings { get; set; }

    /// <summary>
    ///     Gets or sets the programme offering associations.
    /// </summary>
    public DbSet<ProgrammeOfferingAssociationEntity> ProgrammeOfferingAssociations { get; set; }

    /// <summary>
    ///     Gets or sets the historical/future versions of programmes (the spec's
    ///     <c>timelineOverrides</c> mechanism).
    /// </summary>
    public DbSet<TimelineOverrideProgrammeEntity> ProgrammeTimelineOverrides { get; set; }

    /// <summary>
    ///     Gets or sets the rooms.
    /// </summary>
    public DbSet<RoomEntity> Rooms { get; set; }

    /// <summary>
    ///     Gets or sets the learning outcomes.
    /// </summary>
    public DbSet<LearningOutcomeEntity> LearningOutcomes { get; set; }

    /// <summary>
    ///     Gets or sets the test components.
    /// </summary>
    public DbSet<TestComponentEntity> TestComponents { get; set; }

    /// <summary>
    ///     Gets or sets the test component offerings.
    /// </summary>
    public DbSet<TestComponentOfferingEntity> TestComponentOfferings { get; set; }

    /// <summary>
    ///     Gets or sets the test component offering associations.
    /// </summary>
    public DbSet<TestComponentOfferingAssociationEntity> TestComponentOfferingAssociations { get; set; }

    /// <summary>
    ///     Gets or sets the test component offering association attempts.
    /// </summary>
    public DbSet<TestComponentOfferingAssociationAttemptEntity> TestComponentOfferingAssociationAttempts { get; set; }

    /// <summary>
    ///     Configures the database model.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Entity configurations will be applied here
        // Example: modelBuilder.ApplyConfiguration(new PersonEntityConfiguration());

        // Configure many-to-many relationships
        ConfigureProgrammeCoordinatorRelationship(modelBuilder);
        ConfigureProgrammeInstructorRelationship(modelBuilder);
        ConfigureCourseCoordinatorRelationship(modelBuilder);
        ConfigureCourseInstructorRelationship(modelBuilder);
        ConfigureLearningOutcomeHierarchy(modelBuilder);
        ConfigureOfferingRoomRelationships(modelBuilder);
        ConfigureOfferingCourseOfferingRelationships(modelBuilder);
        ConfigureGroupOfferingRelationships(modelBuilder);
        ConfigureAttemptRoomRelationship(modelBuilder);
        ConfigureCourseTimelineOverrideRelationships(modelBuilder);
        ConfigureProgrammeTimelineOverrideRelationships(modelBuilder);

        // Configure hierarchical relationships
        ConfigureProgrammeHierarchy(modelBuilder);
        ConfigureOrganisationHierarchy(modelBuilder);
        ConfigureAcademicSessionHierarchy(modelBuilder);
        ConfigureLearningComponentHierarchy(modelBuilder);
        ConfigureTestComponentHierarchy(modelBuilder);
        ConfigureComponentLearningOutcomeRelationships(modelBuilder);

        // Configure the owned OtherCodes collection shared by every IHasOtherCodes entity
        ConfigureOtherCodes(modelBuilder);

        // Use time-ordered UUID v7 instead of EF Core's default fully-random v4 GUIDs for every
        // entity's primary key - see SequentialGuidValueGenerator's doc comment for why.
        ConfigureSequentialGuidGeneration(modelBuilder);
    }

    /// <summary>
    ///     Applies <see cref="SequentialGuidValueGenerator" /> to every <see cref="BaseEntity.Id" />
    ///     property, so primary keys are generated as time-ordered UUID v7 rather than EF Core's default
    ///     fully-random v4 GUIDs.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    private static void ConfigureSequentialGuidGeneration(ModelBuilder modelBuilder)
    {
        foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (!typeof(BaseEntity).IsAssignableFrom(entityType.ClrType)) continue;

            modelBuilder.Entity(entityType.ClrType)
                .Property(nameof(BaseEntity.Id))
                .HasValueGenerator<SequentialGuidValueGenerator>();
        }
    }

    /// <summary>
    ///     Configures every <see cref="IHasOtherCodes" /> entity's <c>OtherCodes</c> as a proper EF owned
    ///     collection - one dedicated table per owner (e.g. <c>PersonOtherCodes</c>), not a single shared
    ///     table (which would hit the same polymorphic-owner problem <see cref="ConfigureGroupOfferingRelationships" />
    ///     avoided for Group/offerings) and not the opaque, unqueryable <c>OtherCodesJson</c> string
    ///     column this replaces. All owners share the identical shape (<c>codeType</c>/<c>code</c>), so
    ///     this uses one generic local helper instead of 20+ near-identical Fluent API blocks.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    private static void ConfigureOtherCodes(ModelBuilder modelBuilder)
    {
        void Configure<TOwner>(string tableName) where TOwner : class, IHasOtherCodes
        {
            modelBuilder.Entity<TOwner>().OwnsMany(e => e.OtherCodes, oc =>
            {
                oc.ToTable(tableName);
                oc.WithOwner().HasForeignKey("OwnerId");
                oc.Property<int>("Id");
                oc.HasKey("OwnerId", "Id");
                oc.Property(x => x.CodeType).IsRequired().HasMaxLength(256);
                oc.Property(x => x.Code).IsRequired().HasMaxLength(256);
            });
        }

        Configure<AcademicSessionEntity>("AcademicSessionOtherCodes");
        Configure<AddressEntity>("AddressOtherCodes");
        Configure<BuildingEntity>("BuildingOtherCodes");
        Configure<CourseEntity>("CourseOtherCodes");
        Configure<CourseOfferingEntity>("CourseOfferingOtherCodes");
        Configure<CourseOfferingAssociationEntity>("CourseOfferingAssociationOtherCodes");
        Configure<DocumentEntity>("DocumentOtherCodes");
        Configure<GroupEntity>("GroupOtherCodes");
        Configure<LearningComponentEntity>("LearningComponentOtherCodes");
        Configure<LearningComponentOfferingEntity>("LearningComponentOfferingOtherCodes");
        Configure<LearningComponentOfferingAssociationEntity>("LearningComponentOfferingAssociationOtherCodes");
        Configure<LearningOutcomeEntity>("LearningOutcomeOtherCodes");
        Configure<MembershipEntity>("MembershipOtherCodes");
        Configure<OrganisationEntity>("OrganisationOtherCodes");
        Configure<PersonEntity>("PersonOtherCodes");
        Configure<ProgrammeEntity>("ProgrammeOtherCodes");
        Configure<ProgrammeOfferingEntity>("ProgrammeOfferingOtherCodes");
        Configure<ProgrammeOfferingAssociationEntity>("ProgrammeOfferingAssociationOtherCodes");
        Configure<RoomEntity>("RoomOtherCodes");
        Configure<TestComponentEntity>("TestComponentOtherCodes");
        Configure<TestComponentOfferingEntity>("TestComponentOfferingOtherCodes");
        Configure<TestComponentOfferingAssociationEntity>("TestComponentOfferingAssociationOtherCodes");
        Configure<TimelineOverrideCourseEntity>("CourseTimelineOverrideOtherCodes");
        Configure<TimelineOverrideProgrammeEntity>("ProgrammeTimelineOverrideOtherCodes");
    }

    /// <summary>
    ///     Configures the many-to-many relationship between Programme and Person for coordinators.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    private void ConfigureProgrammeCoordinatorRelationship(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProgrammeEntity>()
            .HasMany(p => p.Coordinators)
            .WithMany(p => p.CoordinatedProgrammes)
            .UsingEntity<Dictionary<string, object>>(
                "ProgrammeCoordinator",
                j => j.HasOne<PersonEntity>().WithMany().HasForeignKey("PersonEntityId"),
                j => j.HasOne<ProgrammeEntity>().WithMany().HasForeignKey("ProgrammeEntityId"),
                j =>
                {
                    j.HasKey("ProgrammeEntityId", "PersonEntityId");
                    j.ToTable("ProgrammeCoordinators");
                });
    }

    /// <summary>
    ///     Configures the many-to-many relationship between Programme and Person for instructors.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    private void ConfigureProgrammeInstructorRelationship(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProgrammeEntity>()
            .HasMany(p => p.Instructors)
            .WithMany(p => p.InstructedProgrammes)
            .UsingEntity<Dictionary<string, object>>(
                "ProgrammeInstructor",
                j => j.HasOne<PersonEntity>().WithMany().HasForeignKey("PersonEntityId"),
                j => j.HasOne<ProgrammeEntity>().WithMany().HasForeignKey("ProgrammeEntityId"),
                j =>
                {
                    j.HasKey("ProgrammeEntityId", "PersonEntityId");
                    j.ToTable("ProgrammeInstructors");
                });
    }

    /// <summary>
    ///     Configures the hierarchical relationship between Programme entities (parent-child).
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    private void ConfigureProgrammeHierarchy(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProgrammeEntity>()
            .HasOne(p => p.Parent)
            .WithMany(p => p.Children)
            .HasForeignKey(p => p.ParentEntityId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes in hierarchy
    }

    /// <summary>
    ///     Configures the many-to-many relationship between Course and Person for coordinators.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    private void ConfigureCourseCoordinatorRelationship(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CourseEntity>()
            .HasMany(c => c.CourseCoordinatorEntities)
            .WithMany(p => p.CoordinatedCourses)
            .UsingEntity<Dictionary<string, object>>(
                "CourseCoordinator",
                j => j.HasOne<PersonEntity>().WithMany().HasForeignKey("PersonEntityId"),
                j => j.HasOne<CourseEntity>().WithMany().HasForeignKey("CourseEntityId"),
                j =>
                {
                    j.HasKey("CourseEntityId", "PersonEntityId");
                    j.ToTable("CourseCoordinators");
                });
    }

    /// <summary>
    ///     Configures the many-to-many relationship between Course and Person for instructors.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    private void ConfigureCourseInstructorRelationship(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CourseEntity>()
            .HasMany(c => c.CourseInstructorEntities)
            .WithMany(p => p.InstructedCourses)
            .UsingEntity<Dictionary<string, object>>(
                "CourseInstructor",
                j => j.HasOne<PersonEntity>().WithMany().HasForeignKey("PersonEntityId"),
                j => j.HasOne<CourseEntity>().WithMany().HasForeignKey("CourseEntityId"),
                j =>
                {
                    j.HasKey("CourseEntityId", "PersonEntityId");
                    j.ToTable("CourseInstructors");
                });
    }

    /// <summary>
    ///     Configures a <see cref="TimelineOverrideCourseEntity" />'s own, independent relationships -
    ///     per the spec's <c>CourseProperties</c> schema, each override entry's organisation/
    ///     coordinators/instructors/programmes/learning-outcomes are its own, not shared with the
    ///     parent course or with other override entries, so none of these reuse the equivalent
    ///     relationship already configured for <see cref="CourseEntity" /> itself.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    private void ConfigureCourseTimelineOverrideRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TimelineOverrideCourseEntity>()
            .HasOne(o => o.Course)
            .WithMany(c => c.TimelineOverrides)
            .HasForeignKey(o => o.CourseEntityId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TimelineOverrideCourseEntity>()
            .HasOne(o => o.Organisation)
            .WithMany()
            .HasForeignKey(o => o.OrganisationEntityId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TimelineOverrideCourseEntity>()
            .HasMany(o => o.Coordinators)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "CourseTimelineOverrideCoordinator",
                j => j.HasOne<PersonEntity>().WithMany().HasForeignKey("PersonEntityId"),
                j => j.HasOne<TimelineOverrideCourseEntity>().WithMany()
                    .HasForeignKey("TimelineOverrideCourseEntityId"),
                j =>
                {
                    j.HasKey("TimelineOverrideCourseEntityId", "PersonEntityId");
                    j.ToTable("CourseTimelineOverrideCoordinators");
                });

        modelBuilder.Entity<TimelineOverrideCourseEntity>()
            .HasMany(o => o.Instructors)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "CourseTimelineOverrideInstructor",
                j => j.HasOne<PersonEntity>().WithMany().HasForeignKey("PersonEntityId"),
                j => j.HasOne<TimelineOverrideCourseEntity>().WithMany()
                    .HasForeignKey("TimelineOverrideCourseEntityId"),
                j =>
                {
                    j.HasKey("TimelineOverrideCourseEntityId", "PersonEntityId");
                    j.ToTable("CourseTimelineOverrideInstructors");
                });

        modelBuilder.Entity<TimelineOverrideCourseEntity>()
            .HasMany(o => o.Programmes)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "CourseTimelineOverrideProgramme",
                j => j.HasOne<ProgrammeEntity>().WithMany().HasForeignKey("ProgrammeEntityId"),
                j => j.HasOne<TimelineOverrideCourseEntity>().WithMany()
                    .HasForeignKey("TimelineOverrideCourseEntityId"),
                j =>
                {
                    j.HasKey("TimelineOverrideCourseEntityId", "ProgrammeEntityId");
                    j.ToTable("CourseTimelineOverrideProgrammes");
                });

        modelBuilder.Entity<TimelineOverrideCourseEntity>()
            .HasMany(o => o.LearningOutcomes)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "CourseTimelineOverrideLearningOutcome",
                j => j.HasOne<LearningOutcomeEntity>().WithMany().HasForeignKey("LearningOutcomeEntityId"),
                j => j.HasOne<TimelineOverrideCourseEntity>().WithMany()
                    .HasForeignKey("TimelineOverrideCourseEntityId"),
                j =>
                {
                    j.HasKey("TimelineOverrideCourseEntityId", "LearningOutcomeEntityId");
                    j.ToTable("CourseTimelineOverrideLearningOutcomes");
                });
    }

    /// <summary>
    ///     Configures a <see cref="TimelineOverrideProgrammeEntity" />'s own, independent relationships -
    ///     same rationale as <see cref="ConfigureCourseTimelineOverrideRelationships" />.
    ///     <see cref="TimelineOverrideProgrammeEntity.Children" /> is its own relationship, not a reuse
    ///     of <see cref="ProgrammeEntity.ParentEntityId" />'s live hierarchy column, which reflects only
    ///     the child row's current, live parent.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    private void ConfigureProgrammeTimelineOverrideRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TimelineOverrideProgrammeEntity>()
            .HasOne(o => o.Programme)
            .WithMany(p => p.TimelineOverrides)
            .HasForeignKey(o => o.ProgrammeEntityId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TimelineOverrideProgrammeEntity>()
            .HasOne(o => o.Organisation)
            .WithMany()
            .HasForeignKey(o => o.OrganisationEntityId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TimelineOverrideProgrammeEntity>()
            .HasOne(o => o.Parent)
            .WithMany()
            .HasForeignKey(o => o.ParentEntityId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TimelineOverrideProgrammeEntity>()
            .HasMany(o => o.Children)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "ProgrammeTimelineOverrideChild",
                // Restrict (not the default Cascade): ProgrammeTimelineOverrideChildren's other FK
                // (TimelineOverrideProgrammeEntityId) already has its own Cascade path back to
                // Programmes via ProgrammeTimelineOverrides.ProgrammeEntityId - leaving this one on
                // the default Cascade too creates the same "multiple cascade paths to the same
                // table" SQL Server rejects that ConfigureOrganisationHierarchy/
                // ConfigureProgrammeHierarchy already work around for their own self-referencing FKs.
                j => j.HasOne<ProgrammeEntity>().WithMany().HasForeignKey("ProgrammeEntityId")
                    .OnDelete(DeleteBehavior.Restrict),
                j => j.HasOne<TimelineOverrideProgrammeEntity>().WithMany()
                    .HasForeignKey("TimelineOverrideProgrammeEntityId"),
                j =>
                {
                    j.HasKey("TimelineOverrideProgrammeEntityId", "ProgrammeEntityId");
                    j.ToTable("ProgrammeTimelineOverrideChildren");
                });

        modelBuilder.Entity<TimelineOverrideProgrammeEntity>()
            .HasMany(o => o.Coordinators)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "ProgrammeTimelineOverrideCoordinator",
                j => j.HasOne<PersonEntity>().WithMany().HasForeignKey("PersonEntityId"),
                j => j.HasOne<TimelineOverrideProgrammeEntity>().WithMany()
                    .HasForeignKey("TimelineOverrideProgrammeEntityId"),
                j =>
                {
                    j.HasKey("TimelineOverrideProgrammeEntityId", "PersonEntityId");
                    j.ToTable("ProgrammeTimelineOverrideCoordinators");
                });

        modelBuilder.Entity<TimelineOverrideProgrammeEntity>()
            .HasMany(o => o.Instructors)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "ProgrammeTimelineOverrideInstructor",
                j => j.HasOne<PersonEntity>().WithMany().HasForeignKey("PersonEntityId"),
                j => j.HasOne<TimelineOverrideProgrammeEntity>().WithMany()
                    .HasForeignKey("TimelineOverrideProgrammeEntityId"),
                j =>
                {
                    j.HasKey("TimelineOverrideProgrammeEntityId", "PersonEntityId");
                    j.ToTable("ProgrammeTimelineOverrideInstructors");
                });
    }

    /// <summary>
    ///     Configures the self-referencing many-to-many relationship between LearningOutcome entities
    ///     (a learning outcome can have more than one parent, unlike the other hierarchies in this model).
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    private void ConfigureLearningOutcomeHierarchy(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LearningOutcomeEntity>()
            .HasMany(lo => lo.Parents)
            .WithMany(lo => lo.Children)
            .UsingEntity<Dictionary<string, object>>(
                "LearningOutcomeHierarchy",
                j => j.HasOne<LearningOutcomeEntity>().WithMany().HasForeignKey("ParentEntityId"),
                j => j.HasOne<LearningOutcomeEntity>().WithMany().HasForeignKey("ChildEntityId"),
                j =>
                {
                    j.HasKey("ParentEntityId", "ChildEntityId");
                    j.ToTable("LearningOutcomeHierarchy");
                });
    }

    /// <summary>
    ///     Configures the (unidirectional) many-to-many relationships between offering entities and
    ///     Room - the spec does not define an inverse "offerings" collection on Room, so none is
    ///     declared here either.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    private void ConfigureOfferingRoomRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LearningComponentOfferingEntity>()
            .HasMany(o => o.Rooms)
            .WithMany()
            .UsingEntity(j => j.ToTable("LearningComponentOfferingRooms"));

        modelBuilder.Entity<TestComponentOfferingEntity>()
            .HasMany(o => o.Rooms)
            .WithMany()
            .UsingEntity(j => j.ToTable("TestComponentOfferingRooms"));
    }

    /// <summary>
    ///     Configures the (unidirectional) many-to-many relationships between LearningComponentOffering/
    ///     TestComponentOffering and CourseOffering - the spec does not define an inverse collection on
    ///     CourseOffering for either, so none is declared here either.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    private void ConfigureOfferingCourseOfferingRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LearningComponentOfferingEntity>()
            .HasMany(o => o.CourseOfferings)
            .WithMany()
            .UsingEntity(j => j.ToTable("LearningComponentOfferingCourseOfferings"));

        modelBuilder.Entity<TestComponentOfferingEntity>()
            .HasMany(o => o.CourseOfferings)
            .WithMany()
            .UsingEntity(j => j.ToTable("TestComponentOfferingCourseOfferings"));
    }

    /// <summary>
    ///     Configures the many-to-many relationships between Group and each of the 4 offering types.
    ///     The spec's <c>Group.offeringIds</c> is a polymorphic 0..N reference - each entry is an object
    ///     with exactly one of <c>courseOfferingId</c>/<c>programmeOfferingId</c>/
    ///     <c>learningComponentOfferingId</c>/<c>testComponentOfferingId</c> set
    ///     (<c>minProperties: 1, maxProperties: 1, additionalProperties: false</c> in the spec). Rather
    ///     than model this as one generic/discriminated relationship (which would mean either a nullable
    ///     FK per offering type on a single join table, or a type-discriminator column with no
    ///     database-enforced FK constraint), this is 4 separate ordinary many-to-many relationships, one
    ///     per offering type - each keeps a real FK-enforced join table and needs no discriminator or
    ///     "exactly one of N columns is set" validation anywhere. Bidirectional: the spec also defines
    ///     <c>groupIds</c> on the offering side (via <c>OfferingProperties</c>), which is monomorphic
    ///     (always references Group), so a plain inverse collection covers it.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    private void ConfigureGroupOfferingRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GroupEntity>()
            .HasMany(g => g.CourseOfferings)
            .WithMany(co => co.Groups)
            .UsingEntity(j => j.ToTable("GroupCourseOfferings"));

        modelBuilder.Entity<GroupEntity>()
            .HasMany(g => g.ProgrammeOfferings)
            .WithMany(po => po.Groups)
            .UsingEntity(j => j.ToTable("GroupProgrammeOfferings"));

        modelBuilder.Entity<GroupEntity>()
            .HasMany(g => g.LearningComponentOfferings)
            .WithMany(lco => lco.Groups)
            .UsingEntity(j => j.ToTable("GroupLearningComponentOfferings"));

        modelBuilder.Entity<GroupEntity>()
            .HasMany(g => g.TestComponentOfferings)
            .WithMany(tco => tco.Groups)
            .UsingEntity(j => j.ToTable("GroupTestComponentOfferings"));
    }

    /// <summary>
    ///     Configures the (unidirectional) many-to-many relationship between
    ///     TestComponentOfferingAssociationAttempt and Room - the spec does not define an inverse
    ///     "attempts" collection on Room, so none is declared here either. Mirrors
    ///     <see cref="ConfigureOfferingRoomRelationships" />.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    private void ConfigureAttemptRoomRelationship(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TestComponentOfferingAssociationAttemptEntity>()
            .HasMany(a => a.Rooms)
            .WithMany()
            .UsingEntity(j => j.ToTable("TestComponentOfferingAssociationAttemptRooms"));
    }

    /// <summary>
    ///     Configures the hierarchical relationships for Organisation entities: parent/children
    ///     (one-to-many, mirrors ProgrammeEntity's hierarchy) plus the separate, one-directional
    ///     "root" self-reference (the top-level organisation in the hierarchy - no inverse collection).
    ///     Both are self-referencing FKs on the same entity, which EF cannot disambiguate by
    ///     convention alone, hence this explicit configuration.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    private void ConfigureOrganisationHierarchy(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrganisationEntity>()
            .HasOne(o => o.Parent)
            .WithMany(o => o.Children)
            .HasForeignKey(o => o.ParentEntityId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<OrganisationEntity>()
            .HasOne(o => o.Root)
            .WithMany()
            .HasForeignKey(o => o.RootEntityId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    /// <summary>
    ///     Configures the hierarchical relationships for AcademicSession entities: parent/children
    ///     (one-to-many) plus the separate, one-directional "year" self-reference (the top-level
    ///     academic session this one belongs to - no inverse collection). Same disambiguation need
    ///     as Organisation.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    private void ConfigureAcademicSessionHierarchy(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AcademicSessionEntity>()
            .HasOne(a => a.Parent)
            .WithMany(a => a.Children)
            .HasForeignKey(a => a.ParentEntityId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<AcademicSessionEntity>()
            .HasOne(a => a.Year)
            .WithMany()
            .HasForeignKey(a => a.YearEntityId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    /// <summary>
    ///     Configures the hierarchical relationship between LearningComponent entities (parent-child).
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    private void ConfigureLearningComponentHierarchy(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LearningComponentEntity>()
            .HasOne(lc => lc.Parent)
            .WithMany(lc => lc.Children)
            .HasForeignKey(lc => lc.ParentEntityId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    /// <summary>
    ///     Configures the hierarchical relationship between TestComponent entities (parent-child).
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    private void ConfigureTestComponentHierarchy(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TestComponentEntity>()
            .HasOne(tc => tc.Parent)
            .WithMany(tc => tc.Children)
            .HasForeignKey(tc => tc.ParentEntityId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    /// <summary>
    ///     Configures the many-to-many relationships between LearningOutcome and the two component
    ///     types (LearningComponent, TestComponent) - the spec's <c>learningOutcomeIds</c> on both
    ///     component schemas. Mirrors <see cref="ConfigureGroupOfferingRelationships" />'s plain
    ///     implicit-join-table approach - two separate many-to-many relationships, not a
    ///     shared/polymorphic table.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    private void ConfigureComponentLearningOutcomeRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LearningComponentEntity>()
            .HasMany(lc => lc.LearningOutcomes)
            .WithMany(lo => lo.LearningComponents)
            .UsingEntity(j => j.ToTable("LearningComponentLearningOutcomes"));

        modelBuilder.Entity<TestComponentEntity>()
            .HasMany(tc => tc.LearningOutcomes)
            .WithMany(lo => lo.TestComponents)
            .UsingEntity(j => j.ToTable("TestComponentLearningOutcomes"));

        modelBuilder.Entity<ProgrammeEntity>()
            .HasMany(p => p.LearningOutcomes)
            .WithMany(lo => lo.Programmes)
            .UsingEntity(j => j.ToTable("ProgrammeLearningOutcomes"));
    }
}
