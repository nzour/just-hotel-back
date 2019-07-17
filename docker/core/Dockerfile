FROM mcr.microsoft.com/dotnet/core/sdk:2.2

WORKDIR /app

# Cli для миграций
RUN dotnet tool install -g FluentMigrator.DotNet.Cli

CMD ["dotnet", "run"]