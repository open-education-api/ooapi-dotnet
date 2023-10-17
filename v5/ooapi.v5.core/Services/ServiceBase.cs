﻿using ooapi.v5.Attributes;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Security;
using System.Reflection;

namespace ooapi.v5.core.Services;

internal abstract class ServiceBase
{
    internal readonly IUserRequestContext userRequestContext;
    internal readonly CoreDBContext dataContext;

    public ServiceBase(CoreDBContext dbContext, IUserRequestContext userRequestContext)
    {
        dataContext = dbContext;
        this.userRequestContext = userRequestContext;
    }

    public void HideAttributesBasedOnBivLevel(object item, UserRequestContext userRequestContext)
    {
        bool showBiv_V_Hoog = false;
        bool showBiv_V_Middel = false;
        string curUser = userRequestContext.UserId;

        if (userRequestContext.Bivv == "hoog" || userRequestContext.IsLocal)
            showBiv_V_Hoog = true;
        if (userRequestContext.Bivv == "middel")
            showBiv_V_Middel = true;

        //Show property when:
        // --> (showBiv_V_Hoog == true)
        // --> (showBiv_V_Middel == true) AND (BivVAttribute.middel == true)
        // --> (BivVAttribute.laag == true)


        if (item != null)
        {
            PropertyInfo[] properties = item.GetType().GetProperties();
            foreach (var property in properties)
            {
                bool hideProperty = true;
                var BivVAttribuut = property.GetCustomAttribute<BivVAttribute>();
                if (BivVAttribuut != null)
                {
                    if (property.GetCustomAttribute<BivVAttribute>().Laag)
                    {
                        // property is public
                        hideProperty = false;
                    }
                    else
                    {
                        if (showBiv_V_Hoog == true)
                        {
                            // property is accessible for everyone with with 'hoog' (high) access
                            hideProperty = false;
                        }
                        if (showBiv_V_Middel == true && property.GetCustomAttribute<BivVAttribute>().Middel == true)
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
