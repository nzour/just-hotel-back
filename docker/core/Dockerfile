FROM mcr.microsoft.com/dotnet/core/sdk:2.2

WORKDIR /app

COPY ../src /app

EXPOSE 5000/tcp
EXPOSE 5001/tcp

ENV ASPNETCORE_ENVIRONMENT=Development

CMD ["dotnet", "run"]