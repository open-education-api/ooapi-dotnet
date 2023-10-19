using Microsoft.Extensions.DependencyInjection;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Security;

namespace ooapi.v5.core;

/// <summary>
/// Startup
/// </summary>
public static class CoreStartup
{
    /// <summary>
    /// Method used adding services to dependency injection
    /// </summary>
    /// <param name="servicesCollection">ServiceCollection used for dependency injection</param>
    public static void Startup(IServiceCollection servicesCollection)
    {

        servicesCollection.AddServices();
        servicesCollection.AddRepositories();

        servicesCollection.TryAddScoped<IUserRequestContext, UserRequestContext>();
    }

    private static IServiceCollection AddServices(this IServiceCollection builder)
    {
        builder.TryAddScoped<IAssociationsService, AssociationsService>();
        builder.TryAddScoped<IBuildingsService, BuildingsService>();
        builder.TryAddScoped<IComponentsService, ComponentsService>();
        builder.TryAddScoped<ICoursesService, CoursesService>();
        builder.TryAddScoped<IEducationSpecificationsService, EducationSpecificationsService>();
        builder.TryAddScoped<IGroupsService, GroupsService>();
        builder.TryAddScoped<INewsFeedsService, NewsFeedsService>();
        builder.TryAddScoped<INewsItemsService, NewsItemsService>();
        builder.TryAddScoped<IOfferingsService, OfferingsService>();
        builder.TryAddScoped<IOrganizationsService, OrganizationsService>();
        builder.TryAddScoped<IPersonsService, PersonsService>();
        builder.TryAddScoped<IProgramOfferingService, ProgramOfferingsService>();
        builder.TryAddScoped<IProgramsService, ProgramsService>();
        builder.TryAddScoped<IRoomsService, RoomsService>();
        builder.TryAddScoped<IServiceMetadataService, ServiceMetadataService>();
        builder.TryAddScoped<IAcademicSessionsService, AcademicSessionsService>();

        return builder;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection builder)
    {
        builder.TryAddScoped<IAcademicSessionsRepository, AcademicSessionsRepository>();
        builder.TryAddScoped<IAssociationsRepository, AssociationsRepository>();
        builder.TryAddScoped<IBuildingsRepository, BuildingsRepository>();
        builder.TryAddScoped<IComponentOfferingsRepository, ComponentOfferingsRepository>();
        builder.TryAddScoped<IComponentsRepository, ComponentsRepository>();
        builder.TryAddScoped<ICourseOfferingsRepository, CourseOfferingsRepository>();
        builder.TryAddScoped<ICoursesRepository, CoursesRepository>();
        builder.TryAddScoped<IEducationSpecificationsRepository, EducationSpecificationsRepository>();
        builder.TryAddScoped<IGroupsRepository, GroupsRepository>();
        builder.TryAddScoped<INewsFeedsRepository, NewsFeedsRepository>();
        builder.TryAddScoped<INewsItemsRepository, NewsItemsRepository>();
        builder.TryAddScoped<IOrganizationsRepository, OrganizationsRepository>();
        builder.TryAddScoped<IPersonsRepository, PersonsRepository>();
        builder.TryAddScoped<IProgramOfferingsRepository, ProgramOfferingsRepository>();
        builder.TryAddScoped<IProgramsRepository, ProgramsRepository>();
        builder.TryAddScoped<IRoomsRepository, RoomsRepository>();
        builder.TryAddScoped<IServiceMetadataRepository, ServiceMetadataRepository>();

        return builder;
    }
}