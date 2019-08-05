using FluentMigrator.Runner;

namespace app.Common.Services.Migrator
{
    public class MigratorConfig
    {
        public const string ModeUp = "UP";
        public const string ModeDown = "DOWN";

        public string Mode { get; }
        public long Version { get; }

        public MigratorConfig(string mode, long version)
        {
            Mode = mode;
            Version = version;
        }

        public void Operate(IMigrationRunner runner)
        {
            if (Mode == ModeDown)
            {
                runner.MigrateDown(Version);
            }
            else
            {
                runner.MigrateUp(Version);
            }
        }
    }
}