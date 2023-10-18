using ooapi.v5.Attributes;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Security;
using System.Reflection;

namespace ooapi.v5.core.Services;

internal abstract class ServiceBase
{
    internal readonly IUserRequestContext userRequestContext;
    internal readonly CoreDBContext dataContext;

    protected ServiceBase(CoreDBContext dbContext, IUserRequestContext userRequestContext)
    {
        dataContext = dbContext;
        this.userRequestContext = userRequestContext;
    }

    public void HideAttributesBasedOnBivLevel(object item, UserRequestContext userRequestContext)
    {
        var showBiv_V_Hoog = false;
        var showBiv_V_Middel = false;

        if (userRequestContext.Bivv == "hoog" || userRequestContext.IsLocal)
        {
            showBiv_V_Hoog = true;
        }

        if (userRequestContext.Bivv == "middel")
        {
            showBiv_V_Middel = true;
        }

        if (item != null)
        {
            var properties = item.GetType().GetProperties();
            foreach (var property in properties)
            {
                var hideProperty = true;
                var BivVAttribuut = property.GetCustomAttribute<BivVAttribute>();
                if (BivVAttribuut != null)
                {
                    if (BivVAttribuut.Laag)
                    {
                        // property is public
                        hideProperty = false;
                    }
                    else
                    {
                        if (showBiv_V_Hoog)
                        {
                            // property is accessible for everyone with with 'hoog' (high) access
                            hideProperty = false;
                        }
                        if (showBiv_V_Middel && BivVAttribuut.Middel)
                        {
                            // all properties with 'middel' attribute are accessible for everyone with 'middel' (middle) access
                            hideProperty = false;
                        }
                    }
                }
                if (hideProperty)
                {
                    property.SetValue(item, null);
                }
            }
        }
    }
}
