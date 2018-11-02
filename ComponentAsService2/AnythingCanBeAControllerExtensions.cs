﻿using System.Linq;
using System.Reflection;
using Component.As.Service.Pieces;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace Component.As.Service
{
    public static class AnythingCanBeAControllerExtensions
    {
        /// <summary>Add the <see cref="AnythingCanBeAControllerFeatureProvider"/> so that arbitrary components can be served as controllers</summary>
        /// <param name="mvcBuilder"></param>
        /// <param name="controllerTypesToAdd"></param>
        /// <returns><paramref name="mvcBuilder"/></returns>
        /// <remarks>Used by
        /// <see cref="ComponentAsServiceExtensions.AddComponentAsService(Microsoft.Extensions.DependencyInjection.IServiceCollection,System.Reflection.TypeInfo[])"/>
        /// </remarks>
        public static IMvcBuilder AddAnythingCanBeAController(this IMvcBuilder mvcBuilder, params TypeInfo[] controllerTypesToAdd)
        {
            var acbControllersFeatureProvider = new AnythingCanBeAControllerFeatureProvider(controllerTypesToAdd);
            var featureProviders = mvcBuilder.PartManager.FeatureProviders;
            var controllerFeatureProvider = featureProviders.OfType<ControllerFeatureProvider>().FirstOrDefault();            
            featureProviders.Remove(controllerFeatureProvider);
            featureProviders.Add(acbControllersFeatureProvider);

            mvcBuilder.Services.Add(ServiceDescriptor.Singleton(acbControllersFeatureProvider));            
            return mvcBuilder;
        }
    }
}