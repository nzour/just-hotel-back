using System;
using System.Collections.Generic;
using System.IO;
using app.DependencyInjection;
using FluentMigrator.Runner;
using FluentNHibernate.Conventions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace app.Common.Services.Migrator
{
    public class Migrator : AbstractAssemblyAware
    {
        private const string ConfigFile = "/Common/Services/Migrator/migrations.json";
        
        private IServiceCollection Services { get; }

        private readonly List<MigratorConfig> _configs = new List<MigratorConfig>();

        public MigratorConfig AddConfig
        {
            set => _configs.Add(value);
        }

        public Migrator(IServiceCollection services)
        {
            Services = services;
        }

        public void Execute()
        {
            Prepare();
            ReadConfigs();

            if (_configs.IsEmpty())
            {
                MigrateAll();                
            }
            else
            {
                MigrateConfigs();
            }
        }
        
        private void MigrateAll()
        {
            InScope(runner => runner.MigrateUp());
        }

        private void MigrateConfigs()
        {
            _configs.ForEach(config => InScope(config.Operate));
        }
        
        private void Prepare()
        {
            Services.AddFluentMigratorCore()
                .ConfigureRunner(runner => runner
                    .AddPostgres()
                    .WithGlobalConnectionString(DbAccessor.ConnectionString)
                    .ScanIn(GetAssembly()).For.Migrations())
                .AddLogging(logBuilder => logBuilder.AddFluentMigratorConsole());
        }

        private void ReadConfigs()
        {
            if (!File.Exists(Directory.GetCurrentDirectory() + ConfigFile))
            {
                return;
            }
            
            using (var reader = new StreamReader(Directory.GetCurrentDirectory() + ConfigFile))
            {
                var json = JsonConvert.DeserializeObject<Dictionary<string, long[]>>(reader.ReadToEnd());

                foreach (var (mode, versions) in json)
                {
                    foreach (var version in versions)
                    {
                        _configs.Add(new MigratorConfig(mode, version));
                    }
                }
            }
            
        }
        
        private void InScope(Action<IMigrationRunner> action) 
        {
            var provider = Services.BuildServiceProvider();
            
            using (var scope = provider.CreateScope())
            {
                action.Invoke(scope.ServiceProvider.GetRequiredService<IMigrationRunner>());
            }
        }
    }
}