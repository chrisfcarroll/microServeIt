﻿using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace Component.As.Service.Specs.FinerGrainedActionSelection
{
    public class TestModelBinderFactory : ModelBinderFactory
    {
        public static TestModelBinderFactory Create(IServiceProvider serviceProvider)
        {
            var options = serviceProvider.GetRequiredService<IOptions<MvcOptions>>();
            return new TestModelBinderFactory(
                TestModelMetadataProvider.CreateDefaultProvider(),
                options,
                serviceProvider);
        }

        public static TestModelBinderFactory Create(params IModelBinderProvider[] providers)
        {
            return Create(null, providers);
        }

        public static TestModelBinderFactory Create(
            IModelMetadataProvider metadataProvider,
            params IModelBinderProvider[] providers)
        {
            if (metadataProvider == null)
            {
                metadataProvider = TestModelMetadataProvider.CreateDefaultProvider();
            }

            var options = Options.Create(new MvcOptions());
            foreach (var provider in providers)
            {
                options.Value.ModelBinderProviders.Add(provider);
            }
            return new TestModelBinderFactory(metadataProvider, options);
        }

        public static TestModelBinderFactory CreateDefault(params IModelBinderProvider[] providers)
        {
            return CreateDefault(null, providers);
        }

        public static TestModelBinderFactory CreateDefault(
            IModelMetadataProvider metadataProvider=null,
            params IModelBinderProvider[] providers)
        {
            metadataProvider = metadataProvider??TestModelMetadataProvider.CreateDefaultProvider();

            var options = Options.Create(new MvcOptions());
            foreach (var provider in providers)
            {
                options.Value.ModelBinderProviders.Add(provider);
            }
            new MvcCoreMvcOptionsSetup(new TestHttpRequestStreamReaderFactory()).Configure(options.Value);
            return new TestModelBinderFactory(metadataProvider, options);
        }

        protected TestModelBinderFactory(IModelMetadataProvider metadataProvider, IOptions<MvcOptions> options)
            : this(metadataProvider, options, GetServices(options))
        {
        }

        protected TestModelBinderFactory(
            IModelMetadataProvider metadataProvider,
            IOptions<MvcOptions> options,
            IServiceProvider serviceProvider)
            : base(metadataProvider, options, serviceProvider)
        {
        }

        static IServiceProvider GetServices(IOptions<MvcOptions> options)
        {
            var services = new ServiceCollection();
            services.AddSingleton<ILoggerFactory, NullLoggerFactory>();
            services.AddSingleton(options);
            return services.BuildServiceProvider();
        }
    }
}
