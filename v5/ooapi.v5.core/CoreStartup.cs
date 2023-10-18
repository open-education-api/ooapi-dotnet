using Microsoft.Extensions.DependencyInjection;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;
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
        AppDomain.CurrentDomain.SetData("REGEX_DEFAULT_MATCH_TIMEOUT", TimeSpan.FromSeconds(3));

        servicesCollection.AddServices();

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
        builder.TryAddScoped<IProgramOfferingService, ProgramOfferingService>();
        builder.TryAddScoped<IProgramsService, ProgramsService>();
        builder.TryAddScoped<IRoomsService, RoomsService>();
        builder.TryAddScoped<IServiceMetadataService, ServiceMetadataService>();
        builder.TryAddScoped<IAcademicSessionsService, AcademicSessionsService>();

        return builder;
    }
}
