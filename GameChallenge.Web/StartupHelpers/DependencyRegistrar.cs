using Microsoft.Extensions.DependencyInjection;
using GameChallenge.Core.Data;
using GameChallenge.Core.DBEntities;
using GameChallenge.Core.Interfaces;
using GameChallenge.Core.Services;
using GameChallenge.Infrastructure.Data;
using GameChallenge.Web.EnpointModel;
using System;
using GameChallenge.Web.StartupHelpers;
using GameChallenge.Common.Helpers;
using GameChallenge.Common.Numbers;

namespace GameChallenge.Web
{
    internal class DependencyRegistrar
    {
        private IServiceCollection _services;

        public DependencyRegistrar(IServiceCollection services)
        {
            this._services = services;
        }

        internal void ConfigureDependencies()
        {
            _services.AddHttpContextAccessor();

            //Other classes
            _services.AddSingleton<INumberHelper, NumberHelper>();
            _services.AddScoped<IPlayerCalculations, PlayerCalculations>();


            //Repositories
            _services.AddScoped<IRepository<Player>, EfRepository<Player>>();
            _services.AddScoped<IRepository<Log>, EfRepository<Log>>();
            _services.AddScoped<IRepository<Setting>, EfRepository<Setting>>();

            //Services
            _services.AddScoped<IPlayerService, PlayerService>();
            _services.AddScoped<ILogService, LogService>();
            _services.AddScoped<ISettingService, SettingService>();

            //Other Services
            _services.AddScoped<IResponseGeneric, ResponseGeneric>();
            _services.AddScoped<IWorkContext, WebWorkerContext>();



        }
    }
}