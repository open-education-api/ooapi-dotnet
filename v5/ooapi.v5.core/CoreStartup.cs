using Microsoft.Extensions.DependencyInjection;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Services;

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
    }

    private static IServiceCollection AddServices(this IServiceCollection builder)
    {
        builder.AddScoped<IAcademicSessionsService, AcademicSessionsService>();
        builder.AddScoped<IAssociationsService, AssociationsService>();
        builder.AddScoped<IBuildingsService, BuildingsService>();
        builder.AddScoped<IComponentsService, ComponentsService>();
        builder.AddScoped<ICoursesService, CoursesService>();
        builder.AddScoped<IEducationSpecificationsService, EducationSpecificationsService>();
        builder.AddScoped<IGroupsService, GroupsService>();
        builder.AddScoped<INewsFeedsService, NewsFeedsService>();
        builder.AddScoped<INewsItemsService, NewsItemsService>();
        builder.AddScoped<IOfferingsService, OfferingsService>();
        builder.AddScoped<IOrganizationsService, OrganizationsService>();
        builder.AddScoped<IPersonsService, PersonsService>();
        builder.AddScoped<IProgramOfferingService, ProgramOfferingService>();
        builder.AddScoped<IProgramsService, ProgramsService>();
        builder.AddScoped<IRoomsService, RoomsService>();
        builder.AddScoped<IServiceMetadataService, ServiceMetadataService>();

        return builder;
    }
}
