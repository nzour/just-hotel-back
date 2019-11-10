using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using CommandRunner.Abstraction;
using CommandRunner.Handler;
using FluentMigrator;
using Infrastructure;

namespace Cli.Commands.Util
{
    public class MigrationCreateCommand : AbstractCommand
    {
        private const string FileExtension = ".cs";

        private static readonly string MigrationDirectory =
            Directory.GetCurrentDirectory() + "/../../../../src/infrastructure/NHibernate/Migration/";

        private static readonly string MigrationTemplate =
            Directory.GetCurrentDirectory() + "/../../../Commands/Util/resources/migration.template.txt";

        public override void Execute(ArgumentProvider provider)
        {
            var version = GetVersion();
            var name = "Migration" + version;

            var content = File.ReadAllText(MigrationTemplate);
            content = string.Format(content, version, name);

            var file = File.Create(MigrationDirectory + name + FileExtension);
            file.Write(Encoding.ASCII.GetBytes(content));
            file.Flush();

            Console.WriteLine($"Migration {version} was generated.");
        }

        public override string GetName()
        {
            return "migrations:generate";
        }

        private string GetVersion()
        {
            var version = DateTime.Now.ToString("yyyyMMdd");

            var lastVersion = typeof(InfrastructureModule).Assembly.DefinedTypes
                .Where(t => t.IsSubclassOf(typeof(Migration)))
                .Select(m => m.GetCustomAttribute<MigrationAttribute>()?.Version.ToString())
                .OrderBy(v => v)
                .LastOrDefault(v => v!.StartsWith(version));

            return null == lastVersion
                ? version + "001"
                : (Convert.ToInt64(lastVersion) + 1).ToString();
        }
    }
}