using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleApp.Application;
using SampleApp.Application.Common.Interfaces;
using SampleApp.CommandTests.Common;
using SampleApp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleApp.CommandTests.Base
{
    public class TestServiceProvider : IDisposable
    {
        private ServiceCollection serviceCollection = null;
        private ServiceProvider serviceProvider = null;

        public string DatabaseName { get; private set; }

        public static TestServiceProvider InMemoryContext(Action<ServiceCollection> additionalServices = null)
        {
            string _dbName = $"db-{Guid.NewGuid().ToString().Substring(0, 8)}";

            return new TestServiceProvider(_dbName, additionalServices);
        }

        public IMediator Mediator
        {
            get
            {
                return GetService<IMediator>();
            }
        }

        public TestServiceProvider(string dbName, Action<ServiceCollection> additionalServices = null)
        {
            DatabaseName = dbName;

            serviceCollection = new ServiceCollection();

            IConfiguration _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            serviceCollection.AddSingleton<IConfiguration>(p => _config);

            serviceCollection.AddScoped<ICurrentUser, TestUser>();
            serviceCollection.AddScoped<IDateTime, TestDateTime>();

            serviceCollection.AddApplication(includeValidators: true);
            serviceCollection.AddLogging();


            serviceCollection.AddDbContext<SampleAppDbContext>(opt =>
            {
                opt.UseInMemoryDatabase(databaseName: DatabaseName);
                opt.ConfigureWarnings(a => a.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            }, ServiceLifetime.Scoped, ServiceLifetime.Scoped);

            serviceCollection.AddScoped<ISampleAppDbContext>(provider => provider.GetService<SampleAppDbContext>());
            serviceCollection.AddScoped<DbContext>(provider => provider.GetService<SampleAppDbContext>());

            if (additionalServices != null)
            {
                additionalServices.Invoke(serviceCollection);
            }

            serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public T GetService<T>()
        {
            return serviceProvider.GetService<T>();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                var _dbContext = GetService<DbContext>();

                if (_dbContext != null)
                {
                    _dbContext.Dispose();
                }

                serviceProvider.Dispose();

            }
        }
    }
}
